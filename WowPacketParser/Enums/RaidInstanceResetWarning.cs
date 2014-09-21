namespace WowPacketParser.Enums
{
    public enum RaidInstanceResetWarning
    {
        None       = 0,
        Hours      = 1,    // WARNING! %s is scheduled to reset in %d hour(s).
        Minute     = 2,    // WARNING! %s is scheduled to reset in %d minute(s)!
        MinuteSoon = 3,    // WARNING! %s is scheduled to reset in %d minute(s). Please exit the zone or you will be returned to your bind location!
        Welcome    = 4,    // Welcome to %s. This raid instance is scheduled to reset in %s.
        Expired    = 5     // Raid instance has expired.
    }
}
