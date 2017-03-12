using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGuildAchievementEarned
    {
        public int AchievementID;
        public ulong GuildGUID;
        public Data TimeEarned;
    }
}
