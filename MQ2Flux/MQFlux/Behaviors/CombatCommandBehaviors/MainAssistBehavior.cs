using MediatR;
using MQFlux.Commands.CombatCommands;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors.CombatCommandBehaviors
{
    public interface IMainAssistRequest
    {

    }

    public class MainAssistBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : CombatCommand<TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is IMainAssistRequest)
            {
                var mySpawnId = request.Context.TLO.Me.Spawn.ID.GetValueOrDefault(0u);
                var mainAssistSpawnId = request.Context.TLO.Group?.MainAssist?.Spawn?.ID ?? 0u;

                if (mainAssistSpawnId == 0u || mySpawnId != mainAssistSpawnId)
                {
                    return Task.FromResult(default(TResponse));
                }
            }

            return next();
        }
    }
}
