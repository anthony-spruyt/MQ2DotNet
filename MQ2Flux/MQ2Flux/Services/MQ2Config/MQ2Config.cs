using MediatR;
using MQ2Flux.Models;
using MQ2Flux.Notifications;
using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux
{
    public class MQ2Config : IMQ2Config, IDisposable
    {
        public FluxConfig FluxConfig { get; private set; }

        private const string CONFIG_FILE_NAME = "MQ2Flux.json";
        private readonly IMq2Context context;
        private readonly IMq2Logger logger;
        private readonly IMediator mediator;

        private FileSystemWatcher watcher;
        private bool disposedValue;
        private string path;
        private SemaphoreSlim semaphoreSlim;

        public MQ2Config(IMq2Context context, IMq2Logger logger, IMediator mediator)
        {
            this.context = context;
            this.logger = logger;
            this.mediator = mediator;

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

                        logger.Log("Config saved to disk");
                    }
                    catch (Exception ex)
                    {
                        logger.Log("Failed to save config to disk");
                        logger.LogError(ex);
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

                    logger.Log("Configuration loaded from disk");
                }
                else
                {
                    FluxConfig = new FluxConfig();

                    string json = JsonSerializer.Serialize(FluxConfig);

                    File.WriteAllText(path, json);

                    logger.Log("Default config created and saved to disk");
                }
            }
            catch (Exception ex)
            {
                FluxConfig = new FluxConfig();

                logger.Log("Failed to load config, reverted to default configuration");
                logger.LogError(ex);
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

                        logger.Log("Config change detected");

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
                        logger.Log("Config changed event handling failed");
                        logger.LogError(ex);
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
