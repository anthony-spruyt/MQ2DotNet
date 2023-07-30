using MediatR;
using MQ2DotNet.EQ;
using MQFlux.Core;
using MQFlux.Extensions;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface INotCastingRequest
    {
        /// <summary>
        /// If <see cref="true"/> then this behaviour will not short circuit for bards.
        /// </summary>
        bool AllowBard { get; }
    }

    public class NotCastingBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        private readonly IContext context;

        public NotCastingBehavior(IContext context)
        {
            this.context = context;
        }

        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if
            (
                request is INotCastingRequest notCastingRequest &&
                context.TLO.Me.AmICasting() &&
                !(
                    notCastingRequest.AllowBard &&
                    context.TLO.Me.Class == Class.Bard
                )
            )
            {
                return ShortCircuitResultTask();
            }

            return next();
        }
    }
}
