using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliDestroyItem
    {
        public uint Count;
        public byte SlotNum;
        public byte ContainerId;
    }
}
