using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class LootHandler
    {
        public static void ReadLootItem(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();

            packet.ReadBits("ItemType", 2, indexes);
            packet.ReadBits("ItemUiType", 3, indexes);
            packet.ReadBit("CanTradeToTapList", indexes);

            Substructures.ItemHandler.ReadItemInstance(packet, indexes, "ItemInstance");

            packet.ReadUInt32("Quantity", indexes);
            packet.ReadByte("LootItemType", indexes);
            packet.ReadByte("LootListID", indexes);
        }

        public static void ReadCurrenciesData(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("CurrencyID", idx);
            packet.ReadUInt32("Quantity", idx);
            packet.ReadByte("LootListId", idx);

            packet.ResetBitReader();

            packet.ReadBits("UiType", 3, idx);
        }

        [Parser(Opcode.SMSG_LOOT_RESPONSE)]
        public static void HandleLootResponse(Packet packet)
        {
            packet.ReadPackedGuid128("Owner");
            packet.ReadPackedGuid128("LootObj");
            packet.ReadByteE<LootError>("FailureReason");
            packet.ReadByteE<LootType>("AcquireReason");
            packet.ReadByteE<LootMethod>("LootMethod");
            packet.ReadByteE<ItemQuality>("Threshold");

            packet.ReadUInt32("Coins");

            var itemCount = packet.ReadUInt32("ItemCount");
            var currencyCount = packet.ReadUInt32("CurrencyCount");

            packet.ResetBitReader();

            packet.ReadBit("Acquired");
            packet.ReadBit("AELooting");
            packet.ReadBit("Unused_440");

            for (var i = 0; i < itemCount; ++i)
                ReadLootItem(packet, i, "LootItem");

            for (var i = 0; i < currencyCount; ++i)
                ReadCurrenciesData(packet, i, "Currencies");
        }

        [Parser(Opcode.SMSG_LOOT_REMOVED)]
        public static void HandleLootRemoved(Packet packet)
        {
            packet.ReadPackedGuid128("Owner");
            packet.ReadPackedGuid128("LootObj");
            packet.ReadByte("LootListId");
        }

        [Parser(Opcode.SMSG_COIN_REMOVED)]
        public static void HandleCoinRemoved(Packet packet)
        {
            packet.ReadPackedGuid128("LootObj");
        }

        [Parser(Opcode.SMSG_AE_LOOT_TARGETS)]
        public static void HandleClientAELootTargets(Packet packet)
        {
            packet.ReadUInt32("Count");
        }

        [Parser(Opcode.SMSG_LOOT_RELEASE)]
        public static void HandleLootReleaseResponse(Packet packet)
        {
            packet.ReadPackedGuid128("LootObj");
            packet.ReadPackedGuid128("Owner");
        }

        [Parser(Opcode.SMSG_LOOT_MONEY_NOTIFY)]
        public static void HandleLootMoneyNotify(Packet packet)
        {
            packet.ReadUInt64("Money");
            packet.ReadUInt64("Mod");
            packet.ResetBitReader();
            packet.ReadBit("SoleLooter");
        }

        [Parser(Opcode.SMSG_AE_LOOT_TARGET_ACK)]
        [Parser(Opcode.SMSG_LOOT_RELEASE_ALL)]
        public static void HandleLootZero(Packet packet)
        {
        }
    }
}
