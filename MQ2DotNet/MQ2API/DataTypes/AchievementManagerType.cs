using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// TODO: new TLO
    /// Provides access achievements, achievement categories, and other information surrounding the achievement system.
    /// https://docs.macroquest.org/reference/top-level-objects/tlo-achievement/#achievementmgr-type
    /// </summary>
    [PublicAPI]
    [MQ2Type("achievementmgr")]
    public class AchievementManagerType : MQ2DataType
    {
        public AchievementManagerType(MQ2TypeFactory typeFactory, MQ2TypeVar typeVar) : base(typeFactory, typeVar)
        {
        }

        protected AchievementManagerType(string typeName, MQ2TypeFactory typeFactory, MQ2VarPtr varPtr) : base(typeName, typeFactory, varPtr)
        {
        }

        public override string ToString()
        {
            return OriginalToString();
        }
    }
}
