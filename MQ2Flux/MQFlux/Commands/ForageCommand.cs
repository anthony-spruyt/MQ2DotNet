using MQ2DotNet.MQ2API.DataTypes;
using MQFlux.Behaviors;
using MQFlux.Core;
using MQFlux.Models;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public class ForageCommand :
        PCCommand,
        ICharacterConfigRequest,
        IConsciousRequest,
        INotCastingRequest,
        IAbilityRequest,
        INotAutoAttackingRequest,
        ISpellbookNotOpenRequest,
        INoItemOnCursorRequest,
        INotFeignedDeathRequest
    {
        public AbilityInfo Ability { get; set; }
        public string AbilityName => "Forage";
        public bool AllowBard => true;
        public CharacterConfig Character { get; set; }
        public FluxConfig Config { get; set; }
        public IContext Context { get; set; }
    }
}
