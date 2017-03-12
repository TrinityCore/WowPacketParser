using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildSendGuildXP
    {
        public long GuildXPToLevel;
        public long MemberTotalXP;
        public long MemberWeeklyXP;
        public long GuildTotalXP;
    }
}
