using JetBrains.Annotations;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Data related to switches (levers, buttons, etc) in the zone
    /// Last Verified: 2023-07-02
    /// https://docs.macroquest.org/reference/data-types/datatype-switch/
    /// </summary>
    [PublicAPI]
    [MQ2Type("switch")]
    public class SwitchType : MQ2DataType
    {
        internal SwitchType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            Name = GetMember<StringType>("Name");
        }

        /// <summary>
        /// Toggle the switch, equivalent of clicking on it. Uses an item if you have it on the cursor.
        /// </summary>
        public void Toggle() => GetMember<MQ2DataType>("Toggle");

        /// <summary>
        /// Alias for <see cref="Toggle"/>
        /// </summary>
        public void Use() => Toggle();

        /// <summary>
        /// Target the switch.
        /// </summary>
        public void Target() => GetMember<MQ2DataType>("Target");

        /// <summary>
        /// Switch ID
        /// </summary>
        public uint? ID => GetMember<IntType>("ID");
        
        /// <summary>
        /// X coordinate (Westward-positive)
        /// </summary>
        public float? X => GetMember<FloatType>("X");

        /// <summary>
        /// X coordinate (Westward-positive)
        /// </summary>
        public float? W => X;

        /// <summary>
        /// Y coordinate (Northward-positive)
        /// </summary>
        public float? Y => GetMember<FloatType>("Y");

        /// <summary>
        /// Y coordinate (Northward-positive)
        /// </summary>
        public float? N => Y;

        /// <summary>
        /// Z coordinate (Upward-positive)
        /// </summary>
        public float? Z => GetMember<FloatType>("Z");

        /// <summary>
        /// D coordinate (Upward-positive)
        /// </summary>
        public float? D => Z;

        /// <summary>
        /// X coordinate of "closed" switch (Westward-positive)
        /// </summary>
        public float? DefaultX => GetMember<FloatType>("DefaultX");

        /// <summary>
        /// X coordinate of "closed" switch (Westward-positive)
        /// </summary>
        public float? DefaultW => DefaultX;

        /// <summary>
        /// Y coordinate of "closed" switch (Northward-positive)
        /// </summary>
        public float? DefaultY => GetMember<FloatType>("DefaultY");

        /// <summary>
        /// Y coordinate of "closed" switch (Northward-positive)
        /// </summary>
        public float? DefaultN => DefaultY;

        /// <summary>
        /// Z coordinate of "closed" switch (Upward-positive)
        /// </summary>
        public float? DefaultZ => GetMember<FloatType>("DefaultZ");

        /// <summary>
        /// Z coordinate of "closed" switch (Upward-positive)
        /// </summary>
        public float? DefaultU => DefaultZ;

        /// <summary>
        /// Switch is facing this heading
        /// </summary>
        public HeadingType Heading => GetMember<HeadingType>("Heading");

        /// <summary>
        /// Heading of "closed" switch
        /// </summary>
        public HeadingType DefaultHeading => GetMember<HeadingType>("DefaultHeading");

        /// <summary>
        /// True if the switch is in the "open" state (<see cref="State"/> == 1)
        /// </summary>
        public bool Open => GetMember<BoolType>("Open");
        
        /// <summary>
        /// Direction player must move to meet this switch
        /// </summary>
        public HeadingType HeadingTo => GetMember<HeadingType>("HeadingTo");
        
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// 2D distance from player to this switch in the XY plane
        /// </summary>
        public float? Distance => GetMember<FloatType>("Distance");
        
        /// <summary>
        /// 3D distance from player to this switch
        /// </summary>
        public float? Distance3D => GetMember<FloatType>("Distance3D");
        
        /// <summary>
        /// Returns TRUE if the switch is in LoS
        /// </summary>
        public bool LineOfSight => GetMember<BoolType>("LineOfSight");

        /// <summary>
        /// Returns TRUE if the switch is targeted.
        /// </summary>
        public bool IsTargeted => GetMember<BoolType>("IsTargeted");

        /// <summary>
        /// The "state" of the switch.
        /// </summary>
        public uint? State => GetMember<IntType>("State");

        /// <summary>
        /// Same as <see cref="ID"/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}