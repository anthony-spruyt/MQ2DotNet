using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a task objective.
    /// Last Verified: 2023-07-02
    /// This type does not exist in doco of task type child here -> https://docs.macroquest.org/reference/data-types/datatype-task/
    /// </summary>
    [PublicAPI]
    [MQ2Type("taskobjectivemember")]
    public class TaskObjectiveType : MQ2DataType
    {
        internal TaskObjectiveType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// Instruction text for this objective, as seen in the Quest Journal window
        /// Note that accessing this member will select the task
        /// </summary>
        public string Instruction => GetMember<StringType>("Instruction");

        /// <summary>
        /// Status text for the objective, e.g. 0/1 or Done, as seen in the Quest Journal window
        /// Note that accessing this member will select the task
        /// </summary>
        public string Status => GetMember<StringType>("Status");

        /// <summary>
        /// Zone for the objective, as seen in the Quest Journal window
        /// Note that accessing this member will select the task.
        /// TODO: this needs to be tested. It is a special case where a member can return different data types which makes this tricky.
        /// </summary>
        public bool AllZones
        {
            get
            {
                try
                {
                    return GetMember<StringType>("Zone") == "ALL";
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Zone for the objective, as seen in the Quest Journal window
        /// Note that accessing this member will select the task.
        /// TODO: this needs to be tested. It is a special case where a member can return different data types which makes this tricky.
        /// </summary>
        public ZoneType Zone
        {
            get
            {
                try
                {
                    return GetMember<ZoneType>("Zone");
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Index of this objective in the list (0 based)
        /// Confusingly, ${Task[taskname].Objective[1].Index} == 0
        /// </summary>
        public int? Index => GetMember<IntType>("Index");

        /// <summary>
        /// Unknown, None, Deliver, Kill, Loot, Hail, Explore, Tradeskill, Fishing, Foraging, Cast, UseSkill, DZSwitch, DestroyObject, Collect, Dialogue, NULL
        /// </summary>
        public string Type => GetMember<StringType>("Type");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public int? CurrentCount => GetMember<IntType>("CurrentCount");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public int? RequiredCount => GetMember<IntType>("RequiredCount");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public bool Optional => GetMember<BoolType>("Optional");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public string RequiredItem => GetMember<StringType>("RequiredItem");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public string RequiredSkill => GetMember<StringType>("RequiredSkill");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public string RequiredSpell => GetMember<StringType>("RequiredSpell");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? DZSwitchID => GetMember<IntType>("DZSwitchID");
    }
}