using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum QuestFlags : uint
    {
        None                  = 0x00000000,
        StayAlive             = 0x00000001,
        PartyAccept           = 0x00000002,
        Exploration           = 0x00000004,
        Sharable              = 0x00000008,
        HasCondition          = 0x00000010,
        HideRewardPOI         = 0x00000020,
        Raid                  = 0x00000040,
        ExpansionOnly         = 0x00000080, // Deprecated flag
        NoMoneyFromExperience = 0x00000100,
        HiddenRewards         = 0x00000200,
        Tracking              = 0x00000400,
        DepricateReputation   = 0x00000800,
        Daily                 = 0x00001000,
        FlagsForPvP           = 0x00002000,
        Unavailable           = 0x00004000,
        Weekly                = 0x00008000,
        AutoComplete          = 0x00010000,
        DisplayItemInTracker  = 0x00020000,
        DisableCompletionText = 0x00040000,
        AutoAccept            = 0x00080000,
        PlayerCastOnAccept    = 0x00100000,
        PlayerCastOnComplete  = 0x00200000,
        UpdatePhaseShift      = 0x00400000,
        SoRWhitelist          = 0x00800000,
        LaunchGossipComplete  = 0x01000000,
        RemoveExtraGetItems   = 0x02000000,
        HideUntilDiscovered   = 0x04000000,
        PortraitInQuestLog    = 0x08000000,
        ShowItemWhenCompleted = 0x10000000,
        LaunchGossipAccept    = 0x20000000,
        ItemsGlowWhenDone     = 0x40000000,
        FailOnLogout          = 0x80000000
    }
}
