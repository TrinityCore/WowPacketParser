using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum QuestFlagsEx : uint
    {
        None                                                    = 0x0000000,
        KeepAdditionalItems                                     = 0x0000001,
        SuppressGossipComplete                                  = 0x0000002,
        SuppressGossipAccept                                    = 0x0000004,
        DisallowPlayerAsQuestgiver                              = 0x0000008,
        DisplayClassChoiceRewards                               = 0x0000010,
        DisplaySpecChoiceRewards                                = 0x0000020,
        RemoveFromLogOnPeriodicReset                            = 0x0000040,
        AccountLevelQuest                                       = 0x0000080,
        LegendaryQuest                                          = 0x0000100,
        NoGuildXp                                               = 0x0000200,
        ResetCacheOnAccept                                      = 0x0000400,
        NoAbandonOnceAnyObjectiveComplete                       = 0x0000800,
        RecastAcceptSpellOnLogin                                = 0x0001000,
        UpdateZoneAuras                                         = 0x0002000,
        NoCreditForProxy                                        = 0x0004000,
        DisplayAsDailyQuest                                     = 0x0008000,
        PartOfQuestLine                                         = 0x0010000,
        QuestForInternalBuildsOnly                              = 0x0020000,
        SuppressSpellLearnTextLine                              = 0x0040000,
        DisplayHeaderAsObjectiveForTasks                        = 0x0080000,
        GarrisonNonOwnersAllowed                                = 0x0100000,
        RemoveQuestOnWeeklyReset                                = 0x0200000,
        SuppressFarewellAudioAfterQuestAccept                   = 0x0400000,
        RewardsBypassWeeklyCapsAndSeasonTotal                   = 0x0800000,
        ClearProgressOfCriteriaTreeObjectivesOnAccept           = 0x1000000
    }
}
