using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum RaceMask : ulong
    {
        None                = 0x000000000,
        Human               = 0x000000001,
        Orc                 = 0x000000002,
        Dwarf               = 0x000000004,
        NightElf            = 0x000000008,
        Undead              = 0x000000010,
        Tauren              = 0x000000020,
        Gnome               = 0x000000040,
        Troll               = 0x000000080,
        Goblin              = 0x000000100,
        BloodElf            = 0x000000200,
        Draenei             = 0x000000400,
        FelOrc              = 0x000000800,
        Naga                = 0x000001000,
        Broken              = 0x000002000,
        Skeleton            = 0x000004000,
        Vrykul              = 0x000008000,
        Tuskarr             = 0x000010000,
        ForestTroll         = 0x000020000,
        Taunka              = 0x000040000,
        NorthrendSkeleton   = 0x000080000,
        IceTroll            = 0x000100000,
        Worgen              = 0x000200000,
        Gilnean             = 0x000400000, // Human
        PandarenNeutral     = 0x000800000,
        PandarenAlliance    = 0x001000000,
        PandarenHorde       = 0x002000000,
        Nightborne          = 0x004000000,
        HighmountainTauren  = 0x008000000,
        VoidElf             = 0x010000000,
        LightforgedDraenei  = 0x020000000,
        ZandalariTroll      = 0x040000000,
        KulTiran            = 0x080000000,
        ThinHuman           = 0x100000000,
        DarkIronDwarf       = 0x200000000,
        Vulpera             = 0x400000000,
        MagharOrc           = 0x800000000
    }
}
