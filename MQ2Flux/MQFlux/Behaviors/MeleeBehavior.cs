using MediatR;
using MQFlux.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IMeleeRequest : IContextRequest
    {

    }

    public class MeleeBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : PCCommand<TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is IMeleeRequest meleeRequest &&
                meleeRequest.Context.TLO.Me.Class.PureCaster)
            {
                return Task.FromResult(default(TResponse));
            }

            return next();
        }
    }
}
