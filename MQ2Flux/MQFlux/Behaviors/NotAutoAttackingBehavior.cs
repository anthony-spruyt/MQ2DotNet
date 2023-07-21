using MediatR;
using MQFlux.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface INotAutoAttackingRequest : IContextRequest
    {

    }

    public class NotAutoAttackingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is INotAutoAttackingRequest notAutoAttackingRequest &&
                (notAutoAttackingRequest.Context.TLO.Me.AutoMeleeAttack || notAutoAttackingRequest.Context.TLO.Me.AutoRangeAttack))
            {
                return Task.FromResult(default(TResponse));
            }

            return next();
        }
    }
}
