using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliDoMasterLootRoll
    {
        public ulong Object;
        public byte LootListID;
    }
}
