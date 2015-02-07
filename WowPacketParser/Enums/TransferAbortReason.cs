namespace WowPacketParser.Enums
{
    public enum TransferAbortReason
    {
        None                      = 0,
        Error                     = 1,
        MaxPlayers                = 2,
        InstanceNotFound1         = 3,
        TooManyInstances          = 4,
        Unknown                   = 5,
        ZoneInCombat              = 6,
        InsufficientExpansion     = 7,
        DifficultyUnavailable     = 8,
        UniqueMessage             = 9,
        TooManyRealmInstances     = 10,
        GroupRequired             = 11,
        InstanceNotFound3         = 12,
        InstanceNotFound4         = 13,
        InstanceNotFound5         = 14,
        RealmOnly                 = 15,
        MapNotAllowed             = 16,
        LockedToDifferentInstance = 18,
        AlreadyCompletedEncounter = 19
    }
}
