using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct LFGBlackList
    {
        public ulong? PlayerGuid; // Optional
        public List<LFGBlackListSlot> Slot;
    }
}
