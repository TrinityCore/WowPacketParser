using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliGarrisonAssignFollowerToBuilding
    {
        public ulong NpcGUID;
        public ulong FollowerDBID;
        public int PlotInstanceID;
    }
}
