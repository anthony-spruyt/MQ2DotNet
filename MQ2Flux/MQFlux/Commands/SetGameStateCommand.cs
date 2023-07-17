using MQ2DotNet.EQ;

namespace MQFlux.Commands
{
    public class SetGameStateCommand : ISetCacheCommand<GameState>
    {
        public GameState Value { get; set; }

        public SetGameStateCommand(GameState value)
        {
            Value = value;
        }
    }
}
