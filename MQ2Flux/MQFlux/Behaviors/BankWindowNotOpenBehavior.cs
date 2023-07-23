using MediatR;
using MQFlux.Core;
using MQFlux.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IBankWindowNotOpenRequest : IContextRequest
    {

    }

    public class BankWindowNotOpenBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if
            (
                request is IBankWindowNotOpenRequest bankWindowsNotOpenRequest &&
                (bankWindowsNotOpenRequest.Context.TLO.IsWindowOpen("BigBankWnd") || bankWindowsNotOpenRequest.Context.TLO.IsWindowOpen("GuildBankWnd")))
            {
                return ShortCircuitResultTask();
            }

            return next();
        }
    }
}
