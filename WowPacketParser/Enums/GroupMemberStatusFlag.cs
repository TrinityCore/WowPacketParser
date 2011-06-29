using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum GroupMemberStatusFlag
    {
        Offline = 0x0000,
        Online = 0x0001,
        Pvp = 0x0002,
        Dead = 0x0004,
        Ghost = 0x0008,
        Unknown1 = 0x0010,
        Unknown2 = 0x0020,
        Unknown3 = 0x0040,
        Unknown4 = 0x0080
    }
}
