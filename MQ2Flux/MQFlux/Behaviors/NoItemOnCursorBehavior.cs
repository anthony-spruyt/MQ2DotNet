using MediatR;
using MQFlux.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface INoItemOnCursorRequest : IContextRequest
    {

    }

    public class NoItemOnCursorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : Command<TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is INoItemOnCursorRequest noItemOnCursorRequest &&
                noItemOnCursorRequest.Context.TLO.Cursor != null)
            {
                return Task.FromResult(default(TResponse));
            }

            return next();
        }
    }
}
