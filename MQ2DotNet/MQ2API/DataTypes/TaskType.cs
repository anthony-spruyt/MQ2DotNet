using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// This is the type for your current task.
    /// Last Verified: 2023-07-02
    /// https://docs.macroquest.org/reference/data-types/datatype-task/
    /// </summary>
    [PublicAPI]
    [MQ2Type("task")]
    public class TaskType : MQ2DataType
    {
        public const int MAX_TASK_OBJECTIVES = 20;
        public const int MAX_TASKS = 29;
        public const int MAX_SHARED_TASKS = 1;

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
        /// Returns a string that can be one of the following:
        /// - Unknown
        /// - None
        /// - Deliver
        /// - Kill
        /// - Loot
        /// - Hail
        /// - Explore
        /// - Tradeskill
        /// - Fishing
        /// - Foraging
        /// - Cast
        /// - UseSkill
        /// - DZSwitch
        /// - DestroyObject
        /// - Collect
        /// - Dialogue
        /// TODO: map to an enum.
        /// </summary>
        public string Type => GetMember<StringType>("Type");

        /// <summary>
        /// Returns the task's place (base 1) on the tasklist.
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
        /// Returns specified member in task by name.
        /// </summary>
        /// <param name="name">The task name.</param>
        /// <returns></returns>
        public TaskMemberType GetTaskMember(string name) => _member[name];

        /// <summary>
        /// Returns specified member in task by index.
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
                var index = 1;
                var count = Members;

                while (count.HasValue && index <= count)
                {
                    var item = GetTaskMember(index);

                    if (item != null)
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
        /// Number of members
        /// </summary>
        public uint? Members => GetMember<IntType>("Members");

        /// <summary>
        /// Returns an int of the task ID.
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
                var index = 1;

                while (index <= MAX_TASK_OBJECTIVES)
                {
                    var item = GetTaskObjective(index);

                    if (item != null)
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
        /// First task objective with a status other than "Done"
        /// </summary>
        public TaskObjectiveType Step => GetMember<TaskObjectiveType>("Step");

        /// <summary>
        /// Returns the Quest Window List Index. (if the window actually has the list filled.
        /// </summary>
        public uint? WindowIndex => GetMember<IntType>("WindowIndex");
    }
}