using MediatR;
using MQFlux.Core;
using MQFlux.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface ISpellbookNotOpenRequest : IContextRequest
    {

    }

    public class SpellbookNotOpenBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if (request is ISpellbookNotOpenRequest spellbookNotOpenRequest &&
                spellbookNotOpenRequest.Context.TLO.Me.Class.CanCast &&
                spellbookNotOpenRequest.Context.TLO.IsSpellBookOpen())
            {
                return ShortCircuitResultTask();
            }

            return next();
        }
    }
}
