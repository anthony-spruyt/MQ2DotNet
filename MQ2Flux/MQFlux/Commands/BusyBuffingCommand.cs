using MQFlux.Core;
using MQFlux.Models;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public class BusyBuffingCommand : ISetCacheCommand<BusyBuffingResult>
    {
        public BusyBuffingResult Value { get; set; }

        public BusyBuffingCommand(bool isBusyBuffing)
        {
            Value = new BusyBuffingResult(isBusyBuffing);
        }
    }

    public class BusyBuffingCommandHandler : SetCacheCommandHandler<BusyBuffingCommand, BusyBuffingResult>
    {
        public override string Key => CacheKeys.BusyBuffing;

        public BusyBuffingCommandHandler(ICache cache) : base(cache)
        {
        }
    }
}
