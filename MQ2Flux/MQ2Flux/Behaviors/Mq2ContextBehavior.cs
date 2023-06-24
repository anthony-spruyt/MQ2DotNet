using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux
{
    public class Mq2ContextBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IMq2Context context;

        public Mq2ContextBehavior(IMq2Context context)
        {
            this.context = context;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is IMq2ContextRequest contextRequest)
            {
                contextRequest.Context = context;
            }

            return next();
        }
    }
}