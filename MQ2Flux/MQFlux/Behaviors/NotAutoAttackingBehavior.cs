using MediatR;
using MQFlux.Core;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface INotAutoAttackingRequest : IContextRequest
    {

    }

    public class NotAutoAttackingBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if (request is INotAutoAttackingRequest notAutoAttackingRequest &&
                (notAutoAttackingRequest.Context.TLO.Me.AutoMeleeAttack || notAutoAttackingRequest.Context.TLO.Me.AutoRangeAttack))
            {
                return ShortCircuitResultTask();
            }

            return next();
        }
    }
}
