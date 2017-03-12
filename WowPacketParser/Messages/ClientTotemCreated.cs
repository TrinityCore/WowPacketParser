using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientTotemCreated
    {
        public ulong Totem;
        public int SpellID;
        public int Duration;
        public byte Slot;
    }
}
