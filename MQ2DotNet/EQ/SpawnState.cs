using System.Runtime.Serialization;

namespace MQ2DotNet.EQ
{
    /// <summary>
    /// STAND, SIT, DUCK, BIND, FEIGN, DEAD, STUN, HOVER, MOUNT, UNKNOWN
    /// </summary>
    public enum SpawnState
    {
        [EnumMember(Value = "STAND")]
        Stand,
        [EnumMember(Value = "SIT")]
        Sit,
        [EnumMember(Value = "DUCK")]
        Duck,
        [EnumMember(Value = "BIND")]
        Bind,
        [EnumMember(Value = "FEIGN")]
        Feign,
        [EnumMember(Value = "DEAD")]
        Dead,
        [EnumMember(Value = "STUN")]
        Stun,
        [EnumMember(Value = "HOVER")]
        Hover,
        [EnumMember(Value = "MOUNT")]
        Mount,
        [EnumMember(Value = "UNKNOWN")]
        Unknown
    }
}
