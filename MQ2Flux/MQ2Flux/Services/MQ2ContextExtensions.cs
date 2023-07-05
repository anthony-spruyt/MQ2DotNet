using Microsoft.Extensions.DependencyInjection;
using MQ2DotNet.MQ2API;
using MQ2DotNet.Services;
using System;

namespace MQ2Flux.Services
{
    public static class MQ2ContextExtensions
    {
        public static IServiceCollection AddMQ2Context(this IServiceCollection services, MQ2 mq2, Chat chat, MQ2DotNet.Services.Commands commands, Events events, Spawns spawns, TLO tlo)
        {
            MQ2Context factory(IServiceProvider serviceProvider) => new MQ2Context(mq2, chat, commands, events, spawns, tlo);

            services.AddSingleton<IMQ2Context, MQ2Context>(factory);

            return services;
        }
    }
}
