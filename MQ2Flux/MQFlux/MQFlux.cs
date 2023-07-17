﻿using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MQ2DotNet.MQ2API;
using MQ2DotNet.Program;
using MQ2DotNet.Services;
using MQFlux.Behaviors;
using MQFlux.Commands;
using MQFlux.Services;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux
{
    public class MQFlux : IProgram
    {
        public static Task Yield(CancellationToken cancellationToken) => Task.Delay(200, cancellationToken);

        private readonly MQ2 mq;
        private readonly Chat chat;
        private readonly MQ2DotNet.Services.Commands commands;
        private readonly Events events;
        private readonly Spawns spawns;
        private readonly TLO tlo;

        private ServiceProvider serviceProvider;
        private ILogger<MQFlux> logger;
        private IMediator mediator;
        private IMQLogger mqLogger;
        private bool disposedValue;

        public MQFlux(MQ2 mq, Chat chat, MQ2DotNet.Services.Commands commands, Events events, Spawns spawns, TLO tlo)
        {
            if (mq is null)
            {
                throw new ArgumentNullException(nameof(mq));
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

            this.mq = mq;
            this.chat = chat;
            this.commands = commands;
            this.events = events;
            this.spawns = spawns;
            this.tlo = tlo;
        }

        public async Task Main(CancellationToken token, string[] args)
        {
            Configure(args);

            mqLogger.Log($"Started");

            try
            {
                CancellationToken[] cancellationTokens = await mediator.Send
                (
                    new LoadMQCommands()
                    {
                        CancellationToken = token
                    }
                );

                using (CancellationTokenSource linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationTokens))
                {
                    await mediator.Send(new InitializeCommand(), linkedTokenSource.Token);

                    while (!linkedTokenSource.IsCancellationRequested)
                    {
                        _ = await mediator.Send(new ProcessCommand(),linkedTokenSource.Token);
                        await mediator.Send(new FlushDataTypeErrorsCommand(), linkedTokenSource.Token);
                        await Yield(linkedTokenSource.Token);
                    }
                }

                await mediator.Send(new UnloadMQCommands());
            }
            catch (TaskCanceledException) { }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in {nameof(Main)}");
                mqLogger.LogError(ex);
            }

            mqLogger.Log($"Stopped");

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

                logger = serviceProvider.GetRequiredService<ILogger<MQFlux>>();
                mediator = serviceProvider.GetRequiredService<IMediator>();
                mqLogger = serviceProvider.GetRequiredService<IMQLogger>();

                // Bootstrap instances of singleton services that listen to events.
                // If we dont they will be lazy loaded and not have any event history
                // for current state.
                _ = serviceProvider.GetRequiredService<IEventService>();
                _ = serviceProvider.GetRequiredService<IChatHistory>();
                
                return configuration;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to configure MQFlux", ex);
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
                        config.RegisterServicesFromAssemblyContaining<MQFlux>();
                        config.AddFluxBehaviors();
                    }
                )
                .AddMemoryCache()
                .AddMQContext(mq, chat, commands, events, spawns, tlo)
                .AddEventService()
                .AddChatHistory()
                .AddMQLogging()
                .AddMQFluxConfig()
                .AddMQCommandProvider()
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
        // ~MQFlux()
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
