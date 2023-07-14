using MediatR;
using MQFlux.Behaviors;
using MQFlux.Models;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public class SortInventoryCommand : ICharacterConfigRequest, INotWhenCastingRequest, IRequest<bool>
    {
        public bool AllowBard => true;
        public CharacterConfig Character { get; set; }
        public FluxConfig Config { get; set; }
        public IMQContext Context { get; set; }
    }
}
