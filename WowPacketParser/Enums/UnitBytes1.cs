using System;

namespace WowPacketParser.Enums
{
    public enum UnitStandStateType
    {
        UNIT_STAND_STATE_STAND            = 0,
        UNIT_STAND_STATE_SIT              = 1,
        UNIT_STAND_STATE_SIT_CHAIR        = 2,
        UNIT_STAND_STATE_SLEEP            = 3,
        UNIT_STAND_STATE_SIT_LOW_CHAIR    = 4,
        UNIT_STAND_STATE_SIT_MEDIUM_CHAIR = 5,
        UNIT_STAND_STATE_SIT_HIGH_CHAIR   = 6,
        UNIT_STAND_STATE_DEAD             = 7,
        UNIT_STAND_STATE_KNEEL            = 8,
        UNIT_STAND_STATE_SUBMERGED        = 9
    }

    [Flags]
    public enum UnitStandFlags
    {
        UNIT_STAND_FLAGS_UNK1        = 0x01,
        UNIT_STAND_FLAGS_CREEP       = 0x02,
        UNIT_STAND_FLAGS_UNTRACKABLE = 0x04,
        UNIT_STAND_FLAGS_UNK4        = 0x08,
        UNIT_STAND_FLAGS_UNK5        = 0x10,
        UNIT_STAND_FLAGS_ALL         = 0xFF
    };

    [Flags]
    enum UnitBytes1Flags
    {
        UNIT_BYTE1_FLAG_ALWAYS_STAND = 0x01,
        UNIT_BYTE1_FLAG_HOVER = 0x02,
        UNIT_BYTE1_FLAG_UNK_3 = 0x04,
        UNIT_BYTE1_FLAG_ALL = 0xFF
    };
}
