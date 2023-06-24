using MediatR;
using MQ2Flux.Models;

namespace MQ2Flux.Notifications
{
    public class ConfigUpdateNotification : INotification
    {
        public FluxConfig Config { get; set; }
    }
}
