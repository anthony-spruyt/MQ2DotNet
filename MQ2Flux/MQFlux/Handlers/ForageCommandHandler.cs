using MediatR;
using MQ2DotNet.EQ;
using MQFlux.Commands;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Handlers
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
            if (!request.Character.AutoForage.GetValueOrDefault(false))
            {
                return false;
            }

            var me = request.Context.TLO.Me;
            var originalState = me.Spawn.State;

            // TODO can you forage on a horse?
            if
            (
                !(
                    originalState == SpawnState.Sit ||
                    originalState == SpawnState.Stand
                )
            )
            {
                return false;
            }

            if (originalState == SpawnState.Sit)
            {
                me.Stand();
            }

            if (await abilityService.DoAbilityAsync(request.Ability, "You have scrounged", "You fail to locate", cancellationToken))
            {
                var foragedItemName = request.Context.TLO.Cursor.Name;

                if (request.Character.ForageBlacklist.Contains(foragedItemName))
                {
                    await itemService.DestroyAsync(foragedItemName);
                }
                else
                {
                    await itemService.AutoInventoryAsync(cancellationToken: cancellationToken);
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
