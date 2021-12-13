namespace WowPacketParser.Enums
{
    public enum SetPlayerDifficultyResults
    {
        SetDifficulty       = 0,
        Cooldown            = 1,
        WorldState          = 2,
        EncounterInProgress = 3,
        PlayerInCombat      = 4,
        PlayerBusy          = 5,
        Start               = 6,
        AlreadyInProgress   = 7,
        FailedCondition     = 8,
        Complete            = 9,
    }

    public enum DifficultyChangeType434
    {
        Cooldown                     = 0, // ERR_DIFFICULTY_CHANGE_COOLDOWN_S
        WorldState                   = 1,
        Encounter                    = 2,
        Combat                       = 3,
        PlayerBusy                   = 4,
        Time                         = 5,
        AlreadyStarted               = 6,
        MapDifficultyRequirement     = 7,
        PlayerAlreadyLocked          = 8, // ERR_DIFFICULTY_CHANGE_OTHER_HEROIC_S
        HeroicInstanceAlreadyRunning = 9,
        DisabledInLFG                = 10,
        DifficultyChanged            = 11
    }
}
