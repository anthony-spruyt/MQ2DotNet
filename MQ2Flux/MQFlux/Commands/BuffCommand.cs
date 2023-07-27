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
            var buffGroups = me.SpellBook
                .Select(i => i.Value)
                .Where(i => i.Beneficial && i.Duration > TimeSpan.FromMinutes(1))
                .GroupBy(i => $"{i.GetSpellCategory()}-{i.Subcategory}-{i.SpellIcon.GetValueOrDefault(0u)}").Select(i => i.OrderByDescending(j => j.Level).First());
            var buffSpells = amIBuffingMyself ?
                buffGroups.Where(i => IsValidSelfBuff(me, i)) :
                buffGroups.Where(i => IsValidFriendlyBuff(me, spawn, i));
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
            var isBuff = BuffSpellCategories.Contains(spell.GetSpellCategory());

            if (!isBuff)
            {
                return false;
            }

            if (spell.TargetType == "Self")
            {
                return false;
            }

            var isBuffUseful = IsBuffUseful(spawn.Class, spell);

            if (!isBuffUseful)
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

        private static bool IsBuffUseful(ClassType buffTargetCass, SpellType spell)
        {
            var @class = (Class)buffTargetCass;
            var spellCategory = spell.GetSpellCategory();
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

        private static bool IsValidSelfBuff(CharacterType me, SpellType spell)
        {
            var isBuff = BuffSpellCategories.Contains(spell.GetSpellCategory());

            if (!isBuff)
            {
                return false;
            }

            var isBuffUseful = IsBuffUseful(me.Class, spell);

            if (!isBuffUseful)
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
        private static readonly SpellCategory[] MeleeBuffSpellCategories = new SpellCategory[]
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
            //SpellCategory.UTILITY_BENEFICIAL,
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
            //SpellCategory.UTILITY_BENEFICIAL,
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
            //SpellCategory.UTILITY_BENEFICIAL,
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
            //SpellCategory.UTILITY_BENEFICIAL,
            //SpellCategory.VISION,
            SpellCategory.WISDOM_INTELLIGENCE
        };
    }
}
