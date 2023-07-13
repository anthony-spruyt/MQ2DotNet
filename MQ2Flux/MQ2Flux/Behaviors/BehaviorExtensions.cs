using Microsoft.Extensions.DependencyInjection;

namespace MQ2Flux.Behaviors
{
    public static class BehaviorExtensions
    {
        public static MediatRServiceConfiguration AddFluxBehaviors(this MediatRServiceConfiguration config)
        {
            return config
                .AddOpenBehavior(typeof(MQContextBehavior<,>))
                .AddOpenBehavior(typeof(ConfigBehavior<,>))
                .AddOpenBehavior(typeof(CharacterConfigBehavior<,>))
                .AddOpenBehavior(typeof(InCombatBehavior<,>))
                .AddOpenBehavior(typeof(NotInCombatBehavior<,>))
                .AddOpenBehavior(typeof(StandingStillBehavior<,>))
                .AddOpenBehavior(typeof(CasterBehavior<,>))
                .AddOpenBehavior(typeof(NotWhenCastingBehavior<,>))
                .AddOpenBehavior(typeof(InterruptCastingBehavior<,>))
                .AddOpenBehavior(typeof(NotWhenAutoAttackingBehavior<,>))
                .AddOpenBehavior(typeof(AbilityBehavior<,>))
                .AddOpenBehavior(typeof(NotWhenSpellbookIsOpenBehavior<,>));
        }
    }
}
