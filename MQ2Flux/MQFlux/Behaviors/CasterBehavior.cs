using MediatR;
using MQFlux.Core;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface ICasterRequest : IContextRequest
    {

    }

    public class CasterBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if (request is ICasterRequest casterRequest &&
                !casterRequest.Context.TLO.Me.Class.CanCast)
            {
                return ShortCircuitResultTask();
            }

            return next();
        }
    }
}
