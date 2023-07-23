using MQ2DotNet.EQ;
using MQFlux.Core;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public class GameStateCommand : ISetCacheCommand<GameState>
    {
        public GameState Value { get; set; }

        public GameStateCommand(GameState value)
        {
            Value = value;
        }
    }

    public class GameStateCommandHandler : SetCacheCommandHandler<GameStateCommand, GameState>
    {
        public override string Key => CacheKeys.GameState;

        public GameStateCommandHandler(ICache cache) : base(cache)
        {
        }
    }
}
