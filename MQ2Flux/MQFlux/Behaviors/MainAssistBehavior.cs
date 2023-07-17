using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IMainAssistRequest : IGroupedRequest
    {

    }

    public class MainAssistBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is IMainAssistRequest mainAssistRequest)
            {
                var mySpawnId = mainAssistRequest.Context.TLO.Me.Spawn.ID.GetValueOrDefault(0u);
                var mainAssistSpawnId = mainAssistRequest.Context.TLO.Group?.MainAssist?.Spawn?.ID ?? 0u;

                if (mainAssistSpawnId == 0u || mySpawnId != mainAssistSpawnId)
                {
                    return Task.FromResult(default(TResponse));
                }
            }

            return next();
        }
    }
}
