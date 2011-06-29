using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum GroupTypeFlag
    {
        Normal = 0x00,
        Battleground = 0x01,
        Raid = 0x02,
        Unknown1 = 0x04,
        LookingForDungeon = 0x08,
        Unknown2 = 0x10
    }
}
