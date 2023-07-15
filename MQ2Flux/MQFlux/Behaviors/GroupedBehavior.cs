using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IGroupedRequest : IMQContextRequest
    {

    }

    public class GroupedBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is IGroupedRequest groupedRequest &&
                !groupedRequest.Context.TLO.Me.Grouped)
            {
                return Task.FromResult(default(TResponse));
            }

            return next();
        }
    }
}
