using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class LootHandler
    {
        public static void ReadLootItem(Packet packet, params object[] indexes)
        {
            packet.Translator.ResetBitReader();

            packet.Translator.ReadBits("ItemType", 2, indexes);
            packet.Translator.ReadBits("ItemUiType", 3, indexes);
            packet.Translator.ReadBit("CanTradeToTapList", indexes);
            packet.Translator.ReadUInt32("Item Quantity", indexes);
            packet.Translator.ReadByte("LootItemType", indexes);
            packet.Translator.ReadByte("LootListID", indexes);

            ItemHandler.ReadItemInstance(packet, indexes, "ItemInstance");
        }

        public static void ReadCurrenciesData(Packet packet, params object[] idx)
        {
            packet.Translator.ReadUInt32("CurrencyID", idx);
            packet.Translator.ReadUInt32("Quantity", idx);
            packet.Translator.ReadByte("LootListId", idx);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBits("UiType", 3, idx);
        }

        [Parser(Opcode.SMSG_AE_LOOT_TARGET_ACK)]
        [Parser(Opcode.SMSG_LOOT_RELEASE_ALL)]
        public static void HandleLootZero(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_LOOT_UNIT)]
        public static void HandleLoot(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Unit");
        }

        [Parser(Opcode.SMSG_LOOT_MONEY_NOTIFY)]
        public static void HandleLootMoneyNotify(Packet packet)
        {
            packet.Translator.ReadInt32("Money");
            packet.Translator.ReadBit("SoleLooter");
        }

        [Parser(Opcode.CMSG_SET_LOOT_METHOD)]
        public static void HandleLootMethod(Packet packet)
        {
            packet.Translator.ReadByte("PartyIndex");
            packet.Translator.ReadByteE<LootMethod>("Method");
            packet.Translator.ReadPackedGuid128("Master");
            packet.Translator.ReadInt32E<ItemQuality>("Threshold");
        }

        [Parser(Opcode.SMSG_LOOT_REMOVED)]
        public static void HandleLootRemoved(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Loot Owner");
            packet.Translator.ReadPackedGuid128("LootObj");
            packet.Translator.ReadByte("LootListId");
        }

        [Parser(Opcode.CMSG_LOOT_RELEASE)]
        public static void HandleLootRelease(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Object GUID");
        }

        [Parser(Opcode.SMSG_AE_LOOT_TARGETS)]
        public static void HandleClientAELootTargets(Packet packet)
        {
            packet.Translator.ReadUInt32("Count");
        }

        [Parser(Opcode.SMSG_LOOT_RESPONSE)]
        public static void HandleLootResponse(Packet packet) // 6.0.3.19342 sub_6179EA, sub_83C6C7
        {
            //! TODO Doublecheck the fields for this whole packet. I didn't have many different sniffs to name fields.
            packet.Translator.ReadPackedGuid128("Owner");
            packet.Translator.ReadPackedGuid128("LootObj");
            packet.Translator.ReadByteE<LootError>("FailureReason");
            packet.Translator.ReadByteE<LootType>("AcquireReason");
            packet.Translator.ReadByteE<LootMethod>("LootMethod");
            packet.Translator.ReadByteE<ItemQuality>("Threshold");

            packet.Translator.ReadUInt32("Coins");

            var itemCount = packet.Translator.ReadUInt32("ItemCount");
            var currencyCount = packet.Translator.ReadUInt32("CurrencyCount");

            for (var i = 0; i < itemCount; ++i)
                ReadLootItem(packet, i, "LootItem");

            for (var i = 0; i < currencyCount; ++i)
                ReadCurrenciesData(packet, i, "Currencies");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("Acquired");
            packet.Translator.ReadBit("PersonalLooting");
            packet.Translator.ReadBit("AELooting");
        }

        [Parser(Opcode.SMSG_LOOT_LIST)]
        public static void HandleLootList(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Owner");

            var bit48 = packet.Translator.ReadBit("HasMaster");
            var bit72 = packet.Translator.ReadBit("HasRoundRobinWinner");

            if (bit48)
                packet.Translator.ReadPackedGuid128("Master");

            if (bit72)
                packet.Translator.ReadPackedGuid128("RoundRobinWinner");
        }

        [Parser(Opcode.SMSG_LOOT_ROLL)]
        public static void HandleLootRollResponse(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("LootObj");
            packet.Translator.ReadPackedGuid128("Player");

            ReadLootItem(packet, "LootItem");

            packet.Translator.ReadInt32("Roll");
            packet.Translator.ReadByte("RollType");
            packet.Translator.ResetBitReader();
            packet.Translator.ReadBit("Autopassed");
        }

        [Parser(Opcode.SMSG_LOOT_ROLL_WON)]
        public static void HandleLootRollWon(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("LootObj");

            ReadLootItem(packet, "LootItem");

            packet.Translator.ReadPackedGuid128("Player");

            packet.Translator.ReadInt32("Roll");
            packet.Translator.ReadByte("RollType");
        }

        [Parser(Opcode.SMSG_LOOT_ROLLS_COMPLETE)]
        public static void HandleLootRollsComplete(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("LootObj");
            packet.Translator.ReadByte("LootListID");
        }

        [Parser(Opcode.SMSG_START_LOOT_ROLL)]
        public static void HandleStartLootRoll(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("LootObj");
            packet.Translator.ReadInt32("MapID");

            ReadLootItem(packet, "LootItem");

            packet.Translator.ReadInt32("RollTime");
            packet.Translator.ReadByte("ValidRolls");
            packet.Translator.ReadByte("Method");
        }

        [Parser(Opcode.SMSG_LOOT_RELEASE)]
        public static void HandleLootReleaseResponse(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("LootObj");
            packet.Translator.ReadPackedGuid128("Owner");
        }

        [Parser(Opcode.CMSG_LOOT_ROLL)]
        public static void HandleLootRoll(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("LootObj");
            packet.Translator.ReadByte("LootListID");
            packet.Translator.ReadByteE<LootRollType>("RollType");
        }

        [Parser(Opcode.SMSG_LOOT_ALL_PASSED)]
        public static void HandleLootAllPassed(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("LootObj");
            ReadLootItem(packet, "LootItem");
        }
    }
}
