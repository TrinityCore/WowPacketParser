using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct GarrisonPlotInfo
    {
        public int GarrPlotInstanceID;
        public Vector3 PlotPos;
        public int PlotType;
    }
}
