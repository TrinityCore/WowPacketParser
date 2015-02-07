using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum HighGuidMask
    {
        None    = 0x00,
        Flag01  = 0x01,
        Flag02  = 0x02,
        Flag04  = 0x04,
        Flag08  = 0x08,
        Flag10  = 0x10,
        Flag20  = 0x20,
        Flag40  = 0x40,
        Flag80  = 0x80,
        Flag100 = 0x100
    }
}
