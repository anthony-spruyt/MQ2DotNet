using MediatR;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IContextRequest
    {
        /// <summary>
        /// The context that is set by the middleware. Do not set this when creating a new request.
        /// </summary>
        IContext Context { get; set; }
    }

    public class ContextBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IContext context;

        public ContextBehavior(IContext context)
        {
            this.context = context;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is IContextRequest contextRequest)
            {
                contextRequest.Context = context;
            }

            return next();
        }
    }
}