using MQ2DotNet.MQ2API.DataTypes;
using MQFlux.Behaviors;
using MQFlux.Core;
using MQFlux.Extensions;
using MQFlux.Models;
using MQFlux.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands
{
    public class BuffCommand :
        PCCommand,
        ICharacterConfigRequest,
        IConsciousRequest,
        INotInCombatRequest,
        IStandingStillRequest,
        ICasterRequest,
        INotCastingRequest,
        INoItemOnCursorRequest,
        INotFeignedDeathRequest,
        IIdleTimeRequest
    {
        public bool AllowBard => false;
        public CharacterConfig Character { get; set; }
        public FluxConfig Config { get; set; }
        public IContext Context { get; set; }
        public TimeSpan IdleTime => TimeSpan.FromSeconds(1);
    }

    public class BuffCommandHandler : PCCommandHandler<BuffCommand>
    {
        private readonly ISpellCastingService spellCastingService;
        private readonly ITargetService targetService;
        private readonly IMQLogger mqLogger;

        public BuffCommandHandler(ISpellCastingService spellCastingService, ITargetService targetService, IMQLogger mqLogger)
        {
            this.spellCastingService = spellCastingService;
            this.targetService = targetService;
            this.mqLogger = mqLogger;
        }

        public async override Task<CommandResponse<bool>> Handle(BuffCommand request, CancellationToken cancellationToken)
        {
            if (!request.Character.AutoBuff.GetValueOrDefault(false))
            {
                return CommandResponse.FromResult(false);
            }

            var me = request.Context.TLO.Me;
            IEnumerable<SpawnType> spawns;

            if (me.Grouped)
            {
                await targetService.CycleGroupMembers(cancellationToken);

                spawns = request.Context.TLO.Group.GroupMembers
                    .Where(i => i.Present && i.Spawn.LineOfSight && !i.Spawn.Dead)
                    .Select(i => i.Spawn);
            }
            else
            {
                spawns = new SpawnType[] { me.Spawn };
            }

            foreach (var spawn in spawns)
            {
                if (await TryBuff(me, spawn, cancellationToken))
                {
                    return CommandResponse.FromResult(true);
                }
            }

            return CommandResponse.FromResult(false);
        }

        private async Task<bool> TryBuff(CharacterType me, SpawnType spawn, CancellationToken cancellationToken = default)
        {
            var amIBuffingMyself = spawn.ID == me.ID;
            var bestInClass = me.SpellBook
                .Select(i => i.Value)
                .Where(i => i.Beneficial && i.Duration > TimeSpan.FromMinutes(1))
                .GroupBy(i => $"{i.GetSpellCategory()}-{i.Subcategory}-{i.SpellIcon.GetValueOrDefault(0u)}").Select(i => i.OrderByDescending(j => j.Level).First());
            var buffSpells = amIBuffingMyself ?
                bestInClass.Where(i => IsValidSelfBuff(me, i)) :
                bestInClass.Where(i => IsValidFriendlyBuff(me, spawn, i));
            var buffSpell = buffSpells.FirstOrDefault();

            if
            (
                buffSpell == null || 
                buffSpell.Mana.GetValueOrDefault(0) > (int)me.CurrentMana.GetValueOrDefault(0u) || 
                buffSpell.EnduranceCost.GetValueOrDefault(0u) > (int)me.CurrentEndurance.GetValueOrDefault(0u)
            )
            {
                return false;
            }

            mqLogger.Log($"Buffing [\ao{spawn.DisplayName}\aw] with [\ay{buffSpell.Name}\aw]");

            return
                await targetService.Target(spawn, cancellationToken) &&
                await spellCastingService.Cast(buffSpell, true, cancellationToken);
        }

        private static bool IsValidFriendlyBuff(CharacterType me, SpawnType spawn, SpellType spell)
        {
            var isBuff = spell.IsBuff();

            if (!isBuff)
            {
                return false;
            }

            if (spell.TargetType == "Self")
            {
                return false;
            }

            var doIHaveReagentsToCast = me.DoIHaveReagentsToCast(spell);

            if (!doIHaveReagentsToCast)
            {
                return false;
            }

            var existingBuff = spawn.GetBuff(spell.Name);

            if (existingBuff != null)
            {
                return false;
            }

            var stacks = spell.StacksSpawn((int)spawn.ID.GetValueOrDefault(0u));

            return stacks;
        }

        private static bool IsValidSelfBuff(CharacterType me, SpellType spell)
        {
            var isBuff = spell.IsBuff();

            if (!isBuff)
            {
                return false;
            }

            var doIHaveReagentsToCast = me.DoIHaveReagentsToCast(spell);

            if (!doIHaveReagentsToCast)
            {
                return false;
            }

            var existingBuff = me.GetBuff(spell.Name);

            if (existingBuff != null)
            {
                return false;
            }

            // Doesnt matter what duration is passed in, it always returns true.
            //var duration = spell.MyDuration.GetValueOrDefault(spell.Duration.GetValueOrDefault(TimeSpan.Zero));
            //var stacks = spell.Stacks(duration);
            var stacks = spell.Stacks();

            return stacks;
        }
    }
}
