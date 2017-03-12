namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientGMGrantAchievement
    {
        public int AchievementID;
        public ulong Guid;
        public string Target;
    }
}
