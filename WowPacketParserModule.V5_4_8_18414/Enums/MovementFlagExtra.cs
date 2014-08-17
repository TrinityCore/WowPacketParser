using System;

namespace WowPacketParserModule.V5_4_8_18414.Enums
{
    [Flags]
    public enum MovementFlagExtra : ushort
    {
        NONE                     = 0x00000000,
        NO_STRAFE                = 0x00000001,
        NO_JUMPING               = 0x00000002,
        FULL_SPEED_TURNING       = 0x00000004,
        FULL_SPEED_PITCHING      = 0x00000008,
        ALWAYS_ALLOW_PITCHING    = 0x00000010,
        UNK5                     = 0x00000020,
        UNK6                     = 0x00000040,
        UNK7                     = 0x00000080,
        UNK8                     = 0x00000100,
        UNK9                     = 0x00000200,
        CAN_SWIM_TO_FLY_TRANS    = 0x00000400,
        UNK11                    = 0x00000800,
        UNK12                    = 0x00001000,
        INTERPOLATED_MOVEMENT    = 0x00002000,
        INTERPOLATED_TURNING     = 0x00004000,
        INTERPOLATED_PITCHING    = 0x00008000
    }
}
