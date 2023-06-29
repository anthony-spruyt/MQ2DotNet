using JetBrains.Annotations;
using System.Collections.Generic;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for an alert list (a list of spawn searches).
    /// Last Verified: 2023-06-25
    /// https://docs.macroquest.org/reference/top-level-objects/tlo-alert/#alert-type
    /// </summary>
    [PublicAPI]
    [MQ2Type("alert")]
    public class AlertType : MQ2DataType
    {
        internal AlertType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            _list = new IndexedMember<AlertListType, int>(this, "List");
        }

        /// <summary>
        /// Get the item from the list at the specified index
        /// List[ Index ]
        /// </summary>
        private IndexedMember<AlertListType, int> _list;

        /// <summary>
        /// Get the item from the list at the specified index
        /// List[ Index ]
        /// </summary>
        /// <param name="index">The base 0 index.</param>
        /// <returns></returns>
        public AlertListType GetList(int index) => _list[index];

        public IEnumerable<AlertListType> Lists
        {
            get
            {
                var count = Size ?? 0;
                List<AlertListType> items = new List<AlertListType>((int)count);

                for (int i = 0; i < count; i++)
                {
                    items.Add(GetList(i));
                }

                return items;
            }
        }

        /// <summary>
        /// Get the number of alerts
        /// </summary>
        public uint? Size => GetMember<IntType>("Size");

        /// <summary>
        /// Returns Size as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}