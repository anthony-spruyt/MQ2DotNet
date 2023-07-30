using MediatR;
using MQFlux.Core;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IHybridRequest
    {

    }

    public class HybridBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        private readonly IContext context;

        public HybridBehavior(IContext context)
        {
            this.context = context;
        }

        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if
            (
                request is IHybridRequest &&
                (
                    !context.TLO.Me.Class.CanCast ||
                    context.TLO.Me.Class.PureCaster
                )
            )
            {
                return ShortCircuitResultTask();
            }

            return next();
        }
    }
}
