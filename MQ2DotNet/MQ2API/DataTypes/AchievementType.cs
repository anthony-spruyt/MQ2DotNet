namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// TODO: new data type
    /// Provides the details about a single achievement and allows access to an achievement's objective.
    /// https://docs.macroquest.org/reference/data-types/datatype-achievement/
    /// </summary>
    [MQ2Type("achievement")]
    public class AchievementType : MQ2DataType
    {
        public AchievementType(MQ2TypeFactory typeFactory, MQ2TypeVar typeVar) : base(typeFactory, typeVar)
        {
        }

        protected AchievementType(string typeName, MQ2TypeFactory typeFactory, MQ2VarPtr varPtr) : base(typeName, typeFactory, varPtr)
        {
        }

        public override string ToString()
        {
            return OriginalToString();
        }
    }
}
