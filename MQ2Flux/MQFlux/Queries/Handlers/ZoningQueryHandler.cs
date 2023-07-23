using MQFlux.Core;
using MQFlux.Services;

namespace MQFlux.Queries.Handlers
{
    public class ZoningQueryHandler : CacheQueryHandler<ZoningQuery, bool>
    {
        public override bool Default => false;
        public override string Key => CacheKeys.Zoning;

        public ZoningQueryHandler(ICache cache) : base(cache)
        {
        }
    }
}
