using MQFlux.Behaviors;
using MQFlux.Core;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands
{
    public class AutoAttackCommand : CombatCommand, IMeleeRequest
    {
        public override IContext Context { get; set; }
    }

    public class AutoAttackCommandHandler : CombatCommandHandler<AutoAttackCommand>
    {
        public override Task<CommandResponse<bool>> Handle(AutoAttackCommand request, CancellationToken cancellationToken)
        {
            //TODO
            //if ((request.Context.TLO.Target?.Aggressive).GetValueOrDefault(false) &&
            //    !request.Context.TLO.Me.AutoMeleeAttack)
            //{
            //    request.Context.MQ.DoCommand("/attack on");
            //}

            return CommandResponse.FromResultTask(false);
        }
    }
}
