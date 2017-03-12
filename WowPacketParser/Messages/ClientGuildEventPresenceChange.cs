using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildEventPresenceChange
    {
        public bool LoggedOn;
        public uint VirtualRealmAddress;
        public string Name;
        public ulong Guid;
        public bool Mobile;
    }
}
