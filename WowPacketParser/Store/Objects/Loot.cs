using System.Collections.Generic;
using WowPacketParser.Enums;

namespace WowPacketParser.Store.Objects
{
    public class Loot
    {
        // ObjectType

        public LootType LootType;

        public uint Gold;

        public List<LootItem> LootItems;
    }
}
