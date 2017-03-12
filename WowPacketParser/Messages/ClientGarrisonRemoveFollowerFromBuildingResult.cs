using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGarrisonRemoveFollowerFromBuildingResult
    {
        public ulong FollowerDBID;
        public int Result;
    }
}
