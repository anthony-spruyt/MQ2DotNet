using MediatR;
using MQ2Flux.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Behaviors
{
    public interface INotWhenAutoAttackingRequest : IMQ2ContextRequest
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
