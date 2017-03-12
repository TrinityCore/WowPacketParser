using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGarrisonCompleteMissionResult
    {
        public int Result;
        public GarrisonMission Mission;
        public int MissionRecID;
    }
}
