using MediatR;
using MQ2Flux.Commands;
using MQ2Flux.Extensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Handlers
{
    public class SummonFoodAndDrinkCommandHandler : IRequestHandler<SummonFoodAndDrinkCommand>
    {
        public Task Handle(SummonFoodAndDrinkCommand request, CancellationToken cancellationToken)
        {
            var me = request.Context.TLO.Me;

            if (!me.Spawn.Class.CanCast)
            {
                return Task.CompletedTask;
            }

            var allMyInv = me.Inventory.Flatten();
            var actualFoodCount = allMyInv
                .Where(i => i.NoRent && i.IsEdible())
                .Sum(i => i.StackCount);
            var actualDrinkCount = allMyInv
                .Where(i => i.NoRent && i.IsDrinkable())
                .Sum(i => i.StackCount);

            if (actualFoodCount < 10)
            {

            }

            if (actualDrinkCount < 10)
            {

            }

            return Task.CompletedTask;
        }
    }
}
