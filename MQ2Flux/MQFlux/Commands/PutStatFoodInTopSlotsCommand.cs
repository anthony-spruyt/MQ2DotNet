using MQFlux.Behaviors;
using MQFlux.Core;
using MQFlux.Models;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public class PutStatFoodInTopSlotsCommand :
        PCCommand,
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
