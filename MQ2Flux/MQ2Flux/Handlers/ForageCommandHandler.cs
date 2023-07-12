using MediatR;
using MQ2DotNet.EQ;
using MQ2Flux.Commands;
using MQ2Flux.Extensions;
using MQ2Flux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Handlers
{
    public class ForageCommandHandler : IRequestHandler<ForageCommand, bool>
    {
        private readonly IAbilityService abilityService;
        private readonly IItemService itemService;

        public ForageCommandHandler(IAbilityService abilityService, IItemService itemService)
        {
            this.abilityService = abilityService;
            this.itemService = itemService;
        }

        public async Task<bool> Handle(ForageCommand request, CancellationToken cancellationToken)
        {
            var me = request.Context.TLO.Me;
            var originalState = me.Spawn.State;

            if
            (
                !(
                    originalState == SpawnState.Sit ||
                    originalState == SpawnState.Stand
                ) &&
                me.Combat || 
                me.AutoFire || 
                me.AmICasting() ||
                (
                    me.Spawn.Class.CanCast &&
                    request.Context.TLO.IsSpellBookOpen()
                ) ||
                !(me.GetAbilityID("Forage") > 0) ||
                !me.GetAbilityReady("Forage")
            )
            {
                return false;
            }

            if (originalState == SpawnState.Sit)
            {
                me.Stand();
            }

            if (await abilityService.DoAbilityAsync("Forage", "You have scrounged", "You fail to locate", cancellationToken))
            {
                var foragedItemName = request.Context.TLO.Cursor.Name;

                if (request.Character.ForageBlacklist.Contains(foragedItemName))
                {
                    await itemService.DestroyAsync(foragedItemName);
                }
                else
                {
                    await itemService.AutoInventoryAsync(null, cancellationToken);
                }
            }

            if (originalState == SpawnState.Sit)
            {
                me.Sit();
            }

            return true;
        }
    }
}
