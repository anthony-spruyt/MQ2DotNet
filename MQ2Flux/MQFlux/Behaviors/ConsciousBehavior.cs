using MediatR;
using MQFlux.Core;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IConsciousRequest : IContextRequest
    {

    }

    public class ConsciousBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if (request is IConsciousRequest consciousRequest &&
                consciousRequest.Context.TLO.Me.Dead)
            {
                return ShortCircuitResultTask();
            }

            return next();
        }
    }
}
