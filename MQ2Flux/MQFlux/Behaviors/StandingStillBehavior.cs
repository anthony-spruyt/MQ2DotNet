using MediatR;
using MQFlux.Core;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IStandingStillRequest : IContextRequest
    {

    }

    public class StandingStillBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if (request is IStandingStillRequest standingStillRequest &&
                standingStillRequest.Context.TLO.Me.Moving)
            {
                return ShortCircuitResultTask();
            }

            return next();
        }
    }
}
