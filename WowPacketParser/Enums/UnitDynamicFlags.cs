using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum UnitDynamicFlags
    {
        None                  = 0x0000,
        Lootable              = 0x0001,
        TrackUnit             = 0x0002,
        Tapped                = 0x0004,
        TappedByPlayer        = 0x0008,
        EmpathyInfo           = 0x0010,
        AppearDead            = 0x0020,
        ReferAFriendLinked    = 0x0040,
        TappedByAllThreatList = 0x0080
    }

    [Flags]
    public enum UnitDynamicFlagsWOD
    {
        None                  = 0x0000,
        HideModel             = 0x0001,
        Lootable              = 0x0002,
        TrackUnit             = 0x0004,
        Tapped                = 0x0008,
        TappedByPlayer        = 0x0010,
        EmpathyInfo           = 0x0020,
        AppearDead            = 0x0040,
        ReferAFriendLinked    = 0x0080,
        TappedByAllThreatList = 0x0100
    }
}
