using MediatR;
using MQ2Flux.Commands;
using MQ2Flux.Extensions;
using MQ2Flux.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Handlers
{
    public class DispenseCommandHandler : IRequestHandler<DispenseCommand>
    {
        private readonly IMQ2Logger mq2Logger;
        private readonly IItemService itemService;

        public DispenseCommandHandler(IMQ2Logger mq2Logger, IItemService itemService)
        {
            this.mq2Logger = mq2Logger;
            this.itemService = itemService;
        }

        public async Task Handle(DispenseCommand request, CancellationToken cancellationToken)
        {
            var dispensers = request.Character.Dispensers;
            
            if (!dispensers.Any(i => i.DispenserID == 71979 || string.Compare(i.DispenserName, "Fresh Cookie Dispenser") == 0))
            {
                dispensers.Add(new Models.FoodAndDrinkDispenser() { DispenserID = 71979, SummonID = 71980, TargetCount = 5 });
            }
            if (!dispensers.Any(i => i.DispenserID == 107808 || string.Compare(i.DispenserName, "Spiced Iced Tea Dispenser") == 0))
            {
                dispensers.Add(new Models.FoodAndDrinkDispenser() { DispenserID = 107808, SummonID = 107807, TargetCount = 5 });
            }
            if (!dispensers.Any(i => i.DispenserID == 52191 || string.Compare(i.DispenserName, "Warm Milk Dispenser") == 0))
            {
                dispensers.Add(new Models.FoodAndDrinkDispenser() { DispenserID = 52191, SummonID = 52199, TargetCount = 5 });
            }

            var me = request.Context.TLO.Me;
            var mq2 = request.Context.MQ2;

            if (!me.Moving && (!me.CastTimeLeft.HasValue || me.CastTimeLeft.Value == TimeSpan.Zero))
            {
                var allMyInv = me.Inventory.Flatten();

                foreach (var dispenser in dispensers)
                {
                    if (dispenser.DispenserID.HasValue || !string.IsNullOrWhiteSpace(dispenser.SummonName))
                    {
                        var actualCount = allMyInv
                            .Where(i => (dispenser.SummonID.HasValue && dispenser.SummonID.Value == i.ID) || string.Compare(dispenser.SummonName, i.Name) == 0)
                            .Sum(i => i.StackCount);

                        if (actualCount < dispenser.TargetCount)
                        {
                            var dispenserItem = allMyInv.FirstOrDefault(i => (dispenser.DispenserID.HasValue && dispenser.DispenserID.Value == i.ID) || string.Compare(dispenser.DispenserName, i.Name) == 0);

                            if (dispenserItem != null && dispenserItem.TimerReady.HasValue && dispenserItem.TimerReady.Value == TimeSpan.Zero)
                            {
                                await itemService.UseItemAsync(dispenserItem, "Dispensing", cancellationToken);

                                break;
                            }
                        }
                    }
                }
            }

            var cursor = request.Context.TLO.Cursor;

            if (cursor != null && dispensers.Any(i => (i.SummonID.HasValue && i.SummonID.Value == cursor.ID) || string.Compare(i.SummonName, cursor.Name) == 0))
            {
                mq2Logger.Log($"Putting [\ag{cursor.Name}\aw] into your inventory");
                mq2.DoCommand("/autoinv");
            }
        }
    }
}
