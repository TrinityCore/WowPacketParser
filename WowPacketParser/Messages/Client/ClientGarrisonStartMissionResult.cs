using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGarrisonStartMissionResult
    {
        public int Result;
        public GarrisonMission Mission;
        public List<ulong> FollowerDBIDs;
    }
}
