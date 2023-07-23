using MediatR;
using MQFlux.Core;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface INoItemOnCursorRequest : IContextRequest
    {

    }

    public class NoItemOnCursorBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if (request is INoItemOnCursorRequest noItemOnCursorRequest &&
                noItemOnCursorRequest.Context.TLO.Cursor != null)
            {
                return ShortCircuitResultTask();
            }

            return next();
        }
    }
}
