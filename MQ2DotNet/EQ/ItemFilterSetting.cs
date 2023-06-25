using JetBrains.Annotations;
using System;

namespace MQ2DotNet.EQ
{
    /// <summary>
    /// 1 = AutoRoll, 2 = Need, 4 = Greed, 8 = Never
    /// </summary>
    [PublicAPI]
    [Flags]
    public enum ItemFilterSetting : uint
    {
        AutoRoll = 1U,
        Need = 2U,
        Greed = 4U,
        Never = 8U,
    }
}
