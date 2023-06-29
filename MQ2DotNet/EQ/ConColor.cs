using JetBrains.Annotations;
using System.Runtime.Serialization;

namespace MQ2DotNet.EQ
{
    /// <summary>
    /// GREY, GREEN, LIGHT BLUE, BLUE, WHITE, YELLOW, RED
    /// </summary>
    [PublicAPI]
    public enum ConColor
    {
        [EnumMember(Value = "GREY")]
        Grey,
        [EnumMember(Value = "GREEN")]
        Green,
        [EnumMember(Value = "LIGHT BLUE")]
        LightBlue,
        [EnumMember(Value = "BLUE")]
        Blue,
        [EnumMember(Value = "WHITE")]
        White,
        [EnumMember(Value = "YELLOW")]
        Yellow,
        [EnumMember(Value = "RED")]
        Red
    }
}
