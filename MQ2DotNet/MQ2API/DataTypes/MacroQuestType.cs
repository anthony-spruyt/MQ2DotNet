namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Data types related to the current MacroQuest2 session. These also inherit from the EverQuest Type.
    /// Last Verified: 2023-07-02
    /// https://docs.macroquest.org/reference/data-types/datatype-macroquest/
    /// </summary>
    [MQ2Type("macroquest")]
    public class MacroQuestType : EverQuestType
    {
        internal MacroQuestType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            _path = new IndexedStringMember<string>(this, "Path");
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
        /// The build (client target) for MQ2Main.dll (1 - Live, 2 - Test, 3 - Beta, 4 - Emu)
        /// </summary>
        public uint? Build => GetMember<IntType>("Build");

        /// <summary>
        /// The build (client target) name for MQ2Main.dll (Live, Test, Beta, Emu)
        /// </summary>
        public string BuildName => GetMember<StringType>("BuildName");

        /// <summary>
        /// Directory that Macroquest.exe launched from. When passed root/config/crashdumps/logs/mqini/macros/plugins/resources, returns the respective path
        /// Path[Option]
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
        private readonly IndexedStringMember<string> _path;

        /// <summary>
        /// Directory that Macroquest.exe launched from. When passed root/config/crashdumps/logs/mqini/macros/plugins/resources, returns the respective path
        /// Path[Option]
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
        /// <param name="options"></param>
        /// <returns></returns>
        public string GetPath(string options) => _path[options];
        public string PathRoot => _path["root"];
        public string PathConfig => _path["config"];
        public string PathCrashDumps => _path["crashdumps"];
        public string PathLogs => _path["logs"];
        public string PathMQIni => _path["mqini"];
        public string PathMacros => _path["macros"];
        public string PathPlugins => _path["plugins"];
        public string PathResources => _path["resources"];

        /// <summary>
        /// The full version of MQ2Main.dll
        /// </summary>
        public string Version => GetMember<StringType>("Version");

        /// <summary>
        /// The internal name from MQ2Main.dll ("Next")
        /// </summary>
        public string InternalName => GetMember<StringType>("InternalName");

        /// <summary>
        /// Which parser engine is currently active
        /// </summary>
        public uint? Parser => GetMember<IntType>("Parser");

        /// <summary>
        /// Is anonymized?
        /// </summary>
        public bool Anonymize => GetMember<BoolType>("Anonymize");

        public override string ToString()
        {
            return OriginalToString();
        }
    }
}