using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct InvItem
    {
        public byte ContainerSlot;
        public byte Slot;
    }
}
