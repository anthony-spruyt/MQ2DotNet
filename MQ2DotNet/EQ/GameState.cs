using JetBrains.Annotations;
using System.Runtime.Serialization;

namespace MQ2DotNet.EQ
{
    /// <summary>
    /// State of the game, e.g. char select, in game
    /// </summary>
    [PublicAPI]
    public enum GameState : uint
    {
        [EnumMember(Value = "CHARSELECT")]
        CharSelect = 1U,
        CharCreate = 2U,
        Something = 4U,
        [EnumMember(Value = "INGAME")]
        InGame = 5U,
        [EnumMember(Value = "PRECHARSELECT")]
        PreCharSelect = uint.MaxValue,
        PostFrontLoad = 500U,
        LoggingIn = 253U,
        Unloading = 255U,
        [EnumMember(Value = "UNKNOWN")]
        Unknown = 65535U
    }
}
