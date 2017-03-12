using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliGarrisonSetActiveBuildingSpecialization
    {
        public ulong NpcGUID;
        public int PlotInstanceID;
        public int SpecializationID;
    }
}
