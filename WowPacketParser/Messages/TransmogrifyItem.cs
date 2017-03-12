using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct TransmogrifyItem
    {
        public ulong? SrcItemGUID; // Optional
        public ulong? SrcVoidItemGUID; // Optional
        public int ItemID;
        public uint Slot;
    }
}
