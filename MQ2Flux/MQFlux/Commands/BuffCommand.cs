using MQ2DotNet.EQ;
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
        public TimeSpan IdleTime => TimeSpan.FromSeconds(2);
    }

    public class BuffCommandHandler : PCCommandHandler<BuffCommand>
    {
        private readonly ISpellCastingService spellCastingService;
        private readonly ITargetService targetService;
        private readonly IMQLogger mqLogger;
        private readonly IContext context;

        public BuffCommandHandler(ISpellCastingService spellCastingService, ITargetService targetService, IMQLogger mqLogger, IContext context)
        {
            this.spellCastingService = spellCastingService;
            this.targetService = targetService;
            this.mqLogger = mqLogger;
            this.context = context;
        }

        public async override Task<CommandResponse<bool>> Handle(BuffCommand request, CancellationToken cancellationToken)
        {
            if (!request.Character.AutoBuff.GetValueOrDefault(false))
            {
                return CommandResponse.FromResult(false);
            }

            //if (DateTime.UtcNow.Second % 4 != 0)
            //{
            //    return CommandResponse.FromResult(false);
            //})

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

            var result = await TryBuff(spawns, cancellationToken);

            return CommandResponse.FromResult(result);
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
            var me = context.TLO.Me;
            var comparer = new StacksWithComparer();
            var buffs = me.SpellBook
                        .Select(i => i.Value)
                        .Where
                        (
                            i =>
                            !BuffBlacklist.Contains(i.Name) &&
                            i.RecastTime.GetValueOrDefault(TimeSpan.Zero) <= TimeSpan.FromSeconds(60) &&
                            (
                                (
                                    i.Beneficial &&
                                    i.Duration > TimeSpan.FromMinutes(1) &&
                                    i.Category != SpellCategory.UTILITY_BENEFICIAL &&
                                    BuffSpellCategories.Contains(i.Category)
                                ) ||
                                (
                                    i.Category == SpellCategory.UTILITY_BENEFICIAL &&
                                    (
                                        i.Name.ToLower().Contains("shrink") ||
                                        string.Compare(i.Subcategory, "Haste", true) == 0 ||
                                        string.Compare(i.Subcategory, "Movement", true) == 0
                                    )
                                )
                            ) &&
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
                buffs.SingleTargetGroupBuffs().OrderBy(i => i, comparer) :
                buffs.SingleTargetFriendlyBuffs().OrderBy(i => i, comparer);

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
            var selfBuffs = buffs.SelfBuffs().OrderBy(i => i, comparer);

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
                    if (me.CountBuffs == 15u)
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
            return IsBuffUseful(spawn.Class, spell);
        }

        private static bool IsBuffUseful(ClassType buffTargetCass, SpellType spell)
        {
            var @class = (Class)buffTargetCass;
            var spellCategory = spell.Category;
            switch (@class)
            {
                case Class.Warrior:
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
                case Class.Ranger:
                case Class.Shadowknight:
                case Class.Bard:
                case Class.Beastlord:
                    return HybridBuffSpellCategories.Contains(spellCategory);
                default:
                    return true;
            }
        }

        private static readonly SpellCategory[] MeleeBuffSpellCategories = new SpellCategory[]
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
            //SpellCategory.LEVITATE,
            //SpellCategory.MANA,
            //SpellCategory.MANA_FLOW,
            SpellCategory.MOVEMENT,
            SpellCategory.REGEN,
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
            //SpellCategory.LEVITATE,
            SpellCategory.MANA,
            SpellCategory.MANA_FLOW,
            SpellCategory.MOVEMENT,
            SpellCategory.REGEN,
            SpellCategory.RESIST_BUFF,
            //SpellCategory.RUNE,
            //SpellCategory.SHIELDING,
            //SpellCategory.SPELLSHIELD,
            //SpellCategory.SPELL_FOCUS,
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
            //SpellCategory.LEVITATE,
            SpellCategory.MANA,
            SpellCategory.MANA_FLOW,
            SpellCategory.MOVEMENT,
            SpellCategory.REGEN,
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
            //SpellCategory.LEVITATE,
            SpellCategory.MANA,
            SpellCategory.MANA_FLOW,
            SpellCategory.MOVEMENT,
            SpellCategory.REGEN,
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
            SpellCategory.WISDOM_INTELLIGENCE
        };

        private static readonly string[] BuffBlacklist = new string[]
        {
        };
    }
}
