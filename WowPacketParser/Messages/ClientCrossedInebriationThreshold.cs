using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCrossedInebriationThreshold
    {
        public ulong Guid;
        public int ItemID;
        public int Threshold;
    }
}
