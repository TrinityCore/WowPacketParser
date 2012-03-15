using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum UnitDynamicFlags // 4.x
    {
        None = 0x0000,
        Lootable = 0x0001,
        TrackUnit = 0x0002,
        Tapped = 0x0004,
        TappedByPlayer = 0x0008,
        EmpathyInfo = 0x0010,
        AppearDead = 0x0020,
        ReferAFriendLinked = 0x0040,
        TappedByAllThreatList = 0x0080
    }
}
