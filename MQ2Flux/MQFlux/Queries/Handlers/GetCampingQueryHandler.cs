using MQFlux.Services;

namespace MQFlux.Queries.Handlers
{
    public class GetCampingQueryHandler : GetCacheQueryHandler<GetCampingQuery, bool>
    {
        public override bool Default => false;
        public override string Key => CacheKeys.Camping;

        public GetCampingQueryHandler(ICache cache) : base(cache)
        {
        }
    }
}
