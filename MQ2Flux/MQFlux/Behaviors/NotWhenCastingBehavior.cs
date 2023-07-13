using MediatR;
using MQFlux.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface INotWhenCastingRequest : IMQContextRequest
    {

    }

    public class NotWhenCastingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is INotWhenCastingRequest notWhenCastingRequest &&
                notWhenCastingRequest.Context.TLO.Me.AmICasting())
            {
                return Task.FromResult(default(TResponse));
            }

            return next();
        }
    }
}
