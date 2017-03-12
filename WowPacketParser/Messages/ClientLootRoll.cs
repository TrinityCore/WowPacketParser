using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLootRoll
    {
        public ulong Player;
        public int Roll;
        public LootItem Item;
        public byte RollType;
        public ulong LootObj;
        public bool Autopassed;
    }
}
