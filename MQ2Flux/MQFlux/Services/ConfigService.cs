using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MQFlux.Models;
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
        Task<CharacterConfig> GetCharacterConfig(CancellationToken cancellationToken = default);
        Task<FluxConfig> GetFluxConfig(CancellationToken cancellationToken = default);
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
        private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
            MaxDepth = 64,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
        };
        private const string FLUX_CONFIG_FILE_NAME = "MQFlux.json";
        private readonly IContext context;
        private readonly IMQLogger mqLogger;
        private readonly ILogger<ConfigService> logger;
        private readonly string fluxConfigPath;

        private CharacterConfig characterConfig;
        private FluxConfig fluxConfig;
        private bool disposedValue;
        private SemaphoreSlim getFluxConfigSemaphore;
        private SemaphoreSlim getCharacterConfigSemaphore;

        public ConfigService(IContext context, IMQLogger mqLogger, ILogger<ConfigService> logger)
        {
            this.context = context;
            this.mqLogger = mqLogger;
            this.logger = logger;
            getFluxConfigSemaphore = new SemaphoreSlim(1);
            getCharacterConfigSemaphore = new SemaphoreSlim(1);
            fluxConfigPath = Path.Combine(this.context.MQ.ConfigPath, FLUX_CONFIG_FILE_NAME);
            characterConfig = null;
            fluxConfig = null;
        }

        public async Task<CharacterConfig> GetCharacterConfig(CancellationToken cancellationToken = default)
        {
            if (context.TLO.Me == null)
            {
                return null;
            }

            if (fluxConfig == null)
            {
                _ = await GetFluxConfig(cancellationToken);
            }

            var server = context.TLO.EverQuest.Server;
            var name = context.TLO.Me.Name;

            try
            {
                await getCharacterConfigSemaphore.WaitAsync(cancellationToken);

                if
                (
                    characterConfig == null || // not loaded
                    string.Compare(characterConfig.Name, name, true) != 0 || // different character
                    string.Compare(characterConfig.Server, server, true) != 0
                )
                {
                    var path = Path.Combine(context.MQ.ConfigPath, $"MQFlux.{server}.{name}.json");

                    if (File.Exists(path))
                    {
                        using (Stream stream = File.OpenRead(path))
                        {
                            characterConfig = stream.Length > 0 ?
                                JsonSerializer.Deserialize<CharacterConfig>(stream, options: jsonOptions) :
                                new CharacterConfig(fluxConfig, name, server);
                        }

                        Log("Character config loaded from disk");
                    }
                    else
                    {
                        characterConfig = new CharacterConfig(fluxConfig, name, server);

                        File.WriteAllBytes(path, JsonSerializer.SerializeToUtf8Bytes(characterConfig, options: jsonOptions));

                        Log("Character config created and saved to disk");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                characterConfig = new CharacterConfig(fluxConfig, name, server);

                Log(ex, "Failed to load character config and reverted to defaults");
            }
            finally
            {
                getCharacterConfigSemaphore.Release();
            }

            return characterConfig;
        }

        public async Task<FluxConfig> GetFluxConfig(CancellationToken cancellationToken = default)
        {
            await getFluxConfigSemaphore.WaitAsync(cancellationToken);

            try
            {
                if (fluxConfig == null)
                {
                    if (File.Exists(fluxConfigPath))
                    {
                        using (Stream stream = File.OpenRead(fluxConfigPath))
                        {
                            fluxConfig = stream.Length > 0 ?
                                JsonSerializer.Deserialize<FluxConfig>(stream, options: jsonOptions) :
                                new FluxConfig();
                        }

                        Log("Flux config loaded from disk");
                    }
                    else
                    {
                        fluxConfig = new FluxConfig();

                        File.WriteAllBytes(fluxConfigPath, JsonSerializer.SerializeToUtf8Bytes(fluxConfig, options: jsonOptions));

                        Log("Flux config created and saved to disk");
                    }
                }
            }
            catch (Exception ex)
            {
                fluxConfig = new FluxConfig();

                Log(ex, "Failed to load flux config and reverted to defaults");
            }
            finally
            {
                getFluxConfigSemaphore.Release();
            }

            return fluxConfig;
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

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (getFluxConfigSemaphore != null)
                    {
                        getFluxConfigSemaphore.Dispose();
                        getFluxConfigSemaphore = null;
                    }

                    if (getCharacterConfigSemaphore != null)
                    {
                        getCharacterConfigSemaphore.Dispose();
                        getCharacterConfigSemaphore = null;
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
