using MediatR;
using MQ2Flux.Models;
using MQ2Flux.Notifications;
using MQ2Flux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Handlers
{
    public class ConfigUpdateNotificationHandler : INotificationHandler<ConfigUpdateNotification>
    {
        private readonly ICache cache;

        public ConfigUpdateNotificationHandler(ICache cache)
        {
            this.cache = cache;
        }

        public Task Handle(ConfigUpdateNotification notification, CancellationToken cancellationToken)
        {
            cache.TryRemove(CharacterConfig.CacheKey, out CharacterConfig _);

            return Task.CompletedTask;
        }
    }
}
