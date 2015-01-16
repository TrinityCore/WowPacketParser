using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class LootHandler
    {
        [Parser(Opcode.SMSG_AE_LOOT_TARGET_ACK)]
        public static void HandleLootZero(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_LOOT)]
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

        [Parser(Opcode.CMSG_LOOT_METHOD)]
        public static void HandleLootMethod(Packet packet)
        {
            packet.ReadEnum<LootMethod>("Method", TypeCode.Byte);
            packet.ReadByte("PartyIndex");
            packet.ReadPackedGuid128("Master");
            packet.ReadEnum<ItemQuality>("Threshold", TypeCode.Int32);
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
            // Order guessed
            packet.ReadByte("Threshold");
            packet.ReadByte("LootMethod");
            packet.ReadByte("AcquireReason");
            packet.ReadByte("FailureReason");

            packet.ReadUInt32("Coins");

            var itemCount = packet.ReadUInt32("ItemCount");
            var currencyCount = packet.ReadUInt32("CurrencyCount");

            for (var i = 0; i < itemCount; ++i)
            {
                // Guessed order
                packet.ResetBitReader();

                packet.ReadBits("ItemType", 2, i);
                packet.ReadBits("ItemUiType", 3, i);
                packet.ReadBit("CanTradeToTapList", i);
                packet.ReadUInt32("Item Quantity", i);
                packet.ReadByte("LootItemType", i);
                packet.ReadByte("LootListID", i);

                ItemHandler.ReadItemInstance(packet, i);
            }

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

            // Order guessed
            packet.ReadBit("PersonalLooting");
            packet.ReadBit("Acquired");
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

            packet.ResetBitReader();
            packet.ReadBits("ItemType", 2);
            packet.ReadBits("ItemUiType", 3);
            packet.ReadBit("CanTradeToTapList");
            packet.ReadUInt32("Item Quantity");
            packet.ReadByte("LootItemType");
            packet.ReadByte("LootListID");
            ItemHandler.ReadItemInstance(packet);

            packet.ReadInt32("Roll");
            packet.ReadByte("RollType");
            packet.ResetBitReader();
            packet.ReadBit("Autopassed");
        }

        [Parser(Opcode.SMSG_LOOT_ROLL_WON)]
        public static void HandleLootRollWon(Packet packet)
        {
            packet.ReadPackedGuid128("LootObj");

            packet.ResetBitReader();
            packet.ReadBits("ItemType", 2);
            packet.ReadBits("ItemUiType", 3);
            packet.ReadBit("CanTradeToTapList");
            packet.ReadUInt32("Item Quantity");
            packet.ReadByte("LootItemType");
            packet.ReadByte("LootListID");
            ItemHandler.ReadItemInstance(packet);

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

            packet.ResetBitReader();
            packet.ReadBits("ItemType", 2);
            packet.ReadBits("ItemUiType", 3);
            packet.ReadBit("CanTradeToTapList");
            packet.ReadUInt32("Item Quantity");
            packet.ReadByte("LootItemType");
            packet.ReadByte("LootListID");
            ItemHandler.ReadItemInstance(packet);

            packet.ReadInt32("RollTime");
            packet.ReadByte("ValidRolls");
            packet.ReadByte("Method");
        }
    }
}
