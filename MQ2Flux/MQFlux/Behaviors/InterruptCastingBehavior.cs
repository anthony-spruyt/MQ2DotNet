using MediatR;
using MQFlux.Core;
using MQFlux.Extensions;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IInterruptCastingRequest
    {

    }

    public class InterruptCastingBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        private readonly IContext context;

        public InterruptCastingBehavior(IContext context)
        {
            this.context = context;
        }

        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if (request is IInterruptCastingRequest &&
                context.TLO.Me.AmICasting())
            {
                context.TLO.Me.StopCast();
            }

            return next();
        }
    }
}
