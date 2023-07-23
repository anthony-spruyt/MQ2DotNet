using MQFlux.Core;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public class CampingCommand : ISetCacheCommand<bool>
    {
        public bool Value { get; set; }

        public CampingCommand(bool value)
        {
            Value = value;
        }
    }

    public class CampingCommandHandler : SetCacheCommandHandler<CampingCommand, bool>
    {
        public override string Key => CacheKeys.Camping;

        public CampingCommandHandler(ICache cache) : base(cache)
        {
        }
    }
}
