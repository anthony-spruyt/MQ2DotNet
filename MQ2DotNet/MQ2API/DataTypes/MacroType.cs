namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// The Macro DataType deals with the macro currently running, and nothing else.
    /// Last Verified: 2023-07-02
    /// https://docs.macroquest.org/reference/data-types/datatype-macro/
    /// </summary>
    [MQ2Type("macro")]
    public class MacroType : MQ2DataType
    {
        internal MacroType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            _isTLO = new IndexedMember<BoolType>(this, "IsTLO");
            _isOuterVariable = new IndexedMember<BoolType>(this, "IsOuterVariable");
            _variable = new IndexedMember<MQ2DataType>(this, "Variable");
        }

        /// <summary>
        /// List all undeclared variables. Outputs to chat.
        /// </summary>
        public void Undeclared() => GetMember<MQ2DataType>("Undeclared");

        /// <summary>
        /// The name of the macro currently running e.g. kissassist.mac
        /// </summary>
        public string Name => GetMember<StringType>("Name");

        // This causes a stack overflow.
        ///// <summary>
        ///// How long the macro has been running.
        ///// Doco states seconds, but source looks like milliseconds.
        ///// </summary>
        //public long? RunTime => GetMember<Int64Type>("RunTime");

        /// <summary>
        /// NULL if no macro running, FALSE if mqpause is off, TRUE if mqpause is on
        /// </summary>
        public bool? Paused => GetMember<BoolType>("Paused");

        /// <summary>
        /// The value that was returned by the last completed subroutine
        /// </summary>
        public string Return => GetMember<StringType>("Return");

        /// <summary>
        /// True if the provided parameter an existing TLO.
        /// Can/Should only be used it there is no active macro.
        /// </summary>
        private readonly IndexedMember<BoolType> _isTLO;

        /// <summary>
        /// True if the provided parameter an existing TLO.
        /// Can/Should only be used it there is no active macro.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsTLO(string name) => (bool)_isTLO[name];

        /// <summary>
        /// True if the provided parameter is a defined outer variable.
        /// Can/Should only be used it there is no active macro.
        /// </summary>
        private readonly IndexedMember<BoolType> _isOuterVariable;

        /// <summary>
        /// True if the provided parameter is a defined outer variable.
        /// Can/Should only be used it there is no active macro.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsOuterVar(string name) => (bool)_isOuterVariable[name];

        /// <summary>
        /// Stack depth of the currently executing macro
        /// </summary>
        public uint? StackSize => GetMember<IntType>("StackSize");

        /// <summary>
        /// The number of parameters that were passed to the current subroutine
        /// </summary>
        public uint? Params => GetMember<IntType>("Params");

        /// <summary>
        /// The current line number of the macro being processed
        /// </summary>
        public uint? CurLine => GetMember<IntType>("CurLine");

        /// <summary>
        /// The current subroutine
        /// </summary>
        public string CurSub => GetMember<StringType>("CurSub");

        /// <summary>
        /// List the current line number, macro name, and code of the macro being processed
        /// </summary>
        public string CurCommand => GetMember<StringType>("CurCommand");

        /// <summary>
        /// How much memory the macro is using (bytes).
        /// </summary>
        public uint? MemUse => GetMember<IntType>("MemUse");

        /// <summary>
        /// Returns the value given the name of Macro variable.
        /// </summary>
        private readonly IndexedMember<MQ2DataType> _variable;

        /// <summary>
        /// Returns the value given the name of Macro variable.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public MQ2DataType GetVariable(string name) => _variable[name];
    }
}