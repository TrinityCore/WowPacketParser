using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientVoidStorageContents
    {
        public List<VoidItem> Items;
    }
}
