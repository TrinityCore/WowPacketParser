using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientBattlePayDeliveryEnded
    {
        public ulong DistributionID;
        public List<ItemInstance> Items;
    }
}
