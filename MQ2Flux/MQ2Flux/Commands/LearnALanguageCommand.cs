using MediatR;
using MQ2Flux.Behaviors;
using MQ2Flux.Models;
using MQ2Flux.Services;

namespace MQ2Flux.Commands
{
    public class LearnALanguageCommand : ICharacterConfigRequest, IRequest
    {
        public IMQContext Context { get; set; }
        public CharacterConfig Character { get; set; }
        public FluxConfig Config { get; set; }
    }
}
