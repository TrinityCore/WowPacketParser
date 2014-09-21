namespace WowPacketParser.Enums
{
    public enum ContactResult
    {
        DBError             = 0,
        FriendListFull      = 1,
        Online              = 2,
        Offline             = 3,
        FriendNotFound      = 4,
        FriendRemoved       = 5,
        FriendAddedOnline   = 6,
        FriendAddedOffline  = 7,
        AlreadyOnFriendList = 8,
        CannotBefriendSelf  = 9,
        IsEnemy             = 10,
        IgnoreListFull      = 11,
        CannotIgnoreSelf    = 12,
        IgnoreNotFound      = 13,
        AlreadyOnIgnoreList = 14,
        IgnoreAdded         = 15,
        IgnoreRemoved       = 16,
        IgnoreNameAmbiguous = 17,
        MuteListFull        = 18,
        CannotMuteSelf      = 19,
        MuteNotFound        = 20,
        AlreadyOnMuteList   = 21,
        MuteAdded           = 22,
        MuteRemoved         = 23,
        MuteNameAmbiguous   = 24,
        Unknown1            = 25, // BNet friend related
        Unknown2            = 26, // BNet friend related
        Unknown3            = 27 // BNet friend related
    }
}
