using MQFlux.Behaviors;
using MQFlux.Core;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public class AutoAttackCommand : CombatCommand, IMeleeRequest
    {
        public override IContext Context { get; set; }
    }
}
