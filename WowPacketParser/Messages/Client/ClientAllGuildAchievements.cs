using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAllGuildAchievements
    {
        public List<EarnedAchievement> Earned;
    }
}
