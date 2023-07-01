using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a mercenary.
    /// Last Verified: 2023-07-01
    /// https://docs.macroquest.org/reference/data-types/datatype-mercenary/
    /// </summary>
    [PublicAPI]
    [MQ2Type("mercenary")]
    public class MercenaryType : SpawnType
    {
        internal MercenaryType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// Number of unspent mercenary AA points
        /// </summary>
        public uint? AAPoints => GetMember<IntType>("AAPoints");
        
        /// <summary>
        /// Current stance of the mercenary e.g. Balanced
        /// TODO: map to an enum
        /// </summary>
        public string Stance => GetMember<StringType>("Stance");

        /// <summary>
        /// Current State of the mercenary
        /// </summary>
        public new string State => GetMember<StringType>("State");

        /// <summary>
        /// Current state ID of the mercenary as a number.
        /// </summary>
        public uint? StateID => GetMember<IntType>("StateID");
        
        /// <summary>
        /// Index of the mercenary in your mercenary list (1 based)
        /// </summary>
        public uint? Index => GetMember<IntType>("Index");

        /// <summary>
        /// Name of the mercenary
        /// </summary>
        public new string Name => GetMember<StringType>("Name");

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