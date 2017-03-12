using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliLootRoll
    {
        public ulong LootObj;
        public byte LootListID;
        public byte RollType;
    }
}
