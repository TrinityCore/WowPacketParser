using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSetAnimTier
    {
        public ulong Unit;
        public int Tier;
    }
}
