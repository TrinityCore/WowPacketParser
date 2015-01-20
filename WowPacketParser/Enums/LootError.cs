namespace WowPacketParser.Enums
{
    public enum LootError
    {
        DidntKill            = 0,
        //Unk1               = 1,
        //Unk2               = 2,
        //Unk3               = 3,
        TooFar               = 4,
        BadFacing            = 5,
        Locked               = 6,
        //Unk7               = 7,
        NotStanding          = 8,
        Stunned              = 9,
        PlayerNotFound       = 10,
        PlayTimeExceeded     = 11,
        MasterInvFull        = 12,
        MasterUniqueItem     = 13,
        MasterOther          = 14,
        AlreadyPickpocketed  = 15,
        NotWhileShapeshifted = 16,
        NoLoot               = 17, // NC
        None                 = 18  // NC
    }
}
