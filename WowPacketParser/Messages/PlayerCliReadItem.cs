using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliReadItem
    {
        public byte Slot;
        public byte PackSlot;
    }
}
