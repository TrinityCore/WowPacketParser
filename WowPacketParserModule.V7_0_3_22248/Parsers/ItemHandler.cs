using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class ItemHandler
    {
        [Parser(Opcode.SMSG_ITEM_PUSH_RESULT)]
        public static void HandleItemPushResult(Packet packet)
        {
            packet.ReadPackedGuid128("PlayerGUID");

            packet.ReadByte("Slot");

            packet.ReadInt32("SlotInBag");

            packet.ReadUInt32("QuestLogItemID");
            packet.ReadUInt32("Quantity");
            packet.ReadUInt32("QuantityInInventory");
            packet.ReadInt32("DungeonEncounterID");

            packet.ReadUInt32("BattlePetSpeciesID");
            packet.ReadUInt32("BattlePetBreedID");
            packet.ReadUInt32("BattlePetBreedQuality");
            packet.ReadUInt32("BattlePetLevel");

            packet.ReadPackedGuid128("ItemGUID");

            packet.ResetBitReader();

            packet.ReadBit("Pushed");
            packet.ReadBit("Created");
            packet.ReadBits("DisplayText", 3);
            packet.ReadBit("IsBonusRoll");
            packet.ReadBit("IsEncounterLoot");

            Substructures.ItemHandler.ReadItemInstance(packet, "ItemInstance");
        }

        [Parser(Opcode.CMSG_USE_ITEM)]
        public static void HandleUseItem(Packet packet)
        {
            var useItem = packet.Holder.ClientUseItem = new();
            useItem.PackSlot = packet.ReadByte("PackSlot");
            useItem.ItemSlot = packet.ReadByte("Slot");
            useItem.CastItem = packet.ReadPackedGuid128("CastItem");

            useItem.SpellId = SpellHandler.ReadSpellCastRequest(packet, "Cast");
        }

        [Parser(Opcode.CMSG_USE_TOY)]
        public static void HandleUseToy(Packet packet)
        {
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V7_3_2_25383))
                packet.ReadInt32<ItemId>("ItemID");

            SpellHandler.ReadSpellCastRequest(packet, "Cast");
        }

        [Parser(Opcode.CMSG_BUY_ITEM)]
        public static void HandleBuyItem(Packet packet)
        {
            packet.ReadPackedGuid128("VendorGUID");
            packet.ReadPackedGuid128("ContainerGUID");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V7_2_0_23826))
                Substructures.ItemHandler.ReadItemInstance(packet, "ItemInstance");

            packet.ReadInt32("Quantity");
            packet.ReadUInt32("Muid");
            packet.ReadUInt32("Slot");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_1_5_50232))
                packet.ReadInt32("ItemType");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_2_0_23826))
                Substructures.ItemHandler.ReadItemInstance(packet, "ItemInstance");

            packet.ResetBitReader();

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V10_1_5_50232))
            {
                var itemTypeBits = 2;
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_0_1_36216))
                    itemTypeBits = 3;
                packet.ReadBits("ItemType", itemTypeBits);
            }
        }

        [Parser(Opcode.SMSG_SOCKET_GEMS_SUCCESS)]
        public static void HandleSocketGemsSuccess(Packet packet)
        {
            packet.ReadPackedGuid128("Item");
        }

        public static void ReadItemPurchaseContents(Packet packet, params object[] indexes)
        {
            packet.ReadUInt64("Money");

            for (int i = 0; i < 5; i++)
                V6_0_2_19033.Parsers.ItemHandler.ReadItemPurchaseRefundItem(packet, indexes, i, "ItemPurchaseRefundItem");

            for (int i = 0; i < 5; i++)
                V6_0_2_19033.Parsers.ItemHandler.ReadItemPurchaseRefundCurrency(packet, indexes, i, "ItemPurchaseRefundCurrency");
        }

        [Parser(Opcode.SMSG_SET_ITEM_PURCHASE_DATA)]
        public static void HandleSetItemPurchaseData(Packet packet)
        {
            packet.ReadPackedGuid128("ItemGUID");

            ReadItemPurchaseContents(packet, "ItemPurchaseContents");

            packet.ReadInt32("Flags");
            packet.ReadInt32("PurchaseTime");
        }

        [Parser(Opcode.SMSG_ITEM_CHANGED)]
        public static void HandleItemChanged(Packet packet)
        {
            packet.ReadPackedGuid128("Player");
            Substructures.ItemHandler.ReadItemInstance(packet, "ItemInstanceBefore");
            Substructures.ItemHandler.ReadItemInstance(packet, "ItemInstanceAfter");
        }
    }
}
