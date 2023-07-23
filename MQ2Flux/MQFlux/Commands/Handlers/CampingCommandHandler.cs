using MQFlux.Core;
using MQFlux.Services;
namespace MQFlux.Commands.Handlers
{
    public class CampingCommandHandler : SetCacheCommandHandler<CampingCommand, bool>
    {
        public override string Key => CacheKeys.Camping;

        public CampingCommandHandler(ICache cache) : base(cache)
        {
        }
    }
}
