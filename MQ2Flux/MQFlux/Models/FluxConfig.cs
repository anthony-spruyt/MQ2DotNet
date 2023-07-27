using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MQFlux.Models
{
    public class FluxConfig
    {
        [JsonPropertyOrder(0)]
        public string Version { get; set; } = "1.0.0";
        [JsonPropertyOrder(4)]
        public bool? AutoBuff { get; set; } = true;
        [JsonPropertyOrder(5)]
        public bool? AutoDispenseFoodAndDrink { get; set; } = true;
        [JsonPropertyOrder(6)]
        public bool? AutoEatAndDrink { get; set; } = true;
        [JsonPropertyOrder(7)]
        public bool? AutoForage { get; set; } = true;
        [JsonPropertyOrder(8)]
        public bool? AutoLearnLanguages { get; set; } = true;
        [JsonPropertyOrder(9)]
        public bool? AutoPutStatFoodInTopSlots { get; set; } = true;
        [JsonPropertyOrder(10)]
        public bool? AutoSummonFoodAndDrink { get; set; } = true;
        /// <summary>
        /// Food and drink that should not be consumed, IE stat food and drink.
        /// </summary>
        [JsonPropertyOrder(12)]
        public List<string> DontConsume { get; set; } = new List<string>();
        /// <summary>
        /// Foraged items that should not be kept and destroyed.
        /// </summary>
        [JsonPropertyOrder(13)]
        public List<string> ForageBlacklist { get; set; } = new List<string>();
    }

    public class CharacterConfig : FluxConfig
    {
        public CharacterConfig()
        {
        }

        public CharacterConfig(FluxConfig defaults, string name, string server)
        {
            if (defaults is null)
            {
                throw new ArgumentNullException(nameof(defaults));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            }

            if (string.IsNullOrEmpty(server))
            {
                throw new ArgumentException($"'{nameof(server)}' cannot be null or empty.", nameof(server));
            }

            Name = name;
            Server = server;
            AutoBuff = defaults.AutoBuff;
            AutoDispenseFoodAndDrink = defaults.AutoDispenseFoodAndDrink;
            AutoEatAndDrink = defaults.AutoEatAndDrink;
            AutoForage = defaults.AutoForage;
            AutoLearnLanguages = defaults.AutoLearnLanguages;
            AutoPutStatFoodInTopSlots = defaults.AutoPutStatFoodInTopSlots;
            AutoSummonFoodAndDrink = defaults.AutoSummonFoodAndDrink;
            DontConsume = defaults.DontConsume;
            ForageBlacklist = defaults.ForageBlacklist;
            Version = defaults.Version;
        }

        /// <summary>
        /// The character name.
        /// </summary>
        [JsonPropertyOrder(2)]
        public string Name { get; set; } = null;
        /// <summary>
        /// The server short name.
        /// </summary>
        [JsonPropertyOrder(3)]
        public string Server { get; set; } = null;
        /// <summary>
        /// The buffs configuration.
        /// </summary>
        //public BuffsConfigSection Buffs { get; set; } = new BuffsConfigSection();
        /// <summary>
        /// A list of food and drink dispensers.
        /// </summary>
        [JsonPropertyOrder(11)]
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
}
