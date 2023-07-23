using MediatR;
using MQ2DotNet.EQ;
using MQFlux.Core;
using MQFlux.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public class InGameBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        private readonly IMediator mediator;

        public InGameBehavior(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override async Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new GameStateQuery(), cancellationToken);

            if (response.Result != GameState.InGame)
            {
                return ShortCircuitResult();
            }

            return await next();
        }
    }
}
