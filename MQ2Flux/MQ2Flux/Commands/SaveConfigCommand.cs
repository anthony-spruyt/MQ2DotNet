using MediatR;
using MQ2Flux.Behaviors;
using MQ2Flux.Models;

namespace MQ2Flux.Commands
{
    public class SaveConfigCommand : IConfigRequest
    {
        public FluxConfig Config { get; set; }
        public bool Notify { get; set; } = false;
    }
}
