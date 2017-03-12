using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSwapVoidItem
    {
        public ulong Npc;
        public ulong VoidItem;
        public uint DstSlot;
    }
}
