namespace WowPacketParser.Enums
{
    public enum LootError
    {
        DidntKill            = 0,
        TooFar               = 4,
        BadFacing            = 5,
        Locked               = 6,
        NotStanding          = 8,
        Stunned              = 9,
        PlayerNotFound       = 10,
        PlayTimeExceeded     = 11,
        MasterInvFull        = 12,
        MasterUniqueItem     = 13,
        MasterOther          = 14,
        AlreadyPickpockted   = 15,
        NotWhileShapeshifted = 16,
        NoError              = 17
    }
}
