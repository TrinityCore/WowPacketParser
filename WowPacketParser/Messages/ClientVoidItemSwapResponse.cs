using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientVoidItemSwapResponse
    {
        public ulong VoidItemB;
        public ulong VoidItemA;
        public uint VoidItemSlotB;
        public uint VoidItemSlotA;
    }
}
