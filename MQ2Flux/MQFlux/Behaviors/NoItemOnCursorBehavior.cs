using MediatR;
using MQFlux.Core;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface INoItemOnCursorRequest
    {

    }

    public class NoItemOnCursorBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        private readonly IContext context;

        public NoItemOnCursorBehavior(IContext context)
        {
            this.context = context;
        }

        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if (request is INoItemOnCursorRequest &&
                context.TLO.Cursor != null)
            {
                return ShortCircuitResultTask();
            }

            return next();
        }
    }
}
