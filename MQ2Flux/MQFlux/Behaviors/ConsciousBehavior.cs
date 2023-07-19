using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IConsciousRequest : IContextRequest
    {

    }

    public class ConsciousBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is IConsciousRequest consciousRequest &&
                consciousRequest.Context.TLO.Me.Spawn.Dead)
            {
                return Task.FromResult(default(TResponse));
            }

            return next();
        }
    }
}
