using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAchievementEarned
    {
        public uint EarnerNativeRealm;
        public bool Initial;
        public ulong Earner;
        public Data Time;
        public int AchievementID;
        public uint EarnerVirtualRealm;
        public ulong Sender;
    }
}
