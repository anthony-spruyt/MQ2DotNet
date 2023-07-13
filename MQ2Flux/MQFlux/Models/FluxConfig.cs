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
        public DefaultConfig Defaults { get; set; } = new DefaultConfig();
        /// <summary>
        /// Character specific configuration. Overrides the default configuration.
        /// </summary>
        public List<CharacterConfig> Characters { get; set; } = new List<CharacterConfig>();
    }

    public class DefaultConfig
    {
        public bool? AutoLearnLanguages { get; set; } = true;
        public bool? AutoDispenseFoodAndDrink { get; set; } = true;
        public bool? AutoSummonFoodAndDrink { get; set; } = true;
        public bool? AutoForage { get; set; } = true;
        public bool? AutoEatAndDrink { get; set; } = true;
        public bool? AutoSortInventory { get; set; } = true;
        /// <summary>
        /// Food and drink that should not be consumed, IE stat food and drink.
        /// </summary>
        public List<string> DontConsume { get; set; } = new List<string>();
        /// <summary>
        /// Foraged items that should not be kept and destroyed.
        /// </summary>
        public List<string> ForageBlacklist { get; set; } = new List<string>();
    }

    public class CharacterConfig : DefaultConfig
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
        /// A list of food and drink dispensers.
        /// </summary>
        public List<FoodAndDrinkDispenser> Dispensers { get; set; } = new List<FoodAndDrinkDispenser>();

        internal static readonly string CacheKey = nameof(CharacterConfig);
    }

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
        public static CharacterConfig Effective(this CharacterConfig @this, DefaultConfig defaults)
        {
            if (@this == null)
            {
                return new CharacterConfig();
            }

            if (defaults == null)
            {
                return @this;
            }

            CharacterConfig effective = new CharacterConfig()
            {
                Name = @this.Name,
                Server = @this.Server,
                Dispensers = new List<FoodAndDrinkDispenser>(@this.Dispensers)
            };

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

            if (!effective.AutoSortInventory.HasValue)
            {
                effective.AutoSortInventory = defaults.AutoSortInventory;
            }

            if (!(effective.DontConsume?.Any() ?? false) && (defaults.DontConsume?.Any() ?? false))
            {
                effective.DontConsume = new List<string>(defaults.DontConsume);
            }

            if (!(effective.ForageBlacklist?.Any() ?? false) && (defaults.ForageBlacklist?.Any() ?? false))
            {
                effective.ForageBlacklist = new List<string>(defaults.ForageBlacklist);
            }

            return effective;
        }
    }

}
