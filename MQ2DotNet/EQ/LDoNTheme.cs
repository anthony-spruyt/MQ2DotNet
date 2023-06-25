using JetBrains.Annotations;
using System.Runtime.Serialization;

namespace MQ2DotNet.EQ
{
    [PublicAPI]
    public enum LDoNTheme
    {
        [EnumMember(Value = "All")]
        All,
        [EnumMember(Value = "Deepest Guk")]
        DeepestGuk,
        [EnumMember(Value = "Miragul's")]
        Miraguls,
        [EnumMember(Value = "Mistmoore")]
        Mistmoore,
        [EnumMember(Value = "Rujarkian")]
        Rujarkian,
        [EnumMember(Value = "Takish")]
        Takish,
        [EnumMember(Value = "Unknown")]
        Unknown
    }
}
