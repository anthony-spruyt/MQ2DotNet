using System;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Represents a ground item.
    /// Last Verified: 2023-07-02
    /// https://docs.macroquest.org/reference/data-types/datatype-ground/
    /// </summary>
    [PublicAPI]
    [MQ2Type("ground")]
    public class GroundType : MQ2DataType
    {
        internal GroundType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            _search = new IndexedMember<GroundType, int, GroundType, string>(this, "Search");
        }

        /// <summary>
        /// Create a new instance from a pointer
        /// </summary>
        /// <param name="mq2TypeFactory"></param>
        /// <param name="pGroundItem"></param>
        public GroundType(MQ2TypeFactory mq2TypeFactory, IntPtr pGroundItem)
            : base("ground", mq2TypeFactory, new MQ2VarPtr(pGroundItem))
        {
            _search = new IndexedMember<GroundType, int, GroundType, string>(this, "Search");
        }

        /// <summary>
        /// Picks up the ground spawn (must be within 20 units of it)
        /// </summary>
        /// <returns></returns>
        public bool Grab() => GetMember<BoolType>("Grab");

        /// <summary>
        /// Will cause the toon to target the called for spawn if it exists
        /// </summary>
        /// <returns></returns>
        public GroundType DoTarget() => GetMember<GroundType>("DoTarget");

        /// <summary>
        /// Will cause the toon to face the called for spawn if it exists
        /// </summary>
        /// <returns></returns>
        public GroundType DoFace() => GetMember<GroundType>("DoFace");
        
        /// <summary>
        /// Clears the currently selected ground spawn
        /// </summary>
        public void Reset() => GetMember<MQ2DataType>("Reset");

        /// <summary>
        /// Ground item ID (not the same as item ID, this is like spawn ID)
        /// </summary>
        public int? ID => GetMember<IntType>("ID");
        
        /// <summary>
        /// TODO: Document GroundType.SubID
        /// </summary>
        public int? SubID => GetMember<IntType>("SubID");
        
        /// <summary>
        /// ID of the zone the spawn is in?
        /// </summary>
        public int? ZoneID => GetMember<IntType>("ZoneID");
        
        /// <summary>
        /// X coordinate (Westward-positive)
        /// </summary>
        public float? W => X;
        
        /// <summary>
        /// X coordinate (Westward-positive)
        /// </summary>
        public float? X => GetMember<FloatType>("X");
        
        /// <summary>
        /// Y coordinate (Northward-positive)
        /// </summary>
        public float? N => Y;
        
        /// <summary>
        /// Y coordinate (Northward-positive)
        /// </summary>
        public float? Y => GetMember<FloatType>("Y");
        
        /// <summary>
        /// Z coordinate (Upward-positive)
        /// </summary>
        public float? U => Z;
        /// <summary>
        /// Z coordinate (Upward-positive)
        /// </summary>
        public float? Z => GetMember<FloatType>("Z");
        
        /// <summary>
        /// Internal name
        /// </summary>
        public string Name => GetMember<StringType>("Name");

        /// <summary>
        /// Displays name of the grounspawn
        /// </summary>
        public string DisplayName => GetMember<StringType>("DisplayName");

        /// <summary>
        /// Ground item is facing this heading.
        /// This can also be casted to a <see cref="float"/>
        /// </summary>
        public HeadingType Heading => GetMember<HeadingType>("Heading");

        /// <summary>
        /// Distance from player to ground item
        /// </summary>
        public float? Distance => GetMember<FloatType>("Distance");
        
        /// <summary>
        /// 3D distance from character to the ground item
        /// </summary>
        public float? Distance3D => GetMember<FloatType>("Distance3D");
        
        /// <summary>
        /// Direction player must move to meet this ground item.
        /// This can also be casted to a <see cref="float"/>
        /// </summary>
        public HeadingType HeadingTo => GetMember<HeadingType>("HeadingTo");
        
        /// <summary>
        /// Returns TRUE if ground spawn is in line of sight
        /// </summary>
        public bool LineOfSight => GetMember<BoolType>("LineOfSight");

        /// <summary>
        /// First ground spawn in the linked list
        /// </summary>
        [JsonIgnore]
        public GroundType First => GetMember<GroundType>("First");

        /// <summary>
        /// Last ground spawn in the linked list
        /// </summary>
        [JsonIgnore]
        public GroundType Last => GetMember<GroundType>("Last");

        /// <summary>
        /// Next ground spawn
        /// </summary>
        [JsonIgnore]
        public GroundType Next => GetMember<GroundType>("Next");

        /// <summary>
        /// Previous ground spawn
        /// </summary>
        [JsonIgnore]
        public GroundType Prev => GetMember<GroundType>("Prev");

        /// <summary>
        /// Get a ground spawn by ID or by name.
        /// </summary>
        private IndexedMember<GroundType, int, GroundType, string> _search;

        /// <summary>
        /// Get a ground spawn by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public GroundType Search(string name) => _search[name];

        /// <summary>
        /// Get a ground spawn by ID.
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public GroundType Search(int itemID) => _search[itemID];

        /// <summary>
        /// The closest ground spawn, if any.
        /// </summary>
        /// <returns></returns>
        public GroundType Closest => _search[""];

        /// <summary>
        /// Get a ground spawn by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public GroundType GetGroundSpawn(string name) => _search[name];

        /// <summary>
        /// Get a ground spawn by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GroundType GetGroundSpawn(int id) => _search[id];

        /// <summary>
        /// Same as <see cref="ID"/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}