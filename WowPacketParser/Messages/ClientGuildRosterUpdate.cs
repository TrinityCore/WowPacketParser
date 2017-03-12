using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildRosterUpdate
    {
        public List<ClientGuildRosterMemberData> MemberData;
    }
}
