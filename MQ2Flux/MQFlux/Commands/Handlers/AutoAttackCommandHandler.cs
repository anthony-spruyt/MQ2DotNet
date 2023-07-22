using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands.Handlers
{
    public class AutoAttackCommandHandler : IRequestHandler<AutoAttackCommand, bool>
    {
        public Task<bool> Handle(AutoAttackCommand request, CancellationToken cancellationToken)
        {
            //TODO
            //if ((request.Context.TLO.Target?.Aggressive).GetValueOrDefault(false) &&
            //    !request.Context.TLO.Me.AutoMeleeAttack)
            //{
            //    request.Context.MQ.DoCommand("/attack on");
            //}

            return Task.FromResult(false);
        }
    }
}
