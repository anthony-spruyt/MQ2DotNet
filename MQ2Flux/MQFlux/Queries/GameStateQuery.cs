using MediatR;
using MQ2DotNet.EQ;
using MQFlux.Commands;
using MQFlux.Core;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Queries
{
    public class GameStateQuery : Query<GameState>
    {
    }

    public class GameStateQueryHandler : CacheQueryHandler<GameStateQuery, GameState>
    {
        public override GameState Default => GameState.Unknown;
        public override string Key => CacheKeys.GameState;
        
        private readonly IContext context;
        private readonly IMediator mediator;

        public GameStateQueryHandler(ICache cache, IContext context, IMediator mediator) : base(cache)
        {
            this.context = context;
            this.mediator = mediator;
        }

        public override async Task<QueryResponse<GameState>> Handle(GameStateQuery request, CancellationToken cancellationToken)
        {
            var response = await base.Handle(request, cancellationToken);

            // TODO We are supposed to get an initial event triggered in the event service from MQ2DotNet/MQ client but it does not happen.
            if (response.Result == GameState.Unknown)
            {
                var gameState = context.TLO.EverQuest.GameState.GetValueOrDefault(GameState.Unknown);

                await mediator.Send(new GameStateCommand(gameState), cancellationToken);

                return QueryResponse.FromResult(gameState);
            }

            return QueryResponse.FromResult(response.Result);
        }
    }
}
