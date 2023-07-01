using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// TODO: new data type
    /// Provides access to achievement categories. Achievements are organized hierarchically in the achievements window by categories.
    /// While not required to access achievements, categories may be useful for enumerating lists of achievements.
    /// https://docs.macroquest.org/reference/data-types/datatype-achievementcat/
    /// </summary>
    [PublicAPI]
    [MQ2Type("achievementcat")]
    public class AchievementCatType : MQ2DataType
    {
        public AchievementCatType(MQ2TypeFactory typeFactory, MQ2TypeVar typeVar) : base(typeFactory, typeVar)
        {
        }

        protected AchievementCatType(string typeName, MQ2TypeFactory typeFactory, MQ2VarPtr varPtr) : base(typeName, typeFactory, varPtr)
        {
        }

        public override string ToString()
        {
            return OriginalToString();
        }
    }
}
