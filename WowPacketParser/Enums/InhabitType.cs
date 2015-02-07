using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum InhabitType
    {
        Ground   = 0x1,
        Water    = 0x2,
        Air      = 0x4,
        Anywhere = Ground | Water | Air
    }
}
