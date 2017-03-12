using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLootResponse
    {
        public uint Coins;
        public byte LootMethod;
        public byte Threshold;
        public ulong LootObj;
        public List<LootCurrency> Currencies;
        public bool PersonalLooting;
        public byte AcquireReason;
        public bool Acquired;
        public bool AELooting;
        public ulong Owner;
        public byte FailureReason;
        public List<LootItem> Items;
    }
}
