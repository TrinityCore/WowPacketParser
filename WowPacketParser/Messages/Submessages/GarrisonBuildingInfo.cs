using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct GarrisonBuildingInfo
    {
        public int GarrPlotInstanceID;
        public int GarrBuildingID;
        public UnixTime TimeBuilt;
        public bool Active;
        public Vector3 BuildingPos;
        public float BuildingFacing;
        public int CurrentGarSpecID;
    }
}
