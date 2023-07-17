using MediatR;
using MQ2DotNet.EQ;
using MQFlux.Commands;
using MQFlux.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public class PCActiveBehavior : IPipelineBehavior<ProcessCommand, bool>
    {
        private readonly IMediator mediator;

        public PCActiveBehavior(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<bool> Handle(ProcessCommand request, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var canProcess =
                await mediator.Send(new GetGameStateQuery(), cancellationToken) == GameState.InGame &&
                !await mediator.Send(new GetCampingQuery(), cancellationToken) &&
                !await mediator.Send(new GetZoningQuery(), cancellationToken);

            if (!canProcess)
            {
                return false;
            }

            return await next();
        }
    }
}
