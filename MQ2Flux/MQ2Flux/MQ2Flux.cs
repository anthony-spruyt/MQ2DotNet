using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MQ2DotNet.MQ2API;
using MQ2DotNet.Program;
using MQ2DotNet.Services;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux
{
    public class MQ2Flux : IProgram
    {
        private readonly MQ2 mq2;
        private readonly Chat chat;
        private readonly Commands commands;
        private readonly Events events;
        private readonly Spawns spawns;
        private readonly TLO tlo;

        private ServiceProvider serviceProvider;
        private ILogger<MQ2Flux> logger;
        private IMediator mediator;
        private IMq2Logger mQ2logger;
        private bool disposedValue;

        public MQ2Flux(MQ2 mq2, Chat chat, Commands commands, Events events, Spawns spawns, TLO tlo)
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
        }

        public async Task Main(CancellationToken token, string[] args)
        {
            Configure(args);

            try
            {
                mQ2logger.Log($"Started");

                CancellationToken[] cancellationTokens = await mediator.Send
                (
                    new LoadCommandsRequest()
                    {
                        CancellationToken = token
                    }
                );

                using (CancellationTokenSource linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationTokens))
                {
                    while (!linkedTokenSource.IsCancellationRequested)
                    {
                        if (tlo.EverQuest.GameState == MQ2DotNet.EQ.GameState.InGame)
                        {
                            await mediator.Send
                            (
                                new ProcessRequest()
                                {
                                    Args = args
                                },
                                linkedTokenSource.Token
                            );
                        }

                        DumpMQ2DataTypeErrors(logger);

                        await Task.Yield();
                    }
                }

                await mediator.Send(new UnloadCommandsRequest());

                mQ2logger.Log($"Stopped");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in {nameof(MQ2Flux.Main)}");
                mQ2logger.LogError(ex);
            }

            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }
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
                mQ2logger = serviceProvider.GetRequiredService<IMq2Logger>();

                return configuration;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to configure", ex);
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
                .AddMq2Context(mq2, chat, commands, events, spawns, tlo)
                .AddMq2Logging()
                .AddMq2CommandProvider()
                .AddMQ2Config();
        }

        private void DumpMQ2DataTypeErrors(ILogger<MQ2Flux> logger)
        {
            _ = Task.Run
            (
                () =>
                {
                    try
                    {
                        foreach (var key in MQ2DataType.DataTypeErrors.Keys)
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
