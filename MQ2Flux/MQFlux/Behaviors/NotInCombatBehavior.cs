using MediatR;
using MQ2DotNet.EQ;
using MQFlux.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface INotInCombatRequest : IContextRequest
    {

    }

    public class NotInCombatBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : Command<TResponse>
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
