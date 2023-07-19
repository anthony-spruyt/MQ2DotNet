using MediatR;
using MQFlux.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface INotWhenAutoAttackingRequest : IContextRequest
    {

    }

    public class NotWhenAutoAttackingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is INotWhenAutoAttackingRequest notWhenAutoAttackingRequest &&
                (notWhenAutoAttackingRequest.Context.TLO.Me.AutoMeleeAttack || notWhenAutoAttackingRequest.Context.TLO.Me.AutoRangeAttack))
            {
                return Task.FromResult(default(TResponse));
            }

            return next();
        }
    }
}
