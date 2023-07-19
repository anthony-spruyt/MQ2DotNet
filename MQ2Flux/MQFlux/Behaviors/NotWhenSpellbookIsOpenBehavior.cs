using MediatR;
using MQFlux.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface INotWhenSpellbookIsOpenRequest : IContextRequest
    {
    
    }

    public class NotWhenSpellbookIsOpenBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is INotWhenSpellbookIsOpenRequest notWhenSpellbookIsOpenRequest &&
                notWhenSpellbookIsOpenRequest.Context.TLO.Me.Spawn.Class.CanCast &&
                notWhenSpellbookIsOpenRequest.Context.TLO.IsSpellBookOpen())
            {
                return Task.FromResult(default(TResponse));
            }

            return next();
        }
    }
}
