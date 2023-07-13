using MediatR;
using MQFlux.Models;
using MQFlux.Notifications;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Handlers
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
