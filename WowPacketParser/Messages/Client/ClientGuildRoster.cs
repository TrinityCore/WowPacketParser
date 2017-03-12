using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
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
