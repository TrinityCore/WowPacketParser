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

        [Parser(Opcode.SMSG_INVENTORY_CHANGE_FAILURE)]
        public static void HandleInventoryChangeFailure(Packet packet)
        {
            var result = packet.ReadInt32E<InventoryResult440>("BagResult");

            for (int i = 0; i < 2; i++)
                packet.ReadPackedGuid128("Item", i);

            packet.ReadByte("ContainerBSlot");


            switch (result)
            {
                case InventoryResult440.CantEquipLevel:
                case InventoryResult440.PurchaseLevelTooLow:
                {
                    packet.ReadInt32("Level");
                    break;
                }
                case InventoryResult440.EventAutoEquipBindConfirm:
                {
                    packet.ReadPackedGuid128("SrcContainer");
                    packet.ReadInt32("SrcSlot");
                    packet.ReadPackedGuid128("DstContainer");
                    break;
                }
                case InventoryResult440.ItemMaxLimitCategoryCountExceededIs:
                case InventoryResult440.ItemMaxLimitCategorySocketedExceededIs:
                case InventoryResult440.ItemMaxLimitCategoryEquippedExceededIs:
                {
                    packet.ReadInt32("LimitCategory");
                    break;
                }
            }
        }

        [Parser(Opcode.CMSG_REFORGE_ITEM)]
        public static void HandleReforgeItem(Packet packet)
        {
            packet.ReadPackedGuid128("ReforgerGUID");
            packet.ReadInt32("ContainerId");
            packet.ReadInt32("SlotNum");
            packet.ReadInt32("ItemReforgeRecId");
        }

        [Parser(Opcode.CMSG_MAKE_CONTITIONAL_APPEARANCE_PERMANENT)]
        public static void HandleMakeContitionalAppearancePermanent(Packet packet)
        {
            packet.ReadInt32("ItemModifiedAppearanceID");
        }

        [Parser(Opcode.CMSG_USE_CRITTER_ITEM)]
        public static void HandleUseCritterItem(Packet packet)
        {
            packet.ReadPackedGuid128("ItemGUID");
        }

        [Parser(Opcode.CMSG_DESTROY_ITEM)]
        public static void HandleDestroyItem(Packet packet)
        {
            packet.ReadUInt32("Count");
            packet.ReadByte("ContainerId");
            packet.ReadByte("SlotNum");
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

        [Parser(Opcode.CMSG_ADD_TOY)]
        public static void HandleAddToy(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.CMSG_USE_TOY)]
        public static void HandleUseToy(Packet packet)
        {
            SpellHandler.ReadSpellCastRequest(packet, "Cast");
        }

        [Parser(Opcode.CMSG_ITEM_TEXT_QUERY)]
        public static void HandleItemTextQuery(Packet packet)
        {
            packet.ReadPackedGuid128("ItemGUID");
        }

        [Parser(Opcode.CMSG_OPEN_ITEM)]
        public static void HandleOpenItem(Packet packet)
        {
            packet.ReadByte("Slot");
            packet.ReadByte("PackSlot");
        }

        [Parser(Opcode.CMSG_READ_ITEM)]
        public static void HandleReadItem(Packet packet)
        {
            packet.ReadByte("PackSlot");
            packet.ReadByte("Slot");
        }

        [Parser(Opcode.CMSG_SET_INSERT_ITEMS_LEFT_TO_RIGHT)]
        public static void HandleSetInsertItemsLeftToRight(Packet packet)
        {
            packet.ReadBit("Enable");
        }

        [Parser(Opcode.CMSG_REMOVE_NEW_ITEM)]
        public static void HandleRemoveNewItem(Packet packet)
        {
            packet.ReadPackedGuid128("ItemGUID");
        }

        [Parser(Opcode.CMSG_AZERITE_EMPOWERED_ITEM_VIEWED)]
        public static void HandleItemAzerithEmpoweredItemViewed(Packet packet)
        {
            packet.ReadPackedGuid128("ItemGUID");
        }

        [Parser(Opcode.CMSG_AZERITE_EMPOWERED_ITEM_SELECT_POWER)]
        public static void HandleItemAzerithEmpoweredItemSelectPower(Packet packet)
        {
            packet.ReadByte("ContainerSlot");
            packet.ReadByte("Slot");
            packet.ReadByte("AzeriteTier");
            packet.ReadInt32("AzeritePowerID");
        }

        [Parser(Opcode.CMSG_SELL_ITEM)]
        public static void HandleSellItem(Packet packet)
        {
            packet.ReadPackedGuid128("VendorGUID");
            packet.ReadPackedGuid128("ItemGUID");

            packet.ReadUInt32("Amount");
        }

        [Parser(Opcode.CMSG_BUY_ITEM)]
        public static void HandleBuyItem(Packet packet)
        {
            packet.ReadPackedGuid128("VendorGUID");
            packet.ReadPackedGuid128("ContainerGUID");

            packet.ReadInt32("Quantity");
            packet.ReadUInt32("Muid");
            packet.ReadUInt32("Slot");
            packet.ReadInt32("ItemType");

            Substructures.ItemHandler.ReadItemInstance(packet, "ItemInstance");
        }

        [Parser(Opcode.CMSG_BUY_BACK_ITEM)]
        public static void HandleBuyBackItem(Packet packet)
        {
            packet.ReadPackedGuid128("VendorGUID");
            packet.ReadUInt32("Slot");
        }

        [Parser(Opcode.CMSG_SOCKET_GEMS)]
        public static void HandleSocketGems(Packet packet)
        {
            packet.ReadPackedGuid128("GUID");
            for (var i = 0; i < 3; ++i)
                packet.ReadPackedGuid128("Gem GUID", i);
        }

        [Parser(Opcode.CMSG_REPAIR_ITEM)]
        public static void HandleRepairItem(Packet packet)
        {
            packet.ReadPackedGuid128("NpcGUID");
            packet.ReadPackedGuid128("ItemGUID");

            packet.ResetBitReader();
            packet.ReadBit("UseGuildBank");
        }

        [Parser(Opcode.SMSG_INVENTORY_FULL_OVERFLOW)]
        [Parser(Opcode.SMSG_BAG_CLEANUP_FINISHED)]
        public static void HandleItemZero(Packet packet)
        {
        }
    }
}
