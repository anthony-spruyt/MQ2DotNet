using MediatR;
using MQFlux.Behaviors;
using MQFlux.Models;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public class LearnALanguageCommand : ICharacterConfigRequest, IGroupedRequest, IRequest
    {
        public IMQContext Context { get; set; }
        public CharacterConfig Character { get; set; }
        public FluxConfig Config { get; set; }
    }
}
