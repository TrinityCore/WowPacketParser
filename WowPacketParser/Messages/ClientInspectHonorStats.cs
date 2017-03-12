using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientInspectHonorStats
    {
        public ulong PlayerGUID;
        public uint LifetimeHK;
        public ushort YesterdayHK;
        public ushort TodayHK;
        public byte LifetimeMaxRank;
    }
}
