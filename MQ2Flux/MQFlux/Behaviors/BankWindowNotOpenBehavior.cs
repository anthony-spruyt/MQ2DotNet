using MediatR;
using MQFlux.Core;
using MQFlux.Extensions;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IBankWindowNotOpenRequest
    {

    }

    public class BankWindowNotOpenBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        private readonly IContext context;

        public BankWindowNotOpenBehavior(IContext context)
        {
            this.context = context;
        }

        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if
            (
                request is IBankWindowNotOpenRequest &&
                (context.TLO.IsWindowOpen("BigBankWnd") || context.TLO.IsWindowOpen("GuildBankWnd")))
            {
                return ShortCircuitResultTask();
            }

            return next();
        }
    }
}
