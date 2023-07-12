using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MQ2DotNet.EQ;
using MQ2DotNet.MQ2API;
using MQ2DotNet.Program;
using MQ2DotNet.Services;
using MQ2Flux.Behaviors;
using MQ2Flux.Commands;
using MQ2Flux.Services;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux
{
    public class MQ2Flux : IProgram
    {
        public static Task Yield => Task.Delay(100);

        private readonly MQ2 mq2;
        private readonly Chat chat;
        private readonly MQ2DotNet.Services.Commands commands;
        private readonly Events events;
        private readonly Spawns spawns;
        private readonly TLO tlo;

        private ServiceProvider serviceProvider;
        private ILogger<MQ2Flux> logger;
        private IMediator mediator;
        private IMQ2Logger mQ2logger;
        private GameState gameState;
        private bool disposedValue;

        public MQ2Flux(MQ2 mq2, Chat chat, MQ2DotNet.Services.Commands commands, Events events, Spawns spawns, TLO tlo)
        {
            if (mq2 is null)
            {
                throw new ArgumentNullException(nameof(mq2));
            }

            if (chat is null)
            {
                throw new ArgumentNullException(nameof(chat));
            }

            if (commands is null)
            {
                throw new ArgumentNullException(nameof(commands));
            }

            if (events is null)
            {
                throw new ArgumentNullException(nameof(events));
            }

            if (spawns is null)
            {
                throw new ArgumentNullException(nameof(spawns));
            }

            if (tlo is null)
            {
                throw new ArgumentNullException(nameof(tlo));
            }

            this.mq2 = mq2;
            this.chat = chat;
            this.commands = commands;
            this.events = events;
            this.spawns = spawns;
            this.tlo = tlo;
            gameState = tlo?.EverQuest?.GameState ?? GameState.Unknown;
        }

        public async Task Main(CancellationToken token, string[] args)
        {
            Configure(args);

            mQ2logger.Log($"Started");

            try
            {
                CancellationToken[] cancellationTokens = await mediator.Send
                (
                    new LoadMQ2Commands()
                    {
                        CancellationToken = token
                    }
                );

                events.BeginZone += Events_BeginZone;
                events.EndZone += Events_EndZone;
                events.OnAddGroundItem += Events_OnAddGroundItem;
                events.OnAddSpawn += Events_OnAddSpawn;
                events.OnCleanUI += Events_OnCleanUI;
                events.OnDrawHUD += Events_OnDrawHUD;
                events.OnLoadPlugin += Events_OnLoadPlugin;
                events.OnMacroStart += Events_OnMacroStart;
                events.OnMacroStop += Events_OnMacroStop;
                events.OnReloadUI += Events_OnReloadUI;
                events.OnRemoveGroundItem += Events_OnRemoveGroundItem;
                events.OnRemoveSpawn += Events_OnRemoveSpawn;
                events.OnUnloadPlugin += Events_OnUnloadPlugin;
                events.OnZoned += Events_OnZoned;
                events.SetGameState += Events_SetGameState;

                using (CancellationTokenSource linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationTokens))
                {
                    while (!linkedTokenSource.IsCancellationRequested)
                    {
                        if (gameState == GameState.InGame)
                        {
                            await mediator.Send
                            (
                                new ProcessCommand()
                                {
                                    Args = args
                                },
                                linkedTokenSource.Token
                            );
                        }

                        FlushMQ2DataTypeErrors(logger);

                        await Yield;
                    }
                }

                events.BeginZone -= Events_BeginZone;
                events.EndZone -= Events_EndZone;
                events.OnAddGroundItem -= Events_OnAddGroundItem;
                events.OnAddSpawn -= Events_OnAddSpawn;
                events.OnCleanUI -= Events_OnCleanUI;
                events.OnDrawHUD -= Events_OnDrawHUD;
                events.OnLoadPlugin -= Events_OnLoadPlugin;
                events.OnMacroStart -= Events_OnMacroStart;
                events.OnMacroStop -= Events_OnMacroStop;
                events.OnReloadUI -= Events_OnReloadUI;
                events.OnRemoveGroundItem -= Events_OnRemoveGroundItem;
                events.OnRemoveSpawn -= Events_OnRemoveSpawn;
                events.OnUnloadPlugin -= Events_OnUnloadPlugin;
                events.OnZoned -= Events_OnZoned;
                events.SetGameState -= Events_SetGameState;

                await mediator.Send(new UnloadMQ2Commands());
            }
            catch (TaskCanceledException) { }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in {nameof(Main)}");
                mQ2logger.LogError(ex);
            }

            mQ2logger.Log($"Stopped");

            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }
        }

        private void Events_BeginZone(object sender, EventArgs e)
        {
        }

        private void Events_EndZone(object sender, EventArgs e)
        {
        }

        private void Events_OnAddGroundItem(object sender, MQ2DotNet.MQ2API.DataTypes.GroundType e)
        {
        }

        private void Events_OnAddSpawn(object sender, MQ2DotNet.MQ2API.DataTypes.SpawnType e)
        {
        }

        private void Events_OnCleanUI(object sender, EventArgs e)
        {
        }

        private void Events_OnDrawHUD(object sender, EventArgs e)
        {
        }

        private void Events_OnLoadPlugin(object sender, string e)
        {
        }

        private void Events_OnMacroStart(object sender, string e)
        {
        }

        private void Events_OnMacroStop(object sender, string e)
        {
        }

        private void Events_OnReloadUI(object sender, EventArgs e)
        {
        }
        private void Events_OnRemoveGroundItem(object sender, MQ2DotNet.MQ2API.DataTypes.GroundType e)
        {
        }

        private void Events_OnRemoveSpawn(object sender, MQ2DotNet.MQ2API.DataTypes.SpawnType e)
        {
        }

        private void Events_OnUnloadPlugin(object sender, string e)
        {
        }

        private void Events_OnZoned(object sender, EventArgs e)
        {
        }

        private void Events_SetGameState(object sender, GameState e)
        {
            gameState = e;
        }

        private IConfiguration Configure(string[] args)
        {
            try
            {
                var configuration = ConfigureConfigProviders(args);
                var services = ConfigureServices(configuration);

                serviceProvider = services.BuildServiceProvider();

                logger = serviceProvider.GetRequiredService<ILogger<MQ2Flux>>();
                mediator = serviceProvider.GetRequiredService<IMediator>();
                mQ2logger = serviceProvider.GetRequiredService<IMQ2Logger>();

                return configuration;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to configure MQ2Flux", ex);
            }
        }

        private static IConfiguration ConfigureConfigProviders(string[] args)
        {
            return new ConfigurationBuilder()
                .AddJsonFile
                (
                    Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "appsettings.json"),
                    optional: true,
                    reloadOnChange: true
                )
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
        }

        private IServiceCollection ConfigureServices(IConfiguration configuration)
        {
            return new ServiceCollection()
                .AddLogging
                (
                    loggingConfig =>
                    {
                        loggingConfig.AddConfiguration(configuration.GetSection("Logging"));
                        loggingConfig.AddFile
                        (
                            fileLoggingConfig =>
                            {
                                fileLoggingConfig.RootPath = Path.GetDirectoryName(Path.Combine(Assembly.GetExecutingAssembly().Location, "../../../../../"));
                            }
                        );
                    }
                )
                .AddMediatR
                (
                    config =>
                    {
                        config.RegisterServicesFromAssemblyContaining<MQ2Flux>();
                        config.AddFluxBehaviors();
                    }
                )
                .AddMQ2Context(mq2, chat, commands, events, spawns, tlo)
                .AddMQ2ChatHistory()
                .AddMQ2Logging()
                .AddMQ2Config()
                .AddMQ2CommandProvider()
                .AddAbilityService()
                .AddItemService()
                .AddSpellCastingService();
        }

        private void FlushMQ2DataTypeErrors(ILogger<MQ2Flux> logger)
        {
            if (!MQ2DataType.DataTypeErrors.Any())
            {
                return;
            }

            _ = Task.Run
            (
                () =>
                {
                    try
                    {
                        var keys = MQ2DataType.DataTypeErrors.Keys.ToArray();

                        foreach (var key in keys)
                        {
                            MQ2DataType.DataTypeErrors.TryRemove(key, out var ex);

                            logger.LogError(ex, "{key} in the format of {{name}}_{{index}}_{{typeof(T)}}", key);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Failed to dump MQ2DataType errors.");
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
                    // TODO: dispose managed state (managed objects)
                    if (serviceProvider != null)
                    {
                        serviceProvider.Dispose();
                        serviceProvider = null;
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~MQ2Flux()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
