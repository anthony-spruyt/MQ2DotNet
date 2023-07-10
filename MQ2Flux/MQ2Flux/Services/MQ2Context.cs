using Microsoft.Extensions.DependencyInjection;
using MQ2DotNet.MQ2API;
using MQ2DotNet.Services;
using System;

namespace MQ2Flux.Services
{
    public interface IMQ2Context
    {
        MQ2 MQ2 { get; }
        Chat Chat { get; }
        MQ2DotNet.Services.Commands Commands { get; }
        Events Events { get; }
        Spawns Spawns { get; }
        TLO TLO { get; }
    }

    public static class MQ2ContextExtensions
    {
        public static IServiceCollection AddMQ2Context(this IServiceCollection services, MQ2 mq2, Chat chat, MQ2DotNet.Services.Commands commands, Events events, Spawns spawns, TLO tlo)
        {
            MQ2Context factory(IServiceProvider serviceProvider) => new MQ2Context(mq2, chat, commands, events, spawns, tlo);

            services.AddSingleton<IMQ2Context, MQ2Context>(factory);

            return services;
        }
    }

    public class MQ2Context : IMQ2Context
    {
        public MQ2 MQ2 { get; private set; }

        public Chat Chat { get; private set; }

        public MQ2DotNet.Services.Commands Commands { get; private set; }

        public Events Events { get; private set; }

        public Spawns Spawns { get; private set; }

        public TLO TLO { get; private set; }

        public MQ2Context(MQ2 mq2, Chat chat, MQ2DotNet.Services.Commands commands, Events events, Spawns spawns, TLO tlo)
        {
            MQ2 = mq2;
            Chat = chat;
            Commands = commands;
            Events = events;
            Spawns = spawns;
            TLO = tlo;
        }
    }
}
