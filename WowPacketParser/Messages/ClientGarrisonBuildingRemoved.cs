using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGarrisonBuildingRemoved
    {
        public int GarrPlotInstanceID;
        public int GarrBuildingID;
    }
}
