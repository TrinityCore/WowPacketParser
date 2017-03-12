using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliGarrisonSetActiveBuildingSpecializationCheat
    {
        public int PlotInstanceID;
        public int SpecializationID;
    }
}
