using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ItemInstance
    {
        public int ItemID;
        public int RandomPropertiesSeed;
        public int RandomPropertiesID;
        public ItemBonusInstanceData ItemBonus; // Optional
        public int[] Modifications; // Optional
    }
}
