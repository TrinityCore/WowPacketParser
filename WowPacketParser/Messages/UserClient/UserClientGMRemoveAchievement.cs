namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientGMRemoveAchievement
    {
        public ulong Guid;
        public string Target;
        public int AchievementID;
    }
}
