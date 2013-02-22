using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum QuestFlags2 : uint
    {
        None                            = 0x0000,
        KeepAdditionalItems             = 0x0001,
        SuppressGossipComplete          = 0x0002,
        SuppressGossipAccept            = 0x0004,
        PlayerCannotBeQuestgiver        = 0x0008,
        DisplayClassChoiceRewards       = 0x0010,
        DisplaySpecChoiceRewards        = 0x0020,
        RemoveFromLogOnPeriodicReset    = 0x0040,
        AccountLevelQuest               = 0x0080,
        LegendaryQuest                  = 0x0100,
        NoGuildXp                       = 0x0200,
        ResetCacheOnAccept              = 0x0400,
        NoAbandonWithCompletedObjective = 0x0800,
        RecastAcceptSpellOnLogin        = 0x1000
    }
}
