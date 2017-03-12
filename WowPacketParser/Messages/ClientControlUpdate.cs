using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientControlUpdate
    {
        public bool On;
        public ulong Guid;
    }
}
