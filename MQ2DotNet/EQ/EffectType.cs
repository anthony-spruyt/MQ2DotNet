using JetBrains.Annotations;
using System.Runtime.Serialization;

namespace MQ2DotNet.EQ
{
    /// <summary>
    /// Spell effect type (see below for spell effect types)
    /// Click Inventory - item has a right-click spell and can be cast from inventory
    /// Click Unknown - item has an unknown right-click effect restriction
    /// Click Worn - item has a right-click spell and must be equipped to click it
    /// Combat - weapon has a proc
    /// Spell Scroll - Scribeable spell scroll
    /// Worn - item has a focus effect
    /// </summary>
    [PublicAPI]
    public enum EffectType
    {
        [EnumMember(Value = "Click Inventory")]
        ClickInventory,
        [EnumMember(Value = "Click Unknown")]
        ClickUnknown,
        [EnumMember(Value = "Click Worn")]
        ClickWorn,
        [EnumMember(Value = "Combat")]
        Combat,
        [EnumMember(Value = "Spell Scroll")]
        SpellScroll,
        [EnumMember(Value = "Worn")]
        Worn
    }
}
