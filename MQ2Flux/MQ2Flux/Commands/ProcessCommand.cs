using MediatR;
using MQ2Flux.Behaviors;
using MQ2Flux.Models;
using MQ2Flux.Services;

namespace MQ2Flux.Commands
{
    public class ProcessCommand : ICharacterConfigRequest, IRequest
    {
        public string[] Args { get; set; }
        public IMQ2Context Context { get; set; }
        public FluxConfig Config { get; set; }
        public CharacterConfig Character { get; set; }
    }
}
