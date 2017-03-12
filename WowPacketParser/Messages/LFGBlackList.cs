using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct LFGBlackList
    {
        public ulong PlayerGuid; // Optional
        public List<LFGBlackListSlot> Slot;
    }
}
