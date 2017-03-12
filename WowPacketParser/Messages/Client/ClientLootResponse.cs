using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
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
