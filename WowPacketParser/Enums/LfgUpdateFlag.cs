using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum LfgUpdateFlag
    {
        None          = 0x00,
        CharacterInfo = 0x01,
        Comment       = 0x02,
        GroupLeader   = 0x04,
        Guid          = 0x08,
        Roles         = 0x10,
        Area          = 0x20,
        Unknown7      = 0x40,
        Binded        = 0x80
    }
}
