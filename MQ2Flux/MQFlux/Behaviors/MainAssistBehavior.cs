using MediatR;
using MQFlux.Core;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IMainAssistRequest
    {

    }

    public class MainAssistBehavior<TRequest, TResponse> : CombatCommandBehavior<TRequest> where TRequest : CombatCommand
    {
        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if (request is IMainAssistRequest)
            {
                var mySpawnId = request.Context.TLO.Me.ID.GetValueOrDefault(0u);
                var mainAssistSpawnId = (request.Context.TLO.Group?.MainAssist?.Spawn?.ID).GetValueOrDefault(0u);

                if (mainAssistSpawnId == 0u || mySpawnId != mainAssistSpawnId)
                {
                    return ShortCircuitResultTask();
                }
            }

            return next();
        }
    }
}
