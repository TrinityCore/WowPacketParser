using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class LootHandler
    {
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
            packet.ReadPackedGuid128("LootObj");
            packet.ReadPackedGuid128("Owner");
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

                ItemHandler.ReadItemInstance(ref packet, i);
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
    }
}