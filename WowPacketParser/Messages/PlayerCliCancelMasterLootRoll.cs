using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliCancelMasterLootRoll
    {
        public ulong Object;
        public byte LootListID;
    }
}
