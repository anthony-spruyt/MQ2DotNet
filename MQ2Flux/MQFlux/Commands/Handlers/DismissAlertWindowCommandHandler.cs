using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands.Handlers
{
    public class DismissAlertWindowCommandHandler : IRequestHandler<DismissAlertWindowCommand, Unit>
    {
        public Task<Unit> Handle(DismissAlertWindowCommand request, CancellationToken cancellationToken)
        {
            var me = request.Context.TLO.Me;

            // This interrupts dragging of any old UI framework components. Currently the inventory and a few others are not affected.
            // Once DBG gave ported all UI components to the new framework the throttling can be removed.
            if (DateTime.UtcNow.Second % 5 == 0 && me.SubscriptionDays.GetValueOrDefault(0) == 0)
            {
                request.Context.MQ.DoCommand("/notify AlertWnd ALW_Dismiss_Button leftmouseup");
            }

            return Task.FromResult(Unit.Value);
        }
    }
}
