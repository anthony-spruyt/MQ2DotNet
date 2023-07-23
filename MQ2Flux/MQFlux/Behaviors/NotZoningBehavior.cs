using MediatR;
using MQFlux.Core;
using MQFlux.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public class NotZoningBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        private readonly IMediator mediator;

        public NotZoningBehavior(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override async Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new ZoningQuery(), cancellationToken);

            if (response.Result)
            {
                return ShortCircuitResult();
            }

            return await next();
        }
    }
}
