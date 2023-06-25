using JetBrains.Annotations;

namespace MQ2DotNet.EQ
{
    [PublicAPI]
    public enum SpawnType : uint
    {
        NONE = 0U,
        PC = 1U,
        MOUNT = 2U,
        PET = 3U,
        PCPET = 4U,
        NPCPET = 5U,
        XTARHATER = 6U,
        NPC = 7U,
        CORPSE = 8U,
        TRIGGER = 9U,
        TRAP = 10U,
        TIMER = 11U,
        UNTARGETABLE = 12U,
        CHEST = 13U,
        ITEM = 14U,
        AURA = 15U,
        OBJECT = 16U,
        BANNER = 17U,
        CAMPFIRE = 18U,
        MERCENARY = 19U,
        FLYER = 20U,
        NPCCORPSE = 2000U,
        PCCORPSE = 20001U,
    }
}
