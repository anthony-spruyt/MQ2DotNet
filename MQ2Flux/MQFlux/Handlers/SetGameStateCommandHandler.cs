using MQ2DotNet.EQ;
using MQFlux.Commands;
using MQFlux.Services;

namespace MQFlux.Handlers
{
    public class SetGameStateCommandHandler : SetCacheCommandHandler<SetGameStateCommand, GameState>
    {
        public override string Key => CacheKeys.GameState;

        public SetGameStateCommandHandler(ICache cache) : base(cache)
        {
        }
    }
}
