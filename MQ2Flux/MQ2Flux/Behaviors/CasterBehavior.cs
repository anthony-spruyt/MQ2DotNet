using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Behaviors
{
    public interface ICasterRequest : IMQ2ContextRequest
    {

    }

    public class CasterBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is ICasterRequest casterRequest &&
                !casterRequest.Context.TLO.Me.Spawn.Class.CanCast)
            {
                // Short circuit.
                var response = Task.FromResult(default(TResponse));

                return response;
            }

            return next();
        }
    }
}
