namespace WowPacketParser.Messages.Global.Guild
{
    public unsafe struct GetAchievementMembers
    {
        public ulong GuildGUID;
        public ulong PlayerGUID;
        public int AchievementID;
    }
}
