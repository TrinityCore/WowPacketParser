using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum PlayerFlags : uint // 8.x
    {
        GroupLeader        = 0x00000001,
        AFK                = 0x00000002,
        DND                = 0x00000004,
        GameMaster         = 0x00000008,
        Ghost              = 0x00000010,
        Resting            = 0x00000020,
        VoiceChat          = 0x00000040,
        Unk7               = 0x00000080,
        ContestedPvP       = 0x00000100,
        InPvP              = 0x00000200,
        HideHelm           = 0x00000400,
        HideCloak          = 0x00000800,
        PlayedLongTime     = 0x00001000,
        PlayedTooLong      = 0x00002000,
        OutOfBounds        = 0x00004000,
        Developer          = 0x00008000,
        EnableLowLevelRaid = 0x00010000,
        TaxiBenchmark      = 0x00020000,
        PvPTimerActive     = 0x00040000,
        Commentator        = 0x00080000,
        Unk20              = 0x00100000,
        Unk21              = 0x00200000,
        CommentatorUber    = 0x00400000,
        AllowOnlyAbility   = 0x00800000,
        PetBattleUnlocked  = 0x01000000,
        NoXPGain           = 0x02000000,
        Unk26              = 0x04000000,
        AutoDeclineGuild   = 0x08000000,
        GuildLevelEnabled  = 0x10000000,
        VoidUnlocked       = 0x20000000,
        Mentor             = 0x40000000,
        Unk31              = 0x80000000
    }
}
