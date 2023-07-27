using Microsoft.Extensions.DependencyInjection;

namespace MQFlux.Behaviors
{
    public static class BehaviorExtensions
    {
        public static MediatRServiceConfiguration AddFluxBehaviors(this MediatRServiceConfiguration config)
        {
            return config
                .AddOpenBehavior(typeof(QueryExceptionBehavior<,>))
                .AddOpenBehavior(typeof(CommandExceptionBehavior<,>))
                .AddOpenBehavior(typeof(ContextBehavior<,>))
                .AddOpenBehavior(typeof(FluxConfigBehavior<,>))
                .AddOpenBehavior(typeof(CharacterConfigBehavior<,>))
                .AddOpenBehavior(typeof(InGameBehavior<,>))
                .AddOpenBehavior(typeof(NotZoningBehavior<,>))
                .AddOpenBehavior(typeof(NotCampingBehavior<,>))
                .AddOpenBehavior(typeof(ConsciousBehavior<,>))
                .AddOpenBehavior(typeof(NoItemOnCursorBehavior<,>))
                .AddOpenBehavior(typeof(InCombatBehavior<,>))
                .AddOpenBehavior(typeof(NotInCombatBehavior<,>))
                .AddOpenBehavior(typeof(StandingStillBehavior<,>))
                .AddOpenBehavior(typeof(CasterBehavior<,>))
                .AddOpenBehavior(typeof(NotCastingBehavior<,>))
                .AddOpenBehavior(typeof(InterruptCastingBehavior<,>))
                .AddOpenBehavior(typeof(HybridBehavior<,>))
                .AddOpenBehavior(typeof(MeleeBehavior<,>))
                .AddOpenBehavior(typeof(NotAutoAttackingBehavior<,>))
                .AddOpenBehavior(typeof(AbilityBehavior<,>))
                .AddOpenBehavior(typeof(SpellbookNotOpenBehavior<,>))
                .AddOpenBehavior(typeof(NotFeignedDeathBehavior<,>))
                .AddOpenBehavior(typeof(GroupedBehavior<,>))
                .AddOpenBehavior(typeof(GroupLeaderBehavior<,>))
                .AddOpenBehavior(typeof(FreeInventorySlotBehavior<,>))
                .AddOpenBehavior(typeof(BankWindowNotOpenBehavior<,>))
                .AddOpenBehavior(typeof(MainAssistBehavior<,>))
                .AddOpenBehavior(typeof(IdleTimeBehavior<,>));
        }
    }
}
