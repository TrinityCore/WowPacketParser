using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGarrisonAddFollowerResult
    {
        public GarrisonFollower Follower;
        public int Result;
    }
}
