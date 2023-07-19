using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MQFlux.Models;
using MQFlux.Notifications;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Services
{
    public interface IConfig
    {
        FluxConfig FluxConfig { get; }

        Task SaveAsync(bool notify = false);
    }

    public static class ConfigServiceExtensions
    {
        public static IServiceCollection AddConfig(this IServiceCollection services)
        {
            return services.AddSingleton<IConfig, ConfigService>();
        }
    }

    public class ConfigService : IConfig, IDisposable
    {
        public FluxConfig FluxConfig { get; private set; }

        private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
            MaxDepth = 64,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };
        private const string CONFIG_FILE_NAME = "MQFlux.json";
        private readonly IContext context;
        private readonly IMQLogger mqLogger;
        private readonly IMediator mediator;
        private readonly ILogger<ConfigService> logger;
        private readonly string path;

        private FileSystemWatcher watcher;
        private bool disposedValue;
        private SemaphoreSlim semaphore;

        public ConfigService(IContext context, IMQLogger mqLogger, IMediator mediator, ILogger<ConfigService> logger)
        {
            this.context = context;
            this.mqLogger = mqLogger;
            this.mediator = mediator;
            this.logger = logger;
            semaphore = new SemaphoreSlim(0, 1);
            path = Path.Combine(this.context.MQ.ConfigPath, CONFIG_FILE_NAME);

            Initialize();
        }

        public async Task SaveAsync(bool notify = true)
        {
            if (!notify)
            {
                watcher.Changed -= ConfigChanged;
            }

            await Task.Run
            (
                () =>
                {
                    try
                    {
                        File.WriteAllBytes(path, JsonSerializer.SerializeToUtf8Bytes(FluxConfig, options: jsonOptions));
                    }
                    catch (Exception ex)
                    {
                        Log(ex, "Failed to save config to disk");
                    }
                }
            );

            watcher.Changed += ConfigChanged;
        }

        private void Initialize()
        {
            LoadConfig();
            CreateWatcher();
        }

        private void Log(string text)
        {
            mqLogger.Log(text);
            logger.LogInformation(text);
        }

        private void Log(Exception ex, string message, params object[] args)
        {
            mqLogger.LogError(ex, message);
            logger.LogError(ex, message, args);
        }

        private void CreateWatcher()
        {
            var directory = Path.GetDirectoryName(path);
            var filter = Path.GetFileName(path);

            watcher = new FileSystemWatcher(directory)
            {
                Filter = filter,
                NotifyFilter = NotifyFilters.LastWrite,
                EnableRaisingEvents = true
            };
            watcher.Changed += ConfigChanged;
        }

        private void LoadConfig()
        {
            try
            {
                if (File.Exists(path))
                {
                    using (Stream stream = File.OpenRead(path))
                    {
                        FluxConfig = stream.Length > 0 ?
                            JsonSerializer.Deserialize<FluxConfig>(stream, options: jsonOptions) :
                            new FluxConfig();
                    }

                    Log("Configuration loaded from disk");
                }
                else
                {
                    FluxConfig = new FluxConfig();

                    File.WriteAllBytes(path, JsonSerializer.SerializeToUtf8Bytes(FluxConfig, options: jsonOptions));

                    Log("Default config created and saved to disk");
                }
            }
            catch (Exception ex)
            {
                FluxConfig = new FluxConfig();

                Log(ex, "Failed to load config, reverted to default configuration. Use the \"/flux save\" command to save the default config to disk and overwrite the bad configuration or manually fix it.");
            }
        }

        private void ConfigChanged(object sender, FileSystemEventArgs e)
        {
            Task.Run
            (
                async () =>
                {
                    try
                    {
                        if (!semaphore.Wait(0))
                        {
                            return;
                        }

                        Log("Config change detected");

                        await Task.Delay(1000); // Need to add cancellation token here for shutdown.

                        LoadConfig();

                        await mediator.Publish
                        (
                            new ConfigUpdateNotification()
                            {
                                Config = FluxConfig
                            }
                        );
                    }
                    catch (Exception ex)
                    {
                        Log(ex, "Config changed event handling failed");
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }
            ).ConfigureAwait(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (watcher != null)
                    {
                        watcher.Changed -= ConfigChanged;

                        watcher.Dispose();

                        watcher = null;
                    }

                    if (semaphore != null)
                    {
                        semaphore.Dispose();

                        semaphore = null;
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
