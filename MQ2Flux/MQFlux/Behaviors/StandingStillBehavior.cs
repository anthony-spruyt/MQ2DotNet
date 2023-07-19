using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IStandingStillRequest : IContextRequest
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
