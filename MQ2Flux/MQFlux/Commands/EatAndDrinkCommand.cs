using MediatR;
using MQFlux.Behaviors;
using MQFlux.Models;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public class EatAndDrinkCommand : 
        ICharacterConfigRequest,
        IConsciousRequest, 
        INotWhenCastingRequest, 
        INoItemOnCursorRequest, 
        IRequest<bool>
    {
        public bool AllowBard => true;
        public CharacterConfig Character { get; set; }
        public FluxConfig Config { get; set; }
        public IMQContext Context { get; set; }
    }
}
