using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildAchievementDeleted
    {
        public ulong GuildGUID;
        public int AchievementID;
        public Data TimeDeleted;
    }
}
