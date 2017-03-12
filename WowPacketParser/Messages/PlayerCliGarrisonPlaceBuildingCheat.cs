using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliGarrisonPlaceBuildingCheat
    {
        public bool Activate;
        public int BuildingID;
        public int PlotInstanceID;
    }
}
