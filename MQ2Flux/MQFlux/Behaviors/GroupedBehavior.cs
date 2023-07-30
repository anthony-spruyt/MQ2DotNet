using MediatR;
using MQFlux.Core;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IGroupedRequest
    {

    }

    public class GroupedBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        private readonly IContext context;

        public GroupedBehavior(IContext context)
        {
            this.context = context;
        }

        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if (request is IGroupedRequest &&
                !context.TLO.Me.Grouped)
            {
                return ShortCircuitResultTask();
            }

            return next();
        }
    }
}
