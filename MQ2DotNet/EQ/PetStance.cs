using System.Runtime.Serialization;

namespace MQ2DotNet.EQ
{
    /// <summary>
    /// Current stance, either "FOLLOW" or "GUARD"
    /// </summary>
    public enum PetStance
    {
        [EnumMember(Value = "FOLLOW")]
        Follow,
        [EnumMember(Value = "GUARD")]
        Guard
    }
}
