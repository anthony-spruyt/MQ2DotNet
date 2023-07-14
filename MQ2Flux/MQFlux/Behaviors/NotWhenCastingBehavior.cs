using MediatR;
using MQFlux.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface INotWhenCastingRequest : IMQContextRequest
    {
        /// <summary>
        /// If <see cref="true"/> then this behaviour will not short circuit for bards.
        /// </summary>
        bool AllowBard { get; }
    }

    public class NotWhenCastingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if
            (
                request is INotWhenCastingRequest notWhenCastingRequest &&
                notWhenCastingRequest.Context.TLO.Me.AmICasting() &&
                !(
                    notWhenCastingRequest.AllowBard &&
                    notWhenCastingRequest.Context.TLO.Me.Spawn.Class.ShortName == "BRD"
                )
            )
            {
                return Task.FromResult(default(TResponse));
            }

            return next();
        }
    }
}
