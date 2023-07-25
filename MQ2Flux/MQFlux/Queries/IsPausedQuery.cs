using MQFlux.Core;
using MQFlux.Services;

namespace MQFlux.Queries
{
    public class IsPausedQuery : Query<bool>
    {
    }

    public class IsPausedQueryHandler : CacheQueryHandler<IsPausedQuery, bool>
    {
        public override bool Default => false;
        public override string Key => CacheKeys.IsPaused;

        public IsPausedQueryHandler(ICache cache) : base(cache)
        {
        }
    }
}
