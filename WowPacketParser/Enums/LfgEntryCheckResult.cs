namespace WowPacketParser.Enums
{
    public enum LfgEntryCheckResult
    {
        None                   = 0,
        InsufficientExpansion  = 1,
        TooLowLevel            = 2,
        TooHighLevel           = 3,
        TooLowGearScore        = 4,
        TooHighGearScore       = 5,
        RaidLocked             = 6,
        AttunementTooLowLevel  = 1001,
        AttunementTooHighLevel = 1002,
        QuestNotCompleted      = 1022,
        MissingItem            = 1025,
        NotInSeason            = 1031
    }
}
