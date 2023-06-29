using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// TODO: new data type
    /// https://docs.macroquest.org/reference/data-types/datatype-achievement/
    /// </summary>
    [PublicAPI]
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
