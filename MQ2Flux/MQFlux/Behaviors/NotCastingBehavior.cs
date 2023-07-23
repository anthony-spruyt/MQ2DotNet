using MediatR;
using MQFlux.Core;
using MQFlux.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface INotCastingRequest : IContextRequest
    {
        /// <summary>
        /// If <see cref="true"/> then this behaviour will not short circuit for bards.
        /// </summary>
        bool AllowBard { get; }
    }

    public class NotCastingBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if
            (
                request is INotCastingRequest notCastingRequest &&
                notCastingRequest.Context.TLO.Me.AmICasting() &&
                !(
                    notCastingRequest.AllowBard &&
                    string.Compare(notCastingRequest.Context.TLO.Me.Class.ShortName, "BRD") == 0
                )
            )
            {
                return ShortCircuitResultTask();
            }

            return next();
        }
    }
}
