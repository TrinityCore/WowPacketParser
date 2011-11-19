using System.Collections.Generic;
using WowPacketParser.Enums;

namespace WowPacketParser.Store.Objects
{
    public class Loot
    {
        // ObjectType

        public uint Gold;

        public List<LootItem> LootItems;
    }
}
