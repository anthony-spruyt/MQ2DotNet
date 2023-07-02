using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Represents the state of the frame limiter.
    /// Last Verified: 2023-07-02
    /// https://docs.macroquest.org/reference/top-level-objects/tlo-framelimiter/#framelimiter-type
    /// </summary>
    [PublicAPI]
    [MQ2Type("framelimiter")]
    public class FrameLimiterType : MQ2DataType
    {
        public FrameLimiterType(MQ2TypeFactory typeFactory, MQ2TypeVar typeVar) : base(typeFactory, typeVar)
        {
        }

        protected FrameLimiterType(string typeName, MQ2TypeFactory typeFactory, MQ2VarPtr varPtr) : base(typeName, typeFactory, varPtr)
        {
        }

        /// <summary>
        /// TRUE if the frame limiter feature is currently active.
        /// </summary>
        public bool Enabled => GetMember<BoolType>("Enabled");

        /// <summary>
        /// TRUE if settings for the frame limiter are being saved by character.
        /// </summary>
        public bool SaveByChar => GetMember<BoolType>("SaveByChar");

        /// <summary>
        /// Either "Foreground" or "Background".
        /// TODO: map to enum
        /// </summary>
        public string Status => GetMember<StringType>("Status");

        /// <summary>
        /// Current CPU usage as %
        /// </summary>
        public float? CPU => GetMember<FloatType>("CPU");

        /// <summary>
        /// Current graphics scene frame rate (visible fps).
        /// </summary>
        public float? RenderFPS => GetMember<FloatType>("RenderFPS");

        /// <summary>
        /// Current simulation frame rate (game updates per second).
        /// </summary>
        public float? SimulationFPS => GetMember<FloatType>("SimulationFPS");

        /// <summary>
        /// Value of the target background fps setting.
        /// </summary>
        public float? BackgroundFPS => GetMember<FloatType>("BackgroundFPS");

        /// <summary>
        /// Value of the target foreground fps setting.
        /// </summary>
        public float? ForegroundFPS => GetMember<FloatType>("ForegroundFPS");

        /// <summary>
        /// Value of the minimum simualtion rate setting.
        /// </summary>
        public float? MinSimulationFPS => GetMember<FloatType>("MinSimulationFPS");

        /// <summary>
        /// Value of the clear screen when not rendering setting.
        /// </summary>
        public bool ClearScreen => GetMember<BoolType>("ClearScreen");

        /// <summary>
        /// Same as <see cref="Enabled"/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
