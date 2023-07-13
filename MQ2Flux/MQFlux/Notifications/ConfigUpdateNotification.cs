using MediatR;
using MQFlux.Models;

namespace MQFlux.Notifications
{
    public class ConfigUpdateNotification : INotification
    {
        public FluxConfig Config { get; set; }
    }
}
