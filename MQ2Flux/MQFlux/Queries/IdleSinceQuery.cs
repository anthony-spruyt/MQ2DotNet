using MQFlux.Core;
using MQFlux.Services;
using System;

namespace MQFlux.Queries
{
    public class IdleSinceQuery : Query<DateTime>
    {
    }

    public class IdleSinceQueryHandler : CacheQueryHandler<IdleSinceQuery, DateTime>
    {
        public override DateTime Default => DateTime.UtcNow;
        public override string Key => CacheKeys.IdleSince;

        public IdleSinceQueryHandler(ICache cache) : base(cache)
        {
        }
    }
}
