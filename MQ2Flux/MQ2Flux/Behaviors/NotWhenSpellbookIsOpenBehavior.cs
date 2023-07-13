using MediatR;
using MQ2Flux.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Behaviors
{
    public interface INotWhenSpellbookIsOpenRequest : IMQContextRequest
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
