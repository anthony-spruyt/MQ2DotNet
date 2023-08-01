using MQFlux.Core;
using MQFlux.Models;
using MQFlux.Services;

namespace MQFlux.Queries
{
    public class BusyBuffingQuery : Query<BusyBuffingResult>
    {
    }

    public class BusyBuffingQueryHandler : CacheQueryHandler<BusyBuffingQuery, BusyBuffingResult>
    {
        public override BusyBuffingResult Default => new BusyBuffingResult(true);
        public override string Key => CacheKeys.BusyBuffing;

        public BusyBuffingQueryHandler(ICache cache) : base(cache)
        {
        }
    }
}
