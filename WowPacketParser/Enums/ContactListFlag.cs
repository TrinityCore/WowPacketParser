using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum ContactListFlag
    {
        None     = 0x00,
        Unknown1 = 0x01,
        Unknown2 = 0x02,
        Unknown3 = 0x04
    }
}
