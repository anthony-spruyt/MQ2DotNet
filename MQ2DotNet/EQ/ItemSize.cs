using JetBrains.Annotations;

namespace MQ2DotNet.EQ
{
    /// <summary>
    /// Item size (1 = Small, 2 = Medium, 3 = Large, 4 = Giant)
    /// </summary>
    [PublicAPI]
    public enum ItemSize : uint
    {
        Small = 1U,
        Medium = 2U,
        Large = 3U,
        Giant = 4U
    }
}
