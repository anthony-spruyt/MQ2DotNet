using MQFlux.Services;

namespace MQFlux.Commands.Handlers
{
    public class SetZoningCommandHandler : SetCacheCommandHandler<SetZoningCommand, bool>
    {
        public override string Key => CacheKeys.Zoning;

        public SetZoningCommandHandler(ICache cache) : base(cache)
        {
        }
    }
}
