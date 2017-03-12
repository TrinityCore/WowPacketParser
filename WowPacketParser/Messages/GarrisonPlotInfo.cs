using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GarrisonPlotInfo
    {
        public int GarrPlotInstanceID;
        public Vector3 PlotPos;
        public int PlotType;
    }
}
