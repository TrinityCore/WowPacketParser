using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum GroupTypeFlag : byte
    {
        Normal              = 0x00,
        Battleground        = 0x01,
        Raid                = 0x02,
        LFGRestricted       = 0x04,
        LookingForDungeon   = 0x08,
        OnePersonParty      = 0x10,
        Unk20               = 0x20,
        IsEveryoneAssistant = 0x40
    }
}
