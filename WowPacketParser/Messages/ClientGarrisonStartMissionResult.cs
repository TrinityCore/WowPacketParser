using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGarrisonStartMissionResult
    {
        public int Result;
        public GarrisonMission Mission;
        public List<ulong> FollowerDBIDs;
    }
}
