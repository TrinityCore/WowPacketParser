using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildRoster
    {
        public List<ClientGuildRosterMemberData> MemberData;
        public int GuildFlags;
        public string WelcomeText;
        public string InfoText;
        public Data CreateDate;
        public int NumAccounts;
        public int MaxWeeklyRep;
    }
}
