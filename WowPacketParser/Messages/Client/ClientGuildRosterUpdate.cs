using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGuildRosterUpdate
    {
        public List<ClientGuildRosterMemberData> MemberData;
    }
}
