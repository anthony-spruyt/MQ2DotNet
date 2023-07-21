namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Provides access to world locations such as a character's bound location.
    /// Last Verified: 2023-07-03
    /// https://docs.macroquest.org/reference/data-types/datatype-worldlocation/
    /// </summary>
    /// <remarks>This type is only used for character's bound locations, VarPtr.Dword is an index in CHARINFO2::BoundLocations</remarks>
    [MQ2Type("worldlocation")]
    public class WorldLocationType : MQ2DataType
    {
        internal WorldLocationType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// The location's ID (Zone ID)
        /// </summary>
        public uint? ID => GetMember<IntType>("ID");

        /// <summary>
        /// Access to the zone data.
        /// </summary>
        public ZoneType Zone => GetMember<ZoneType>("Zone");
        
        /// <summary>
        /// Y coordinate (Northward-positive)
        /// </summary>
        public float? Y => GetMember<FloatType>("Y");
        
        /// <summary>
        /// X coordinate (Westward-positive)
        /// </summary>
        public float? X => GetMember<FloatType>("X");
        
        /// <summary>
        /// Z coordinate (Upward-positive)
        /// </summary>
        public float? Z => GetMember<FloatType>("Z");

        /// <summary>
        /// At the point of binding, what direction was the character facing.
        /// </summary>
        public float? Heading => GetMember<FloatType>("Heading");
    }
}