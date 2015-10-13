namespace WowPacketParser.Enums
{
    public enum QuestReasonType
    {
        DontHaveReq           = 0,
        LowLevel              = 1,
        WrongClass            = 5,
        WrongRace             = 6,
        AlreadyDone           = 7,
        OnlyOneTimed          = 12,
        AlreadyOn             = 13,
        LowExpansion          = 16,
        AlreadyOn2            = 18,
        MissingItem           = 21,
        NotEnoughMoney        = 23,
        NoRemainingQuests     = 26,
        CantCompleteTiredTime = 27,
        AlreadyCompletedToday = 29,
        Unknown50             = 50,
        Unknown58             = 58
    }

    public enum QuestReasonTypeWoD
    {
        None                    = 0,
        FailedLowLevel          = 1,
        FailedWrongRace         = 6,
        AlreadyDone             = 7,
        OnlyOneTimed            = 12,
        AlreadyOn1              = 13,
        FailedExpansion         = 16,
        AlreadyOn2              = 18,
        FailedMissingItems      = 21,
        FailedNotEnoughMoney    = 23,
        FailedCais              = 24,
        AlreadyDoneDaily        = 26,
        FailedSpell             = 28,
        HasInProgress           = 30 
    }
}
