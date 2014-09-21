namespace WowPacketParser.Enums
{
    public enum LfgError
    {
        None                    = 0,
        PlayerDead              = 1,
        Falling                 = 2,
        InVehicle               = 3,
        Fatigue                 = 4,
        InvalidTeleportLocation = 6,
        Charming                = 8 // Not 100%, can be 7
    }
}
