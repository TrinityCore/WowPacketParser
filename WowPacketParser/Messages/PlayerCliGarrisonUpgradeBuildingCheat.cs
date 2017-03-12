using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliGarrisonUpgradeBuildingCheat
    {
        public int PlotInstanceID;
        public bool Immediate;
    }
}
