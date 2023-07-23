using MQFlux.Core;
using MQFlux.Services;

namespace MQFlux.Queries
{
    public class CampingQuery : Query<bool>
    {
    }

    public class CampingQueryHandler : CacheQueryHandler<CampingQuery, bool>
    {
        public override bool Default => false;
        public override string Key => CacheKeys.Camping;

        public CampingQueryHandler(ICache cache) : base(cache)
        {
        }
    }
}
