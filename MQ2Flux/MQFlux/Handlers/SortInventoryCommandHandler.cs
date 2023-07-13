using MediatR;
using MQFlux.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Handlers
{
    public class SortInventoryCommandHandler : IRequestHandler<SortInventoryCommand, bool>
    {
        public async Task<bool> Handle(SortInventoryCommand request, CancellationToken cancellationToken)
        {
            if (!request.Character.AutoSortInventory.GetValueOrDefault(false))
            {
                return false;
            }

            //TODO
            // Get the most nutritious stat food and drink items.
            // If they arent in the first two slots then swap them out.
            await Task.CompletedTask;

            return false;
        }
    }
}
