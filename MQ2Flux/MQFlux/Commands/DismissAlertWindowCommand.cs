using MQFlux.Core;
using MQFlux.Extensions;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands
{
    public class DismissAlertWindowCommand : PCCommand
    {
    }

    public class DismissAlertWindowCommandHandler : PCCommandHandler<DismissAlertWindowCommand>
    {
        private readonly IContext context;

        public DismissAlertWindowCommandHandler(IContext context)
        {
            this.context = context;
        }

        public override Task<CommandResponse<bool>> Handle(DismissAlertWindowCommand request, CancellationToken cancellationToken)
        {
            var me = context.TLO.Me;

            if
            (
                me.SubscriptionDays.GetValueOrDefault(0) == 0 &&
                context.TLO.IsWindowOpen("AlertWnd")
            )
            {
                context.MQ.DoCommand("/notify AlertWnd ALW_Dismiss_Button leftmouseup");

                return CommandResponse.FromResultTask(true);
            }

            return CommandResponse.FromResultTask(false);
        }
    }
}
