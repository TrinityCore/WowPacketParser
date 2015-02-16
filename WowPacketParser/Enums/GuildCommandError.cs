namespace WowPacketParser.Enums
{
    public enum GuildCommandError
    {
        Success               = 0,
        GuildInternal         = 1,
        AlreadyInGuild        = 2,
        AlreadyInGuild2       = 3,
        InvitedToGuild        = 4,
        AlreadyInvitedToGuild = 5,
        InvalidName           = 6,
        NameExists            = 7,
        LeaderLeave           = 8,
        PlayerNotInGuild      = 9,
        PlayerNotInGuild2     = 10,
        PlayerNotFound        = 11,
        PlayerOppositeFaction = 12,
        RankTooHigh           = 13,
        RankTooLow            = 14,
        RanksLocked           = 17,
        RankInUse             = 18,
        IgnoringYou           = 19,
        Unk20                 = 20,
        Unk21                 = 21,
        Unk24                 = 24, // Appears with CommandType ViewTab (21) after SMSG_GUILD_BANK_QUERY_RESULTS 0, 0, -1, 0, 0
        WithdrawLimit         = 25,
        NotEnoughMoney        = 26,
        BankFull              = 28,
        ItemNotFound          = 29,
        TooMuchMoney          = 31,
        WrongTab              = 32,
        RequiresAuthenticator = 34,
        BankVoucherFailed     = 35
    }
}
