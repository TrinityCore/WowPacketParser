namespace WowPacketParser.Messages.UserClient.GM
{
    public unsafe struct GrantAchievement
    {
        public int AchievementID;
        public ulong Guid;
        public string Target;
    }
}
