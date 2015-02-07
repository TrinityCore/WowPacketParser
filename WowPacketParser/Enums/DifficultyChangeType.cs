namespace WowPacketParser.Enums
{
    public enum DifficultyChangeType
    {
        PlayerDifficulty1 = 0,
        SpellDuration     = 1,
        WorldState        = 2,
        Encounter         = 3,
        Combat            = 4,
        Unknown1          = 5,
        Time              = 6,
        Unknown2          = 7,
        MapDifficulty     = 8,
        PlayerDifficulty2 = 9
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
