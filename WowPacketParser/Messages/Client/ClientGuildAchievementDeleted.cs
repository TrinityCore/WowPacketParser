using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGuildAchievementDeleted
    {
        public ulong GuildGUID;
        public int AchievementID;
        public Data TimeDeleted;
    }
}
