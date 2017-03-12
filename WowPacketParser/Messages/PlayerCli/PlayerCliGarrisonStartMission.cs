using System.Collections.Generic;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliGarrisonStartMission
    {
        public ulong NpcGUID;
        public List<ulong> FollowerDBIDs;
        public int MissionRecID;
    }
}
