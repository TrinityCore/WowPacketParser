using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class ItemHandler
    {
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

        public static void ReadCliItemTextCache(Packet packet, params object[] idx)
        {
            var length = packet.ReadBits("TextLength", 13, idx);
            packet.ReadWoWString("Text", length, idx);
        }

        public static void ReadInvUpdate(Packet packet, params object[] indexes)
        {
            var bits2 = packet.ReadBits("InvItemCount", 2);
            for (int i = 0; i < bits2; i++)
            {
                packet.ReadByte("ContainerSlot", i);
                packet.ReadByte("Slot", i);
            }
        }

        [Parser(Opcode.SMSG_BUY_FAILED, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBuyFailed(Packet packet)
        {
            packet.ReadPackedGuid128("VendorGUID");
            packet.ReadUInt32<ItemId>("Muid");
            packet.ReadInt32E<BuyResult>("Reason");
        }

        [Parser(Opcode.SMSG_BUY_SUCCEEDED, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBuyItemResponse(Packet packet)
        {
            packet.ReadPackedGuid128("VendorGUID");
            packet.ReadUInt32("Muid");
            packet.ReadInt32("NewQuantity");
            packet.ReadUInt32("QuantityBought");
        }

        [Parser(Opcode.SMSG_SET_PROFICIENCY, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSetProficency(Packet packet)
        {
            packet.ReadUInt32E<UnknownFlags>("ProficiencyMask");
            packet.ReadByteE<ItemClass>("ProficiencyClass");
        }

        [Parser(Opcode.CMSG_REFORGE_ITEM, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleReforgeItem(Packet packet)
        {
            packet.ReadPackedGuid128("ReforgerGUID");
            packet.ReadInt32("ContainerId");
            packet.ReadInt32("SlotNum");
            packet.ReadInt32("ItemReforgeRecId");
        }

        [Parser(Opcode.SMSG_ITEM_EXPIRE_PURCHASE_REFUND, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleItemExpirePurchaseRefund(Packet packet)
        {
            packet.ReadPackedGuid128("ItemGUID");
        }

        [Parser(Opcode.CMSG_USE_TOY)]
        public static void HandleUseToy(Packet packet)
        {
            SpellHandler.ReadSpellCastRequest(packet, "Cast");
        }

        [Parser(Opcode.SMSG_COIN_REMOVED, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleCoinRemoved(Packet packet)
        {
            packet.ReadPackedGuid128("LootObj");
        }

        [Parser(Opcode.SMSG_CROSSED_INEBRIATION_THRESHOLD, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleCrossedInebriationThreshold(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadInt32("Threshold");
            packet.ReadInt32<ItemId>("ItemID");
        }

        [Parser(Opcode.SMSG_ENCHANTMENT_LOG, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleEnchantmentLog(Packet packet)
        {
            packet.ReadPackedGuid128("Owner");
            packet.ReadPackedGuid128("Caster");
            packet.ReadPackedGuid128("ItemGUID");
            packet.ReadUInt32<ItemId>("ItemID");
            packet.ReadUInt32("Enchantment");
            packet.ReadUInt32("EnchantSlot");
        }

        [Parser(Opcode.SMSG_INVENTORY_CHANGE_FAILURE, ClientVersionBuild.V3_4_4_59817)]
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

        [Parser(Opcode.SMSG_ITEM_COOLDOWN, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleItemCooldown(Packet packet)
        {
            packet.ReadPackedGuid128("ItemGuid");
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadInt32("Cooldown");
        }

        [Parser(Opcode.SMSG_ITEM_ENCHANT_TIME_UPDATE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleItemEnchantTimeUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("Item Guid");
            packet.ReadUInt32("DurationLeft");
            packet.ReadUInt32("Slot");
            packet.ReadPackedGuid128("Player Guid");
        }

        [Parser(Opcode.SMSG_ITEM_PURCHASE_REFUND_RESULT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleItemPurchaseRefundResult(Packet packet)
        {
            packet.ReadPackedGuid128("ItemGUID");
            packet.ReadByte("Result");
            var hasContents = packet.ReadBit("HasContents");
            packet.ResetBitReader();

            if (hasContents)
                ReadItemPurchaseContents(packet, "Contents");
        }

        [Parser(Opcode.SMSG_ITEM_PUSH_RESULT, ClientVersionBuild.V3_4_4_59817)]
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

        [Parser(Opcode.SMSG_ITEM_TIME_UPDATE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleItemTimeUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("GUID");
            packet.ReadUInt32("DurationLeft");
        }

        [Parser(Opcode.SMSG_QUERY_ITEM_TEXT_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleQueryItemTextResponse(Packet packet)
        {
            packet.ReadBit("Valid");
            packet.ResetBitReader();

            ReadCliItemTextCache(packet, "Item");
            packet.ReadPackedGuid128("Id");
        }

        [Parser(Opcode.SMSG_READ_ITEM_RESULT_FAILED, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleReadItemResultFailed(Packet packet)
        {
            packet.ReadPackedGuid128("ItemGUID");
            packet.ReadUInt32("Delay");
            packet.ReadBits("Subcode", 2);
        }

        [Parser(Opcode.SMSG_READ_ITEM_RESULT_OK, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleReadItemResultOk(Packet packet)
        {
            packet.ReadPackedGuid128("Item");
        }

        [Parser(Opcode.SMSG_REMOVE_ITEM_PASSIVE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleRemoveItemPassive(Packet packet)
        {
            packet.ReadUInt32<SpellId>("SpellID");
        }

        [Parser(Opcode.SMSG_SELL_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSellResponse(Packet packet)
        {
            packet.ReadPackedGuid128("VendorGUID");
            var itemGuidCount = packet.ReadUInt32("ItemGuidCount");

            packet.ReadInt32E<SellResult>("Reason");

            for (var i = 0; i < itemGuidCount; ++i)
                packet.ReadPackedGuid128("ItemGuid", i);
        }

        [Parser(Opcode.SMSG_SEND_ITEM_PASSIVES, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSendItemPassives(Packet packet)
        {
            var spellCount = packet.ReadUInt32("SpellCount");

            for (var i = 0; i < spellCount; ++i)
                packet.ReadInt32("SpellID", i);
        }

        [Parser(Opcode.SMSG_SET_ITEM_PURCHASE_DATA, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSetItemPurchaseData(Packet packet)
        {
            packet.ReadPackedGuid128("ItemGUID");

            ReadItemPurchaseContents(packet, "ItemPurchaseContents");

            packet.ReadInt32("Flags");
            packet.ReadInt32("PurchaseTime");
        }

        [Parser(Opcode.SMSG_SOCKET_GEMS_SUCCESS, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSocketGemsSuccess(Packet packet)
        {
            packet.ReadPackedGuid128("Item");
        }

        [Parser(Opcode.CMSG_ADD_TOY, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAddToy(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.CMSG_AUTOBANK_ITEM, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAutoItem(Packet packet)
        {
            ReadInvUpdate(packet);
            packet.ReadByteE<BankType>("BankType");

            packet.ReadByte("Bag");
            packet.ReadByte("Slot");
        }

        [Parser(Opcode.CMSG_AUTOSTORE_BANK_ITEM, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAutoStoreItem(Packet packet)
        {
            ReadInvUpdate(packet);

            packet.ReadByte("Bag");
            packet.ReadByte("Slot");
        }

        [Parser(Opcode.CMSG_AUTO_EQUIP_ITEM, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAutoEquipItem(Packet packet)
        {
            ReadInvUpdate(packet);

            packet.ReadByte("PackSlot");
            packet.ReadByte("Slot");
        }

        [Parser(Opcode.CMSG_AUTO_EQUIP_ITEM_SLOT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAutoEquipItemSlot(Packet packet)
        {
            ReadInvUpdate(packet);

            packet.ReadPackedGuid128("Item");
            packet.ReadByte("ItemDstSlot");
        }

        [Parser(Opcode.CMSG_AUTO_GUILD_BANK_ITEM, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAutoGuildBankItem(Packet packet)
        {
            packet.ReadPackedGuid128("Banker");
            packet.ReadByte("BankTab");
            packet.ReadByte("BankSlot");
            packet.ReadByte("ContainerItemSlot");

            var hasContainerSlot = packet.ReadBit();

            if (hasContainerSlot)
                packet.ReadByte("ContainerSlot");
        }

        [Parser(Opcode.CMSG_AUTO_STORE_BAG_ITEM, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAutoStoreBagItem(Packet packet)
        {
            ReadInvUpdate(packet);

            packet.ReadByte("ContainerSlotB");
            packet.ReadByte("ContainerSlotA");
            packet.ReadByte("SlotA");
        }

        [Parser(Opcode.CMSG_AUTO_STORE_GUILD_BANK_ITEM, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAutoStoreGuildBankItem(Packet packet)
        {
            packet.ReadPackedGuid128("Banker");
            packet.ReadByte("BankTab");
            packet.ReadByte("BankSlot");
        }

        [Parser(Opcode.CMSG_BUY_BACK_ITEM, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBuyBackItem(Packet packet)
        {
            packet.ReadPackedGuid128("VendorGUID");
            packet.ReadUInt32("Slot");
        }

        [Parser(Opcode.CMSG_BUY_ITEM, ClientVersionBuild.V3_4_4_59817)]
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

        [Parser(Opcode.CMSG_DESTROY_ITEM, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleDestroyItem(Packet packet)
        {
            packet.ReadUInt32("Count");
            packet.ReadByte("ContainerId");
            packet.ReadByte("SlotNum");
        }

        [Parser(Opcode.CMSG_GET_ITEM_PURCHASE_DATA, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleGetItemPurchaseData(Packet packet)
        {
            packet.ReadPackedGuid128("ItemGUID");
        }

        [Parser(Opcode.CMSG_ITEM_PURCHASE_REFUND, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleItemPurchaseRefund(Packet packet)
        {
            packet.ReadPackedGuid128("ItemGUID");
        }

        [Parser(Opcode.CMSG_ITEM_TEXT_QUERY, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleItemTextQuery(Packet packet)
        {
            packet.ReadPackedGuid128("ItemGUID");
        }

        [Parser(Opcode.CMSG_OPEN_ITEM, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleOpenItem(Packet packet)
        {
            packet.ReadByte("Slot");
            packet.ReadByte("PackSlot");
        }

        [Parser(Opcode.CMSG_READ_ITEM, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleReadItem(Packet packet)
        {
            packet.ReadByte("PackSlot");
            packet.ReadByte("Slot");
        }

        [Parser(Opcode.CMSG_REMOVE_NEW_ITEM, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleRemoveNewItem(Packet packet)
        {
            packet.ReadPackedGuid128("ItemGUID");
        }

        [Parser(Opcode.CMSG_REPAIR_ITEM, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleRepairItem(Packet packet)
        {
            packet.ReadPackedGuid128("NpcGUID");
            packet.ReadPackedGuid128("ItemGUID");

            packet.ResetBitReader();
            packet.ReadBit("UseGuildBank");
        }

        [Parser(Opcode.CMSG_SELL_ITEM, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSellItem(Packet packet)
        {
            packet.ReadPackedGuid128("VendorGUID");
            packet.ReadPackedGuid128("ItemGUID");

            packet.ReadUInt32("Amount");
        }

        [Parser(Opcode.CMSG_SOCKET_GEMS, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSocketGems(Packet packet)
        {
            packet.ReadPackedGuid128("GUID");
            for (var i = 0; i < 3; ++i)
                packet.ReadPackedGuid128("Gem GUID", i);
        }

        [Parser(Opcode.CMSG_SPLIT_ITEM, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSplitItem(Packet packet)
        {
            ReadInvUpdate(packet);

            packet.ReadByte("FromPackSlot");
            packet.ReadByte("FromSlot");
            packet.ReadByte("ToPackSlot");
            packet.ReadByte("ToSlot");
            packet.ReadInt32("Quantity");
        }

        [Parser(Opcode.CMSG_SWAP_INV_ITEM, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSwapInvItem(Packet packet)
        {
            ReadInvUpdate(packet);

            packet.ReadByte("Slot2");
            packet.ReadByte("Slot1");
        }

        [Parser(Opcode.CMSG_SWAP_ITEM, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSwapItem(Packet packet)
        {
            ReadInvUpdate(packet);

            packet.ReadByte("ContainerSlotB");
            packet.ReadByte("ContainerSlotA");
            packet.ReadByte("SlotB");
            packet.ReadByte("SlotA");
        }

        [Parser(Opcode.CMSG_USE_CRITTER_ITEM, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleUseCritterItem(Packet packet)
        {
            packet.ReadPackedGuid128("ItemGUID");
        }

        [Parser(Opcode.CMSG_USE_ITEM)]
        public static void HandleUseItem(Packet packet)
        {
            var useItem = packet.Holder.ClientUseItem = new();
            useItem.PackSlot = packet.ReadByte("PackSlot");
            useItem.ItemSlot = packet.ReadByte("Slot");
            useItem.CastItem = packet.ReadPackedGuid128("CastItem");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_4_59817))
                useItem.SpellId = SpellHandler.ReadSpellCastRequest344(packet, "Cast");
            else
                useItem.SpellId = SpellHandler.ReadSpellCastRequest(packet, "Cast");
        }

        [Parser(Opcode.CMSG_WRAP_ITEM, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleWrapItem(Packet packet)
        {
            ReadInvUpdate(packet, "InvUpdate");
        }

        [Parser(Opcode.SMSG_ITEM_CHANGED)]
        public static void HandleItemChanged(Packet packet)
        {
            packet.ReadPackedGuid128("Player");
            Substructures.ItemHandler.ReadItemInstance(packet, "ItemInstanceBefore");
            Substructures.ItemHandler.ReadItemInstance(packet, "ItemInstanceAfter");
        }

        [Parser(Opcode.SMSG_OPEN_CONTAINER)]
        public static void HandleOpenContainer(Packet packet)
        {
            packet.ReadPackedGuid128("ItemGUID");
        }

        [Parser(Opcode.SMSG_REFORGE_RESULT)]
        public static void HandleItemReforgeResult(Packet packet)
        {
            packet.ReadBit("Successful");
        }

        [Parser(Opcode.SMSG_BAG_CLEANUP_FINISHED, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_INVENTORY_FULL_OVERFLOW, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleItemZero(Packet packet)
        {
        }
    }
}
