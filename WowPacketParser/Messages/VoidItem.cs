using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct VoidItem
    {
        public ulong Guid;
        public ulong Creator;
        public uint Slot;
        public ItemInstance Item;
    }
}
