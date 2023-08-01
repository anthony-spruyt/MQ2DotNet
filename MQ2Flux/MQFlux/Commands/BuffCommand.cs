using MediatR;
using MQ2DotNet.EQ;
using MQ2DotNet.MQ2API.DataTypes;
using MQFlux.Behaviors;
using MQFlux.Core;
using MQFlux.Extensions;
using MQFlux.Models;
using MQFlux.Queries;
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
        public TimeSpan IdleTime => TimeSpan.FromSeconds(5);
    }

    public class BuffCommandHandler : PCCommandHandler<BuffCommand>
    {
        private readonly ISpellCastingService spellCastingService;
        private readonly ITargetService targetService;
        private readonly IMQLogger mqLogger;
        private readonly IContext context;
        private readonly IMacroService macroService;
        private readonly IMediator mediator;

        public BuffCommandHandler(ISpellCastingService spellCastingService, ITargetService targetService, IMQLogger mqLogger, IContext context, IMacroService macroService, IMediator mediator)
        {
            this.spellCastingService = spellCastingService;
            this.targetService = targetService;
            this.mqLogger = mqLogger;
            this.context = context;
            this.macroService = macroService;
            this.mediator = mediator;
        }

        public async override Task<CommandResponse<bool>> Handle(BuffCommand request, CancellationToken cancellationToken)
        {
            if (!request.Character.AutoBuff.GetValueOrDefault(false))
            {
                return CommandResponse.FromResult(false);
            }

            var response = await mediator.Send(new BusyBuffingQuery(), cancellationToken);
            var notBusyBuffingFrequency = TimeSpan.FromSeconds(10);

            if (!response.Result.IsBusyBuffing && response.Result.Timestamp.Add(notBusyBuffingFrequency) > DateTime.UtcNow)
            {
                return CommandResponse.FromResult(false);
            }

            var me = context.TLO.Me;
            IEnumerable<SpawnType> spawns;

            if (me.Grouped)
            {
                spawns = context.TLO.Group.GroupMembers
                    .Where(i => i.Present && i.Spawn.LineOfSight && !i.Spawn.Dead)
                    .Select(i => i.Spawn);
            }
            else
            {
                spawns = new SpawnType[] { me };
            }

            try
            {
                await macroService.Pause(cancellationToken);

                var didBuff = await TryBuff(spawns, cancellationToken);

                await mediator.Send(new BusyBuffingCommand(didBuff), cancellationToken);

                return CommandResponse.FromResult(didBuff);
            }
            finally
            {
                macroService.Resume();
            }
        }

        private class StacksWithComparer : IComparer<SpellType>
        {
            public int Compare(SpellType x, SpellType y)
            {
                if (x.ID == y.ID)
                {
                    return 0;
                }
                else if (x.StacksWith(y.Name))
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
        }

        private async Task<bool> TryBuff(IEnumerable<SpawnType> spawns, CancellationToken cancellationToken = default)
        {
            var maxRecastTime = TimeSpan.FromSeconds(60);
            var minDuration = TimeSpan.FromMinutes(1);
            var me = context.TLO.Me;
            var comparer = new StacksWithComparer();
            var buffs = me.SpellBook
                        .Select(i => i.Value)
                        .Where
                        (
                            i =>
                            i.Beneficial &&
                            i.RecastTime.GetValueOrDefault(TimeSpan.Zero) <= maxRecastTime &&
                            i.Duration >= minDuration &&
                            BuffSpellCategories.Contains(i.Category) &&
                            !SubcategoryBlacklist.Contains(i.Subcategory) &&
                            !BuffBlacklist.Contains(i.Name) &&
                            !i.HasSPA(SPA.FRAGILE) &&
                            !i.HasSPA(SPA.FRAGILE_DEFENSE)
                        )
                        .HighestLevelSpellLineSpells();

            // AoE group buffs
            if (me.Grouped)
            {
                var areaOfEffectGroupBuffs = buffs.AreaOfEffectGroupBuffs().OrderBy(i => i, comparer);

                foreach (var buff in areaOfEffectGroupBuffs)
                {
                    var canCast = CanCast(me, buff);

                    if (!canCast)
                    {
                        continue;
                    }

                    var willLand = WillLand(me, me, buff);

                    if (!willLand)
                    {
                        continue;
                    }

                    mqLogger.Log($"Buffing group with [\ay{buff.Name}\aw]");
                    await spellCastingService.Cast(buff, waitForSpellReady: true, cancellationToken);

                    return true;
                }
            }

            // ST buffs
            var singleTargetBuffs = me.Grouped ?
                buffs.SingleTargetGroupBuffs().OrderBy(i => i, comparer).ThenByDescending(i => i.Level) :
                buffs.SingleTargetFriendlyBuffs().OrderBy(i => i, comparer).ThenByDescending(i => i.Level);

            foreach (var spawn in spawns)
            {
                await targetService.Target(spawn, waitForBuffsPopulated: true, cancellationToken);

                var target = context.TLO.Target;

                foreach (var buff in singleTargetBuffs)
                {
                    var useful = IsBuffUseful(spawn, buff);

                    if (!useful)
                    {
                        continue;
                    }

                    var canCast = CanCast(me, buff);

                    if (!canCast)
                    {
                        continue;
                    }

                    var willLand = WillLand(me, spawn, buff);

                    if (!willLand)
                    {
                        continue;
                    }

                    mqLogger.Log($"Buffing [\ao{spawn.DisplayName}\aw] with [\ay{buff.Name}\aw]");
                    await spellCastingService.Cast(buff, waitForSpellReady: true, cancellationToken);

                    return true;
                }

                await Task.Delay(50, cancellationToken);
            }

            // self buffs
            var selfBuffs = buffs.SelfBuffs().OrderBy(i => i, comparer).ThenByDescending(i => i.Level);

            foreach (var buff in selfBuffs)
            {
                var canCast = CanCast(me, buff);

                if (!canCast)
                {
                    continue;
                }

                var willLand = WillLand(me, me, buff);

                if (!willLand)
                {
                    continue;
                }

                mqLogger.Log($"Buffing myself with [\ay{buff.Name}\aw]");
                await spellCastingService.Cast(buff, waitForSpellReady: true, cancellationToken);

                return true;
            }

            return false;
        }

        private static bool CanCast(CharacterType me, SpellType buff)
        {
            var canCast = me.PctMana > 20u && me.PctEndurance > 20u &&
                me.CurrentMana >= buff.Mana &&
                me.CurrentEndurance >= buff.EnduranceCost &&
                me.DoIHaveReagentsToCast(buff);

            return canCast;
        }

        private static bool WillLand(CharacterType me, SpawnType spawn, SpellType spell)
        {
            var amIBuffingMyself = spawn.ID == me.ID;

            if (amIBuffingMyself)
            {
                var existingBuff = me.GetBuff(spell.Name);

                if ((existingBuff?.ID).GetValueOrDefault(0) > 0)
                {
                    return false;
                }
            }
            else
            {
                var existingBuff = spawn.GetBuff(spell.Name);

                if ((existingBuff?.ID).GetValueOrDefault(0u) > 0u)
                {
                    return false;
                }
            }

            if (spell.Name.ToLower().Contains("shrink"))
            {
                return spawn.Height > 2;
            }

            if (amIBuffingMyself)
            {
                if (!spell.WillLand(me))
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    // For some reason if you have all your buff slots filled then stacksTarget is false.
                    if (me.CountBuffs >= 15u)
                    {
                        me.Buffs.Last().Remove();
                    }

                    if (!spell.WillLand(spawn))
                    {
                        return false;
                    }
                }
                catch (BuffsNotPopulatedException)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsBuffUseful(SpawnType spawn, SpellType spell)
        {
            var usefulToClass = IsBuffUseful(spawn.Class, spell);

            if (!usefulToClass)
            {
                return false;
            }

            var worthABuffSlot = IsWorthABuffSlot(spawn, spell);

            if (!worthABuffSlot)
            {
                return false;
            }

            return true;
        }

        private static bool IsWorthABuffSlot(SpawnType spawn, SpellType spell)
        {
            var longTermBuffCount = spawn.Buffs.Count(i => i.DurationWindow.GetValueOrDefault(0u) == 0u);

            // No slots left
            if (longTermBuffCount >= 15)
            {
                return false;
            }
            // 1 or 2 slot left
            else if (longTermBuffCount >= 13)
            {
                var spellName = spell.Name.ToLower();

                if (spell.Category == SpellCategory.RESIST_BUFF || spellName.Contains("resist") || spellName.Contains("endure"))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsBuffUseful(ClassType buffTargetCass, SpellType spell)
        {
            var @class = (Class)buffTargetCass;
            var spellCategory = spell.Category;
            switch (@class)
            {
                case Class.Warrior:
                    return MeleeBuffSpellCategories.Union(TankBuffSpellCategories).Contains(spellCategory);
                case Class.Monk:
                case Class.Rogue:
                case Class.Berserker:
                    return MeleeBuffSpellCategories.Contains(spellCategory);
                case Class.Cleric:
                case Class.Druid:
                case Class.Shaman:
                case Class.Necromancer:
                case Class.Wizard:
                case Class.Mage:
                case Class.Enchanter:
                    return CasterBuffSpellCategories.Contains(spellCategory);
                case Class.Paladin:
                case Class.Shadowknight:
                    return HybridBuffSpellCategories.Union(TankBuffSpellCategories).Contains(spellCategory);
                case Class.Ranger:
                case Class.Bard:
                case Class.Beastlord:
                    return HybridBuffSpellCategories.Contains(spellCategory);
                default:
                    return true;
            }
        }

        private static readonly SpellCategory[] TankBuffSpellCategories = new SpellCategory[]
        {
            SpellCategory.AEGOLISM,
            SpellCategory.AGILITY,
            SpellCategory.ARMOR_CLASS,
            SpellCategory.ATTACK,
            //SpellCategory.CHARISMA,
            SpellCategory.DAMAGE_SHIELD,
            SpellCategory.DEFENSIVE,
            SpellCategory.DEXTERITY,
            SpellCategory.ENDURANCE,
            //SpellCategory.FAST, // Fast heals?
            SpellCategory.HASTE,
            //SpellCategory.HASTE_SPELL_FOCUS,
            SpellCategory.HEALTH,
            //SpellCategory.HEALTH_MANA,
            SpellCategory.HP_BUFFS,
            SpellCategory.HP_TYPE_ONE,
            SpellCategory.HP_TYPE_TWO,
            SpellCategory.LEVITATE,
            //SpellCategory.MANA,
            //SpellCategory.MANA_FLOW,
            SpellCategory.MOVEMENT,
            SpellCategory.REGEN,
            SpellCategory.RESIST_BUFF,
            SpellCategory.RUNE,
            SpellCategory.SHIELDING,
            SpellCategory.SPELLSHIELD,
            //SpellCategory.SPELL_FOCUS,
            SpellCategory.SPELL_GUARD,
            SpellCategory.STAMINA,
            SpellCategory.STATISTIC_BUFFS,
            SpellCategory.STRENGTH,
            SpellCategory.SYMBOL,
            SpellCategory.UTILITY_BENEFICIAL,
            //SpellCategory.VISION,
            //SpellCategory.WISDOM_INTELLIGENCE
        };

        private static readonly SpellCategory[] MeleeBuffSpellCategories = new SpellCategory[]
        {
            SpellCategory.AEGOLISM,
            SpellCategory.AGILITY,
            SpellCategory.ARMOR_CLASS,
            SpellCategory.ATTACK,
            //SpellCategory.CHARISMA,
            //SpellCategory.DAMAGE_SHIELD,
            //SpellCategory.DEFENSIVE,
            SpellCategory.DEXTERITY,
            SpellCategory.ENDURANCE,
            //SpellCategory.FAST, // Fast heals?
            SpellCategory.HASTE,
            //SpellCategory.HASTE_SPELL_FOCUS,
            SpellCategory.HEALTH,
            //SpellCategory.HEALTH_MANA,
            SpellCategory.HP_BUFFS,
            SpellCategory.HP_TYPE_ONE,
            SpellCategory.HP_TYPE_TWO,
            SpellCategory.LEVITATE,
            //SpellCategory.MANA,
            //SpellCategory.MANA_FLOW,
            SpellCategory.MOVEMENT,
            //SpellCategory.REGEN,
            SpellCategory.RESIST_BUFF,
            //SpellCategory.RUNE,
            //SpellCategory.SHIELDING,
            //SpellCategory.SPELLSHIELD,
            //SpellCategory.SPELL_FOCUS,
            //SpellCategory.SPELL_GUARD,
            SpellCategory.STAMINA,
            SpellCategory.STATISTIC_BUFFS,
            SpellCategory.STRENGTH,
            SpellCategory.SYMBOL,
            SpellCategory.UTILITY_BENEFICIAL,
            //SpellCategory.VISION,
            //SpellCategory.WISDOM_INTELLIGENCE
        };

        private static readonly SpellCategory[] CasterBuffSpellCategories = new SpellCategory[]
        {
            SpellCategory.AEGOLISM,
            //SpellCategory.AGILITY,
            //SpellCategory.ARMOR_CLASS,
            //SpellCategory.ATTACK,
            SpellCategory.CHARISMA,
            //SpellCategory.DAMAGE_SHIELD,
            //SpellCategory.DEFENSIVE,
            //SpellCategory.DEXTERITY,
            //SpellCategory.ENDURANCE,
            //SpellCategory.FAST, // Fast heals?
            //SpellCategory.HASTE,
            SpellCategory.HASTE_SPELL_FOCUS,
            SpellCategory.HEALTH,
            SpellCategory.HEALTH_MANA,
            SpellCategory.HP_BUFFS,
            SpellCategory.HP_TYPE_ONE,
            SpellCategory.HP_TYPE_TWO,
            SpellCategory.LEVITATE,
            SpellCategory.MANA,
            SpellCategory.MANA_FLOW,
            SpellCategory.MOVEMENT,
            SpellCategory.REGEN,
            SpellCategory.RESIST_BUFF,
            //SpellCategory.RUNE,
            //SpellCategory.SHIELDING,
            //SpellCategory.SPELLSHIELD,
            SpellCategory.SPELL_FOCUS,
            //SpellCategory.SPELL_GUARD,
            //SpellCategory.STAMINA,
            SpellCategory.STATISTIC_BUFFS,
            //SpellCategory.STRENGTH,
            SpellCategory.SYMBOL,
            SpellCategory.UTILITY_BENEFICIAL,
            //SpellCategory.VISION,
            SpellCategory.WISDOM_INTELLIGENCE
        };

        private static readonly SpellCategory[] HybridBuffSpellCategories = new SpellCategory[]
        {
            SpellCategory.AEGOLISM,
            SpellCategory.AGILITY,
            SpellCategory.ARMOR_CLASS,
            SpellCategory.ATTACK,
            SpellCategory.CHARISMA,
            SpellCategory.DAMAGE_SHIELD,
            SpellCategory.DEFENSIVE,
            SpellCategory.DEXTERITY,
            SpellCategory.ENDURANCE,
            //SpellCategory.FAST, // Fast heals?
            SpellCategory.HASTE,
            SpellCategory.HASTE_SPELL_FOCUS,
            SpellCategory.HEALTH,
            SpellCategory.HEALTH_MANA,
            SpellCategory.HP_BUFFS,
            SpellCategory.HP_TYPE_ONE,
            SpellCategory.HP_TYPE_TWO,
            SpellCategory.LEVITATE,
            SpellCategory.MANA,
            SpellCategory.MANA_FLOW,
            SpellCategory.MOVEMENT,
            SpellCategory.REGEN,
            SpellCategory.RESIST_BUFF,
            //SpellCategory.RUNE,
            //SpellCategory.SHIELDING,
            //SpellCategory.SPELLSHIELD,
            SpellCategory.SPELL_FOCUS,
            SpellCategory.SPELL_GUARD,
            SpellCategory.STAMINA,
            SpellCategory.STATISTIC_BUFFS,
            SpellCategory.STRENGTH,
            SpellCategory.SYMBOL,
            SpellCategory.UTILITY_BENEFICIAL,
            //SpellCategory.VISION,
            SpellCategory.WISDOM_INTELLIGENCE
        };

        private static readonly SpellCategory[] BuffSpellCategories = new SpellCategory[]
        {
            SpellCategory.AEGOLISM,
            SpellCategory.AGILITY,
            SpellCategory.ARMOR_CLASS,
            SpellCategory.ATTACK,
            SpellCategory.CHARISMA,
            SpellCategory.DAMAGE_SHIELD,
            SpellCategory.DEFENSIVE,
            SpellCategory.DEXTERITY,
            SpellCategory.ENDURANCE,
            //SpellCategory.FAST, // Fast heals?
            SpellCategory.HASTE,
            SpellCategory.HASTE_SPELL_FOCUS,
            SpellCategory.HEALTH,
            SpellCategory.HEALTH_MANA,
            SpellCategory.HP_BUFFS,
            SpellCategory.HP_TYPE_ONE,
            SpellCategory.HP_TYPE_TWO,
            SpellCategory.LEVITATE,
            SpellCategory.MANA,
            SpellCategory.MANA_FLOW,
            SpellCategory.MOVEMENT,
            SpellCategory.REGEN,
            SpellCategory.RESIST_BUFF,
            SpellCategory.RUNE,
            SpellCategory.SHIELDING,
            SpellCategory.SPELLSHIELD,
            SpellCategory.SPELL_FOCUS,
            SpellCategory.SPELL_GUARD,
            SpellCategory.STAMINA,
            SpellCategory.STATISTIC_BUFFS,
            SpellCategory.STRENGTH,
            SpellCategory.SYMBOL,
            SpellCategory.UTILITY_BENEFICIAL,
            //SpellCategory.VISION,
            SpellCategory.WISDOM_INTELLIGENCE
        };

        private static readonly string[] BuffBlacklist = new string[]
        {
            "Share Wolf Form"
        };

        private static readonly string[] SubcategoryBlacklist = new string[]
        {
            "Misc",
            "Invisibility",
            "Summoned",
            "Undead",
            "Invulnerability"
        };
    }
}
