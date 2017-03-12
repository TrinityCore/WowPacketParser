using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGarrisonCompleteMissionResult
    {
        public int Result;
        public GarrisonMission Mission;
        public int MissionRecID;
    }
}
