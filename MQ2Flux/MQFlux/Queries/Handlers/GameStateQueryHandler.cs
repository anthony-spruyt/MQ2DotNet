using MQ2DotNet.EQ;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Queries.Handlers
{
    public class GameStateQueryHandler : CacheQueryHandler<GameStateQuery, GameState>
    {
        public override GameState Default => GameState.Unknown;
        public override string Key => CacheKeys.GameState;

        private readonly IContext context;

        public GameStateQueryHandler(ICache cache, IContext context) : base(cache)
        {
            this.context = context;
        }

        public override async Task<GameState> Handle(GameStateQuery request, CancellationToken cancellationToken)
        {
            var result = await base.Handle(request, cancellationToken);

            // TODO We are supposed to get an initial event triggered in the event service from MQ2DotNet/MQ client but it does not happen.
            if (result == GameState.Unknown)
            {
                result = context.TLO.EverQuest.GameState ?? GameState.Unknown;
            }

            return result;
        }
    }
}
