using MediatR;
using MQ2Flux.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Handlers
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
