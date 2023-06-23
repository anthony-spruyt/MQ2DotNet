using JetBrains.Annotations;
using System;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a buff on the current target spawn
    /// </summary>
    [PublicAPI]
    [MQ2Type("targetbuff")]
    [Obsolete("This type doesnt exist in MQ anymore.")]
    public class TargetBuffType : SpellType
    {
        internal TargetBuffType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// Index (0 based) of this buff in the target's buff window, i.e. slot #
        /// </summary>
        public int? Index => GetMember<IntType>("Index");
    }
}