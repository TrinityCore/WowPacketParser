using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGarrisonBuildingSetActiveSpecializationResult
    {
        public int CurrentGarrSpecID;
        public int Result;
        public int GarrPlotInstanceID;
    }
}
