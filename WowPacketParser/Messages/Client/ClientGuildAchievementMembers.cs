using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGuildAchievementMembers
    {
        public ulong GuildGUID;
        public List<ClientGuildAchievementMember> Member;
        public int AchievementID;
    }
}
