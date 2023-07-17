using MQFlux.Queries;
using MQFlux.Services;

namespace MQFlux.Handlers
{
    public class GetZoningQueryHandler : GetCacheQueryHandler<GetZoningQuery, bool>
    {
        public override bool Default => false;
        public override string Key => CacheKeys.Zoning;

        public GetZoningQueryHandler(ICache cache) : base(cache)
        {
        }
    }
}
