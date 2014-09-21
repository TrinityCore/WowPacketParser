namespace WowPacketParser.Enums
{
    public enum PetTameFailureReason
    {
        UnknownError         = 0,
        InvalidCreature      = 1,
        TooMany              = 2,
        CreatureAlreadyOwned = 3,
        NotTameable          = 4,
        AnotherSummonActive  = 5,
        UnitsCannotTame      = 6,
        NoPetAvailable       = 7,
        InternalError        = 8,
        TooHighLevel         = 9,
        Dead                 = 10,
        NotDead              = 11,
        CannotControlExotic  = 12,
        InvalidSlot          = 13
    }
}
