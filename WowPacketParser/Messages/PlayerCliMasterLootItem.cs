using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliMasterLootItem
    {
        public List<LootRequest> Loot;
        public ulong Target;
    }
}
