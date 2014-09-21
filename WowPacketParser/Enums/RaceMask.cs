using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum RaceMask
    {
        None              = 0x00000000,
        Human             = 0x00000001,
        Orc               = 0x00000002,
        Dwarf             = 0x00000004,
        NightElf          = 0x00000008,
        Undead            = 0x00000010,
        Tauren            = 0x00000020,
        Gnome             = 0x00000040,
        Troll             = 0x00000080,
        Goblin            = 0x00000100,
        BloodElf          = 0x00000200,
        Draenei           = 0x00000400,
        FelOrc            = 0x00000800,
        Naga              = 0x00001000,
        Broken            = 0x00002000,
        Skeleton          = 0x00004000,
        Vrykul            = 0x00008000,
        Tuskarr           = 0x00010000,
        ForestTroll       = 0x00020000,
        Taunka            = 0x00040000,
        NorthrendSkeleton = 0x00080000,
        IceTroll          = 0x00100000,
        Worgen            = 0x00200000,
        Gilnean           = 0x00400000, // Human
        PandarenNeutral   = 0x00800000,
        PandarenAlliance  = 0x01000000,
        PandarenHorde     = 0x02000000
    }
}
