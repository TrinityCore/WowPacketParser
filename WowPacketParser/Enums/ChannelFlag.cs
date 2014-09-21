using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum ChannelFlag
    {
        None     = 0x00,
        Custom   = 0x01,
        Unknown1 = 0x02,
        Trade    = 0x04,
        NotLfg   = 0x08,
        General  = 0x10,
        City     = 0x20,
        Lfg      = 0x40,
        Voice    = 0x80
    }
}
