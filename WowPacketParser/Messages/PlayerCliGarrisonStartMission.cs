using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliGarrisonStartMission
    {
        public ulong NpcGUID;
        public List<ulong> FollowerDBIDs;
        public int MissionRecID;
    }
}
