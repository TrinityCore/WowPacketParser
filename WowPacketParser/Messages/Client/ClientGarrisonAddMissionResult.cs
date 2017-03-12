using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGarrisonAddMissionResult
    {
        public GarrisonMission Mission;
        public int Result;
    }
}
