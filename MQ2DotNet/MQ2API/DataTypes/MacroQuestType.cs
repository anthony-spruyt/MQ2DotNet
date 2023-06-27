using JetBrains.Annotations;
using System.Text.Json.Serialization;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for general information about MQ2.
    /// Last Verified: 2023-06-27
    /// </summary>
    [PublicAPI]
    [MQ2Type("macroquest")]
    public class MacroQuestType : MQ2DataType
    {
        internal MacroQuestType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            Path = new IndexedStringMember<string>(this, "Path");
        }

        /// <summary>
        /// Last normal error message.
        /// Comment in native MQ client: // QUIT SETTING THIS MANUALLY, USE MacroError, FatalError, ETC
        /// </summary>
        public string Error => GetMember<StringType>("Error");

        /// <summary>
        /// Last syntax error message
        /// </summary>
        public string SyntaxError => GetMember<StringType>("SyntaxError");

        /// <summary>
        /// Last MQ2Data parsing error message
        /// </summary>
        public string MQ2DataError => GetMember<StringType>("MQ2DataError");

        /// <summary>
        /// Date that MQ2Main.dll was built
        /// </summary>
        public string BuildDate => GetMember<StringType>("BuildDate");

        /// <summary>
        /// Build number
        /// </summary>
        public uint? Build => GetMember<IntType>("Build");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public string BuildName => GetMember<StringType>("BuildName");

        /// <summary>
        /// Gets the path by string index.
        /// 
        /// Index values:
        /// - root
        /// - config
        /// - crashdumps
        /// - logs
        /// - mqini
        /// - macros
        /// - plugins
        /// - resources
        /// </summary>
        [JsonIgnore]
        public IndexedStringMember<string> Path { get; }

        /// <summary>
        /// Version number of MQ2Main.dll
        /// </summary>
        public string Version => GetMember<StringType>("Version");
        
        /// <summary>
        /// Internal name of build e.g. RedGuides
        /// </summary>
        public string InternalName => GetMember<StringType>("InternalName");

        /// <summary>
        /// Parser version.
        /// </summary>
        public uint? Parser => GetMember<IntType>("Parser");

        /// <summary>
        /// Is anonymized?
        /// </summary>
        public bool Anonymize => GetMember<BoolType>("Anonymize");

        public override string ToString()
        {
            return typeof(MacroQuestType).FullName;
        }
    }
}