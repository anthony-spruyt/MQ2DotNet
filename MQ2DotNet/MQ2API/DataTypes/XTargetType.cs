using JetBrains.Annotations;
using MQ2DotNet.Services;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for an entry in the xtarget list.
    /// Last Verified: 2023-06-28
    /// </summary>
    [PublicAPI]
    [MQ2Type("xtarget")]
    public class XTargetType : MQ2DataType
    {
        internal XTargetType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// Extended target type e.g. Auto Hater
        /// </summary>
        public string TargetType => GetMember<StringType>("TargetType");
        
        /// <summary>
        /// Spawn ID
        /// </summary>
        public uint? ID => GetMember<IntType>("ID");
        
        /// <summary>
        /// Spawn's name
        /// </summary>
        public string Name => GetMember<StringType>("Name");
        
        /// <summary>
        /// Your percentage aggro on the spawn
        /// </summary>
        public uint? PctAggro => GetMember<IntType>("PctAggro");

        /// <summary>
        /// Spawn in the XTarget slot
        /// </summary>
        public SpawnType GetSpawn(TLO tlo) => tlo.GetSpawn($"id {ID}");
    }
}