using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSendAllItemDurability
    {
        public List<int> MaxDurability;
    }
}
