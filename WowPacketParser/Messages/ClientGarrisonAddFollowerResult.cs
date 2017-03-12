using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGarrisonAddFollowerResult
    {
        public GarrisonFollower Follower;
        public int Result;
    }
}
