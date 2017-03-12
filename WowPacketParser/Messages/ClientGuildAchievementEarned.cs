using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
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
