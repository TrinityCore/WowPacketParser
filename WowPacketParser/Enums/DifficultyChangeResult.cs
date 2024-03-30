namespace WowPacketParser.Enums
{
    public enum DifficultyChangeResult
    {
        Cooldown = 0,
        EventInProgress = 1,
        EncounterInProgress = 2,
        PlayerCombat = 3,
        PlayerBusy = 4,
        PlayerOnVehicle = 5,
        LoadingScreenEnable = 6,
        DifficultyChangeAlreadyInProgress = 7,
        MapDifficultyConditionNotSatisfied = 8,
        PlayerAlreadyLockedToDifferentInstance = 9,
        HeroicInstanceAlreadyRunningOtherParty = 10,
        DisabledInLFG = 11,
        Success = 12
    }
}
