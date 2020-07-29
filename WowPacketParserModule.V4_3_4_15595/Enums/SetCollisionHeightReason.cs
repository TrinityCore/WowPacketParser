using System;

namespace WowPacketParserModule.V4_3_4_15595.Enums
{
    [Flags]
    public enum SetCollisionHeightReason : uint
    {
        Mounted         = 0x0,
        ScaleChanged    = 0x1
    }
}