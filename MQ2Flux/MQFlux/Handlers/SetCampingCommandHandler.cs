using MQFlux.Commands;
using MQFlux.Services;

namespace MQFlux.Handlers
{
    public class SetCampingCommandHandler : SetCacheCommandHandler<SetCampingCommand, bool>
    {
        public override string Key => CacheKeys.Camping;

        public SetCampingCommandHandler(ICache cache) : base(cache)
        {
        }
    }
}
