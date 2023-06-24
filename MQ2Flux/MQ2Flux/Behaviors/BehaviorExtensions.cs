using Microsoft.Extensions.DependencyInjection;

namespace MQ2Flux
{
    public static class BehaviorExtensions
    {
        public static MediatRServiceConfiguration AddFluxBehaviors(this MediatRServiceConfiguration config)
        {
            return config
                .AddOpenBehavior(typeof(Mq2ContextBehavior<,>))
                .AddOpenBehavior(typeof(ConfigBehavior<,>))
                .AddOpenBehavior(typeof(CharacterConfigBehavior<,>));
        }
    }
}
