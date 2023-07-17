using MediatR;
using MQ2DotNet.EQ;
using MQFlux.Commands;
using MQFlux.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors.ProcessCommandBehaviors
{
    public class InGameBehavior : IPipelineBehavior<ProcessCommand, bool>
    {
        private readonly IMediator mediator;

        public InGameBehavior(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<bool> Handle(ProcessCommand request, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetGameStateQuery(), cancellationToken) == GameState.InGame && await next();
        }
    }
}
