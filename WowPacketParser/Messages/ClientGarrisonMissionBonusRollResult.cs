using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGarrisonMissionBonusRollResult
    {
        public int Result;
        public bool RollSucceeded;
        public int MissionRecID;
        public GarrisonMission Mission;
        public int BonusIndex;
    }
}
