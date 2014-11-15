using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class ItemHandler
    {
        public static void ReadItemInstance(ref Packet packet, params object[] indexes)
        {
            packet.ReadUInt32("ItemID", indexes);
            packet.ReadUInt32("RandomPropertiesSeed", indexes);
            packet.ReadUInt32("RandomPropertiesID", indexes);
            packet.ResetBitReader();
            var hasBonuses = packet.ReadBit("HasItemBonus", indexes);
            var hasModifications = packet.ReadBit("HasModifications", indexes);
            if (hasBonuses)
            {
                packet.ReadByte("Context", indexes);

                var bonusCount = packet.ReadUInt32();
                for (var j = 0; j < bonusCount; ++j)
                    packet.ReadUInt32("BonusListID", indexes, j);
            }

            if (hasModifications)
            {
                var modificationCount = packet.ReadUInt32() / 4;
                for (var j = 0; j < modificationCount; ++j)
                    packet.ReadUInt32("Modification", indexes, j);
            }
        }

        [Parser(Opcode.SMSG_ITEM_ENCHANT_TIME_UPDATE)]
        public static void HandleItemEnchantTimeUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("Item Guid");
            packet.ReadUInt32("Duration");
            packet.ReadUInt32("Slot");
            packet.ReadPackedGuid128("Player Guid");
        }

        [Parser(Opcode.CMSG_ITEM_REFUND_INFO)]
        public static void HandleItemRefundInfo(Packet packet)
        {
            packet.ReadPackedGuid128("Item Guid");
        }

        [Parser(Opcode.SMSG_SET_PROFICIENCY)]
        public static void HandleSetProficency(Packet packet)
        {
            packet.ReadEnum<UnknownFlags>("ProficiencyMask", TypeCode.UInt32);
            packet.ReadEnum<ItemClass>("ProficiencyClass", TypeCode.Byte);
        }

        [Parser(Opcode.CMSG_TRANSMOGRIFY_ITEMS)]
        public static void HandleTransmogrifyItems(Packet packet)
        {
            var int16 = packet.ReadInt32("ItemsCount");
            packet.ReadPackedGuid128("Npc");

            for (int i = 0; i < int16; i++)
            {
                packet.ResetBitReader();

                var bit16 = packet.ReadBit("HasSrcItem", i);
                var bit40 = packet.ReadBit("HasSrcVoidItem", i);

                packet.ReadUInt32("ItemID", i);
                packet.ReadUInt32("RandomPropertiesSeed", i);
                packet.ReadUInt32("RandomPropertiesID", i);
                packet.ResetBitReader();
                var hasBonuses = packet.ReadBit("HasItemBonus", i);
                var hasModifications = packet.ReadBit("HasModifications", i);
                if (hasBonuses)
                {
                    packet.ReadByte("Context", i);

                    var bonusCount = packet.ReadUInt32();
                    for (var j = 0; j < bonusCount; ++j)
                        packet.ReadUInt32("BonusListID", i, j);
                }

                if (hasModifications)
                {
                    var modificationCount = packet.ReadUInt32() / 4;
                    for (var j = 0; j < modificationCount; ++j)
                        packet.ReadUInt32("Modification", i, j);
                }

                packet.ReadInt32("Slot", i);

                if (bit16)
                    packet.ReadPackedGuid128("SrcItemGUID", i);

                if (bit40)
                    packet.ReadPackedGuid128("SrcVoidItemGUID", i);
            }
        }

        [Parser(Opcode.CMSG_SELL_ITEM)]
        public static void HandleSellItem(Packet packet)
        {
            packet.ReadPackedGuid128("VendorGUID");
            packet.ReadPackedGuid128("ItemGUID");

            packet.ReadUInt32("Amount");
        }

        [Parser(Opcode.SMSG_BUY_ITEM)]
        public static void HandleBuyItemResponse(Packet packet)
        {
            packet.ReadPackedGuid128("VendorGUID");
            packet.ReadUInt32("QuantityBought");
            packet.ReadUInt32("Muid");
            packet.ReadUInt32("NewQuantity");
        }

        [Parser(Opcode.SMSG_BUY_FAILED)]
        public static void HandleBuyFailed(Packet packet)
        {
            packet.ReadPackedGuid128("VendorGUID");
            packet.ReadEntry<UInt32>(StoreNameType.Item, "Muid");
            packet.ReadEnum<BuyResult>("Reason", TypeCode.Byte);
        }

        [Parser(Opcode.CMSG_BUYBACK_ITEM)]
        public static void HandleBuyBackItem(Packet packet)
        {
            packet.ReadPackedGuid128("VendorGUID");
            packet.ReadUInt32("Slot");
        }

        [Parser(Opcode.CMSG_USE_ITEM)]
        public static void HandleUseItem(Packet packet)
        {
            packet.ReadByte("PackSlot");
            packet.ReadByte("Slot");
            packet.ReadPackedGuid128("CastItem");

            SpellHandler.ReadSpellCastRequest(ref packet);
        }

        [Parser(Opcode.CMSG_DESTROY_ITEM)]
        public static void HandleDestroyItem(Packet packet)
        {
            packet.ReadUInt32("Count");
            packet.ReadByte("SlotNum");
            packet.ReadByte("ContainerId");
        }

        [Parser(Opcode.CMSG_REPAIR_ITEM)]
        public static void HandleRepairItem(Packet packet)
        {
            packet.ReadPackedGuid128("NpcGUID");
            packet.ReadPackedGuid128("ItemGUID");

            packet.ResetBitReader();
            packet.ReadBit("UseGuildBank");
        }

        [Parser(Opcode.CMSG_AUTOSTORE_LOOT_ITEM)]
        public static void HandleAutoStoreLootItem(Packet packet)
        {
            var int16 = packet.ReadInt32("Count");

            for (var i = 0; i < int16; ++i)
            {
                packet.ReadPackedGuid128("LootObj", i);
                packet.ReadByte("Slot", i);
            }
        }

        [Parser(Opcode.CMSG_AUTOBANK_ITEM)]
        [Parser(Opcode.CMSG_AUTOEQUIP_ITEM)]
        [Parser(Opcode.CMSG_AUTOSTORE_BANK_ITEM)]
        public static void HandleAutoItem(Packet packet)
        {
            var bits2 = packet.ReadBits("InvItemCount", 2);
            for (int i = 0; i < bits2; i++)
            {
                packet.ReadByte("ContainerSlot", i);
                packet.ReadByte("Slot", i);
            }

            packet.ReadByte("Slot");
            packet.ReadByte("PackSlot");
        }

        [Parser(Opcode.CMSG_AUTOSTORE_BAG_ITEM)]
        public static void HandleAutoBagItem(Packet packet)
        {
            var bits2 = packet.ReadBits("InvItemCount", 2);
            for (int i = 0; i < bits2; i++)
            {
                packet.ReadByte("ContainerSlot", i);
                packet.ReadByte("Slot", i);
            }

            packet.ReadByte("ContainerSlotB");
            packet.ReadByte("ContainerSlotA");
            packet.ReadByte("SlotA");
        }

        [Parser(Opcode.SMSG_COIN_REMOVED)]
        public static void HandleCoinRemoved(Packet packet)
        {
            packet.ReadPackedGuid128("LootObj");
        }
    }
}
