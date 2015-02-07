using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum ContactEntryFlag
    {
        None           = 0x00,
        Friend         = 0x01,
        Ignored        = 0x02,
        Muted          = 0x04,
        RecruitAFriend = 0x08
    }
}
