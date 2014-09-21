using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum FactionFlag
    {
        None       = 0x00,
        Visible    = 0x01,
        AtWar      = 0x02,
        Hidden     = 0x04,
        Inivisible = 0x08,
        Peace      = 0x10,
        Inactive   = 0x20,
        Rival      = 0x40
    }
}
