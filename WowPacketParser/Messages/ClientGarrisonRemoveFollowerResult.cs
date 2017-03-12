using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGarrisonRemoveFollowerResult
    {
        public ulong FollowerDBID;
        public int Result;
    }
}
