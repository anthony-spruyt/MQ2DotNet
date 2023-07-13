using MediatR;
using MQ2Flux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Behaviors
{
    public interface IMQ2ContextRequest
    {
        /// <summary>
        /// The context that is set by the middleware. Do not set this when creating a new request.
        /// </summary>
        IMQ2Context Context { get; set; }
    }

    public class MQ2ContextBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IMQ2Context context;

        public MQ2ContextBehavior(IMQ2Context context)
        {
            this.context = context;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is IMQ2ContextRequest contextRequest)
            {
                contextRequest.Context = context;
            }

            return next();
        }
    }
}