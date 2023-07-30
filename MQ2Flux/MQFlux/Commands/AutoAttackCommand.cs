using MQFlux.Behaviors;
using MQFlux.Core;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands
{
    public class AutoAttackCommand : CombatCommand, IMeleeRequest
    {
    }

    public class AutoAttackCommandHandler : CombatCommandHandler<AutoAttackCommand>
    {
        public override Task<CommandResponse<bool>> Handle(AutoAttackCommand request, CancellationToken cancellationToken)
        {
            //TODO
            //if ((context.TLO.Target?.Aggressive).GetValueOrDefault(false) &&
            //    !context.TLO.Me.AutoMeleeAttack)
            //{
            //    context.MQ.DoCommand("/attack on");
            //}

            return CommandResponse.FromResultTask(false);
        }
    }
}
