using System.Runtime.Serialization;

namespace MQ2DotNet.EQ
{
    /// <summary>
    /// The status of the member - one of the following: Unknown, Online, Offline, In Dynamic Zone, Link Dead.
    /// </summary>
    public enum DZStatus
    {
        [EnumMember(Value = "Unknown")]
        Unknown,
        [EnumMember(Value = "Online")]
        Online,
        [EnumMember(Value = "Offline")]
        Offline,
        [EnumMember(Value = "In Dynamic Zone")]
        InDynamicZone,
        [EnumMember(Value = "Link Dead")]
        LinkDead
    }
}
