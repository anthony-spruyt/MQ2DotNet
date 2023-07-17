using MediatR;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Handlers
{
    public abstract class SetCacheCommandHandler<TRequest, TValue> : IRequestHandler<TRequest> where TRequest : ISetCacheCommand<TValue>
    {
        public abstract string Key { get; }

        private readonly ICache cache;

        protected SetCacheCommandHandler(ICache cache)
        {
            this.cache = cache;
        }

        public Task Handle(TRequest request, CancellationToken cancellationToken)
        {
            cache.AddOrUpdate(Key, request.Value);

            return Task.CompletedTask;
        }
    }
}
