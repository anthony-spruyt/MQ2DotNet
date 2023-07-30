using MediatR;
using MQ2DotNet.EQ;
using MQFlux.Core;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IFreeInventorySlotsRequest
    {
        int MinimumEmptySlots { get; }
        ItemSize MinimumSize { get; }
    }

    public class FreeInventorySlotBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        private readonly IContext context;

        public FreeInventorySlotBehavior(IContext context)
        {
            this.context = context;
        }

        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if (request is IFreeInventorySlotsRequest freeInventorySlotsRequest &&
                context.TLO.Me.FreeInventory.GetValueOrDefault(0) < freeInventorySlotsRequest.MinimumEmptySlots &&
                context.TLO.Me.LargestFreeInventory < (uint)freeInventorySlotsRequest.MinimumSize)
            {
                return ShortCircuitResultTask();
            }

            return next();
        }
    }
}
