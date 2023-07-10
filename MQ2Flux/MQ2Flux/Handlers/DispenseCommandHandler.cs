using MediatR;
using MQ2Flux.Commands;
using MQ2Flux.Extensions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Handlers
{
    internal class DispenseCommandHandler : IRequestHandler<DispenseCommand>
    {
        public Task Handle(DispenseCommand request, CancellationToken cancellationToken)
        {
            var mq2 = request.Context.MQ2;
            var me = request.Context.TLO.Me;
            var allMyInv = me.Inventory.Flatten();
            var dispensers = request.Character.Dispensers;
            var cursor = request.Context.TLO.Cursor;

            if (!dispensers.Any(i => i.DispenserID == 71979))
            {
                dispensers.Add(new Models.FoodAndDrinkDispenser() { DispenserID = 71979, SummonID = 71980, TargetCount = 5 });
            }
            if (!dispensers.Any(i => i.DispenserID == 107808))
            {
                dispensers.Add(new Models.FoodAndDrinkDispenser() { DispenserID = 107808, SummonID = 107807, TargetCount = 5 });
            }

            if (!me.Moving && (!me.CastTimeLeft.HasValue || me.CastTimeLeft.Value == TimeSpan.Zero))
            {
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
                                mq2.DoCommand($"/useitem {dispenserItem.Name}");

                                break;
                            }
                        }
                    }
                }
            }

            if (cursor != null && dispensers.Any(i => (i.SummonID.HasValue && i.SummonID.Value == cursor.ID) || string.Compare(i.SummonName, cursor.Name) == 0))
            {
                mq2.DoCommand("/autoinv");
            }

            return Task.CompletedTask;
        }
    }
}
