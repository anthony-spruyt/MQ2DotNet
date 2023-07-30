using MediatR;
using MQFlux.Core;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IMainAssistRequest
    {

    }

    public class MainAssistBehavior<TRequest, TResponse> : CombatCommandBehavior<TRequest> where TRequest : CombatCommand
    {
        private readonly IContext context;

        public MainAssistBehavior(IContext context)
        {
            this.context = context;
        }

        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if (request is IMainAssistRequest)
            {
                var mySpawnId = context.TLO.Me.ID.GetValueOrDefault(0u);
                var mainAssistSpawnId = (context.TLO.Group?.MainAssist?.Spawn?.ID).GetValueOrDefault(0u);

                if (mainAssistSpawnId == 0u || mySpawnId != mainAssistSpawnId)
                {
                    return ShortCircuitResultTask();
                }
            }

            return next();
        }
    }
}
