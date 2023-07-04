using JetBrains.Annotations;
using MQ2DotNet.Services;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Contains the data related to your extended target list.
    /// Last Verified: 2023-07-03
    /// https://docs.macroquest.org/reference/data-types/datatype-xtarget/
    /// </summary>
    [PublicAPI]
    [MQ2Type("xtarget")]
    public class XTargetType : MQ2DataType
    {
        internal XTargetType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// Extended target type
        /// - Empty Target
        /// - Auto Hater
        /// - Specific PC
        /// - Specific NPC
        /// - Target's Target
        /// - Group Tank
        /// - Group Tank's Target
        /// - Group Assist
        /// - Group Assist Target
        /// - Group Puller
        /// - Group Puller Target
        /// - Group Mark 1
        /// - Group Mark 2
        /// - Group Mark 3
        /// - Raid Assist 1
        /// - Raid Assist 2
        /// - Raid Assist 3
        /// - Raid Assist 1 Target
        /// - Raid Assist 2 Target
        /// - Raid Assist 3 Target
        /// - Raid Mark 1
        /// - Raid Mark 2
        /// - Raid Mark 3
        /// - Pet Target
        /// - Mercenary Target
        /// TODO: convert to enum
        /// </summary>
        public string TargetType => GetMember<StringType>("TargetType");

        /// <summary>
        /// ID of specified XTarget (Spawn ID).
        /// </summary>
        public uint? ID => GetMember<IntType>("ID");

        /// <summary>
        /// Name of specified XTarget.
        /// </summary>
        public string Name => GetMember<StringType>("Name");

        /// <summary>
        /// Your percentage aggro on the spawn.
        /// PctAggro of specified XTarget.
        /// </summary>
        public uint? PctAggro => GetMember<IntType>("PctAggro");

        /// <summary>
        /// Spawn in the XTarget slot
        /// </summary>
        public SpawnType GetSpawn(TLO tlo) => tlo.GetSpawn($"id {ID}");

        /// <summary>
        /// Spawn in the XTarget slot
        /// </summary>
        public SpawnType Spawn => TLO.Instance?.GetSpawn($"id {ID}");
    }
}