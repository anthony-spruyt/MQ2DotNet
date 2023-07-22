using MediatR;
using MQFlux.Commands;
using MQFlux.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IBankWindowNotOpenRequest : IContextRequest
    {

    }

    public class BankWindowNotOpenBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : Command<TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if
            (
                request is IBankWindowNotOpenRequest bankWindowsNotOpenRequest &&
                (bankWindowsNotOpenRequest.Context.TLO.IsWindowOpen("BigBankWnd") || bankWindowsNotOpenRequest.Context.TLO.IsWindowOpen("GuildBankWnd")))
            {
                return Task.FromResult(default(TResponse));
            }

            return next();
        }
    }
}
