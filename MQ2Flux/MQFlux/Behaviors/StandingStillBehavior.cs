using MediatR;
using MQFlux.Core;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IStandingStillRequest
    {

    }

    public class StandingStillBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        private readonly IContext context;

        public StandingStillBehavior(IContext context)
        {
            this.context = context;
        }

        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if (request is IStandingStillRequest &&
                context.TLO.Me.Moving)
            {
                return ShortCircuitResultTask();
            }

            return next();
        }
    }
}
