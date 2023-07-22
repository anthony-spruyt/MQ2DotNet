using MediatR;
using MQFlux.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface ICasterRequest : IContextRequest
    {

    }

    public class CasterBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : PCCommand<TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is ICasterRequest casterRequest &&
                !casterRequest.Context.TLO.Me.Class.CanCast)
            {
                return Task.FromResult(default(TResponse));
            }

            return next();
        }
    }
}
