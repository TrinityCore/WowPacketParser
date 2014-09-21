namespace WowPacketParser.Enums
{
    public enum BattlegroundError
    {
        IncorrectBattlegroundId          = 0,
        NotReport                        = -1,
        GroupJoinDeserter                = -2,
        IncorrectPartySize               = -3,
        TooManyQueues                    = -4,
        NotAllowedWhileQueuedForNonRated = -5,
        NotAllowedWhileQueuedForArena    = -6,
        TeamHasLeftArenaQueue            = -7,
        NotAllowedInBattleground         = -8,
        Unknown9                         = -9,
        PlayersNotInSameRangeIndex       = -10,
        CouldntJoinQueueInTime           = -11,
        JoinFailedAsGroup                = -12,
        CantJoinWhileUsingLFG            = -13,
        NotAllowedWhileInRandomQueue     = -14,
        NotAllowedWhileInNotRandomQueue  = -15
    }

    public enum BattlegroundError4x
    {
        None                                   = 0,
        GroupJoinBattlegroundDeserters         = 2,
        ArenaTeamPartySize                     = 3,
        BattlegroundTooManyQueues              = 4,
        BattlegroundCannotQueueForRated        = 5,
        BattlegroundQueuedForRated             = 6,
        BattlegroundTeamLeftQueue1             = 7,
        BattlegroundNotInBattleground          = 8,
        BattlegroundJoinXPGain                 = 9, // error text doesn't exist ingame?
        BattlegroundJoinRangeIndex             = 10,
        CouldntJoinQueueInTime                 = 11, // ERR_BATTLEGROUND_JOIN_TIMED_OUT
        JoinFailedAsGroup                      = 12, // ERR_BATTLEGROUND_JOIN_TIMED_OUT
        BattlegroundTeamLeftQueue2             = 13,
        LFGCantUseBattleground                 = 14,
        InRandomBG                             = 15,
        InNonRandomBG                          = 16,
        BGDeveloperOnly                        = 17,
        BattlegroundInvitationDeclined         = 18,
        MeetingStoneNotFound                   = 19,
        WargameRequestFailed                   = 20,
        BattlefieldTeamPartySize               = 22,
        NotOnTournamentRealm                   = 23,
        BattlegroundPlayersFromDifferentRealms = 24,
        QueueSuccessful                        = 30,
        RemoveFromPVPQueueGrantLevel           = 33,
        RemoveFromPVPQueueFactionChange        = 34,
        JoinAsGroupFailed                      = 35,
        BattlegroundDupeQueue                  = 43
    }
}
