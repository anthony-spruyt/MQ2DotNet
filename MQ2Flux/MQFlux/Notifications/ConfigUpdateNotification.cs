using MediatR;
using MQFlux.Models;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Notifications
{
    public class ConfigUpdateNotification : INotification
    {
        public FluxConfig Config { get; set; }
    }

    public class ConfigUpdateNotificationHandler : INotificationHandler<ConfigUpdateNotification>
    {
        private readonly ICache cache;

        public ConfigUpdateNotificationHandler(ICache cache)
        {
            this.cache = cache;
        }

        public Task Handle(ConfigUpdateNotification notification, CancellationToken cancellationToken)
        {
            cache.TryRemove(CacheKeys.CharacterConfig, out CharacterConfig _);

            return Task.CompletedTask;
        }
    }
}
