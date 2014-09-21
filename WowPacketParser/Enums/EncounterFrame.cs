namespace WowPacketParser.Enums
{
    public enum EncounterFrame
    {
        Engage            = 0,
        Disengage         = 1,
        UpdatePriority    = 2,
        AddTimer          = 3,
        EnableObjective   = 4,
        UpdateObjective   = 5,
        DisableObjective  = 6,
        SortEncounterList = 7 // Unsure
    }

    public enum EncounterFrame434
    {
        SetCombatResLimit   = 0,
        ResetCombatResLimit = 1,
        Engage              = 2,
        Disengage           = 3,
        UpdatePriority      = 4,
        AddTimer            = 5,
        EnableObjective     = 6,
        UpdateObjective     = 7,
        DisableObjective    = 8,
        SortEncounterList   = 9,
        AddCombatResLimit   = 10
    }
}
