using MQ2DotNet.MQ2API.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MQFlux.Extensions
{
    public static class ItemTypeExtensions
    {
        public static bool IsTimerReady(this ItemType @this)
        {
            return @this.TimerReady.GetValueOrDefault(TimeSpan.Zero) == TimeSpan.Zero;
        }

        public static bool HasCastTime(this ItemType @this)
        {
            return @this.CastTime.HasValue ?
                @this.CastTime.Value > TimeSpan.Zero :
                @this.Clicky != null && @this.Clicky.CastTime.GetValueOrDefault(TimeSpan.Zero) > TimeSpan.Zero;
        }

        /// <summary>
        /// Flatten all inventory content. IE top level inventory and all container contents.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IEnumerable<ItemType> Flatten(this IEnumerable<ItemType> @this)
        {
            var containers = @this.Containers();
            var items = new List<ItemType>(@this);

            foreach (var container in containers)
            {
                foreach (var item in container.Contents)
                {
                    if (item.ID.GetValueOrDefault(0) > 0)
                    {
                        items.Add(item);
                    }
                }
            }

            return items;
        }

        /// <summary>
        /// Filter out non container items.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IEnumerable<ItemType> Containers(this IEnumerable<ItemType> @this)
        {
            return @this.Where(i => i.IsAContainer());
        }

        /// <summary>
        /// Can I eat this?
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsEdible(this ItemType @this)
        {
            return @this.CanUse && string.Compare(@this.Type, "Food", true) == 0;
        }

        /// <summary>
        /// Can I drink this?
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsDrinkable(this ItemType @this)
        {
            return @this.CanUse && string.Compare(@this.Type, "Drink", true) == 0;
        }

        /// <summary>
        /// Can I consume this?
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsConsumable(this ItemType @this)
        {
            return @this.IsDrinkable() || @this.IsEdible();
        }

        public static bool IsAContainer(this ItemType @this)
        {
            return @this.Container > 0;
        }

        public static int GetNutrientScore(this ItemType @this, CharacterType me = null)
        {
            var score = 0;
            var meleeSkillModifier = 1;
            var casterSkillModifier = 1;

            if (me != null)
            {
                if (me.Spawn.Class.PureCaster)
                {
                    meleeSkillModifier = 0;
                }
                else
                {
                    casterSkillModifier = 0;
                }
            }

            if (@this.IsConsumable())
            {
                score += (int)@this.AC.GetValueOrDefault(0) * meleeSkillModifier;
                score += (int)@this.Accuracy.GetValueOrDefault(0) * meleeSkillModifier;
                score += (int)@this.AGI.GetValueOrDefault(0) * meleeSkillModifier;
                score += (int)@this.Attack.GetValueOrDefault(0) * meleeSkillModifier;
                score += (int)@this.Avoidance.GetValueOrDefault(0) * meleeSkillModifier;
                score += (int)@this.CHA.GetValueOrDefault(0) * casterSkillModifier;
                score += (int)@this.Clairvoyance.GetValueOrDefault(0) * casterSkillModifier;
                score += (int)@this.DamageShieldMitigation.GetValueOrDefault(0);
                score += (int)@this.DamShield.GetValueOrDefault(0);
                score += (int)@this.DEX.GetValueOrDefault(0) * meleeSkillModifier;
                score += (int)@this.DoTShielding.GetValueOrDefault(0);
                score += (int)@this.Endurance.GetValueOrDefault(0) * meleeSkillModifier;
                score += (int)@this.EnduranceRegen.GetValueOrDefault(0) * meleeSkillModifier;
                score += (int)@this.Haste.GetValueOrDefault(0) * meleeSkillModifier;
                score += (int)@this.HealAmount.GetValueOrDefault(0);
                score += (int)@this.HeroicAGI.GetValueOrDefault(0) * meleeSkillModifier;
                score += (int)@this.HeroicCHA.GetValueOrDefault(0);
                score += (int)@this.HeroicDEX.GetValueOrDefault(0) * meleeSkillModifier;
                score += (int)@this.HeroicINT.GetValueOrDefault(0);
                score += (int)@this.HeroicSTA.GetValueOrDefault(0) * meleeSkillModifier;
                score += (int)@this.HeroicSTR.GetValueOrDefault(0) * meleeSkillModifier;
                score += (int)@this.HeroicWIS.GetValueOrDefault(0);
                score += (int)@this.HP.GetValueOrDefault(0) * meleeSkillModifier * 10;
                score += (int)@this.HPRegen.GetValueOrDefault(0) * meleeSkillModifier;
                score += (int)@this.INT.GetValueOrDefault(0) * casterSkillModifier;
                score += (int)@this.Luck.GetValueOrDefault(0);
                score += (int)@this.Mana.GetValueOrDefault(0) * casterSkillModifier * 10;
                score += (int)@this.ManaRegen.GetValueOrDefault(0) * casterSkillModifier;
                score += (int)@this.STA.GetValueOrDefault(0) * meleeSkillModifier;
                score += (int)@this.STR.GetValueOrDefault(0) * meleeSkillModifier;
                score += (int)@this.StunResist.GetValueOrDefault(0);
                score += (int)@this.svCold.GetValueOrDefault(0);
                score += (int)@this.svCorruption.GetValueOrDefault(0);
                score += (int)@this.svDisease.GetValueOrDefault(0);
                score += (int)@this.svFire.GetValueOrDefault(0);
                score += (int)@this.svMagic.GetValueOrDefault(0);
                score += (int)@this.svPoison.GetValueOrDefault(0);
                score += (int)@this.WIS.GetValueOrDefault(0) * casterSkillModifier;
            }

            return score;
        }
    }
}
