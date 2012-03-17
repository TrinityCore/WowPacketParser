namespace WowPacketParser.Enums
{
    public enum BattlegroundError
    {
        IncorrectBattlegroundId           = 0,
        NotReport                         = -1,
        GroupJoinDeserter                 = -2,
        IcorrectPartySize                 = -3,
        TooManyQueues                     = -4,
        NotAllowedWhileQueuedForNonRated  = -5,
        NotAllowedWhileQueuedForArena     = -6,
        TeamHasLeftArenaQueue             = -7,
        NotAllowedInBattleground          = -8,
        Unknown9                          = -9,
        PlayersNotInSameRangeIndex        = -10,
        CouldntJoinQueueInTime            = -11,
        JoinFailedAsGroup                 = -12,
        CantJoinWhileUsingLFG             = -13,
        NotAllowedWhileInRandomQueue      = -14,
        NotAllowedWhileInNotRandomQueue   = -15
    }

    public enum BattlegroundError430
    {
        None                            = 0,
        GroupJoinDeserter               = 2,
        IcorrectPartySize               = 3,
        TooManyQueues                   = 4,
        NotAllowedWhileQueuedForNonRated= 5,
        NotAllowedWhileQueuedForArena   = 6,
        TeamHasLeftArenaQueue           = 7,
        NotAllowedInBattleground        = 8,
        JoinXPGain                      = 9, // error text doesn't exist ingame?
        PlayersNotInSameRangeIndex      = 10,
        CouldntJoinQueueInTime          = 11,
        JoinFailedAsGroup               = 12,
        TeamHasLeftQueue                = 13, // same message as 7
        NotAllowedWhileInPVEQueue       = 14,
        NotAllowedWhileInRandomQueue    = 15,
        NotAllowedWhileInNotRandomQueue = 16,
        BGDevOnly                       = 17,
        BGInvitationDecline             = 18,
        MeetingStoneNotFound            = 19,
        WargameRequestFailed            = 20,
        IncorrectPartySize              = 22,
        NotAllowedOnTournamentRealm     = 23,
        PlayersFromOtherRealmInParty    = 24,
        QueueSuccessful                 = 30,
        RemovedBecauseOfLevelGrant      = 33,
        RemovedBecauseOfFactionChange   = 34,
        JoinAsGroupFailed               = 35,
        GroupPlayerAlreadyQueued        = 43,
    }
}
