using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGarrisonPlaceBuildingResult
    {
        public int Result;
        public GarrisonBuildingInfo BuildingInfo;
    }
}
