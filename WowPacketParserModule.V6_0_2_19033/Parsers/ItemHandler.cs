using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class ItemHandler
    {
        public static int ReadItemInstance(Packet packet, params object[] indexes)
        {
            var itemId = packet.Translator.ReadInt32<ItemId>("ItemID", indexes);
            packet.Translator.ReadUInt32("RandomPropertiesSeed", indexes);
            packet.Translator.ReadUInt32("RandomPropertiesID", indexes);

            packet.Translator.ResetBitReader();

            var hasBonuses = packet.Translator.ReadBit("HasItemBonus", indexes);
            var hasModifications = packet.Translator.ReadBit("HasModifications", indexes);
            if (hasBonuses)
            {
                packet.Translator.ReadByte("Context", indexes);

                var bonusCount = packet.Translator.ReadUInt32();
                for (var j = 0; j < bonusCount; ++j)
                    packet.Translator.ReadUInt32("BonusListID", indexes, j);
            }

            if (hasModifications)
            {
                var mask = packet.Translator.ReadUInt32();
                for (var j = 0; mask != 0; mask >>= 1, ++j)
                    if ((mask & 1) != 0)
                        packet.Translator.ReadInt32(((ItemModifier)j).ToString(), indexes);
            }

            packet.Translator.ResetBitReader();

            return itemId;
        }

        public static void ReadItemPurchaseContents(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadInt32("Money");

            for (int i = 0; i < 5; i++)
                ReadItemPurchaseRefundItem(packet, indexes, i, "ItemPurchaseRefundItem");

            for (int i = 0; i < 5; i++)
                ReadItemPurchaseRefundCurrency(packet, indexes, i, "ItemPurchaseRefundCurrency");
        }

        public static void ReadItemPurchaseRefundItem(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadInt32<ItemId>("ItemID", indexes);
            packet.Translator.ReadInt32("ItemCount", indexes);
        }

        public static void ReadItemPurchaseRefundCurrency(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadInt32("CurrencyID", indexes);
            packet.Translator.ReadInt32("CurrencyCount", indexes);
        }

        public static void ReadInvUpdate(Packet packet, params object[] indexes)
        {
            var bits2 = packet.Translator.ReadBits("InvItemCount", 2, indexes);
            for (int i = 0; i < bits2; i++)
            {
                packet.Translator.ReadByte("ContainerSlot", indexes, i, "Items");
                packet.Translator.ReadByte("Slot", indexes, i, "Items");
            }
        }

        [Parser(Opcode.SMSG_ITEM_PURCHASE_REFUND_RESULT)]
        public static void HandleItemPurchaseRefundResult(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("ItemGUID");
            packet.Translator.ReadByte("Result");
            var hasContents = packet.Translator.ReadBit("HasContents");
            packet.Translator.ResetBitReader();

            if (hasContents)
                ReadItemPurchaseContents(packet, "Contents");
        }

        [Parser(Opcode.CMSG_SORT_BAGS)]
        [Parser(Opcode.CMSG_SORT_BANK_BAGS)]
        [Parser(Opcode.CMSG_SORT_REAGENT_BANK_BAGS)]
        [Parser(Opcode.SMSG_SORT_BAGS_ACK)]
        public static void HandleItemZero(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_ITEM_ENCHANT_TIME_UPDATE)]
        public static void HandleItemEnchantTimeUpdate(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Item Guid");
            packet.Translator.ReadUInt32("Duration");
            packet.Translator.ReadUInt32("Slot");
            packet.Translator.ReadPackedGuid128("Player Guid");
        }

        [Parser(Opcode.CMSG_GET_ITEM_PURCHASE_DATA)]
        [Parser(Opcode.CMSG_USE_CRITTER_ITEM)]
        public static void HandleItemRefundInfo(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("ItemGUID");
        }

        [Parser(Opcode.SMSG_SET_PROFICIENCY)]
        public static void HandleSetProficency(Packet packet)
        {
            packet.Translator.ReadUInt32E<UnknownFlags>("ProficiencyMask");
            packet.Translator.ReadByteE<ItemClass>("ProficiencyClass");
        }

        [Parser(Opcode.CMSG_TRANSMOGRIFY_ITEMS)]
        public static void HandleTransmogrifyItems(Packet packet)
        {
            var int16 = packet.Translator.ReadInt32("ItemsCount");
            packet.Translator.ReadPackedGuid128("Npc");

            for (int i = 0; i < int16; i++)
            {
                packet.Translator.ResetBitReader();

                var bit16 = packet.Translator.ReadBit("HasSrcItem", i);
                var bit40 = packet.Translator.ReadBit("HasSrcVoidItem", i);

                ReadItemInstance(packet, i);

                packet.Translator.ReadInt32("Slot", i);

                if (bit16)
                    packet.Translator.ReadPackedGuid128("SrcItemGUID", i);

                if (bit40)
                    packet.Translator.ReadPackedGuid128("SrcVoidItemGUID", i);
            }
        }

        [Parser(Opcode.CMSG_SELL_ITEM)]
        public static void HandleSellItem(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("VendorGUID");
            packet.Translator.ReadPackedGuid128("ItemGUID");

            packet.Translator.ReadUInt32("Amount");
        }

        [Parser(Opcode.SMSG_BUY_SUCCEEDED)]
        public static void HandleBuyItemResponse(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("VendorGUID");
            packet.Translator.ReadUInt32("Muid");
            packet.Translator.ReadInt32("NewQuantity");
            packet.Translator.ReadUInt32("QuantityBought");
        }

        [Parser(Opcode.SMSG_BUY_FAILED)]
        public static void HandleBuyFailed(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("VendorGUID");
            packet.Translator.ReadUInt32<ItemId>("Muid");
            packet.Translator.ReadByteE<BuyResult>("Reason");
        }

        [Parser(Opcode.CMSG_BUY_BACK_ITEM)]
        public static void HandleBuyBackItem(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("VendorGUID");
            packet.Translator.ReadUInt32("Slot");
        }

        [Parser(Opcode.CMSG_USE_ITEM)]
        public static void HandleUseItem(Packet packet)
        {
            packet.Translator.ReadByte("PackSlot");
            packet.Translator.ReadByte("Slot");
            packet.Translator.ReadPackedGuid128("CastItem");

            SpellHandler.ReadSpellCastRequest(packet, "Cast");
        }

        [Parser(Opcode.CMSG_ADD_TOY)]
        public static void HandleAddToy(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.CMSG_USE_TOY)]
        public static void HandleUseToy(Packet packet)
        {
            packet.Translator.ReadInt32<ItemId>("ItemID");
            SpellHandler.ReadSpellCastRequest(packet, "Cast");
        }

        [Parser(Opcode.CMSG_DESTROY_ITEM)]
        public static void HandleDestroyItem(Packet packet)
        {
            packet.Translator.ReadUInt32("Count");
            packet.Translator.ReadByte("ContainerId");
            packet.Translator.ReadByte("SlotNum");
        }

        [Parser(Opcode.CMSG_REPAIR_ITEM)]
        public static void HandleRepairItem(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("NpcGUID");
            packet.Translator.ReadPackedGuid128("ItemGUID");

            packet.Translator.ResetBitReader();
            packet.Translator.ReadBit("UseGuildBank");
        }

        [Parser(Opcode.CMSG_LOOT_ITEM)]
        public static void HandleAutoStoreLootItem(Packet packet)
        {
            var int16 = packet.Translator.ReadInt32("Count");

            for (var i = 0; i < int16; ++i)
            {
                packet.Translator.ReadPackedGuid128("LootObj", i);
                packet.Translator.ReadByte("Slot", i);
            }
        }

        [Parser(Opcode.CMSG_AUTOBANK_ITEM)]
        [Parser(Opcode.CMSG_AUTO_EQUIP_ITEM)]
        [Parser(Opcode.CMSG_AUTOSTORE_BANK_ITEM)]
        [Parser(Opcode.CMSG_SWAP_INV_ITEM)]
        public static void HandleAutoItem(Packet packet)
        {
            var bits2 = packet.Translator.ReadBits("InvItemCount", 2);
            for (int i = 0; i < bits2; i++)
            {
                packet.Translator.ReadByte("ContainerSlot", i);
                packet.Translator.ReadByte("Slot", i);
            }

            packet.Translator.ReadByte("Slot");
            packet.Translator.ReadByte("PackSlot");
        }

        [Parser(Opcode.CMSG_SWAP_ITEM)]
        public static void HandleSwapInvItem(Packet packet)
        {
            var bits2 = packet.Translator.ReadBits("InvItemCount", 2);
            for (int i = 0; i < bits2; i++)
            {
                packet.Translator.ReadByte("ContainerSlot", i);
                packet.Translator.ReadByte("Slot", i);
            }

            packet.Translator.ReadByte("DestBag");
            packet.Translator.ReadByte("SrcBag");
            packet.Translator.ReadByte("DestSlot");
            packet.Translator.ReadByte("SrcSlot");
        }

        [Parser(Opcode.CMSG_AUTO_STORE_BAG_ITEM)]
        public static void HandleAutoBagItem(Packet packet)
        {
            var bits2 = packet.Translator.ReadBits("InvItemCount", 2);
            for (int i = 0; i < bits2; i++)
            {
                packet.Translator.ReadByte("ContainerSlot", i);
                packet.Translator.ReadByte("Slot", i);
            }

            packet.Translator.ReadByte("ContainerSlotB");
            packet.Translator.ReadByte("ContainerSlotA");
            packet.Translator.ReadByte("SlotA");
        }

        [Parser(Opcode.SMSG_COIN_REMOVED)]
        public static void HandleCoinRemoved(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("LootObj");
        }

        [Parser(Opcode.SMSG_INVENTORY_CHANGE_FAILURE)]
        public static void HandleInventoryChangeFailure(Packet packet)
        {
            var result = packet.Translator.ReadByteE<InventoryResult>("BagResult");

            for (int i = 0; i < 2; i++)
                packet.Translator.ReadPackedGuid128("Item", i);

            packet.Translator.ReadByte("ContainerBSlot");

            if (result == InventoryResult.CantEquipLevel || result == InventoryResult.PurchaseLevelTooLow)
                packet.Translator.ReadInt32("Level");
        }

        [Parser(Opcode.CMSG_SPLIT_ITEM)]
        public static void HandleSplitItem(Packet packet)
        {
            var bits2 = packet.Translator.ReadBits("InvItemCount", 2);
            for (int i = 0; i < bits2; i++)
            {
                packet.Translator.ReadByte("ContainerSlot", i);
                packet.Translator.ReadByte("Slot", i);
            }

            packet.Translator.ReadByte("SrcBag");
            packet.Translator.ReadByte("SrcSlot");
            packet.Translator.ReadByte("DestBag");
            packet.Translator.ReadByte("DestSlot");
            packet.Translator.ReadUInt32("Count");
        }

        [Parser(Opcode.CMSG_BUY_ITEM)]
        public static void HandleBuyItem(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("VendorGUID");
            packet.Translator.ReadPackedGuid128("ContainerGUID");

            ReadItemInstance(packet);

            packet.Translator.ReadInt32("Quantity");
            packet.Translator.ReadUInt32("Muid");
            packet.Translator.ReadUInt32("Slot");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBits("ItemType", 2);
        }

        // To-Do: Need In-Game review
        [Parser(Opcode.SMSG_ITEM_PUSH_RESULT)]
        public static void HandleItemPushResult(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("PlayerGUID");

            packet.Translator.ReadByte("Slot");

            packet.Translator.ReadInt32("SlotInBag");

            ReadItemInstance(packet);

            packet.Translator.ReadUInt32("QuestLogItemID");
            packet.Translator.ReadUInt32("Quantity");
            packet.Translator.ReadUInt32("QuantityInInventory");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173))
                packet.Translator.ReadInt32("DungeonEncounterID");

            packet.Translator.ReadUInt32("BattlePetBreedID");
            packet.Translator.ReadUInt32("BattlePetBreedQuality");
            packet.Translator.ReadUInt32("BattlePetSpeciesID");
            packet.Translator.ReadUInt32("BattlePetLevel");

            packet.Translator.ReadPackedGuid128("ItemGUID");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("Pushed");
            packet.Translator.ReadBit("Created");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173))
                packet.Translator.ReadBits("DisplayText", 2);
            packet.Translator.ReadBit("IsBonusRoll");
            packet.Translator.ReadBit("IsEncounterLoot");
        }

        [Parser(Opcode.SMSG_SELL_RESPONSE)]
        public static void HandleSellResponse(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("VendorGUID");
            packet.Translator.ReadPackedGuid128("ItemGUID");
            packet.Translator.ReadByteE<SellResult>("Reason");
        }

        [Parser(Opcode.SMSG_ITEM_TIME_UPDATE)]
        public static void HandleItemTimeUpdate(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("GUID");
            packet.Translator.ReadUInt32("DurationLeft");
        }

        [Parser(Opcode.SMSG_ENCHANTMENT_LOG)]
        public static void HandleEnchantmentLog(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Caster");
            packet.Translator.ReadPackedGuid128("Owner");
            packet.Translator.ReadPackedGuid128("ItemGUID");
            packet.Translator.ReadUInt32("ItemID");
            packet.Translator.ReadUInt32("Enchantment");
            packet.Translator.ReadUInt32("EnchantSlot");
        }

        [Parser(Opcode.SMSG_READ_ITEM_RESULT_OK)]
        public static void HandleReadItemResultOk(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Item");
        }

        [Parser(Opcode.SMSG_ITEM_EXPIRE_PURCHASE_REFUND)]
        public static void HandleItemExpirePurchaseRefund(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("ItemGUID");
        }

        [Parser(Opcode.CMSG_SET_SORT_BAGS_RIGHT_TO_LEFT)]
        public static void HandleSortBagsRightToLeft(Packet packet)
        {
            packet.Translator.ReadBit("Disable");
        }

        [Parser(Opcode.CMSG_BUY_REAGENT_BANK)]
        public static void HandleReagentBankBuy(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Banker");
        }

        [Parser(Opcode.CMSG_DEPOSIT_REAGENT_BANK)]
        public static void HandleReagentBankDeposit(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Banker");
        }

        [Parser(Opcode.SMSG_ITEM_COOLDOWN)]
        public static void HandleItemCooldown(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("ItemGuid");
            packet.Translator.ReadInt32<SpellId>("SpellID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173))
                packet.Translator.ReadInt32("Duration");
        }

        [Parser(Opcode.SMSG_CROSSED_INEBRIATION_THRESHOLD)]
        public static void HandleCrossedInebriationThreshold(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Guid");
            packet.Translator.ReadInt32("Threshold");
            packet.Translator.ReadInt32("ItemID");
        }

        [Parser(Opcode.SMSG_SET_ITEM_PURCHASE_DATA)]
        public static void HandleSetItemPurchaseData(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("ItemGUID");

            ReadItemPurchaseContents(packet, "ItemPurchaseContents");

            packet.Translator.ReadInt32("Flags");
            packet.Translator.ReadInt32("PurchaseTime");
        }

        [Parser(Opcode.CMSG_SOCKET_GEMS)]
        public static void HandleSocketGems(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("GUID");
            for (var i = 0; i < 3; ++i)
                packet.Translator.ReadPackedGuid128("Gem GUID", i);
        }

        [Parser(Opcode.SMSG_SOCKET_GEMS)]
        public static void HandleSocketGemsResult(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Item");

            for (var i = 0; i < 3; i++)
                packet.Translator.ReadInt32("Socket", i);

            packet.Translator.ReadInt32("SocketMatch");
        }

        public static void ReadCliItemTextCache(Packet packet, params object[] idx)
        {
            var length = packet.Translator.ReadBits("TextLength", 13, idx);
            packet.Translator.ReadWoWString("Text", length, idx);
        }

        [Parser(Opcode.CMSG_ITEM_TEXT_QUERY)]
        public static void HandleItemTextQuery(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Item Guid");
        }

        [Parser(Opcode.SMSG_QUERY_ITEM_TEXT_RESPONSE)]
        public static void HandleQueryItemTextResponse(Packet packet)
        {
            packet.Translator.ReadBit("Valid");
            packet.Translator.ResetBitReader();

            packet.Translator.ReadPackedGuid128("Id");
            ReadCliItemTextCache(packet, "Item");
        }

        [Parser(Opcode.CMSG_WRAP_ITEM)]
        public static void HandleWrapItem(Packet packet)
        {
            ReadInvUpdate(packet, "InvUpdate");
        }
    }
}
