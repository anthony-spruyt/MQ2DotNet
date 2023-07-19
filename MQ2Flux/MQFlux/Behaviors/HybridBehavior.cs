using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IHybridRequest : IContextRequest
    {

    }

    public class HybridBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if
            (
                request is IHybridRequest hybridRequest &&
                (
                    !hybridRequest.Context.TLO.Me.Spawn.Class.CanCast || 
                    hybridRequest.Context.TLO.Me.Spawn.Class.PureCaster
                )
            )
            {
                return Task.FromResult(default(TResponse));
            }

            return next();
        }
    }
}
