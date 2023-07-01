using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// TODO: new data type
    /// Represents a single objective of an achievement
    /// https://docs.macroquest.org/reference/data-types/datatype-achievementobj/
    /// </summary>
    [PublicAPI]
    [MQ2Type("achievementobj")]
    public class AchievementObjType : MQ2DataType
    {
        public AchievementObjType(MQ2TypeFactory typeFactory, MQ2TypeVar typeVar) : base(typeFactory, typeVar)
        {
        }

        protected AchievementObjType(string typeName, MQ2TypeFactory typeFactory, MQ2VarPtr varPtr) : base(typeName, typeFactory, varPtr)
        {
        }

        public override string ToString()
        {
            return OriginalToString();
        }
    }
}
