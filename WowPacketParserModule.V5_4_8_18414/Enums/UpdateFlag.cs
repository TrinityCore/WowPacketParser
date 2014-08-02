using System;

namespace WowPacketParserModule.V5_4_8_18414.Enums
{
    [Flags]
    enum UpdateFlag
    {
        NONE                  = 0x0000,
        SELF                  = 0x0001,
        TRANSPORT             = 0x0002,
        HAS_TARGET            = 0x0004,
        UNKNOWN               = 0x0008,
        LOWGUID               = 0x0010,
        LIVING                = 0x0020,
        STATIONARY_POSITION   = 0x0040,
        VEHICLE               = 0x0080,
        GO_TRANSPORT_POSITION = 0x0100,
        ROTATION              = 0x0200,
        UNK3                  = 0x0400,
        ANIMKITS              = 0x0800,
        UNK5                  = 0x1000,
        UNK6                  = 0x2000,
    }
}
