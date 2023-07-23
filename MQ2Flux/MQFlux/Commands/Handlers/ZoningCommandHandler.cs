using MQFlux.Core;
using MQFlux.Services;
namespace MQFlux.Commands.Handlers
{
    public class ZoningCommandHandler : SetCacheCommandHandler<ZoningCommand, bool>
    {
        public override string Key => CacheKeys.Zoning;

        public ZoningCommandHandler(ICache cache) : base(cache)
        {
        }
    }
}
