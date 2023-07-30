using MediatR;
using MQFlux.Core;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IGroupLeaderRequest : IGroupedRequest
    {

    }

    public class GroupLeaderBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        private readonly IContext context;

        public GroupLeaderBehavior(IContext context)
        {
            this.context = context;
        }

        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if (request is IGroupLeaderRequest &&
                !context.TLO.Me.AmIGroupLeader)
            {
                return ShortCircuitResultTask();
            }

            return next();
        }
    }
}
