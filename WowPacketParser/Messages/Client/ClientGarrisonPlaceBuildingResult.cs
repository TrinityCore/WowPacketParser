using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGarrisonPlaceBuildingResult
    {
        public int Result;
        public GarrisonBuildingInfo BuildingInfo;
    }
}
