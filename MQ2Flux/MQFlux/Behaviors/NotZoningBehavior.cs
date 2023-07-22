using MediatR;
using MQFlux.Commands;
using MQFlux.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public class NotZoningBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : PCCommand<TResponse>
    {
        private readonly IMediator mediator;

        public NotZoningBehavior(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (await mediator.Send(new ZoningQuery(), cancellationToken))
            {
                return default;
            }

            return await next();
        }
    }
}
