using MediatR;
using MQFlux.Core;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IGroupLeaderRequest : IGroupedRequest
    {

    }

    public class GroupLeaderBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if (request is IGroupLeaderRequest groupLeaderRequest &&
                !groupLeaderRequest.Context.TLO.Me.AmIGroupLeader)
            {
                return ShortCircuitResultTask();
            }

            return next();
        }
    }
}
