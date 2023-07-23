using MQ2DotNet.EQ;
using MQFlux.Core;

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
}
