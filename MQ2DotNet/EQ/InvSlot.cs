using JetBrains.Annotations;

namespace MQ2DotNet.EQ
{
    /// <summary>
    /// Pre <see cref="Expansion.TOL"/> item slot numbers.
    /// </summary>
    /// <remarks>Constants.h</remarks>
    [PublicAPI]
    public enum InvSlot
    {
        InvSlot_Charm = 0,
        InvSlot_LeftEar,
        InvSlot_Head,
        InvSlot_Face,
        InvSlot_RightEar,
        InvSlot_Neck,
        InvSlot_Shoulders,
        InvSlot_Arms,
        InvSlot_Back,
        InvSlot_LeftWrist,
        InvSlot_RightWrist,
        InvSlot_Range,
        InvSlot_Hands,
        InvSlot_Primary,
        InvSlot_Secondary,
        InvSlot_LeftFingers,
        InvSlot_RightFingers,
        InvSlot_Chest,
        InvSlot_Legs,
        InvSlot_Feet,
        InvSlot_Waist,
        InvSlot_PowerSource,
        InvSlot_Ammo,
        InvSlot_Bag1,
        InvSlot_Bag2,
        InvSlot_Bag3,
        InvSlot_Bag4,
        InvSlot_Bag5,
        InvSlot_Bag6,
        InvSlot_Bag7,
        InvSlot_Bag8,
        InvSlot_Bag9,
        InvSlot_Bag10,
        InvSlot_Held,
        InvSlot_Max,
        InvSlot_FirstWornItem = InvSlot_Charm,
        InvSlot_LastWornItem = InvSlot_Ammo,
        InvSlot_FirstBagSlot = InvSlot_Bag1,
        InvSlot_LastBagSlot = InvSlot_Bag10,
        InvSlot_LastBonusBagSlot = InvSlot_Bag10,
        InvSlot_Cursor = InvSlot_Held,
        InvSlot_NumInvSlots = InvSlot_Held // held is not technically an item in the inventory (its the cursor)
    }

    /// <summary>
    /// Post <see cref="Expansion.TOL"/> item slot numbers.
    /// </summary>
    /// <remarks>Constants.h</remarks>
    [PublicAPI]
    public enum InvSlot2
    {
        InvSlot_Charm = 0,
        InvSlot_LeftEar,
        InvSlot_Head,
        InvSlot_Face,
        InvSlot_RightEar,
        InvSlot_Neck,
        InvSlot_Shoulders,
        InvSlot_Arms,
        InvSlot_Back,
        InvSlot_LeftWrist,
        InvSlot_RightWrist,
        InvSlot_Range,
        InvSlot_Hands,
        InvSlot_Primary,
        InvSlot_Secondary,
        InvSlot_LeftFingers,
        InvSlot_RightFingers,
        InvSlot_Chest,
        InvSlot_Legs,
        InvSlot_Feet,
        InvSlot_Waist,
        InvSlot_PowerSource,
        InvSlot_Ammo,
        InvSlot_Bag1,
        InvSlot_Bag2,
        InvSlot_Bag3,
        InvSlot_Bag4,
        InvSlot_Bag5,
        InvSlot_Bag6,
        InvSlot_Bag7,
        InvSlot_Bag8,
        InvSlot_Bag9,
        InvSlot_Bag10,
        InvSlot_Bag11,
        InvSlot_Bag12,
        InvSlot_Held,
        InvSlot_Max,
        InvSlot_FirstWornItem = InvSlot_Charm,
        InvSlot_LastWornItem = InvSlot_Ammo,
        InvSlot_FirstBagSlot = InvSlot_Bag1,
        InvSlot_LastBagSlot = InvSlot_Bag10,
        InvSlot_LastBonusBagSlot = InvSlot_Bag12,
        InvSlot_Cursor = InvSlot_Held,
        InvSlot_NumInvSlots = InvSlot_Held // held is not technically an item in the inventory (its the cursor)
    }
}
