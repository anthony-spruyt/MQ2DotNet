using MQFlux.Core;
using MQFlux.Services;
using System;

namespace MQFlux.Commands
{
    public class IdleSinceCommand : ISetCacheCommand<DateTime>
    {
        public DateTime Value { get; set; }

        public IdleSinceCommand()
        {
            Value = DateTime.UtcNow;
        }
    }

    public class IdleSinceCommandHandler : SetCacheCommandHandler<IdleSinceCommand, DateTime>
    {
        public override string Key => CacheKeys.IdleSince;

        public IdleSinceCommandHandler(ICache cache) : base(cache)
        {
        }
    }
}