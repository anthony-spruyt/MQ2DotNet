using JetBrains.Annotations;

namespace MQ2DotNet.EQ
{
    // /*0x00*/ ARMOR Head;
    // /*0x14*/ ARMOR Chest;
    // /*0x28*/ ARMOR Arms;
    // /*0x3c*/ ARMOR Wrists;
    // /*0x50*/ ARMOR Hands;
    // /*0x64*/ ARMOR Legs;
    // /*0x78*/ ARMOR Feet;
    // /*0x8c*/ ARMOR Primary;
    // /*0xa0*/ ARMOR Offhand;

    /// <summary>
    /// 
    /// </summary>
    [PublicAPI]
    public enum EquipmentSlot
    {
        Head = 0,
        Chest = 1,
        Arms = 2,
        Wrists = 3,
        Hands = 4,
        Legs = 5,
        Feet = 6,
        Primary = 7,
        Offhand = 8
    }
}
