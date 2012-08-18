using System.Collections.Generic;
using PacketParser.Misc;

namespace PacketParser.DataStructures
{
    public class Loot
    {
        public uint Gold;

        public ICollection<LootItem> LootItems;
    }
}
