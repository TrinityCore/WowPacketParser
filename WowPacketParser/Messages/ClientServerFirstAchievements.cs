using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientServerFirstAchievements
    {
        public List<ServerFirstAchievement> Achievements;
    }
}
