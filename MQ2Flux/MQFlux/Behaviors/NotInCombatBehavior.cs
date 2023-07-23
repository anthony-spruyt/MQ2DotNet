using MediatR;
using MQ2DotNet.EQ;
using MQFlux.Core;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface INotInCombatRequest : IContextRequest
    {

    }

    public class NotInCombatBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if (request is INotInCombatRequest notInCombatRequest &&
                notInCombatRequest.Context.TLO.Me.CombatState == CombatState.Combat)
            {
                return ShortCircuitResultTask();
            }

            return next();
        }
    }
}
