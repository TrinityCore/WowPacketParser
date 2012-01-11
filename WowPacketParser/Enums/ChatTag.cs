using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum ChatTag
    {
        None  = 0x00,
        Afk   = 0x01,
        Dnd   = 0x02,
        Gm    = 0x04,
        Com   = 0x08,
        Dev   = 0x10,
        Unk40 = 0x40
    }
}
