using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLFGBlackList
    {
        public ulong Guid;
        public List<ClientLFGBlackListSlot> Slots;
    }
}
