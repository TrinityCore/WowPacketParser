using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSendAllItemDurability
    {
        public List<int> MaxDurability;
    }
}
