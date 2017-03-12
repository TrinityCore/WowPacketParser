using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientHealthUpdate
    {
        public ulong Guid;
        public int Health;
    }
}
