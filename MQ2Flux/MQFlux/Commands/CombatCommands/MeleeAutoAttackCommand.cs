using MQFlux.Behaviors.CombatCommandBehaviors;
using MQFlux.Services;

namespace MQFlux.Commands.CombatCommands
{
    // Testing... TODO 
    public class MeleeAutoAttackCommand : CombatCommand<bool>, IMainAssistRequest
    {
        public override IMQContext Context { get; set; }
    }
}
