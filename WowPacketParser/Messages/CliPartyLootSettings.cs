using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliPartyLootSettings
    {
        public ulong LootMaster;
        public byte LootMethod;
        public byte LootThreshold;
    }
}
