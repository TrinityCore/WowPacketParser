using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildInviteDeclined
    {
        public string Name;
        public bool AutoDecline;
        public uint VirtualRealmAddress;
    }
}
