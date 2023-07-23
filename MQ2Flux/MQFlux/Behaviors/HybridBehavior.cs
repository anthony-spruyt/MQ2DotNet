using MediatR;
using MQFlux.Core;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IHybridRequest : IContextRequest
    {

    }

    public class HybridBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if
            (
                request is IHybridRequest hybridRequest &&
                (
                    !hybridRequest.Context.TLO.Me.Class.CanCast ||
                    hybridRequest.Context.TLO.Me.Class.PureCaster
                )
            )
            {
                return ShortCircuitResultTask();
            }

            return next();
        }
    }
}
