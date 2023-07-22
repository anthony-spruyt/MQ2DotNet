using MQ2DotNet.EQ;
using MQFlux.Services;

namespace MQFlux.Commands.Handlers
{
    public class GameStateCommandHandler : SetCacheCommandHandler<GameStateCommand, GameState>
    {
        public override string Key => CacheKeys.GameState;

        public GameStateCommandHandler(ICache cache) : base(cache)
        {
        }
    }
}
