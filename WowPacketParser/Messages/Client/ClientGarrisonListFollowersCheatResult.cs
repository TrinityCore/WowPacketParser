using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGarrisonListFollowersCheatResult
    {
        public List<GarrisonFollower> Followers;
    }
}
