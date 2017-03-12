using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct QualifiedGUID
    {
        public ulong Guid;
        public uint VirtualRealmAddress;
    }
}
