using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
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

        [Parser(Opcode.SMSG_AE_LOOT_TARGETS)]
        public static void HandleClientAELootTargets(Packet packet)
        {
            packet.ReadUInt32("Count");
        }

        [Parser(Opcode.SMSG_LEGACY_LOOT_RULES)]
        public static void HandleLegacyLootRules(Packet packet)
        {
            packet.ReadBit("LegacyRulesActive");
        }

        [Parser(Opcode.SMSG_LOOT_ALL_PASSED)]
        public static void HandleLootAllPassed(Packet packet)
        {
            packet.ReadPackedGuid128("LootObj");
            packet.ReadInt32("DungeonEncounterID");
            ReadLootItem(packet, "LootItem");
        }

        [Parser(Opcode.SMSG_LOOT_LIST)]
        public static void HandleLootList(Packet packet)
        {
            packet.ReadPackedGuid128("Owner");
            packet.ReadPackedGuid128("LootObj");

            var hasMaster = packet.ReadBit("HasMaster");
            var hasRoundRobin = packet.ReadBit("HasRoundRobinWinner");

            if (hasMaster)
                packet.ReadPackedGuid128("Master");

            if (hasRoundRobin)
                packet.ReadPackedGuid128("RoundRobinWinner");
        }

        [Parser(Opcode.SMSG_LOOT_MONEY_NOTIFY)]
        public static void HandleLootMoneyNotify(Packet packet)
        {
            packet.ReadUInt64("Money");
            packet.ReadUInt64("Mod");
            packet.ResetBitReader();
            packet.ReadBit("SoleLooter");
        }

        [Parser(Opcode.SMSG_LOOT_RELEASE)]
        public static void HandleLootReleaseResponse(Packet packet)
        {
            packet.ReadPackedGuid128("LootObj");
            packet.ReadPackedGuid128("Owner");
        }

        [Parser(Opcode.SMSG_LOOT_REMOVED)]
        public static void HandleLootRemoved(Packet packet)
        {
            packet.ReadPackedGuid128("Owner");
            packet.ReadPackedGuid128("LootObj");
            packet.ReadByte("LootListId");
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

        [Parser(Opcode.SMSG_LOOT_ROLL)]
        public static void HandleLootRollServer(Packet packet)
        {
            packet.ReadPackedGuid128("LootObj");
            packet.ReadPackedGuid128("Winner");
            packet.ReadInt32("Roll");
            packet.ReadByte("RollType");
            packet.ReadInt32("DungeonEncounterID");
            ReadLootItem(packet, "LootItem");
            packet.ResetBitReader();
            packet.ReadBit("Autopassed");
            packet.ReadBit("OffSpec");
        }

        [Parser(Opcode.SMSG_LOOT_ROLLS_COMPLETE)]
        public static void HandleLootRollsComplete(Packet packet)
        {
            packet.ReadPackedGuid128("LootObj");
            packet.ReadByte("LootListID");
            packet.ReadInt32("DungeonEncounterID");
        }

        [Parser(Opcode.SMSG_LOOT_ROLL_WON)]
        public static void HandleLootRollWon(Packet packet)
        {
            packet.ReadPackedGuid128("LootObj");
            packet.ReadPackedGuid128("Player");
            packet.ReadInt32("Roll");
            packet.ReadByte("RollType");
            packet.ReadInt32("DungeonEncounterID");
            ReadLootItem(packet, "LootItem");
            packet.ReadBit("MainSpec");
        }

        [Parser(Opcode.SMSG_MASTER_LOOT_CANDIDATE_LIST)]
        public static void HandleMasterLootCandidateList(Packet packet)
        {
            packet.ReadPackedGuid128("LootObj");
            var candidateCount = packet.ReadUInt32("CandidateCount");

            for (var i = 0; i < candidateCount; ++i)
                packet.ReadPackedGuid128("Player", i);
        }

        [Parser(Opcode.SMSG_START_LOOT_ROLL)]
        public static void HandleLootStartRoll(Packet packet)
        {
            packet.ReadPackedGuid128("LootObj");
            packet.ReadInt32<MapId>("MapID");
            packet.ReadUInt32("RollTime");
            packet.ReadByte("ValidRolls");

            for (var i = 0; i < 3; i++)
                packet.ReadUInt32E<LootRollIneligibilityReason>("LootRollIneligibleReason");

            packet.ReadByteE<LootMethod>("Method");

            packet.ReadInt32("DungeonEncounterID");
            ReadLootItem(packet, "LootItem");
        }

        [Parser(Opcode.CMSG_LOOT_ITEM)]
        public static void HandleAutoStoreLootItem(Packet packet)
        {
            var count = packet.ReadUInt32("Count");

            for (var i = 0; i < count; ++i)
            {
                packet.ReadPackedGuid128("Object", i);
                packet.ReadByte("LootListID", i);
            }

            packet.ReadBit("IsSoftInteract");
        }

        [Parser(Opcode.CMSG_LOOT_MONEY)]
        public static void HandleLootMoney(Packet packet)
        {
            packet.ReadBit("IsSoftInteract");
        }

        [Parser(Opcode.CMSG_LOOT_RELEASE)]
        public static void HandleLootRelease(Packet packet)
        {
            packet.ReadPackedGuid128("ObjectGUID");
        }

        [Parser(Opcode.CMSG_LOOT_ROLL)]
        public static void HandleLootRoll(Packet packet)
        {
            packet.ReadPackedGuid128("LootObj");
            packet.ReadByte("LootListID");
            packet.ReadByteE<LootRollType>("RollType");
        }

        [Parser(Opcode.CMSG_LOOT_UNIT)]
        public static void HandleLoot(Packet packet)
        {
            packet.ReadPackedGuid128("Unit");
        }

        [Parser(Opcode.CMSG_MASTER_LOOT_ITEM)]
        public static void HandleMasterLootItem(Packet packet)
        {
            var count = packet.ReadUInt32("Count");
            packet.ReadPackedGuid128("Target");

            for (var i = 0; i < count; ++i)
            {
                packet.ReadPackedGuid128("Object", i);
                packet.ReadByte("LootListID", i);
            }
        }

        [Parser(Opcode.CMSG_OPT_OUT_OF_LOOT)]
        public static void HandleOptOutOfLoot(Packet packet)
        {
            packet.ReadBool("PassOnLoot");
        }

        [Parser(Opcode.SMSG_AE_LOOT_TARGET_ACK)]
        [Parser(Opcode.SMSG_LOOT_RELEASE_ALL)]
        public static void HandleLootZero(Packet packet)
        {
        }
    }
}
