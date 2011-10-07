namespace WowPacketParser.Enums
{
    public enum  GuildCommandError
    {
        Success                 = 0,
//        GuildInternal           = 1,
//        AlreadyInGuild          = 2,
//        AlreadyInGuild2        = 3,
        InvitedToGuild          = 4,
        AlreadyInvitedToGuild   = 5,
        InvalidName             = 6,
        NameExists              = 7,
        LeaderLeave             = 8,
        Permissions             = 9,
//        PlayerNotInGuild        = 10,
//        PlayerNotInGuild2      = 11,
        PlayerNotFound          = 12,
        MaxRank                 = 13,
        RankTooHigh             = 14,
        RankTooLow              = 15,
        RanksLocked             = 17,
        RankInUse               = 18,
        IgnoringYou             = 19,
        Unk24                   = 24, // Appears with CommandType ViewTab (21) after SMSG_GUILD_BANK_LIST 0, 0, -1, 0, 0
        WithdrawLimit           = 25,
        NotEnoughMoney          = 26,
        BankFull                = 28,
        ItemNotFound            = 29
    }
}
