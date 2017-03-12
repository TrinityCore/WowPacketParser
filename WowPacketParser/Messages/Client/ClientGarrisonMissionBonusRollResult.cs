using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
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
