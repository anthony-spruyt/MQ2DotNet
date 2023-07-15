using MediatR;
using MQFlux.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Handlers
{
    public class DismissAlertWindowCommandHandler : IRequestHandler<DismissAlertWindowCommand>
    {
        public Task Handle(DismissAlertWindowCommand request, CancellationToken cancellationToken)
        {
            var me = request.Context.TLO.Me;

            // This interrupts dragging of any old UI framework components. Currently the inventory and a few others are not affected.
            // Once DBG gave ported all UI components to the new framework the throttling can be removed.
            if (DateTime.UtcNow.Second % 5 == 0 && me.SubscriptionDays.GetValueOrDefault(0) == 0)
            {
                request.Context.MQ.DoCommand("/notify AlertWnd ALW_Dismiss_Button leftmouseup");
            }

            return Task.CompletedTask;
        }
    }
}
