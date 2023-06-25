using JetBrains.Annotations;

namespace MQ2DotNet.EQ
{
    /// <summary>
    /// State of the game, e.g. char select, in game
    /// </summary>
    [PublicAPI]
    public enum GameState : uint
    {
#pragma warning disable 1591
        CharSelect = 1U,
        CharCreate = 2U,
        Something = 4U,
        InGame = 5U,
        PreCharSelect = uint.MaxValue,
        PostFrontLoad = 500U,
        LoggingIn = 253U,
        Unloading = 255U,
        Unknown = 65535U
#pragma warning restore 1591
    }
}
