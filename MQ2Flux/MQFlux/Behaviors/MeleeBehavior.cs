using MediatR;
using MQFlux.Core;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IMeleeRequest : IContextRequest
    {

    }

    public class MeleeBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if (request is IMeleeRequest meleeRequest &&
                meleeRequest.Context.TLO.Me.Class.PureCaster)
            {
                return ShortCircuitResultTask();
            }

            return next();
        }
    }
}
