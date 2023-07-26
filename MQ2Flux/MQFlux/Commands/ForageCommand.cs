using MQ2DotNet.EQ;
using MQ2DotNet.MQ2API.DataTypes;
using MQFlux.Behaviors;
using MQFlux.Core;
using MQFlux.Models;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

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
        public CharacterConfigSection Character { get; set; }
        public FluxConfig Config { get; set; }
        public IContext Context { get; set; }
    }

    public class ForageCommandHandler : PCCommandHandler<ForageCommand>
    {
        private readonly IAbilityService abilityService;
        private readonly IItemService itemService;

        public ForageCommandHandler(IAbilityService abilityService, IItemService itemService)
        {
            this.abilityService = abilityService;
            this.itemService = itemService;
        }

        public override async Task<CommandResponse<bool>> Handle(ForageCommand request, CancellationToken cancellationToken)
        {
            if (!request.Character.AutoForage.GetValueOrDefault(false))
            {
                return CommandResponse.FromResult(false);
            }

            var me = request.Context.TLO.Me;
            var originalState = me.State;

            // TODO can you forage on a horse?
            if (originalState != SpawnState.Sit && originalState != SpawnState.Stand)
            {
                return CommandResponse.FromResult(false);
            }

            if (originalState == SpawnState.Sit)
            {
                me.Stand();
            }

            if (await abilityService.DoAbility(request.Ability, "You have scrounged", "You fail to locate", cancellationToken))
            {
                // Make sure there is something on the cursor, it is possible for something to ninja it.
                if (request.Context.TLO.Cursor != null)
                {
                    var foragedItemName = request.Context.TLO.Cursor.Name;

                    if (request.Character.ForageBlacklist.Contains(foragedItemName))
                    {
                        //await itemService.DropAsync(foragedItemName);
                        await itemService.Destroy(foragedItemName);
                    }
                    else
                    {
                        await itemService.AutoInventory(cancellationToken: cancellationToken);
                    }
                }
            }

            if (originalState == SpawnState.Sit)
            {
                me.Sit();
            }

            return CommandResponse.FromResult(true);
        }
    }
}
