namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientServerFirstAchievement
    {
        public ulong PlayerGUID;
        public string Name;
        public int AchievementID;
        public bool GuildAchievement;
    }
}
