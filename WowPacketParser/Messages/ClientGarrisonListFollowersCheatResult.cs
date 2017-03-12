using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGarrisonListFollowersCheatResult
    {
        public List<GarrisonFollower> Followers;
    }
}
