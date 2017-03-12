using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliMasterLootItem
    {
        public List<LootRequest> Loot;
        public ulong Target;
    }
}
