using MediatR;
using MQFlux.Core;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IGroupedRequest : IContextRequest
    {

    }

    public class GroupedBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if (request is IGroupedRequest groupedRequest &&
                !groupedRequest.Context.TLO.Me.Grouped)
            {
                return ShortCircuitResultTask();
            }

            return next();
        }
    }
}
