using MQFlux.Commands;
using MQFlux.Services;

namespace MQFlux.Handlers
{
    public class SetZoningCommandHandler : SetCacheCommandHandler<SetZoningCommand, bool>
    {
        public override string Key => CacheKeys.Zoning;

        public SetZoningCommandHandler(ICache cache) : base(cache)
        {
        }
    }
}
