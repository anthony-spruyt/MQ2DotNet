using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for a task.
    /// Last Verified: 2023-06-27
    /// </summary>
    [PublicAPI]
    [MQ2Type("task")]
    public class TaskType : MQ2DataType
    {
        public const int MAX_TASK_ELEMENTS = 20;

        internal TaskType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            _member = new IndexedMember<TaskMemberType, string, TaskMemberType, int>(this, "Member");
            _objective = new IndexedMember<TaskObjectiveType, string, TaskObjectiveType, int>(this, "Objective");
        }

        /// <summary>
        /// Select the task in the task window
        /// </summary>
        /// <returns>True if success</returns>
        public bool Select() => GetMember<BoolType>("Select");

        /// <summary>
        /// Type of task, either Shared, Quest or Unknown
        /// TODO: map to an enum.
        /// </summary>
        public string Type => GetMember<StringType>("Type");
        
        /// <summary>
        /// Index of the task in your task list, 1 based
        /// </summary>
        public int? Index => GetMember<IntType>("Index");
        
        /// <summary>
        /// The leader of the task
        /// </summary>
        public TaskMemberType Leader => GetMember<TaskMemberType>("Leader");
        
        /// <summary>
        /// Name/title of the task
        /// </summary>
        public string Title => GetMember<StringType>("Title");
        
        /// <summary>
        /// Time remaining on the task
        /// </summary>
        public TimeSpan? Timer => GetMember<TimeStampType>("Timer");

        /// <summary>
        /// Member of the task, by name or index (1 based)
        /// </summary>
        private IndexedMember<TaskMemberType, string, TaskMemberType, int> _member;

        /// <summary>
        /// Get a task member.
        /// </summary>
        /// <param name="name">The task name.</param>
        /// <returns></returns>
        public TaskMemberType GetTaskMember(string name) => _member[name];

        /// <summary>
        /// Get a task member.
        /// </summary>
        /// <param name="index">The 1 based index.</param>
        /// <returns></returns>
        public TaskMemberType GetTaskMember(int index) => _member[index];

        /// <summary>
        /// All task members.
        /// </summary>
        public  IEnumerable<TaskMemberType> TaskMembers
        {
            get
            {
                var items = new List<TaskMemberType>();
                var index = 1;
                var count = Members;

                while (count.HasValue)
                {
                    var item = GetTaskMember(index);

                    if (item != null && index <= count)
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

        /// <summary>
        /// Number of members
        /// </summary>
        public uint? Members => GetMember<IntType>("Members");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public int? ID => GetMember<IntType>("ID");

        /// <summary>
        /// Task objective by name or index (1 based)
        /// </summary>
        public IndexedMember<TaskObjectiveType, string, TaskObjectiveType, int> _objective;

        /// <summary>
        /// Get a task objective.
        /// </summary>
        /// <param name="name">The task objective name.</param>
        /// <returns></returns>
        public TaskObjectiveType GetTaskObjective(string name) => _objective[name];

        /// <summary>
        /// Get a task objective.
        /// </summary>
        /// <param name="index">The 1 based index.</param>
        /// <returns></returns>
        public TaskObjectiveType GetTaskObjective(int index) => _objective[index];

        /// <summary>
        /// All task objective.
        /// </summary>
        public IEnumerable<TaskObjectiveType> TaskObjectives
        {
            get
            {
                var items = new List<TaskObjectiveType>();
                var index = 1;

                while (true)
                {
                    var item = GetTaskObjective(index);

                    if (item != null && index <= MAX_TASK_ELEMENTS)
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

        /// <summary>
        /// First task objective with a status other than "Done"
        /// </summary>
        public TaskObjectiveType Step => GetMember<TaskObjectiveType>("Step");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public uint? WindowIndex => GetMember<IntType>("WindowIndex");
    }
}