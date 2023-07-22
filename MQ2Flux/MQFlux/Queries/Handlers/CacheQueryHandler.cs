using MediatR;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Queries.Handlers
{
    public abstract class CacheQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : Query<TResponse>
    {
        public abstract TResponse Default { get; }
        public abstract string Key { get; }

        private readonly ICache cache;

        protected CacheQueryHandler(ICache cache)
        {
            this.cache = cache;
        }

        public virtual Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            if (cache.TryGetValue(Key, out TResponse value))
            {
                return Task.FromResult(value);
            }

            return Task.FromResult(Default);
        }
    }
}
