using MediatR;
using MQ2DotNet.EQ;
using MQFlux.Core;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface INotInCombatRequest
    {

    }

    public class NotInCombatBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        private readonly IContext context;

        public NotInCombatBehavior(IContext context)
        {
            this.context = context;
        }

        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if (request is INotInCombatRequest &&
                context.TLO.Me.CombatState == CombatState.Combat)
            {
                return ShortCircuitResultTask();
            }

            return next();
        }
    }
}
