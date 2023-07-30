using MQ2DotNet.EQ;
using MQFlux.Behaviors;
using MQFlux.Core;
using MQFlux.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands
{
    public class MeditateCommand :
        PCCommand,
        IStandingStillRequest,
        INotCastingRequest,
        ICasterRequest,
        IConsciousRequest,
        INotFeignedDeathRequest,
        IBankWindowNotOpenRequest,
        IIdleTimeRequest
    {
        public bool AllowBard => false;
        public TimeSpan IdleTime => TimeSpan.FromSeconds(5);
    }

    public class MeditateCommandHandler : PCCommandHandler<MeditateCommand>
    {
        private readonly IMQLogger mqLogger;
        private readonly IContext context;

        public MeditateCommandHandler(IMQLogger mqLogger, IContext context)
        {
            this.mqLogger = mqLogger;
            this.context = context;
        }

        public override async Task<CommandResponse<bool>> Handle(MeditateCommand request, CancellationToken cancellationToken)
        {
            var me = context.TLO.Me;

            // TODO
            if
            (
                //DateTime.UtcNow.Second % 2 != 0 ||
                !me.Standing ||
                me.XTargets.Any(i => i.PctAggro == 100u)
            )
            {
                return CommandResponse.FromResult(false);
            }

            bool medBreak;

            if (me.CombatState == CombatState.Combat)
            {
                medBreak =
                    me.PctMana < 90 && me.PctHPs > 75 ||
                    me.PctMana < 33;
            }
            else
            {
                medBreak = me.PctMana < 90;
            }

            if (medBreak)
            {
                await Task.Delay(1000, cancellationToken);

                mqLogger.Log("Sitting down to med", TimeSpan.Zero);

                me.Sit();

                await Task.Delay(1000, cancellationToken);

                return CommandResponse.FromResult(true);
            }

            return CommandResponse.FromResult(false);
        }
    }
}
