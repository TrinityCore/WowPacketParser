using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum ChannelMemberFlag
    {
        None      = 0x00,
        Owner     = 0x01,
        Moderator = 0x02,
        Voiced    = 0x04,
        Muted     = 0x08,
        Custom    = 0x10,
        MicMuted  = 0x20,
        Unknown1  = 0x40,
        Unknown2  = 0x80
    }
}
