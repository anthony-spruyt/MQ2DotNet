using MQ2DotNet.EQ;

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
