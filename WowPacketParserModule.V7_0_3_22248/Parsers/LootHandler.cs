using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class LootHandler
    {
        public static void ReadLootItem(Packet packet, params object[] indexes)
        {
            packet.Translator.ResetBitReader();

            packet.Translator.ReadBits("ItemType", 2, indexes);
            packet.Translator.ReadBits("ItemUiType", 3, indexes);
            packet.Translator.ReadBit("CanTradeToTapList", indexes);

            V6_0_2_19033.Parsers.ItemHandler.ReadItemInstance(packet, indexes, "ItemInstance");

            packet.Translator.ReadUInt32("Quantity", indexes);
            packet.Translator.ReadByte("LootItemType", indexes);
            packet.Translator.ReadByte("LootListID", indexes);
        }

        [Parser(Opcode.SMSG_LOOT_RESPONSE)]
        public static void HandleLootResponse(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Owner");
            packet.Translator.ReadPackedGuid128("LootObj");
            packet.Translator.ReadByteE<LootError>("FailureReason");
            packet.Translator.ReadByteE<LootType>("AcquireReason");
            packet.Translator.ReadByteE<LootMethod>("LootMethod");
            packet.Translator.ReadByteE<ItemQuality>("Threshold");

            packet.Translator.ReadUInt32("Coins");

            var itemCount = packet.Translator.ReadUInt32("ItemCount");
            var currencyCount = packet.Translator.ReadUInt32("CurrencyCount");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("Acquired");
            packet.Translator.ReadBit("AELooting");
            packet.Translator.ReadBit("PersonalLooting");

            for (var i = 0; i < itemCount; ++i)
                ReadLootItem(packet, i, "LootItem");

            for (var i = 0; i < currencyCount; ++i)
                V6_0_2_19033.Parsers.LootHandler.ReadCurrenciesData(packet, i, "Currencies");
        }

        [Parser(Opcode.SMSG_START_LOOT_ROLL)]
        public static void HandleLootStartRoll(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("LootObj");
            packet.Translator.ReadInt32<MapId>("MapID");
            packet.Translator.ReadUInt32("RollTime");
            packet.Translator.ReadByte("ValidRolls");
            packet.Translator.ReadByteE<LootMethod>("Method");
            ReadLootItem(packet, "LootItem");
        }

        [Parser(Opcode.SMSG_LOOT_ROLL)]
        public static void HandleLootRollServer(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("LootObj");
            packet.Translator.ReadPackedGuid128("Player");
            packet.Translator.ReadInt32("Roll");
            packet.Translator.ReadByte("RollType");
            ReadLootItem(packet, "LootItem");
            packet.Translator.ReadBit("Autopassed");
        }

        [Parser(Opcode.SMSG_LOOT_ROLL_WON)]
        public static void HandleLootRollWon(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("LootObj");
            packet.Translator.ReadPackedGuid128("Player");
            packet.Translator.ReadInt32("Roll");
            packet.Translator.ReadByte("RollType");
            ReadLootItem(packet, "LootItem");
            packet.Translator.ReadBit("MainSpec");
        }

        [Parser(Opcode.SMSG_LOOT_ALL_PASSED)]
        public static void HandleLootAllPassed(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("LootObj");
            ReadLootItem(packet, "LootItem");
        }
    }
}
