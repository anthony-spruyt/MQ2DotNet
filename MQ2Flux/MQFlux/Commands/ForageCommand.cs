using MediatR;
using MQ2DotNet.MQ2API.DataTypes;
using MQFlux.Behaviors;
using MQFlux.Models;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public class ForageCommand : 
        ICharacterConfigRequest,
        IConsciousRequest,
        INotWhenCastingRequest, 
        IAbilityRequest, 
        INotWhenAutoAttackingRequest, 
        INotWhenSpellbookIsOpenRequest, 
        INoItemOnCursorRequest, 
        IRequest<bool>
    {
        public AbilityInfo Ability { get; set; }
        public string AbilityName => "Forage";
        public bool AllowBard => true;
        public CharacterConfig Character { get; set; }
        public FluxConfig Config { get; set; }
        public IMQContext Context { get; set; }
    }
}
