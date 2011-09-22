using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum CalendarFlag
    {
        None = 0x000,
        Normal = 0x001,
        InvitesLocked = 0x010,
        WithoutInvites = 0x040,
        GuildEvent = 0x400,
    }
}
