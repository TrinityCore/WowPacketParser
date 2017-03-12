using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientSetLootMethod
    {
        public ulong Master;
        public int Threshold;
        public byte Method;
        public byte PartyIndex;
    }
}
