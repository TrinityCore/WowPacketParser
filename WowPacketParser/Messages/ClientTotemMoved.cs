using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientTotemMoved
    {
        public ulong Totem;
        public byte Slot;
        public byte NewSlot;
    }
}
