using MQFlux.Core;
using MQFlux.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands.Handlers
{
    public class DoneMeditatingCommandHandler : PCCommandHandler<DoneMeditatingCommand>
    {
        private readonly IContext context;
        private readonly IMQLogger mqLogger;

        public DoneMeditatingCommandHandler(IContext context, IMQLogger mqLogger)
        {
            this.context = context;
            this.mqLogger = mqLogger;
        }

        public override Task<CommandResponse<bool>> Handle(DoneMeditatingCommand request, CancellationToken cancellationToken)
        {
            var me = context.TLO.Me;

            if (!me.Standing && me.PctMana > 99 && me.PctHPs > 99)
            {
                me.Stand();

                mqLogger.Log("Standing up because I am finished meditating", TimeSpan.Zero);

                return CommandResponse.FromResultTask(true);
            }

            return CommandResponse.FromResultTask(false);
        }
    }
}
