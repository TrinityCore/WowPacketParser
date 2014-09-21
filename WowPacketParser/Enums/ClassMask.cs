using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum ClassMask
    {
        None        = 0x0000,
        Warrior     = 0x0001,
        Paladin     = 0x0002,
        Hunter      = 0x0004,
        Rogue       = 0x0008,
        Priest      = 0x0010,
        DeathKnight = 0x0020,
        Shaman      = 0x0040,
        Mage        = 0x0080,
        Warlock     = 0x0100,
        Monk        = 0x0200,
        Druid       = 0x0400
    }
}
