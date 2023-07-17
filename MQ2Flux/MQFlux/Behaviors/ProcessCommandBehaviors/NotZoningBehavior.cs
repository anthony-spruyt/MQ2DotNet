using MediatR;
using MQFlux.Commands;
using MQFlux.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors.ProcessCommandBehaviors
{
    public class NotZoningBehavior : IPipelineBehavior<ProcessCommand, bool>
    {
        private readonly IMediator mediator;

        public NotZoningBehavior(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<bool> Handle(ProcessCommand request, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            return !await mediator.Send(new GetZoningQuery(), cancellationToken) && await next();
        }
    }
}
