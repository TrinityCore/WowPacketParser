using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientRespondInspectAchievements
    {
        public ulong Player;
        public AllAchievements Data;
    }
}
