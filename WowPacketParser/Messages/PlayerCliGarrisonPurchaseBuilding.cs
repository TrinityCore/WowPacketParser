using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliGarrisonPurchaseBuilding
    {
        public ulong NpcGUID;
        public int BuildingID;
        public int PlotInstanceID;
    }
}
