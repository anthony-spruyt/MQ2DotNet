using Microsoft.Extensions.DependencyInjection;

namespace MQ2Flux.Behaviors
{
    public static class BehaviorExtensions
    {
        public static MediatRServiceConfiguration AddFluxBehaviors(this MediatRServiceConfiguration config)
        {
            return config
                .AddOpenBehavior(typeof(MQ2ContextBehavior<,>))
                .AddOpenBehavior(typeof(ConfigBehavior<,>))
                .AddOpenBehavior(typeof(CharacterConfigBehavior<,>));
        }
    }
}
