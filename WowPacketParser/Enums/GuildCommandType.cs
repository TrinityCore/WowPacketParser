namespace WowPacketParser.Enums
{
    public enum  GuildCommandType
    {
        Create = 0,
        Invite = 1,
        Quit = 3,
        GuildChat = 5,
        Promote = 6,
        Demote = 7,
        Removed = 8,
        Leader = 10,
        UpdateMOTD = 11,
        Chat = 13,
        Founder = 14,
        CreateRank = 16,
        EditPublicNote = 19,
        Unk21 = 21, // Appears after SMSG_GUILD_BANK_LIST Money (0, 0, -1, 0, 0)
        Unk22 = 22,
        Unk25 = 25
    }
}
