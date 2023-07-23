using MediatR;
using MQ2DotNet.EQ;
using MQFlux.Core;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IFreeInventorySlotsRequest : IContextRequest
    {
        int MinimumEmptySlots { get; }
        ItemSize MinimumSize { get; }
    }

    public class FreeInventorySlotBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if (request is IFreeInventorySlotsRequest freeInventorySlotsRequest &&
                freeInventorySlotsRequest.Context.TLO.Me.FreeInventory.GetValueOrDefault(0) < freeInventorySlotsRequest.MinimumEmptySlots &&
                freeInventorySlotsRequest.Context.TLO.Me.LargestFreeInventory < (uint)freeInventorySlotsRequest.MinimumSize)
            {
                return ShortCircuitResultTask();
            }

            return next();
        }
    }
}
