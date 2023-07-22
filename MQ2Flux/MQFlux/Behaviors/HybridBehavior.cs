using MediatR;
using MQFlux.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IHybridRequest : IContextRequest
    {

    }

    public class HybridBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : Command<TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
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
                return Task.FromResult(default(TResponse));
            }

            return next();
        }
    }
}
