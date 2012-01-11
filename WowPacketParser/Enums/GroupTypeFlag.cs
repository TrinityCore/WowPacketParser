using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum GroupTypeFlag
    {
        Normal            = 0x00,
        Battleground      = 0x01,
        Raid              = 0x02,
        Unk4              = 0x04,
        LookingForDungeon = 0x08,
        Unk10             = 0x10,
        Unk40             = 0x40
    }
}
