using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MQ2DotNet.MQ2API;
using MQ2DotNet.Program;
using MQ2DotNet.Services;
using MQ2Flux.Behaviors;
using MQ2Flux.Commands;
using MQ2Flux.Queries;
using MQ2Flux.Services;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux
{
    public class MQ2Flux : IProgram
    {
        public static Task Yield(CancellationToken cancellationToken) => Task.Delay(100, cancellationToken);

        private readonly MQ2 mq2;
        private readonly Chat chat;
        private readonly MQ2DotNet.Services.Commands commands;
        private readonly Events events;
        private readonly Spawns spawns;
        private readonly TLO tlo;

        private ServiceProvider serviceProvider;
        private ILogger<MQ2Flux> logger;
        private IMediator mediator;
        private IMQ2Logger mq2logger;
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
        }

        public async Task Main(CancellationToken token, string[] args)
        {
            Configure(args);

            mq2logger.Log($"Started");

            try
            {
                CancellationToken[] cancellationTokens = await mediator.Send
                (
                    new LoadMQ2Commands()
                    {
                        CancellationToken = token
                    }
                );

                using (CancellationTokenSource linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationTokens))
                {
                    while (!linkedTokenSource.IsCancellationRequested)
                    {
                        var canProcess = await mediator.Send(new CanProcessQuery(), linkedTokenSource.Token);

                        if (canProcess)
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

                        await mediator.Send(new FlushDataTypeErrorsCommand(), linkedTokenSource.Token);

                        await Yield(linkedTokenSource.Token);
                    }
                }

                await mediator.Send(new UnloadMQ2Commands());
            }
            catch (TaskCanceledException) { }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in {nameof(Main)}");
                mq2logger.LogError(ex);
            }

            mq2logger.Log($"Stopped");

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
                mq2logger = serviceProvider.GetRequiredService<IMQ2Logger>();

                // Bootstrap instances of singleton services that listen to events.
                // If we dont they will be lazy loaded and not have any event history
                // for current state.
                _ = serviceProvider.GetRequiredService<IEventService>();
                _ = serviceProvider.GetRequiredService<IMQ2ChatHistory>();
                
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
                .AddEventService()
                .AddMQ2ChatHistory()
                .AddMQ2Logging()
                .AddMQ2Config()
                .AddMQ2CommandProvider()
                .AddAbilityService()
                .AddItemService()
                .AddSpellCastingService();
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
