using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum LfgUpdateFlag
    {
        None = 0x00,
        CharacterInfo = 0x01,
        Comment = 0x02,
        Unknown1 = 0x04,
        Guid = 0x08,
        Roles = 0x10,
        Unknown2 = 0x20,
        Unknown3 = 0x40,
        Unknown4 = 0x80
    }
}
