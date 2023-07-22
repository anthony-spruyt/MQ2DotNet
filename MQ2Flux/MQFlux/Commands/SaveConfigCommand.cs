using MediatR;
using MQFlux.Behaviors;
using MQFlux.Models;

namespace MQFlux.Commands
{
    public class SaveConfigCommand : Command<Unit>, IConfigRequest
    {
        public FluxConfig Config { get; set; }
        public bool Notify { get; set; } = false;
    }
}
