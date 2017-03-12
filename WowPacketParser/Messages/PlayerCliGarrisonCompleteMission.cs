using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliGarrisonCompleteMission
    {
        public ulong NpcGUID;
        public int MissionRecID;
    }
}
