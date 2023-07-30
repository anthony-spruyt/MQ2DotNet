using System.Runtime.Serialization;

namespace MQ2DotNet.EQ
{
    /// <summary>
    /// Beneficial(Group), Beneficial, Detrimental, Unknown
    /// </summary>
    public enum SpellKind
    {
        [EnumMember(Value = "Unknown")]
        Unknown,
        [EnumMember(Value = "Detrimental")]
        Detrimental,
        [EnumMember(Value = "Beneficial")]
        Beneficial,
        [EnumMember(Value = "Beneficial(Group)")]
        BeneficialGroup
    }
}
