using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum CreatureTypeFlag
    {
        None = 0x00000000,
        Tameable = 0x00000001,
        GhostVisible = 0x00000002,
        Boss = 0x00000004,
        Unknown2 = 0x00000008,
        Unknown3 = 0x00000010,
        Unknown4 = 0x00000020,
        SpellAttackable = 0x00000040,
        Unknown6 = 0x00000080,
        HerbLoot = 0x00000100,
        MiningLoot = 0x00000200,
        Unknown7 = 0x00000400,
        CastWhileMounted = 0x000800,
        CanAssist = 0x00001000,
        HasPetActionBar = 0x00002000,
        Unknown11 = 0x00004000,
        EngineerLoot = 0x00008000,
        Exotic = 0x00010000,
        Unknown12 = 0x00020000,
        IsSiegeWeapon = 0x00040000,
        Unknown14 = 0x00080000,
        Unknown15 = 0x00100000,
        DisableAnimation = 0x00200000,
        Unknown17 = 0x00400000,
        Unknown18 = 0x00800000,
        Unknown19 = 0x01000000,
        Unknown20 = 0x02000000,
        NoSpecificType = 0x04000000,
        Unknown22 = 0x08000000,
        Unknown23 = 0x10000000
    }
}
