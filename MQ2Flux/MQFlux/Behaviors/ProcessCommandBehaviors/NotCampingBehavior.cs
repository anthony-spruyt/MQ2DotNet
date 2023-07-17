using MediatR;
using MQFlux.Commands;
using MQFlux.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors.ProcessCommandBehaviors
{
    public class NotCampingBehavior : IPipelineBehavior<ProcessCommand, bool>
    {
        private readonly IMediator mediator;

        public NotCampingBehavior(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<bool> Handle(ProcessCommand request, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            return !await mediator.Send(new GetCampingQuery(), cancellationToken) && await next();
        }
    }
}
