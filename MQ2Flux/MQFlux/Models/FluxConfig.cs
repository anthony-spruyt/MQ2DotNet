using System.Collections.Generic;
using System.Linq;

namespace MQFlux.Models
{
    public class FluxConfig
    {
        public string Version { get; set; } = "1.0.0";
        /// <summary>
        /// Configuration that applies to all characters.
        /// </summary>
        public DefaultConfigSection Defaults { get; set; } = new DefaultConfigSection();
        /// <summary>
        /// Character specific configuration. Overrides the default configuration.
        /// </summary>
        public List<CharacterConfigSection> Characters { get; set; } = new List<CharacterConfigSection>();
    }

    public class DefaultConfigSection
    {
        public bool? AutoBuff { get; set; } = true;
        public bool? AutoDispenseFoodAndDrink { get; set; } = true;
        public bool? AutoEatAndDrink { get; set; } = true;
        public bool? AutoForage { get; set; } = true;
        public bool? AutoLearnLanguages { get; set; } = true;
        public bool? AutoPutStatFoodInTopSlots { get; set; } = true;
        public bool? AutoSummonFoodAndDrink { get; set; } = true;
        /// <summary>
        /// Food and drink that should not be consumed, IE stat food and drink.
        /// </summary>
        public List<string> DontConsume { get; set; } = new List<string>();
        /// <summary>
        /// Foraged items that should not be kept and destroyed.
        /// </summary>
        public List<string> ForageBlacklist { get; set; } = new List<string>();
    }

    public class CharacterConfigSection : DefaultConfigSection
    {
        /// <summary>
        /// The character name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The server short name.
        /// </summary>
        public string Server { get; set; }
        /// <summary>
        /// The buffs configuration.
        /// </summary>
        //public BuffsConfigSection Buffs { get; set; } = new BuffsConfigSection();
        /// <summary>
        /// A list of food and drink dispensers.
        /// </summary>
        public List<FoodAndDrinkDispenser> Dispensers { get; set; } = new List<FoodAndDrinkDispenser>();
    }

    //public class BuffsConfigSection
    //{
        //public List<BuffConfig> Buffs { get; set; } = new List<BuffConfig>();
    //}

    //public enum BuffSource
    //{
        //Spell,
        //Item
    //}

    //public class BuffConfig
    //{
        //public string Name { get; set; } = null;

        //public BuffSource Type { get; set; } = BuffSource.Spell;
    //}

    public class FoodAndDrinkDispenser
    {
        /// <summary>
        /// The dispenser item ID. Either this or the <see cref="DispenserName"/> needs to be defined.
        /// </summary>
        public int? DispenserID { get; set; } = null;
        /// <summary>
        /// The dispenser item name. Either this or the <see cref="DispenserID"/> needs to be defined.
        /// </summary>
        public string DispenserName { get; set; } = null;
        /// <summary>
        /// The summoned item ID. Either this or the <see cref="SummonName"/> needs to be defined.
        /// </summary>
        public int? SummonID { get; set; } = null;
        /// <summary>
        /// The summoned item name. Either this or the <see cref="SummonID"/> needs to be defined.
        /// </summary>
        public string SummonName { get; set; } = null;
        /// <summary>
        /// The target stack size of the summoned item to maintain.
        /// </summary>
        public int TargetCount { get; set; } = 0;
    }

    public static class FluxConfigExtensions
    {
        /// <summary>
        /// Determine the effective configuration.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaults"></param>
        /// <returns></returns>
        public static CharacterConfigSection Effective(this CharacterConfigSection @this, DefaultConfigSection defaults)
        {
            if (@this == null)
            {
                return new CharacterConfigSection();
            }

            if (defaults == null)
            {
                return @this;
            }

            CharacterConfigSection effective = new CharacterConfigSection()
            {
                Name = @this.Name,
                Server = @this.Server,
                //Buffs = @this.Buffs,
                Dispensers = new List<FoodAndDrinkDispenser>(@this.Dispensers)
            };

            if (!effective.AutoBuff.HasValue)
            {
                effective.AutoBuff = defaults.AutoBuff;
            }

            if (!effective.AutoDispenseFoodAndDrink.HasValue)
            {
                effective.AutoDispenseFoodAndDrink = defaults.AutoDispenseFoodAndDrink;
            }

            if (!effective.AutoEatAndDrink.HasValue)
            {
                effective.AutoEatAndDrink = defaults.AutoEatAndDrink;
            }

            if (!effective.AutoForage.HasValue)
            {
                effective.AutoForage = defaults.AutoForage;
            }

            if (!effective.AutoLearnLanguages.HasValue)
            {
                effective.AutoLearnLanguages = defaults.AutoLearnLanguages;
            }

            if (!effective.AutoSummonFoodAndDrink.HasValue)
            {
                effective.AutoSummonFoodAndDrink = defaults.AutoSummonFoodAndDrink;
            }

            if (!effective.AutoPutStatFoodInTopSlots.HasValue)
            {
                effective.AutoPutStatFoodInTopSlots = defaults.AutoPutStatFoodInTopSlots;
            }

            if (!((effective.DontConsume?.Any()).GetValueOrDefault(false)) && ((defaults.DontConsume?.Any()).GetValueOrDefault(false)))
            {
                effective.DontConsume = new List<string>(defaults.DontConsume);
            }

            if (!((effective.ForageBlacklist?.Any()).GetValueOrDefault(false)) && ((defaults.ForageBlacklist?.Any()).GetValueOrDefault(false)))
            {
                effective.ForageBlacklist = new List<string>(defaults.ForageBlacklist);
            }

            return effective;
        }
    }

}
