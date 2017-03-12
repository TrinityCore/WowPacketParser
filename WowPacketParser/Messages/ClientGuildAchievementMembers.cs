using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildAchievementMembers
    {
        public ulong GuildGUID;
        public List<ClientGuildAchievementMember> Member;
        public int AchievementID;
    }
}
