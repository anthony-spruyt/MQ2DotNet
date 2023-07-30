using MQFlux.Behaviors;
using MQFlux.Core;
using MQFlux.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands
{
    public class DoneMeditatingCommand :
        PCCommand,
        IConsciousRequest,
        IStandingStillRequest,
        INotCastingRequest,
        ICasterRequest,
        ISpellbookNotOpenRequest,
        IBankWindowNotOpenRequest,
        INotFeignedDeathRequest,
        IIdleTimeRequest
    {
        public bool AllowBard => false;
        public TimeSpan IdleTime => TimeSpan.FromSeconds(5);
    }

    public class DoneMeditatingCommandHandler : PCCommandHandler<DoneMeditatingCommand>
    {
        private readonly IContext context;
        private readonly IMQLogger mqLogger;

        public DoneMeditatingCommandHandler(IContext context, IMQLogger mqLogger)
        {
            this.context = context;
            this.mqLogger = mqLogger;
        }

        public async override Task<CommandResponse<bool>> Handle(DoneMeditatingCommand request, CancellationToken cancellationToken)
        {
            var me = context.TLO.Me;

            if (!me.Standing && me.PctMana > 99 && me.PctHPs > 99)
            {
                await Task.Delay(1000, cancellationToken);

                mqLogger.Log("Standing up because I am finished meditating", TimeSpan.Zero);

                me.Stand();

                await Task.Delay(1000, cancellationToken);

                return CommandResponse.FromResult(true);
            }

            return CommandResponse.FromResult(false);
        }
    }
}
