using MQFlux.Behaviors;
using MQFlux.Models;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public class PutStatFoodInTopSlotsCommand :
        Command<bool>,
        ICharacterConfigRequest,
        IConsciousRequest,
        INotCastingRequest,
        INoItemOnCursorRequest
    {
        public bool AllowBard => true;
        public CharacterConfig Character { get; set; }
        public FluxConfig Config { get; set; }
        public IContext Context { get; set; }
    }
}
