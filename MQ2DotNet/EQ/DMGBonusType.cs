using JetBrains.Annotations;
using System.Runtime.Serialization;

namespace MQ2DotNet.EQ
{
    [PublicAPI]
    public enum DMGBonusType
    {
        [EnumMember(Value = "None")]
        None,
        [EnumMember(Value = "Magic")]
        Magic,
        [EnumMember(Value = "Fire")]
        Fire,
        [EnumMember(Value = "Cold")]
        Cold,
        [EnumMember(Value = "Poison")]
        Poison,
        [EnumMember(Value = "Disease")]
        Disease
    }
}
