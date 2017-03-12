using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLootRollWon
    {
        public ulong Winner;
        public ulong LootObj;
        public byte RollType;
        public int Roll;
        public LootItem Item;
    }
}
