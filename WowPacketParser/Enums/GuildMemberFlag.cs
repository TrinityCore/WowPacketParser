using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum GuildMemberFlag // 4.x
    {
        None   = 0x00,
        Online = 0x01,
        AFK    = 0x02,
        DND    = 0x04,
        Mobile = 0x08
    }
}

