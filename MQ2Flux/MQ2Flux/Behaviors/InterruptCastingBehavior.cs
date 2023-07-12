using MediatR;
using MQ2Flux.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Behaviors
{
    public interface IInterruptCastingRequest : IMQ2ContextRequest
    {

    }

    public class InterruptCastingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
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
