using MQ2DotNet.MQ2API.DataTypes;
using System;
using System.Linq;

namespace MQFlux.Extensions
{

    /// <summary>
    /// TODO: move these to the <see cref="CharacterType"/> class when they have been tested.
    /// </summary>
    public static class CharacterTypeExtensions
    {
        public static bool DoIHaveReagentsToCast(this CharacterType @this, SpellType spell)
        {
            var reagents = spell.GetReagents();

            if (reagents.Count() == 0)
            {
                return true;
            }

            var inventory = @this.Inventory.Flatten();

            foreach (var reagent in reagents)
            {
                var availableReagentCount = inventory.Where(i => i.ID.GetValueOrDefault(0u) == reagent.ItemID).Sum(i => i.StackCount.GetValueOrDefault(0u));
                if (availableReagentCount < reagent.RequiredCount)
                {
                    return false;
                }
            }

            return true;
        }

        public static AbilityInfo GetAbilityInfo(this CharacterType @this, string name)
        {
            return new AbilityInfo(name, @this);
        }

        public static bool AmICasting(this CharacterType @this)
        {
            return @this.CastTimeLeft.HasValue && @this.CastTimeLeft.Value > TimeSpan.Zero;
        }

        /*
         * https://www.redguides.com/community/threads/mq2feedme-questions.68357/#post-374540
         * I don't use the plugin, but the "Level" setting is connected to your toons answers to these levels of Hunger or Thirst.
         * I think they range from 0 (super hungry/thirsty) to 10000(? - full) So setting them to 3500 to 5000 should force it to eat/drink before you get to your stat food/drink.
         * If you set them above 5000 I think it could cause issues with spam trying to eat before you can fit the meal (too full.) 
         */

        public static bool AmIHungry(this CharacterType @this, uint threshold = 3500)
        {
            if (!@this.Hunger.HasValue)
            {
                return false;
            }

            return @this.Hunger.Value < threshold;
        }

        public static bool AmIThirsty(this CharacterType @this, uint threshold = 3500)
        {
            if (!@this.Thirst.HasValue)
            {
                return false;
            }

            return @this.Thirst.Value < threshold;
        }
    }
}
