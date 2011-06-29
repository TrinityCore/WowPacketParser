using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum ChatTag
    {
        None = 0x0,
        Afk = 0x1,
        Dnd = 0x2,
        Gm = 0x4,
        Unknown = 0x8
    }
}
