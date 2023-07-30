using MediatR;
using MQFlux.Core;
using MQFlux.Extensions;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface ISpellbookNotOpenRequest
    {

    }

    public class SpellbookNotOpenBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        private readonly IContext context;

        public SpellbookNotOpenBehavior(IContext context)
        {
            this.context = context;
        }

        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if (request is ISpellbookNotOpenRequest &&
                context.TLO.Me.Class.CanCast &&
                context.TLO.IsSpellBookOpen())
            {
                return ShortCircuitResultTask();
            }

            return next();
        }
    }
}
