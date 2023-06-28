using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for details about another type.
    /// Last Verified: 2023-06-28
    /// </summary>
    [PublicAPI]
    [MQ2Type("type")]
    public class TypeType : MQ2DataType
    {
        public const int MAX_MEMBERS = 1000;

        internal TypeType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            _member = new IndexedStringMember<int, IntType, string>(this, "Member");
        }

        /// <summary>
        /// Type name
        /// </summary>
        public StringType Name => GetMember<StringType>("Name");

        /// <summary>
        /// Member name from an internal ID number (1 based), or ID number from name
        /// </summary>
        [JsonIgnore]
        private IndexedStringMember<int, IntType, string> _member { get; }

        public string GetMember(int id) => _member[id];

        public int GetMemberId(string name) => (int)_member[name];

        public IEnumerable<string> Members
        {
            get
            {
                var items = new List<string>();
                var index = 1; // or 0?

                while (true)
                {
                    var item = GetMember(index);

                    if (item != null && index <= MAX_MEMBERS)
                    {
                        items.Add(item);
                        index++;
                    }
                    else
                    {
                        break;
                    }
                }

                return items;
            }
        }
    }
}