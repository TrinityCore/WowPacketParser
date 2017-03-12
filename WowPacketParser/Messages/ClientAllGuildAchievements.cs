using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAllGuildAchievements
    {
        public List<EarnedAchievement> Earned;
    }
}
