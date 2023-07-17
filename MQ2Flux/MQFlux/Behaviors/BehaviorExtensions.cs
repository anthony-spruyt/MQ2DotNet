using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MQFlux.Commands;

namespace MQFlux.Behaviors
{
    public static class BehaviorExtensions
    {
        public static MediatRServiceConfiguration AddFluxBehaviors(this MediatRServiceConfiguration config)
        {
            return config
                .AddBehavior<IPipelineBehavior<ProcessCommand, bool>, PCActiveBehavior>()
                .AddOpenBehavior(typeof(MQContextBehavior<,>))
                .AddOpenBehavior(typeof(ConfigBehavior<,>))
                .AddOpenBehavior(typeof(CharacterConfigBehavior<,>))
                .AddOpenBehavior(typeof(ConsciousBehavior<,>))
                .AddOpenBehavior(typeof(NoItemOnCursorBehavior<,>))
                .AddOpenBehavior(typeof(InCombatBehavior<,>))
                .AddOpenBehavior(typeof(NotInCombatBehavior<,>))
                .AddOpenBehavior(typeof(StandingStillBehavior<,>))
                .AddOpenBehavior(typeof(CasterBehavior<,>))
                .AddOpenBehavior(typeof(NotWhenCastingBehavior<,>))
                .AddOpenBehavior(typeof(InterruptCastingBehavior<,>))
                .AddOpenBehavior(typeof(HybridBehavior<,>))
                .AddOpenBehavior(typeof(MeleeBehavior<,>))
                .AddOpenBehavior(typeof(NotWhenAutoAttackingBehavior<,>))
                .AddOpenBehavior(typeof(AbilityBehavior<,>))
                .AddOpenBehavior(typeof(NotWhenSpellbookIsOpenBehavior<,>))
                .AddOpenBehavior(typeof(GroupedBehavior<,>))
                .AddOpenBehavior(typeof(GroupLeaderBehavior<,>))
                .AddOpenBehavior(typeof(FreeInventorySlotBehavior<,>));
        }
    }
}
