namespace WowPacketParser.Messages.UserClient.GM
{
    public unsafe struct RemoveAchievement
    {
        public ulong Guid;
        public string Target;
        public int AchievementID;
    }
}
