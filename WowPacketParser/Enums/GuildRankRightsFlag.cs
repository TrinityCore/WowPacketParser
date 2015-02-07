using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum GuildRankRightsFlag : uint
    {
        None                 = 0x00000000,
        GuildChatListen      = 0x00000001,
        GuildChatSpeak       = 0x00000002,
        OfficerChatListen    = 0x00000004,
        OfficerChatSpeak     = 0x00000008,
        Invite               = 0x00000010,
        Remove               = 0x00000020,
        Roster               = 0x00000040,
        Promote              = 0x00000080,
        Demote               = 0x00000100,
        Unk200               = 0x00000200,
        Unk400               = 0x00000400,
        Unk800               = 0x00000800,
        SetMOTD              = 0x00001000,
        EditPublicNote       = 0x00002000,
        ViewOfficerNote      = 0x00004000,
        EditOfficerNote      = 0x00008000,
        ModifyGuildInfo      = 0x00010000,
        WithdrawGoldLock     = 0x00020000,
        WithdrawRepair       = 0x00040000,
        WithdrawGold         = 0x00080000,
        CreateGuildEvent     = 0x00100000,
        InAuthenticatedRank  = 0x00200000,
        EditGuildBankTabInfo = 0x00400000,
        RemoveGuildEvent     = 0x00800000,
        Unk1000000           = 0x01000000,
        Unk2000000           = 0x02000000,
        Unk4000000           = 0x04000000,
        Unk8000000           = 0x08000000,
        Unk10000000          = 0x10000000,
        Unk20000000          = 0x20000000,
        Unk40000000          = 0x40000000,
        Unk80000000          = 0x80000000
    }
}
