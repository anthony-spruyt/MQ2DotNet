using JetBrains.Annotations;
using System.Runtime.Serialization;

namespace MQ2DotNet.EQ
{
    /// <summary>
    /// COMBAT, DEBUFFED, COOLDOWN, ACTIVE, RESTING, UNKNOWN
    /// </summary>
    [PublicAPI]
    public enum CombatState
    {
        [EnumMember(Value = "COMBAT")]
        Combat,
        [EnumMember(Value = "DEBUFFED")]
        Debuffed,
        [EnumMember(Value = "COOLDOWN")]
        Cooldown,
        [EnumMember(Value = "ACTIVE")]
        Active,
        [EnumMember(Value = "RESTING")]
        Resting,
        [EnumMember(Value = "UNKNOWN")]
        Unknown
    }
}
