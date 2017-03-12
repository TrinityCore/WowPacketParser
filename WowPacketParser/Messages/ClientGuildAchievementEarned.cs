using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildAchievementEarned
    {
        public int AchievementID;
        public ulong GuildGUID;
        public Data TimeEarned;
    }
}
