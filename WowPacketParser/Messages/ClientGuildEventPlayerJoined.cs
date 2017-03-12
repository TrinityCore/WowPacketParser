using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildEventPlayerJoined
    {
        public ulong Guid;
        public string Name;
        public uint VirtualRealmAddress;
    }
}
