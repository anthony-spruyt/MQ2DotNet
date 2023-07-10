using MQ2DotNet.MQ2API.DataTypes;
using System.Collections.Generic;
using System.Linq;

namespace MQ2Flux.Extensions
{
    public static class ItemTypeExtensions
    {
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
                if (container.ContentSize > 0)
                {
                    foreach (var item in container.Contents)
                    {
                        if (item.ID.HasValue && item.ID > 0)
                        {
                            items.Append(item);
                        }
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
            return @this.Where(i => i.ID.HasValue && i.ID > 0 && i.Container > 0);
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

        public static int GetNutrientScore(this ItemType @this)
        {
            var score = 0;

            if (@this.IsConsumable())
            {
                score += (int?)@this.AC ?? 0;
                score += (int?)@this.Accuracy ?? 0;
                score += (int?)@this.AGI ?? 0;
                score += (int?)@this.Attack ?? 0;
                score += (int?)@this.Avoidance ?? 0;
                score += (int?)@this.CHA ?? 0;
                score += (int?)@this.Clairvoyance ?? 0;
                score += (int?)@this.DamageShieldMitigation ?? 0;
                score += (int?)@this.DamShield ?? 0;
                score += (int?)@this.DEX ?? 0;
                score += (int?)@this.DoTShielding ?? 0;
                score += (int?)@this.Endurance ?? 0;
                score += (int?)@this.EnduranceRegen ?? 0;
                score += (int?)@this.Haste ?? 0;
                score += (int?)@this.HealAmount ?? 0;
                score += (int?)@this.HeroicAGI ?? 0;
                score += (int?)@this.HeroicCHA ?? 0;
                score += (int?)@this.HeroicDEX ?? 0;
                score += (int?)@this.HeroicINT ?? 0;
                score += (int?)@this.HeroicSTA ?? 0;
                score += (int?)@this.HeroicSTR ?? 0;
                score += (int?)@this.HeroicWIS ?? 0;
                score += (int?)@this.HP ?? 0;
                score += (int?)@this.HPRegen ?? 0;
                score += (int?)@this.INT ?? 0;
                score += (int?)@this.Luck ?? 0;
                score += (int?)@this.Mana ?? 0;
                score += (int?)@this.ManaRegen ?? 0;
                score += (int?)@this.STA ?? 0;
                score += (int?)@this.STR ?? 0;
                score += (int?)@this.StunResist ?? 0;
                score += (int?)@this.svCold ?? 0;
                score += (int?)@this.svCorruption ?? 0;
                score += (int?)@this.svDisease ?? 0;
                score += (int?)@this.svFire ?? 0;
                score += (int?)@this.svMagic ?? 0;
                score += (int?)@this.svPoison ?? 0;
                score += (int?)@this.WIS ?? 0;
            }

            return score;
        }
    }
}
