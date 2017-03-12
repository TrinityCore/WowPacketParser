using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBattlePayDeliveryEnded
    {
        public ulong DistributionID;
        public List<ItemInstance> Items;
    }
}
