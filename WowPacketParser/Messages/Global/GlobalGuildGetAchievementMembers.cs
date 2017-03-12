namespace WowPacketParser.Messages.Global
{
    public unsafe struct GlobalGuildGetAchievementMembers
    {
        public ulong GuildGUID;
        public ulong PlayerGUID;
        public int AchievementID;
    }
}
