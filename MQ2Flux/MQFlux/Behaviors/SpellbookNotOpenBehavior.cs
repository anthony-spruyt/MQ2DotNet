using MediatR;
using MQFlux.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface ISpellbookNotOpenRequest : IContextRequest
    {
    
    }

    public class SpellbookNotOpenBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is ISpellbookNotOpenRequest spellbookNotOpenRequest &&
                spellbookNotOpenRequest.Context.TLO.Me.Spawn.Class.CanCast &&
                spellbookNotOpenRequest.Context.TLO.IsSpellBookOpen())
            {
                return Task.FromResult(default(TResponse));
            }

            return next();
        }
    }
}
