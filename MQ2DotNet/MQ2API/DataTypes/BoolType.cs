using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// A Boolean expression is one that has just two possible outcomes: 1 (TRUE) and 0 (FALSE). Technically TRUE doesn't have to be 1, but it's always treated that way.
    /// Last Verified: 2023-07-02
    /// https://docs.macroquest.org/reference/data-types/datatype-bool/
    /// </summary>
    [PublicAPI]
    [MQ2Type("bool")]
    public class BoolType : MQ2DataType
    {
        internal BoolType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
        }

        /// <summary>
        /// Implicit conversion to a bool
        /// </summary>
        /// <param name="typeVar"></param>
        public static implicit operator bool(BoolType typeVar)
        {
            // A null will be converted to a false
            // GroundType::LineOfSight -> Int
            // Pretty much everything else uses the Dword prop.
            return typeVar?.VarPtr.Dword != 0 || typeVar?.VarPtr.Int != 0;
        }

        /// <summary>
        /// Implicit conversion to a nullable bool
        /// </summary>
        /// <param name="typeVar"></param>
        public static implicit operator bool?(BoolType typeVar)
        {
            
            if (typeVar == null || typeVar.VarPtr == null)
            {
                return null;
            }

            // GroundType::LineOfSight -> Int
            // Pretty much everything else uses the Dword prop.
            return typeVar?.VarPtr.Dword != 0 || typeVar?.VarPtr.Int != 0;
        }

        /// <summary>
        /// "TRUE" for non-zero, or "FALSE" for zero
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}