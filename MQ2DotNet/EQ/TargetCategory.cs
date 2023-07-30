using System.Runtime.Serialization;

namespace MQ2DotNet.EQ
{
    /// <summary>
    /// Target type:
    /// {Target_AE_No_Players_Pets}
    /// {Single Friendly (or Target's Target}
    /// {Pet Owner}
    /// {Target of Target}
    /// {Free Target}
    /// {Beam}
    /// {Single in Group}
    /// {Directional AE}
    /// {Group v2}
    /// {AE PC v2}
    /// {No Pets}
    /// {Pet2}
    /// {Caster PB NPC}
    /// {Caster PB PC}
    /// {Special Muramites}
    /// {Chest}
    /// {Hatelist2}
    /// {Hatelist}
    /// {AE Summoned}
    /// {AE Undead}
    /// {Targeted AE Tap}
    /// {Uber Dragons}
    /// {Uber Giants}
    /// {Plant}
    /// {Corpse}
    /// {Pet}
    /// {LifeTap}
    /// {Summoned}
    /// {Undead}
    /// {Animal}
    /// {Targeted AE}
    /// {Self}
    /// {Single}
    /// {PB AE}
    /// {Group v1}
    /// {AE PC v1}
    /// {Line of Sight}
    /// {None}
    /// {Unknown}
    /// TODO: convert to enum
    /// </summary>
    public enum TargetCategory
    {
        [EnumMember(Value = "Target_AE_No_Players_Pets")]
        Target_AE_No_Players_Pets,
        [EnumMember(Value = "Single Friendly (or Target's Target")]
        Single_Friendly_or_Targets_Target,
        [EnumMember(Value = "Pet Owner")]
        Pet_Owner,
        [EnumMember(Value = "Target of Target")]
        Target_of_Target,
        [EnumMember(Value = "Free Target")]
        Free_Target,
        [EnumMember(Value = "Beam")]
        Beam,
        [EnumMember(Value = "Single in Group")]
        Single_in_Group,
        [EnumMember(Value = "Directional AE")]
        Directional_AE,
        [EnumMember(Value = "Group v2")]
        Group_v2,
        [EnumMember(Value = "AE PC v2")]
        AE_PC_v2,
        [EnumMember(Value = "No Pets")]
        No_Pets,
        [EnumMember(Value = "Pet2")]
        Pet2,
        [EnumMember(Value = "Caster PB NPC")]
        Caster_PB_NPC,
        [EnumMember(Value = "Caster PB PC")]
        Caster_PB_PC,
        [EnumMember(Value = "Special Muramites")]
        Special_Muramites,
        [EnumMember(Value = "Chest")]
        Chest,
        [EnumMember(Value = "Hatelist2")]
        Hatelist2,
        [EnumMember(Value = "Hatelist")]
        Hatelist,
        [EnumMember(Value = "AE Summoned")]
        AE_Summoned,
        [EnumMember(Value = "AE Undead")]
        AE_Undead,
        [EnumMember(Value = "Targeted AE Tap")]
        Targeted_AE_Tap,
        [EnumMember(Value = "Uber Dragons")]
        Uber_Dragons,
        [EnumMember(Value = "Uber Giants")]
        Uber_Giants,
        [EnumMember(Value = "Plant")]
        Plant,
        [EnumMember(Value = "Corpse")]
        Corpse,
        [EnumMember(Value = "Pet")]
        Pet,
        [EnumMember(Value = "LifeTap")]
        LifeTap,
        [EnumMember(Value = "Summoned")]
        Summoned,
        [EnumMember(Value = "Undead")]
        Undead,
        [EnumMember(Value = "Animal")]
        Animal,
        [EnumMember(Value = "Targeted AE")]
        Targeted_AE,
        [EnumMember(Value = "Self")]
        Self,
        [EnumMember(Value = "Single")]
        Single,
        [EnumMember(Value = "PB AE")]
        PB_AE,
        [EnumMember(Value = "Group v1")]
        Group_v1,
        [EnumMember(Value = "AE PC v1")]
        AE_PC_v1,
        [EnumMember(Value = "Line of Sight")]
        Line_of_Sight,
        [EnumMember(Value = "None")]
        None,
        [EnumMember(Value = "Unknown")]
        Unknown,
    }
}
