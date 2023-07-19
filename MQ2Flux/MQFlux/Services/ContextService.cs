using Microsoft.Extensions.DependencyInjection;
using MQ2DotNet.MQ2API;
using MQ2DotNet.Services;
using System;

namespace MQFlux.Services
{
    public interface IContext
    {
        MQ2 MQ { get; }
        Chat Chat { get; }
        MQ2DotNet.Services.Commands Commands { get; }
        Events Events { get; }
        Spawns Spawns { get; }
        TLO TLO { get; }
    }

    public static class ContextServiceExtensions
    {
        public static IServiceCollection AddContext(this IServiceCollection services, MQ2 mq, Chat chat, MQ2DotNet.Services.Commands commands, Events events, Spawns spawns, TLO tlo)
        {
            ContextService factory(IServiceProvider serviceProvider) => new ContextService(mq, chat, commands, events, spawns, tlo);

            services.AddSingleton<IContext, ContextService>(factory);

            return services;
        }
    }

    public class ContextService : IContext
    {
        public MQ2 MQ { get; private set; }

        public Chat Chat { get; private set; }

        public MQ2DotNet.Services.Commands Commands { get; private set; }

        public Events Events { get; private set; }

        public Spawns Spawns { get; private set; }

        public TLO TLO { get; private set; }

        public ContextService(MQ2 mq, Chat chat, MQ2DotNet.Services.Commands commands, Events events, Spawns spawns, TLO tlo)
        {
            MQ = mq;
            Chat = chat;
            Commands = commands;
            Events = events;
            Spawns = spawns;
            TLO = tlo;
        }
    }
}
