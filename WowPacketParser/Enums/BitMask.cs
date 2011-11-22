using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum BitMask
    {
        Byte4 = 0x000001,
        Byte0 = 0x000002,
        Byte1 = 0x000004,
        Byte2 = 0x000008,
        Byte6 = 0x000010,
        Byte3 = 0x000020,
        Byte7 = 0x000040,
        Byte5 = 0x000080,
    }
}

