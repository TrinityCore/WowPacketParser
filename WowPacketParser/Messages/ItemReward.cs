using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ItemReward
    {
        public int ItemID;
        public int ItemDisplayID;
        public uint Quantity;
    }
}
