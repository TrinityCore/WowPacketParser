using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientLFGBlackList
    {
        public ulong Guid;
        public List<ClientLFGBlackListSlot> Slots;
    }
}
