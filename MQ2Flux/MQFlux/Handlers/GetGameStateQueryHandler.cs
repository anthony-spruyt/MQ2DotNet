using MQ2DotNet.EQ;
using MQFlux.Queries;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Handlers
{
    public class GetGameStateQueryHandler : GetCacheQueryHandler<GetGameStateQuery, GameState>
    {
        public override GameState Default => GameState.Unknown;
        public override string Key => CacheKeys.GameState;

        private readonly IMQContext context;

        public GetGameStateQueryHandler(ICache cache, IMQContext context) : base(cache)
        {
            this.context = context;
        }

        public override async Task<GameState> Handle(GetGameStateQuery request, CancellationToken cancellationToken)
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
