using System.Collections.Generic;
using WowPacketParser.Enums;

namespace WowPacketParser.Store.Objects
{
    public record ItemInstance
    {
        public int ItemID;
        public uint RandomPropertiesSeed; // removed in 8.1.5
        public uint RandomPropertiesID; // removed in 8.1.5

        // bonus
        public byte Context;
        public uint[] BonusListIDs;

        // modifier
        public Dictionary<ItemModifier, int> ItemModifier = new Dictionary<ItemModifier, int>();
    }
}
