using MediatR;
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

    public class NotCastingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if
            (
                request is INotCastingRequest notCastingRequest &&
                notCastingRequest.Context.TLO.Me.AmICasting() &&
                !(
                    notCastingRequest.AllowBard &&
                    string.Compare(notCastingRequest.Context.TLO.Me.Spawn.Class.ShortName, "BRD") == 0
                )
            )
            {
                return Task.FromResult(default(TResponse));
            }

            return next();
        }
    }
}
