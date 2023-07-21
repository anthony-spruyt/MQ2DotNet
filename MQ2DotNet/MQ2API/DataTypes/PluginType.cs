namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Data for the specified plugin.
    /// Last Verified: 2023-07-01
    /// https://docs.macroquest.org/reference/data-types/datatype-plugin/
    /// </summary>
    [MQ2Type("plugin")]
    public class PluginType : MQ2DataType
    {
        public const int MAX_PLUGINS = 100;

        internal PluginType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// Name of the plugin.
        /// </summary>
        public string Name => GetMember<StringType>("Name");

        /// <summary>
        /// Version number of the plugin.
        /// </summary>
        public float? Version => GetMember<FloatType>("Version");

        /// <summary>
        /// Is the plugin loaded?
        /// </summary>
        public bool IsLoaded => GetMember<BoolType>("IsLoaded");

        /// <summary>
        /// Same as <see cref="Name"/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}