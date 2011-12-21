using System.Collections.Generic;

namespace WowPacketParser.Store.Objects
{
    public class Loot
    {
        public uint Gold;

        public ICollection<LootItem> LootItems;
    }
}
