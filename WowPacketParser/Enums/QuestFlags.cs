using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum QuestFlags : uint
    {
        None          = 0x00000000,
        StayAlive     = 0x00000001,
        PartyAccept   = 0x00000002,
        Exploration   = 0x00000004,
        Sharable      = 0x00000008,
        None2         = 0x00000010,
        Epic          = 0x00000020,
        Raid          = 0x00000040,
        TBC           = 0x00000080,
        DeliverMore   = 0x00000100,
        HiddenRewards = 0x00000200,
        AutoRewarded  = 0x00000400,
        TBCRaces      = 0x00000800,
        Daily         = 0x00001000,
        Repleatable   = 0x00002000,
        Unavailable   = 0x00004000,
        Weekly        = 0x00008000,
        AutoComplete  = 0x00010000,
        SpecialItem   = 0x00020000,
        ObjText       = 0x00040000,
        AutoAccept    = 0x00080000,
        Unk100000     = 0x00100000,
        Unk200000     = 0x00200000,
        Unk400000     = 0x00400000,
        Unk800000     = 0x00800000,
        Unk1000000    = 0x01000000
    }
}
