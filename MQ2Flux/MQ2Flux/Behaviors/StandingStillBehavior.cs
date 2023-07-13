using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Behaviors
{
    public interface IStandingStillRequest : IMQ2ContextRequest
    {

    }

    public class StandingStillBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is IStandingStillRequest standingStillRequest &&
                standingStillRequest.Context.TLO.Me.Moving)
            {
                return Task.FromResult(default(TResponse));
            }

            return next();
        }
    }
}
