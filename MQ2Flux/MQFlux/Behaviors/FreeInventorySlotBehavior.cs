using MediatR;
using MQ2DotNet.EQ;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IFreeInventorySlotsRequest : IContextRequest
    {
        int MinimumEmptySlots { get; }
        ItemSize MinimumSize { get; }
    }

    public class FreeInventorySlotBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is IFreeInventorySlotsRequest freeInventorySlotsRequest &&
                freeInventorySlotsRequest.Context.TLO.Me.FreeInventory.GetValueOrDefault(0) < freeInventorySlotsRequest.MinimumEmptySlots &&
                freeInventorySlotsRequest.Context.TLO.Me.LargestFreeInventory < (uint)freeInventorySlotsRequest.MinimumSize)
            {
                return Task.FromResult(default(TResponse));
            }

            return next();
        }
    }
}
