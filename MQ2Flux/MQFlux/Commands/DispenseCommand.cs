using MQ2DotNet.MQ2API.DataTypes;
using MQFlux.Behaviors;
using MQFlux.Core;
using MQFlux.Extensions;
using MQFlux.Models;
using MQFlux.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands
{
    public class DispenseCommand :
        PCCommand,
        ICharacterConfigRequest,
        IConsciousRequest,
        INotInCombatRequest,
        IStandingStillRequest,
        INotCastingRequest,
        INoItemOnCursorRequest,
        INotFeignedDeathRequest,
        IIdleTimeRequest
    {
        public bool AllowBard => false;
        public CharacterConfigSection Character { get; set; }
        public FluxConfig Config { get; set; }
        public IContext Context { get; set; }
        public TimeSpan IdleTime => TimeSpan.FromSeconds(1);
    }

    public class DispenseCommandHandler : PCCommandHandler<DispenseCommand>
    {
        private readonly IItemService itemService;
        private readonly IMacroService macroService;

        public DispenseCommandHandler(IItemService itemService, IMacroService macroService)
        {
            this.itemService = itemService;
            this.macroService = macroService;
        }

        public override async Task<CommandResponse<bool>> Handle(DispenseCommand request, CancellationToken cancellationToken)
        {
            if (!request.Character.AutoDispenseFoodAndDrink.GetValueOrDefault(false))
            {
                return CommandResponse.FromResult(false);
            }

            var dispensers = request.Character.Dispensers;

            AddDefaultDispensers(dispensers);

            if (await Dispense(dispensers, request.Context.TLO.Me, cancellationToken))
            {
                await itemService.AutoInventory(cursor => cursor != null && dispensers.Any(i => (i.SummonID.HasValue && i.SummonID.Value == cursor.ID) || string.Compare(i.SummonName, cursor.Name) == 0), cancellationToken);

                return CommandResponse.FromResult(true);
            }

            return CommandResponse.FromResult(false);
        }

        private static void AddDefaultDispensers(List<FoodAndDrinkDispenser> dispensers)
        {
            if (!dispensers.Any(i => i.DispenserID == 71979 || string.Compare(i.DispenserName, "Fresh Cookie Dispenser") == 0))
            {
                dispensers.Add(new FoodAndDrinkDispenser() { DispenserID = 71979, SummonID = 71980, TargetCount = 5 });
            }
            if (!dispensers.Any(i => i.DispenserID == 107808 || string.Compare(i.DispenserName, "Spiced Iced Tea Dispenser") == 0))
            {
                dispensers.Add(new FoodAndDrinkDispenser() { DispenserID = 107808, SummonID = 107807, TargetCount = 5 });
            }
            if (!dispensers.Any(i => i.DispenserID == 52191 || string.Compare(i.DispenserName, "Warm Milk Dispenser") == 0))
            {
                dispensers.Add(new FoodAndDrinkDispenser() { DispenserID = 52191, SummonID = 52199, TargetCount = 5 });
            }
        }

        private async Task<bool> Dispense(List<FoodAndDrinkDispenser> dispensers, CharacterType me, CancellationToken cancellationToken)
        {
            var allMyInv = me.Inventory.Flatten();

            foreach (var dispenser in dispensers)
            {
                if (dispenser.DispenserID.HasValue || !string.IsNullOrWhiteSpace(dispenser.DispenserName))
                {
                    var actualCount = allMyInv
                        .Where(i => (dispenser.SummonID.HasValue && dispenser.SummonID.Value == i.ID) || string.Compare(dispenser.SummonName, i.Name) == 0)
                        .Sum(i => i.Stack);

                    if (actualCount < dispenser.TargetCount)
                    {
                        var dispenserItem = allMyInv.FirstOrDefault(i => (dispenser.DispenserID.HasValue && dispenser.DispenserID.Value == i.ID) || string.Compare(dispenser.DispenserName, i.Name) == 0);

                        if (dispenserItem != null && dispenserItem.IsTimerReady())
                        {
                            try
                            {
                                await macroService.Pause(cancellationToken);

                                return await itemService.UseItem(dispenserItem, "Dispensing", cancellationToken);
                            }
                            finally
                            {
                                macroService.Resume();
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}
