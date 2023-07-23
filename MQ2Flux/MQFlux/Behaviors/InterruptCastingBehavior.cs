using MediatR;
using MQFlux.Core;
using MQFlux.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IInterruptCastingRequest : IContextRequest
    {

    }

    public class InterruptCastingBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if (request is IInterruptCastingRequest interruptCastingRequest &&
                interruptCastingRequest.Context.TLO.Me.AmICasting())
            {
                interruptCastingRequest.Context.TLO.Me.StopCast();
            }

            return next();
        }
    }
}
