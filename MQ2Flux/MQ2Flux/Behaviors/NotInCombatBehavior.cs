using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Behaviors
{
    public interface INotInCombatRequest : IMQ2ContextRequest
    {

    }

    public class NotInCombatBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is INotInCombatRequest notInCombatRequest &&
                notInCombatRequest.Context.TLO.Me.CombatState == MQ2DotNet.EQ.CombatState.Combat)
            {
                return Task.FromResult(default(TResponse));
            }

            return next();
        }
    }
}
