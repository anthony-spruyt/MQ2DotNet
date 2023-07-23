using MQFlux.Behaviors;
using MQFlux.Core;
using MQFlux.Models;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public class EatAndDrinkCommand :
        PCCommand,
        ICharacterConfigRequest,
        IConsciousRequest,
        INotCastingRequest,
        INoItemOnCursorRequest,
        INotFeignedDeathRequest,
        IBankWindowNotOpenRequest
    {
        public bool AllowBard => true;
        public CharacterConfig Character { get; set; }
        public FluxConfig Config { get; set; }
        public IContext Context { get; set; }
    }
}
