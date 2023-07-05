using MediatR;
using Microsoft.Extensions.Logging;
using MQ2Flux.Models;
using MQ2Flux.Notifications;
using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Services
{
    public class MQ2Config : IMQ2Config, IDisposable
    {
        public FluxConfig FluxConfig { get; private set; }

        private const string CONFIG_FILE_NAME = "MQ2Flux.json";
        private readonly IMQ2Context context;
        private readonly IMQ2Logger mq2Logger;
        private readonly IMediator mediator;
        private readonly ILogger<MQ2Config> logger;
        private readonly string path;

        private FileSystemWatcher watcher;
        private bool disposedValue;
        private SemaphoreSlim semaphoreSlim;

        public MQ2Config(IMQ2Context context, IMQ2Logger mq2Logger, IMediator mediator, ILogger<MQ2Config> logger)
        {
            this.context = context;
            this.mq2Logger = mq2Logger;
            this.mediator = mediator;
            this.logger = logger;
            semaphoreSlim = new SemaphoreSlim(0, 1);
            path = Path.Combine(this.context.MQ2.ConfigPath, CONFIG_FILE_NAME);

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
                        var json = JsonSerializer.Serialize(FluxConfig);

                        File.WriteAllText(path, json);

                        
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
            mq2Logger.Log(text);
            logger.LogInformation(text);
        }

        private void Log(Exception ex, string message, params object[] args)
        {
            mq2Logger.LogError(ex, message);
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
                    string json = File.ReadAllText(path);

                    FluxConfig = JsonSerializer.Deserialize<FluxConfig>(json);

                    Log("Configuration loaded from disk");
                }
                else
                {
                    FluxConfig = new FluxConfig();

                    string json = JsonSerializer.Serialize(FluxConfig);

                    File.WriteAllText(path, json);

                    Log("Default config created and saved to disk");
                }
            }
            catch (Exception ex)
            {
                FluxConfig = new FluxConfig();

                Log(ex, "Failed to load config, reverted to default configuration");
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
                        if (!semaphoreSlim.Wait(0))
                        {
                            return;
                        }

                        Log("Config change detected");

                        await Task.Delay(1000);

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
                        semaphoreSlim.Release();
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

                    if (semaphoreSlim != null)
                    {
                        semaphoreSlim.Dispose();

                        semaphoreSlim = null;
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
