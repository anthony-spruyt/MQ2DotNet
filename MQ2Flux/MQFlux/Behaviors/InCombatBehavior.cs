using MediatR;
using MQ2DotNet.EQ;
using MQFlux.Core;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IInCombatRequest : IContextRequest
    {

    }

    public class InCombatBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if (request is IInCombatRequest inCombatRequest &&
                inCombatRequest.Context.TLO.Me.CombatState != CombatState.Combat)
            {
                return ShortCircuitResultTask();
            }

            return next();
        }
    }
}
