using System.Collections.Generic;
using System.Linq;

namespace MQ2Flux.Models
{
    public class FluxConfig
    {
        public string Version { get; set; }
        /// <summary>
        /// Configuration that applies to all characters.
        /// </summary>
        public DefaultConfig Defaults { get; set; }
        /// <summary>
        /// Character specific configuration. Overrides the default configuration.
        /// </summary>
        public List<CharacterConfig> Characters { get; set; }

        public FluxConfig()
        {
            Version = "1.0.0";
            Defaults = new DefaultConfig();
            Characters = new List<CharacterConfig>();
        }
    }

    public class DefaultConfig
    {
        /// <summary>
        /// Food and drink that should not be consumed, IE stat food and drink.
        /// </summary>
        public List<string> DontConsume { get; set; }

        public DefaultConfig()
        {
            DontConsume = new List<string>();
        }
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
        public List<FoodAndDrinkDispenser> Dispensers { get; set; }

        public CharacterConfig() : base()
        {
            Dispensers = new List<FoodAndDrinkDispenser>();
        }
    }

    public class FoodAndDrinkDispenser
    {
        /// <summary>
        /// The dispenser item ID. Either this or the <see cref="DispenserName"/> needs to be defined.
        /// </summary>
        public int? DispenserID { get; set; }
        /// <summary>
        /// The dispenser item name. Either this or the <see cref="DispenserID"/> needs to be defined.
        /// </summary>
        public string DispenserName { get; set; }
        /// <summary>
        /// The summoned item ID. Either this or the <see cref="SummonName"/> needs to be defined.
        /// </summary>
        public int? SummonID { get; set; }
        /// <summary>
        /// The summoned item name. Either this or the <see cref="SummonID"/> needs to be defined.
        /// </summary>
        public string SummonName { get; set; }
        public int TargetCount { get; set; }
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

            if (!(effective.DontConsume?.Any() ?? false) && (defaults.DontConsume?.Any() ?? false))
            {
                effective.DontConsume = new List<string>(defaults.DontConsume);
            }

            return effective;
        }
    }

}
