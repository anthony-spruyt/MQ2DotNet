using MediatR;
using MQFlux.Core;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface INotAutoAttackingRequest
    {

    }

    public class NotAutoAttackingBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        private readonly IContext context;

        public NotAutoAttackingBehavior(IContext context)
        {
            this.context = context;
        }

        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if (request is INotAutoAttackingRequest &&
                (context.TLO.Me.AutoMeleeAttack || context.TLO.Me.AutoRangeAttack))
            {
                return ShortCircuitResultTask();
            }

            return next();
        }
    }
}
