using MediatR;
using MQFlux.Commands;
using MQFlux.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public class NotCampingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : PCCommand<TResponse>
    {
        private readonly IMediator mediator;

        public NotCampingBehavior(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (await mediator.Send(new CampingQuery(), cancellationToken))
            {
                return default;
            }

            return await next();
        }
    }
}
