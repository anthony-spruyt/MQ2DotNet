using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IGroupLeaderRequest : IGroupedRequest
    {

    }

    public class GroupLeaderBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is IGroupLeaderRequest groupLeaderRequest &&
                !groupLeaderRequest.Context.TLO.Me.AmIGroupLeader)
            {
                return Task.FromResult(default(TResponse));
            }

            return next();
        }
    }
}
