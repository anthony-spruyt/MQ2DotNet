using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IMeleeRequest : IMQContextRequest
    {

    }

    public class MeleeBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is IMeleeRequest meleeRequest &&
                meleeRequest.Context.TLO.Me.Spawn.Class.PureCaster)
            {
                return Task.FromResult(default(TResponse));
            }

            return next();
        }
    }
}
