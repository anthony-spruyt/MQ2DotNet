using MQ2DotNet.EQ;
using MQFlux.Services;

namespace MQFlux.Commands.Handlers
{
    public class SetGameStateCommandHandler : SetCacheCommandHandler<SetGameStateCommand, GameState>
    {
        public override string Key => CacheKeys.GameState;

        public SetGameStateCommandHandler(ICache cache) : base(cache)
        {
        }
    }
}
