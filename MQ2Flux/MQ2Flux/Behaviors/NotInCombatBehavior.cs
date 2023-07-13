using MediatR;
using MQ2DotNet.EQ;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Behaviors
{
    public interface INotInCombatRequest : IMQContextRequest
    {

    }

    public class NotInCombatBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is INotInCombatRequest notInCombatRequest &&
                notInCombatRequest.Context.TLO.Me.CombatState == CombatState.Combat)
            {
                return Task.FromResult(default(TResponse));
            }

            return next();
        }
    }
}
