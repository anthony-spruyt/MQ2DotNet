using JetBrains.Annotations;
using System.Collections.Generic;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Contains information about data types.
    /// Last Verified: 2023-07-02
    /// https://docs.macroquest.org/reference/data-types/datatype-type/
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
        /// Member name based on an internal ID number (based on 1 through N, not all values will be used).
        /// Member[N]
        /// 
        /// Member internal ID number based on name (will be a number from 1 to N)
        /// Member[name]
        /// </summary>
        private IndexedStringMember<int, IntType, string> _member;

        /// <summary>
        /// Member name based on an internal ID number (based on 1 through N, not all values will be used).
        /// Member[N]
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetMemberName(int id) => _member[id];

        /// <summary>
        /// Member internal ID number based on name (will be a number from 1 to N)
        /// Member[name]
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int? GetMemberId(string name) => (int?)_member[name];

        /// <summary>
        /// This is likely not reliable.
        /// TODO: test this.
        /// </summary>
        public IEnumerable<string> Members
        {
            get
            {
                var index = 1;

                while (index <= MAX_MEMBERS)
                {
                    var item = GetMemberName(index);

                    if (item != null )
                    {
                        index++;

                        yield return item;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

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