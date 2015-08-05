using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class LootHandler
    {
        public static void ReadLootItem(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();

            packet.ReadBits("ItemType", 2, indexes);
            packet.ReadBits("ItemUiType", 3, indexes);
            packet.ReadBit("CanTradeToTapList", indexes);
            packet.ReadUInt32("Item Quantity", indexes);
            packet.ReadByte("LootItemType", indexes);
            packet.ReadByte("LootListID", indexes);

            ItemHandler.ReadItemInstance(packet, indexes, "ItemInstance");
        }

        [Parser(Opcode.SMSG_AE_LOOT_TARGET_ACK)]
        [Parser(Opcode.SMSG_LOOT_RELEASE_ALL)]
        public static void HandleLootZero(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_LOOT_UNIT)]
        public static void HandleLoot(Packet packet)
        {
            packet.ReadPackedGuid128("Unit");
        }

        [Parser(Opcode.SMSG_LOOT_MONEY_NOTIFY)]
        public static void HandleLootMoneyNotify(Packet packet)
        {
            packet.ReadInt32("Money");
            packet.ReadBit("SoleLooter");
        }

        [Parser(Opcode.CMSG_SET_LOOT_METHOD)]
        public static void HandleLootMethod(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadByteE<LootMethod>("Method");
            packet.ReadPackedGuid128("Master");
            packet.ReadInt32E<ItemQuality>("Threshold");
        }

        [Parser(Opcode.SMSG_LOOT_REMOVED)]
        public static void HandleLootRemoved(Packet packet)
        {
            packet.ReadPackedGuid128("Loot Owner");
            packet.ReadPackedGuid128("LootObj");
            packet.ReadByte("LootListId");
        }

        [Parser(Opcode.CMSG_LOOT_RELEASE)]
        public static void HandleLootRelease(Packet packet)
        {
            packet.ReadPackedGuid128("Object GUID");
        }

        [Parser(Opcode.SMSG_AE_LOOT_TARGETS)]
        public static void HandleClientAELootTargets(Packet packet)
        {
            packet.ReadUInt32("Count");
        }

        [Parser(Opcode.SMSG_LOOT_RESPONSE)]
        public static void HandleLootResponse(Packet packet) // 6.0.3.19342 sub_6179EA, sub_83C6C7
        {
            //! TODO Doublecheck the fields for this whole packet. I didn't have many different sniffs to name fields.
            packet.ReadPackedGuid128("Owner");
            packet.ReadPackedGuid128("LootObj");
            packet.ReadByteE<LootError>("FailureReason");
            packet.ReadByteE<LootType>("AcquireReason");
            packet.ReadByteE<LootMethod>("LootMethod");
            packet.ReadByteE<ItemQuality>("Threshold");

            packet.ReadUInt32("Coins");

            var itemCount = packet.ReadUInt32("ItemCount");
            var currencyCount = packet.ReadUInt32("CurrencyCount");

            for (var i = 0; i < itemCount; ++i)
                ReadLootItem(packet, i, "LootItem");

            for (var i = 0; i < currencyCount; ++i)
            {
                // Order guessed
                packet.ReadUInt32("CurrencyID", i);
                packet.ReadUInt32("Quantity", i);
                packet.ReadByte("LootListId", i);

                packet.ResetBitReader();

                packet.ReadBits("UiType", 3, i);
            }

            packet.ResetBitReader();

            packet.ReadBit("Acquired");
            packet.ReadBit("PersonalLooting");
            packet.ReadBit("AELooting");
        }

        [Parser(Opcode.SMSG_LOOT_LIST)]
        public static void HandleLootList(Packet packet)
        {
            packet.ReadPackedGuid128("Owner");

            var bit48 = packet.ReadBit("HasMaster");
            var bit72 = packet.ReadBit("HasRoundRobinWinner");

            if (bit48)
                packet.ReadPackedGuid128("Master");

            if (bit72)
                packet.ReadPackedGuid128("RoundRobinWinner");
        }

        [Parser(Opcode.SMSG_LOOT_ROLL)]
        public static void HandleLootRollResponse(Packet packet)
        {
            packet.ReadPackedGuid128("LootObj");
            packet.ReadPackedGuid128("Player");

            ReadLootItem(packet, "LootItem");

            packet.ReadInt32("Roll");
            packet.ReadByte("RollType");
            packet.ResetBitReader();
            packet.ReadBit("Autopassed");
        }

        [Parser(Opcode.SMSG_LOOT_ROLL_WON)]
        public static void HandleLootRollWon(Packet packet)
        {
            packet.ReadPackedGuid128("LootObj");

            ReadLootItem(packet, "LootItem");

            packet.ReadPackedGuid128("Player");

            packet.ReadInt32("Roll");
            packet.ReadByte("RollType");
        }

        [Parser(Opcode.SMSG_LOOT_ROLLS_COMPLETE)]
        public static void HandleLootRollsComplete(Packet packet)
        {
            packet.ReadPackedGuid128("LootObj");
            packet.ReadByte("LootListID");
        }

        [Parser(Opcode.SMSG_START_LOOT_ROLL)]
        public static void HandleStartLootRoll(Packet packet)
        {
            packet.ReadPackedGuid128("LootObj");
            packet.ReadInt32("MapID");

            ReadLootItem(packet, "LootItem");

            packet.ReadInt32("RollTime");
            packet.ReadByte("ValidRolls");
            packet.ReadByte("Method");
        }

        [Parser(Opcode.SMSG_LOOT_RELEASE)]
        public static void HandleLootReleaseResponse(Packet packet)
        {
            packet.ReadPackedGuid128("LootObj");
            packet.ReadPackedGuid128("Owner");
        }

        [Parser(Opcode.CMSG_LOOT_ROLL)]
        public static void HandleLootRoll(Packet packet)
        {
            packet.ReadPackedGuid128("LootObj");
            packet.ReadByte("LootListID");
            packet.ReadByteE<LootRollType>("RollType");
        }

        [Parser(Opcode.SMSG_LOOT_ALL_PASSED)]
        public static void HandleLootAllPassed(Packet packet)
        {
            packet.ReadPackedGuid128("LootObj");
            ReadLootItem(packet, "LootItem");
        }
    }
}
