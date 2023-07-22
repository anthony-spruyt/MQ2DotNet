using MediatR;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Queries.Handlers
{
    public abstract class GetCacheQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public abstract TResponse Default { get; }
        public abstract string Key { get; }

        private readonly ICache cache;

        protected GetCacheQueryHandler(ICache cache)
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
