using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientServerFirstAchievement
    {
        public ulong PlayerGUID;
        public string Name;
        public int AchievementID;
        public bool GuildAchievement;
    }
}
