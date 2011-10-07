using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum ItemFlag : uint
    {
        None                 = 0x00000000,
        Bound                = 0x00000001,
        Conjured             = 0x00000002,
        Openable             = 0x00000004,
        Heroic               = 0x00000008,
        Broken               = 0x00000010,
        Indestructible       = 0x00000020,
        Usable               = 0x00000040,
        NoEquipCooldown      = 0x00000080,
        Wrapper              = 0x00000200,
        IgnoreBagSpace       = 0x00000400,
        PartyLoot            = 0x00000800,
        Refundable           = 0x00001000,
        Charter              = 0x00002000,
        Unknown8000          = 0x00008000,
        Unknown10000         = 0x00010000,
        Prospectable         = 0x00040000,
        UniqueEquipped       = 0x00080000,
        UsableInArena        = 0x00200000,
        Throwable            = 0x00400000,
        SpecialUse           = 0x00800000,
        SmartLoot            = 0x02000000, // Need to have profesion and recipe not learned
        NotUsableInArena     = 0x04000000,
        AccountBound         = 0x08000000,
        EnchantmentScroll    = 0x10000000,
        Millable             = 0x20000000,
        BindOnPickUpTradable = 0x80000000
    }
}
