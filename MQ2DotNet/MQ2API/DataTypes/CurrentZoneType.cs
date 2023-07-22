using MQ2DotNet.Services;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Extends the zone type with additional information about the current zone.
    /// Last Verified: 2023-07-03
    /// https://docs.macroquest.org/reference/data-types/datatype-currentzone/
    /// </summary>
    [MQ2Type("currentzone")]
    public class CurrentZoneType : ZoneType
    {
        internal CurrentZoneType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// ID of the zone
        /// </summary>
        public new int? ID => GetMember<IntType>("ID");
        
        /// <summary>
        /// Full zone name e.g. "The Plane of Knowledge"
        /// </summary>
        public new string Name => GetMember<StringType>("Name");

        /// <summary>
        /// Short zone name e.g. "PoKnowledge"
        /// </summary>
        public new string ShortName => GetMember<StringType>("ShortName");

        /// <summary>
        /// Zone type:0=Indoor Dungeon 1=Outdoor 2=Outdoor City 3=Dungeon City 4=Indoor City 5=Outdoor Dungeon.
        /// TODO: test the enum conversion.
        /// </summary>
        public EQ.ZoneType? ZoneType => GetMember<IntType>("ZoneType");

        /// <summary>
        /// Alias for <see cref="ZoneType"/>
        /// </summary>
        public EQ.ZoneType? Type => ZoneType;

        /// <summary>
        /// Minimum clip plane allowed in zone.
        /// </summary>
        public float? MinClip => GetMember<FloatType>("MinClip");

        /// <summary>
        /// Maximum clip plane allowed in zone.
        /// </summary>
        public float? MaxClip => GetMember<FloatType>("MaxClip");

        /// <summary>
        /// Is the zone a dungeon. Mounts cannot be used.
        /// Same as Indoor.
        /// </summary>
        public bool Dungeon => Indoor;

        /// <summary>
        /// True if this is an Indoor zone. Mounts cannot be used.
        /// </summary>
        public bool Indoor => GetMember<BoolType>("Indoor");

        /// <summary>
        /// True if this is an outdoor zone. Mounts can be used.
        /// </summary>
        public bool Outdoor => GetMember<BoolType>("Outdoor");

        /// <summary>
        /// True if binding isn't allowed in this zone outside specified bindable areas.
        /// </summary>
        public bool NoBind => GetMember<BoolType>("NoBind");

        public ZoneType Zone => ID.HasValue ? TLO.Instance?.GetZone(ID.Value) : null;

        /// <summary>
        /// Same as <see cref="ZoneType.Name"/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}