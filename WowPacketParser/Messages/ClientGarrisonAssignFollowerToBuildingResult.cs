using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGarrisonAssignFollowerToBuildingResult
    {
        public ulong FollowerDBID;
        public int Result;
        public int PlotInstanceID;
    }
}
