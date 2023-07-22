using MQFlux.Behaviors;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public class AutoAttackCommand : CombatCommand<bool>, IMeleeRequest
    {
        public override IContext Context { get; set; }
    }
}
