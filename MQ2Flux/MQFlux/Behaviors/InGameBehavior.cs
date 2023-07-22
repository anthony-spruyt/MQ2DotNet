using MediatR;
using MQ2DotNet.EQ;
using MQFlux.Commands;
using MQFlux.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public class InGameBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : PCCommand<TResponse>
    {
        private readonly IMediator mediator;

        public InGameBehavior(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (await mediator.Send(new GameStateQuery(), cancellationToken) != GameState.InGame)
            {
                return default;
            }

            return await next();
        }
    }
}
