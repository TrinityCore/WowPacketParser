using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
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
