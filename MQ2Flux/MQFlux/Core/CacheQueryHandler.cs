using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Core
{
    public abstract class CacheQueryHandler<TRequest, TResult> : QueryHandler<TRequest, TResult>
        where TRequest : Query<TResult>
    {
        public abstract TResult Default { get; }
        public abstract string Key { get; }

        private readonly ICache cache;

        protected CacheQueryHandler(ICache cache)
        {
            this.cache = cache;
        }

        public override Task<QueryResponse<TResult>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            if (cache.TryGetValue(Key, out TResult value))
            {
                return QueryResponse.FromResultTask(value);
            }

            return QueryResponse.FromResultTask(Default);
        }
    }
}
