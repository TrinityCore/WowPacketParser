namespace WowPacketParser.Enums
{
    public enum  GuildCommandError
    {
        Success = 0,
        Permissions = 9,
        MaxRank = 13,
        Unk24 = 24, // Appears with EventType 21 after SMSG_GUILD_BANK_LIST 0, 0, -1, 0, 0
        NotEnoughMoney = 26,
        BankFull = 28,
        ItemNotFound = 29
    }
}
