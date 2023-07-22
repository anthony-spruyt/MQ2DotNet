using MediatR;
using MQFlux.Commands;
using MQFlux.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IInterruptCastingRequest : IContextRequest
    {

    }

    public class InterruptCastingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : Command<TResponse>
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
