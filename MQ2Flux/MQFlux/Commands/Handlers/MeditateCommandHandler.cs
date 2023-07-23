using MediatR;
using MQ2DotNet.EQ;
using MQFlux.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands.Handlers
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
                //DateTime.UtcNow.Second % 2 != 0 ||
                !me.Standing ||
                me.XTargets.Any(i => i.PctAggro == 100u)
            )
            {
                return Task.FromResult(false);
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
                medBreak = me.PctMana < 98;
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
