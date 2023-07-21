using System.Collections.Generic;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Grants access to your friends list.
    /// Last Verified: 2023-07-02
    /// https://docs.macroquest.org/reference/top-level-objects/tlo-friends/#friends-type
    /// </summary>
    [MQ2Type("friend")]
    public class FriendsType : MQ2DataType
    {
        public const int MAX_FRIENDS = 200;

        internal FriendsType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            _friend = new IndexedStringMember<int, BoolType, string>(this, "Friend");
        }

        /// <summary>
        /// Name of a friend by index (1 based) or true/false if a name is on your friend list
        /// </summary>
        private readonly IndexedStringMember<int, BoolType, string> _friend;

        /// <summary>
        /// Returns the name of friend list member # (base 1)
        /// Friend[#]
        /// </summary>
        /// <param name="nth">The nth friend (base 1)</param>
        /// <returns></returns>
        public string GetFriend(int nth) => _friend[nth];

        /// <summary>
        /// Returns TRUE if name is on your friend list
        /// Friend[name]
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsFriend(string name) => _friend[name];

        public IEnumerable<string> Friends
        {
            get
            {
                var i = 1;
                string friend;

                while (true && i <= MAX_FRIENDS)
                {
                    friend = GetFriend(i);

                    if (string.IsNullOrWhiteSpace(friend))
                    {
                        break;
                    }

                    i++;

                    yield return friend;
                }
            }
        }

        /// <summary>
        /// Number of friends on your friends list
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}