using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum CalendarFlag
    {
        None           = 0x000000,
        Normal         = 0x000001,
        InvitesLocked  = 0x000010,
        WithoutInvites = 0x000040,
        GuildEvent     = 0x000400,
        Unk10000       = 0x010000,
        Unk400000      = 0x400000
    }
}
