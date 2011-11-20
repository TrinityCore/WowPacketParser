using System.Collections.Generic;
using WowPacketParser.Enums;

namespace WowPacketParser.Store.Objects
{
    public class Loot
    {
        public uint Gold;

        public List<LootItem> LootItems;
    }
}
