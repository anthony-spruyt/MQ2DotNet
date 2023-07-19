using MediatR;
using MQ2DotNet.EQ;
using MQFlux.Commands;
using MQFlux.Extensions;
using MQFlux.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Handlers
{
    public class MeditateCommandHandler : IRequestHandler<MeditateCommand, bool>
    {
        private readonly IMQLogger mqLogger;

        public MeditateCommandHandler(IMQLogger mqLogger)
        {
            this.mqLogger = mqLogger;
        }

        public Task<bool> Handle(MeditateCommand request, CancellationToken cancellationToken)
        {
            var me = request.Context.TLO.Me;

            // TODO
            if
            (
                DateTime.UtcNow.Second % 2 != 0 || 
                !me.Spawn.Standing || 
                me.XTargets.Any(i => i.PctAggro > 80u) ||
                request.Context.TLO.IsWindowOpen("BigBankWnd") || request.Context.TLO.IsWindowOpen("GuildBankWnd")
            )
            {
                return Task.FromResult(false);
            }

            bool medBreak;

            if (me.CombatState == CombatState.Combat)
            {
                medBreak = me.PctMana < 90 && me.PctHPs > 75;
            }
            else
            {
                medBreak = me.PctMana < 90;
            }

            if (medBreak)
            {
                me.Sit();

                mqLogger.Log("Sitting down to med", TimeSpan.Zero);

                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
