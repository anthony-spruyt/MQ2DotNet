using MediatR;
using MQ2Flux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Behaviors
{
    public interface IMQContextRequest
    {
        /// <summary>
        /// The context that is set by the middleware. Do not set this when creating a new request.
        /// </summary>
        IMQContext Context { get; set; }
    }

    public class MQContextBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IMQContext context;

        public MQContextBehavior(IMQContext context)
        {
            this.context = context;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is IMQContextRequest contextRequest)
            {
                contextRequest.Context = context;
            }

            return next();
        }
    }
}