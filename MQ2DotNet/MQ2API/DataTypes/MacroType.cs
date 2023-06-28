using JetBrains.Annotations;
using System;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for information about the currently running macro.
    /// Last Verified: 2023-06-27
    /// </summary>
    [PublicAPI]
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
        /// Prints undeclared variables to chat
        /// </summary>
        public void Undeclared() => GetMember<MQ2DataType>("Undeclared");

        /// <summary>
        /// Name of the currently running macro including extension e.g. kissassist.mac
        /// </summary>
        public string Name => GetMember<StringType>("Name");
        
        /// <summary>
        /// Time in milliseconds that the macro has been running for
        /// </summary>
        public TimeSpan? RunTime => GetMember<Int64Type>("RunTime");
        
        /// <summary>
        /// Macro currently paused?
        /// </summary>
        public bool Paused => GetMember<BoolType>("Paused");
        
        /// <summary>
        /// Value returned by the last subroutine call
        /// </summary>
        public string Return => GetMember<StringType>("Return");

        /// <summary>
        /// Is the given name a Top Level Object?
        /// Can/Should only be used it there is no active macro.
        /// </summary>
        private IndexedMember<BoolType> _isTLO;

        /// <summary>
        /// Is the given name a variable declared with outer scope?
        /// Can/Should only be used it there is no active macro.
        /// </summary>
        private IndexedMember<BoolType> _isOuterVariable;
        
        /// <summary>
        /// Stack depth of the currently executing macro
        /// </summary>
        public uint? StackSize => GetMember<IntType>("StackSize");
        
        /// <summary>
        /// Number of parameters supplied to the currently executing macro
        /// </summary>
        public uint? Params => GetMember<IntType>("Params");
        
        /// <summary>
        /// Line the currently executing macro is on
        /// </summary>
        public uint? CurLine => GetMember<IntType>("CurLine");

        /// <summary>
        /// Subroutine currently being executed, including arguments e.g. "MySub(string arg1)"
        /// </summary>
        public string CurSub => GetMember<StringType>("CurSub");

        /// <summary>
        /// Current command to be run by the executed macro
        /// </summary>
        public string CurCommand => GetMember<StringType>("CurCommand");
        
        /// <summary>
        /// Memory usage in bytes of the currently executing macro
        /// </summary>
        public uint? MemUse => GetMember<IntType>("MemUse");

        /// <summary>
        /// TODO: new member
        /// </summary>
        private IndexedMember<MQ2DataType> _variable;
    }
}