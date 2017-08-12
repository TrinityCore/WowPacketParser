﻿using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class LootHandler
    {
        public static void ReadLootItem(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();

            packet.ReadBits("ItemType", 2, indexes);
            packet.ReadBits("ItemUiType", 3, indexes);
            packet.ReadBit("CanTradeToTapList", indexes);

            V6_0_2_19033.Parsers.ItemHandler.ReadItemInstance(packet, indexes, "ItemInstance");

            packet.ReadUInt32("Quantity", indexes);
            packet.ReadByte("LootItemType", indexes);
            packet.ReadByte("LootListID", indexes);
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
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V7_2_0_23826))
                packet.ReadBit("PersonalLooting");

            for (var i = 0; i < itemCount; ++i)
                ReadLootItem(packet, i, "LootItem");

            for (var i = 0; i < currencyCount; ++i)
                V6_0_2_19033.Parsers.LootHandler.ReadCurrenciesData(packet, i, "Currencies");
        }

        [Parser(Opcode.SMSG_START_LOOT_ROLL)]
        public static void HandleLootStartRoll(Packet packet)
        {
            packet.ReadPackedGuid128("LootObj");
            packet.ReadInt32<MapId>("MapID");
            packet.ReadUInt32("RollTime");
            packet.ReadByte("ValidRolls");
            packet.ReadByteE<LootMethod>("Method");
            ReadLootItem(packet, "LootItem");
        }

        [Parser(Opcode.SMSG_LOOT_ROLL)]
        public static void HandleLootRollServer(Packet packet)
        {
            packet.ReadPackedGuid128("LootObj");
            packet.ReadPackedGuid128("Player");
            packet.ReadInt32("Roll");
            packet.ReadByte("RollType");
            ReadLootItem(packet, "LootItem");
            packet.ReadBit("Autopassed");
        }

        [Parser(Opcode.SMSG_LOOT_ROLL_WON)]
        public static void HandleLootRollWon(Packet packet)
        {
            packet.ReadPackedGuid128("LootObj");
            packet.ReadPackedGuid128("Player");
            packet.ReadInt32("Roll");
            packet.ReadByte("RollType");
            ReadLootItem(packet, "LootItem");
            packet.ReadBit("MainSpec");
        }

        [Parser(Opcode.SMSG_LOOT_ALL_PASSED)]
        public static void HandleLootAllPassed(Packet packet)
        {
            packet.ReadPackedGuid128("LootObj");
            ReadLootItem(packet, "LootItem");
        }

        [Parser(Opcode.SMSG_LOOT_LIST, ClientVersionBuild.V7_2_0_23826)]
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
    }
}
