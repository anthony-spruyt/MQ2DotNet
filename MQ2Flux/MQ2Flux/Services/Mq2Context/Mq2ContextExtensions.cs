using Microsoft.Extensions.DependencyInjection;
using MQ2DotNet.MQ2API;
using MQ2DotNet.Services;
using System;

namespace MQ2Flux
{
    public static class Mq2ContextExtensions
    {
        public static IServiceCollection AddMq2Context(this IServiceCollection services, MQ2 mq2, Chat chat, Commands commands, Events events, Spawns spawns, TLO tlo)
        {
            Mq2Context factory(IServiceProvider serviceProvider) => new Mq2Context(mq2, chat, commands, events, spawns, tlo);

            services.AddSingleton<IMq2Context, Mq2Context>(factory);

            return services;
        }
    }
}
