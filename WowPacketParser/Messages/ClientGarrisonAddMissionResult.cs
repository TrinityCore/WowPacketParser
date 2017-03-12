using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGarrisonAddMissionResult
    {
        public GarrisonMission Mission;
        public int Result;
    }
}
