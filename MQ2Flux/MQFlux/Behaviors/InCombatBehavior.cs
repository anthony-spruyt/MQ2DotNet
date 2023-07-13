using MediatR;
using MQ2DotNet.EQ;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IInCombatRequest : IMQContextRequest
    {
        
    }

    public class InCombatBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is IInCombatRequest inCombatRequest &&
                inCombatRequest.Context.TLO.Me.CombatState != CombatState.Combat)
            {
                return Task.FromResult(default(TResponse));
            }

            return next();
        }
    }
}
