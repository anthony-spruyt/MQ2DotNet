namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// TODO: new data type
    /// Represents a single objective of an achievement
    /// https://docs.macroquest.org/reference/data-types/datatype-achievementobj/
    /// </summary>
    [MQ2Type("achievementobj")]
    public class AchievementObjType : MQ2DataType
    {
        public AchievementObjType(MQ2TypeFactory typeFactory, MQ2TypeVar typeVar) : base(typeFactory, typeVar)
        {
        }

        public override string ToString()
        {
            return OriginalToString();
        }
    }
}
