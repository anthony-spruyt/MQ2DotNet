using System;
using System.Collections.Generic;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Contains all the data about your fellowship
    /// Last Verified: 2023-07-03
    /// https://docs.macroquest.org/reference/data-types/datatype-fellowship/
    /// </summary>
    [MQ2Type("fellowship")]
    public class FellowshipType : MQ2DataType
    {
        internal FellowshipType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            _member = new IndexedMember<FellowshipMemberType, string, FellowshipMemberType, int>(this, "Member");
            _sharing = new IndexedMember<BoolType, int>(this, "Sharing");
        }

        /// <summary>
        /// Returns TRUE if a fellowship exists.
        /// </summary>
        public bool Exists => GetMember<BoolType>("Exists");

        /// <summary>
        /// Fellowship ID.
        /// </summary>
        public uint? ID => GetMember<IntType>("ID");

        /// <summary>
        /// Fellowship leader's name.
        /// </summary>
        public string Leader => GetMember<StringType>("Leader");

        /// <summary>
        /// Fellowship Message of the Day.
        /// </summary>
        public string MotD => GetMember<StringType>("MotD");

        /// <summary>
        /// Number of members in the fellowship.
        /// </summary>
        public uint? Members => GetMember<IntType>("Members");

        /// <summary>
        /// Member data by name or #.
        /// Member[ name|# ]
        /// </summary>
        private readonly IndexedMember<FellowshipMemberType, string, FellowshipMemberType, int> _member;

        /// <summary>
        /// Member data by name.
        /// Member[ name ]
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public FellowshipMemberType GetFellowshipMember(string name) => _member[name];

        /// <summary>
        /// Member data by # (base 1).
        /// Member[ # ]
        /// </summary>
        /// <param name="index">The base 1 index.</param>
        /// <returns></returns>
        public FellowshipMemberType GetFellowshipMember(int index) => _member[index];

        public IEnumerable<FellowshipMemberType> FellowshipMembers
        {
            get
            {
                var count = (int)Members.GetValueOrDefault(0u);

                for (int i = 0; i < count; i++)
                {
                    yield return GetFellowshipMember(i + 1);
                }
            }
        }

        /// <summary>
        /// Time left on current campfire.
        /// </summary>
        /// <remarks>Stores data in the <see cref="MQ2VarPtr.Dword"/> field.</remarks>
        public TimeSpan? CampfireDuration => GetMember<TicksType>("CampfireDuration");

        /// <summary>
        /// Campfire Y location.
        /// </summary>
        public float? CampfireY => GetMember<FloatType>("CampfireY");

        /// <summary>
        /// Campfire X location.
        /// </summary>
        public float? CampfireX => GetMember<FloatType>("CampfireX");

        /// <summary>
        /// Campfire Z location.
        /// </summary>
        public float? CampfireZ => GetMember<FloatType>("CampfireZ");

        /// <summary>
        /// Zone information for the zone that contains your campfire.
        /// </summary>
        public ZoneType CampfireZone => GetMember<ZoneType>("CampfireZone");

        /// <summary>
        /// TRUE if campfire is up, FALSE if not.
        /// </summary>
        public bool Campfire => GetMember<BoolType>("Campfire");

        /// <summary>
        /// Returns TRUE if exp sharing is enabled for the Nth member.
        /// Sharing[ N ]
        /// </summary>
        private readonly IndexedMember<BoolType, int> _sharing;

        /// <summary>
        /// Returns TRUE if exp sharing is enabled for the Nth (base 1) member.
        /// Sharing[ N ]
        /// </summary>
        /// <param name="index">The base 1 index.</param>
        /// <returns></returns>
        public bool IsSharing(int nth) => _sharing[nth];

        /// <summary>
        /// TRUE if currently in a fellowship, FALSE if not.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}