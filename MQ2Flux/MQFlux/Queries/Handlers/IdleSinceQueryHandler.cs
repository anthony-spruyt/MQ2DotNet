using MQFlux.Core;
using MQFlux.Services;
using System;

namespace MQFlux.Queries.Handlers
{
    public class IdleSinceQueryHandler : CacheQueryHandler<IdleSinceQuery, DateTime>
    {
        public override DateTime Default => DateTime.UtcNow;
        public override string Key => CacheKeys.IdleSince;

        public IdleSinceQueryHandler(ICache cache) : base(cache)
        {
        }
    }
}
