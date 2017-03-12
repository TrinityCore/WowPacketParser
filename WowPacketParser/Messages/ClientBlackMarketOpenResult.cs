using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBlackMarketOpenResult
    {
        public ulong NpcGUID;
        public bool Open;
    }
}
