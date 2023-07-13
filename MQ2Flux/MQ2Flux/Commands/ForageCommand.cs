using MediatR;
using MQ2DotNet.MQ2API.DataTypes;
using MQ2Flux.Behaviors;
using MQ2Flux.Models;
using MQ2Flux.Services;

namespace MQ2Flux.Commands
{
    public class ForageCommand : ICharacterConfigRequest, INotWhenCastingRequest, IAbilityRequest, INotWhenAutoAttackingRequest, INotWhenSpellbookIsOpenRequest, IRequest<bool>
    {
        public CharacterConfig Character { get; set; }
        public FluxConfig Config { get; set; }
        public IMQ2Context Context { get; set; }
        public string AbilityName { get; } = "Forage";
        public AbilityInfo Ability { get; set; }
    }
}
