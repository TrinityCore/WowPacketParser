using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientVoidStorageTransferChanges
    {
        public List<ulong> RemovedItems;
        public List<VoidItem> AddedItems;
    }
}
