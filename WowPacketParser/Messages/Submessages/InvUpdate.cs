using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct InvUpdate
    {
        public List<InvItem> Items;
    }
}
