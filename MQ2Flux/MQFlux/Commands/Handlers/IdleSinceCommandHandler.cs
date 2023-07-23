using MQFlux.Core;
using MQFlux.Services;
using System;

namespace MQFlux.Commands.Handlers
{
    public class IdleSinceCommandHandler : SetCacheCommandHandler<IdleSinceCommand, DateTime>
    {
        public override string Key => CacheKeys.IdleSince;

        public IdleSinceCommandHandler(ICache cache) : base(cache)
        {
        }
    }
}