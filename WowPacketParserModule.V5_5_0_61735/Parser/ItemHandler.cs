using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class ItemHandler
    {
        public static void ReadUIEventToast(Packet packet, params object[] args)
        {
            packet.ReadInt32("UiEventToastID", args);
            packet.ReadInt32("Asset", args);
        }

        public static void ReadItemPurchaseRefundItem(Packet packet, params object[] indexes)
        {
            packet.ReadInt32<ItemId>("ItemID", indexes);
            packet.ReadInt32("ItemCount", indexes);
        }

        public static void ReadItemPurchaseRefundCurrency(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("CurrencyID", indexes);
            packet.ReadInt32("CurrencyCount", indexes);
        }

        public static void ReadItemPurchaseContents(Packet packet, params object[] indexes)
        {
            packet.ReadUInt64("Money");

            for (int i = 0; i < 5; i++)
                ReadItemPurchaseRefundItem(packet, indexes, i, "ItemPurchaseRefundItem");

            for (int i = 0; i < 5; i++)
                ReadItemPurchaseRefundCurrency(packet, indexes, i, "ItemPurchaseRefundCurrency");
        }

        [Parser(Opcode.SMSG_REFORGE_RESULT)]
        public static void HandleItemReforgeResult(Packet packet)
        {
            packet.ReadBit("Successful");
        }

        [Parser(Opcode.SMSG_ITEM_PURCHASE_REFUND_RESULT)]
        public static void HandleItemPurchaseRefundResult(Packet packet)
        {
            packet.ReadPackedGuid128("ItemGUID");
            packet.ReadByte("Result");
            var hasContents = packet.ReadBit("HasContents");
            packet.ResetBitReader();

            if (hasContents)
                ReadItemPurchaseContents(packet, "Contents");
        }

        [Parser(Opcode.SMSG_SET_ITEM_PURCHASE_DATA)]
        public static void HandleSetItemPurchaseData(Packet packet)
        {
            packet.ReadPackedGuid128("ItemGUID");

            ReadItemPurchaseContents(packet, "ItemPurchaseContents");

            packet.ReadInt32("Flags");
            packet.ReadInt32("PurchaseTime");
        }

        [Parser(Opcode.SMSG_ITEM_EXPIRE_PURCHASE_REFUND)]
        public static void HandleItemExpirePurchaseRefund(Packet packet)
        {
            packet.ReadPackedGuid128("ItemGUID");
        }

        [Parser(Opcode.SMSG_ADD_ITEM_PASSIVE)]
        [Parser(Opcode.SMSG_REMOVE_ITEM_PASSIVE)]
        public static void HandleItemPassive(Packet packet)
        {
            packet.ReadUInt32<SpellId>("SpellID");
        }

        [Parser(Opcode.SMSG_SEND_ITEM_PASSIVES)]
        public static void HandleSendItemPassives(Packet packet)
        {
            var spellCount = packet.ReadUInt32("SpellCount");

            for (var i = 0; i < spellCount; ++i)
                packet.ReadInt32("SpellID", i);
        }

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
            packet.ReadByte("BattlePetBreedQuality");

            packet.ReadUInt32("BattlePetLevel");

            packet.ReadPackedGuid128("ItemGUID");

            packet.ResetBitReader();

            packet.ReadBit("Pushed");
            packet.ReadBit("Created");
            packet.ReadBit("Unused440");
            packet.ReadBits("DisplayText", 3);
            packet.ReadBit("IsBonusRoll");
            packet.ReadBit("IsEncounterLoot");

            Substructures.ItemHandler.ReadItemInstance(packet);
        }

        [Parser(Opcode.SMSG_CROSSED_INEBRIATION_THRESHOLD)]
        public static void HandleCrossedInebriationThreshold(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadInt32("Threshold");
            packet.ReadInt32<ItemId>("ItemID");
        }

        [Parser(Opcode.SMSG_SELL_RESPONSE)]
        public static void HandleSellResponse(Packet packet)
        {
            packet.ReadPackedGuid128("VendorGUID");
            var itemGuidCount = packet.ReadUInt32("ItemGuidCount");

            packet.ReadInt32E<SellResult>("Reason");

            for (var i = 0; i < itemGuidCount; ++i)
                packet.ReadPackedGuid128("ItemGuid", i);
        }

        [Parser(Opcode.SMSG_BUY_SUCCEEDED)]
        public static void HandleBuyItemResponse(Packet packet)
        {
            packet.ReadPackedGuid128("VendorGUID");
            packet.ReadUInt32("Muid");
            packet.ReadInt32("NewQuantity");
            packet.ReadUInt32("QuantityBought");
        }

        [Parser(Opcode.SMSG_BUY_FAILED)]
        public static void HandleBuyFailed(Packet packet)
        {
            packet.ReadPackedGuid128("VendorGUID");
            packet.ReadUInt32<ItemId>("Muid");
            packet.ReadInt32E<BuyResult>("Reason");
        }

        [Parser(Opcode.SMSG_ITEM_CHANGED)]
        public static void HandleItemChanged(Packet packet)
        {
            packet.ReadPackedGuid128("Player");
            Substructures.ItemHandler.ReadItemInstance(packet, "ItemInstanceBefore");
            Substructures.ItemHandler.ReadItemInstance(packet, "ItemInstanceAfter");
        }

        [Parser(Opcode.SMSG_ENCHANTMENT_LOG)]
        public static void HandleEnchantmentLog(Packet packet)
        {
            packet.ReadPackedGuid128("Owner");
            packet.ReadPackedGuid128("Caster");
            packet.ReadPackedGuid128("ItemGUID");
            packet.ReadUInt32<ItemId>("ItemID");
            packet.ReadUInt32("Enchantment");
            packet.ReadUInt32("EnchantSlot");
        }

        [Parser(Opcode.SMSG_SOCKET_GEMS_SUCCESS)]
        public static void HandleSocketGemsSuccess(Packet packet)
        {
            packet.ReadPackedGuid128("Item");
        }

        [Parser(Opcode.SMSG_SET_PROFICIENCY)]
        public static void HandleSetProficency(Packet packet)
        {
            packet.ReadUInt32E<UnknownFlags>("ProficiencyMask");
            packet.ReadByteE<ItemClass>("ProficiencyClass");
        }

        [Parser(Opcode.SMSG_ITEM_TIME_UPDATE)]
        public static void HandleItemTimeUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("GUID");
            packet.ReadUInt32("DurationLeft");
        }

        [Parser(Opcode.SMSG_ITEM_ENCHANT_TIME_UPDATE)]
        public static void HandleItemEnchantTimeUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("Item Guid");
            packet.ReadUInt32("DurationLeft");
            packet.ReadUInt32("Slot");
            packet.ReadPackedGuid128("Player Guid");
        }

        [Parser(Opcode.SMSG_READ_ITEM_RESULT_OK)]
        public static void HandleReadItemResultOk(Packet packet)
        {
            packet.ReadPackedGuid128("Item");
        }

        [Parser(Opcode.SMSG_READ_ITEM_RESULT_FAILED)]
        public static void HandleReadItemResultFailed(Packet packet)
        {
            packet.ReadPackedGuid128("ItemGUID");
            packet.ReadUInt32("Delay");
            packet.ReadBits("Subcode", 2);
        }

        [Parser(Opcode.SMSG_ITEM_COOLDOWN)]
        public static void HandleItemCooldown(Packet packet)
        {
            packet.ReadPackedGuid128("ItemGuid");
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadInt32("Cooldown");
        }

        [Parser(Opcode.SMSG_INVENTORY_FULL_OVERFLOW)]
        public static void HandleItemZero(Packet packet)
        {
        }
    }
}
