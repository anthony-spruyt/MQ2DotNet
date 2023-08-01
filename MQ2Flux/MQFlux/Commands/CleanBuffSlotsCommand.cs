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
    public class CleanBuffSlotsCommand :
        PCCommand,
        INotCastingRequest
    {
        public bool AllowBard => true;
    }

    public class CleanBuffSlotsCommandHandler : PCCommandHandler<CleanBuffSlotsCommand>
    {
        private readonly IContext context;
        private readonly IMQLogger mqLogger;

        public CleanBuffSlotsCommandHandler(IContext context, IMQLogger mqLogger)
        {
            this.context = context;
            this.mqLogger = mqLogger;
        }

        public override async Task<CommandResponse<bool>> Handle(CleanBuffSlotsCommand request, CancellationToken cancellationToken)
        {
            var me = context.TLO.Me;
            var slotThreshold = 13u;

            if (me.CountBuffs > slotThreshold)
            {
                var buffsToRemoveCount = (int)(me.CountBuffs.GetValueOrDefault(0u) - slotThreshold);

                var removeTheseBuffs = me.Buffs
                    .Where
                    (
                        i =>
                        {
                            var spellName = i.Name.ToLower();

                            return i.Category == SpellCategory.RESIST_BUFF || spellName.Contains("resist") || spellName.Contains("endure");
                        }
                    )
                    .OrderBy(i => i.Level)
                    .Take(buffsToRemoveCount);

                foreach (var buff in removeTheseBuffs)
                {
                    buff.Remove();

                    mqLogger.Log($"\awRemoving [\ay{buff.Name}\aw] - I am low on buff slots", TimeSpan.Zero);

                    await Task.Yield();
                }

                await Task.Delay(1000, cancellationToken);

                return CommandResponse.FromResult(true);
            }

            return CommandResponse.FromResult(false);
        }
    }
}
