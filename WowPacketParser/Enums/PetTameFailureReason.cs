namespace WowPacketParser.Enums
{
    public enum PetTameFailureReason
    {
        InvalidCreature = 0,
        TooMany = 1,
        CreatureAlreadyOwned = 2,
        NotTameable = 3,
        AnotherSummonActive = 4,
        UnitsCannotTame = 5,
        NoPetAvailable = 6,
        InternalError = 7,
        TooHighLevel = 8,
        Dead = 9,
        NotDead = 10,
        CannotControlExotic = 11,
        UnknownError = 12
    }
}
