using MediatR;
using MQ2Flux.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Handlers
{
    public class DismissAlertWindowCommandHandler : IRequestHandler<DismissAlertWindowCommand>
    {
        public Task Handle(DismissAlertWindowCommand request, CancellationToken cancellationToken)
        {
            var me = request.Context.TLO.Me;

            if (me.SubscriptionDays == null || me.SubscriptionDays == 0)
            {
                request.Context.MQ2.DoCommand("/notify AlertWnd ALW_Dismiss_Button leftmouseup");
            }

            return Task.CompletedTask;
        }
    }
}
