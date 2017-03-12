using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientVoidStorageTransferChanges
    {
        public List<ulong> RemovedItems;
        public List<VoidItem> AddedItems;
    }
}
