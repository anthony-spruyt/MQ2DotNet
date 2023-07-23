using MQFlux.Core;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public class ZoningCommand : ISetCacheCommand<bool>
    {
        public bool Value { get; set; }

        public ZoningCommand(bool value)
        {
            Value = value;
        }
    }

    public class ZoningCommandHandler : SetCacheCommandHandler<ZoningCommand, bool>
    {
        public override string Key => CacheKeys.Zoning;

        public ZoningCommandHandler(ICache cache) : base(cache)
        {
        }
    }
}
