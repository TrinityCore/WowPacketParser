using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class ItemHandler
    {
        [Parser(Opcode.SMSG_SET_PROFICIENCY)]
        public static void HandleSetProficency(Packet packet)
        {
            packet.Translator.ReadUInt32E<UnknownFlags>("Mask");
            packet.Translator.ReadByteE<ItemClass>("Class");
        }

        [Parser(Opcode.SMSG_ITEM_ENCHANT_TIME_UPDATE)]
        public static void HandleItemEnchantTimeUpdate(Packet packet)
        {
            var itemGuid = new byte[8];
            var playerGuid = new byte[8];

            packet.Translator.ReadInt32("Slot");
            packet.Translator.ReadInt32("Duration");

            itemGuid[5] = packet.Translator.ReadBit();
            itemGuid[0] = packet.Translator.ReadBit();
            playerGuid[7] = packet.Translator.ReadBit();
            playerGuid[4] = packet.Translator.ReadBit();
            itemGuid[4] = packet.Translator.ReadBit();
            itemGuid[3] = packet.Translator.ReadBit();
            playerGuid[3] = packet.Translator.ReadBit();
            playerGuid[0] = packet.Translator.ReadBit();
            playerGuid[5] = packet.Translator.ReadBit();
            itemGuid[2] = packet.Translator.ReadBit();
            playerGuid[1] = packet.Translator.ReadBit();
            itemGuid[7] = packet.Translator.ReadBit();
            playerGuid[6] = packet.Translator.ReadBit();
            itemGuid[6] = packet.Translator.ReadBit();
            playerGuid[2] = packet.Translator.ReadBit();
            itemGuid[1] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(itemGuid, 1);
            packet.Translator.ReadXORByte(playerGuid, 3);
            packet.Translator.ReadXORByte(itemGuid, 0);
            packet.Translator.ReadXORByte(itemGuid, 6);
            packet.Translator.ReadXORByte(playerGuid, 6);
            packet.Translator.ReadXORByte(playerGuid, 1);
            packet.Translator.ReadXORByte(playerGuid, 7);
            packet.Translator.ReadXORByte(itemGuid, 3);
            packet.Translator.ReadXORByte(playerGuid, 0);
            packet.Translator.ReadXORByte(itemGuid, 2);
            packet.Translator.ReadXORByte(itemGuid, 4);
            packet.Translator.ReadXORByte(itemGuid, 5);
            packet.Translator.ReadXORByte(playerGuid, 2);
            packet.Translator.ReadXORByte(itemGuid, 7);
            packet.Translator.ReadXORByte(playerGuid, 5);
            packet.Translator.ReadXORByte(playerGuid, 4);

            packet.Translator.WriteGuid("Player GUID", playerGuid);
            packet.Translator.WriteGuid("Item GUID", itemGuid);
        }

        [Parser(Opcode.CMSG_REFORGE_ITEM)]
        public static void HandleItemSendReforge(Packet packet)
        {
            packet.Translator.ReadInt32("Bag");
            packet.Translator.ReadInt32("Reforge Entry");
            packet.Translator.ReadInt32("Slot");

            var guid = packet.Translator.StartBitStream(3, 5, 4, 6, 1, 0, 7, 2);
            packet.Translator.ParseBitStream(guid, 2, 0, 6, 4, 3, 5, 1, 7);
            packet.Translator.WriteGuid("Reforger Guid", guid);

        }

        [Parser(Opcode.SMSG_REFORGE_RESULT)]
        public static void HandleItemReforgeResult(Packet packet)
        {
            packet.Translator.ReadBit("Successful");
        }

        [Parser(Opcode.CMSG_ITEM_UPGRADE)]
        public static void HandleItemSendUpgrade(Packet packet)
        {
            var itemGUID = new byte[8];
            var npcGUID = new byte[8];

            packet.Translator.ReadInt32("Bag");
            packet.Translator.ReadInt32("Slot");
            packet.Translator.ReadInt32("Reforge Entry");

            itemGUID[7] = packet.Translator.ReadBit();
            itemGUID[4] = packet.Translator.ReadBit();
            npcGUID[3] = packet.Translator.ReadBit();
            itemGUID[0] = packet.Translator.ReadBit();
            npcGUID[5] = packet.Translator.ReadBit();
            npcGUID[0] = packet.Translator.ReadBit();
            itemGUID[1] = packet.Translator.ReadBit();
            itemGUID[2] = packet.Translator.ReadBit();
            npcGUID[2] = packet.Translator.ReadBit();
            itemGUID[3] = packet.Translator.ReadBit();
            npcGUID[4] = packet.Translator.ReadBit();
            npcGUID[6] = packet.Translator.ReadBit();
            itemGUID[5] = packet.Translator.ReadBit();
            npcGUID[7] = packet.Translator.ReadBit();
            npcGUID[1] = packet.Translator.ReadBit();
            itemGUID[6] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(itemGUID, 6);
            packet.Translator.ReadXORByte(itemGUID, 1);
            packet.Translator.ReadXORByte(npcGUID, 7);
            packet.Translator.ReadXORByte(itemGUID, 5);
            packet.Translator.ReadXORByte(itemGUID, 4);
            packet.Translator.ReadXORByte(npcGUID, 6);
            packet.Translator.ReadXORByte(itemGUID, 0);
            packet.Translator.ReadXORByte(npcGUID, 3);
            packet.Translator.ReadXORByte(itemGUID, 7);
            packet.Translator.ReadXORByte(npcGUID, 2);
            packet.Translator.ReadXORByte(npcGUID, 4);
            packet.Translator.ReadXORByte(npcGUID, 5);
            packet.Translator.ReadXORByte(itemGUID, 3);
            packet.Translator.ReadXORByte(npcGUID, 1);
            packet.Translator.ReadXORByte(npcGUID, 0);
            packet.Translator.ReadXORByte(itemGUID, 2);

            packet.Translator.WriteGuid("Item GUID", itemGUID);
            packet.Translator.WriteGuid("NPC GUID", npcGUID);
        }

        [Parser(Opcode.CMSG_GET_ITEM_PURCHASE_DATA)]
        public static void HandleItemRefundInfo(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 0, 4, 6, 3, 2, 1, 7, 5);
            packet.Translator.ParseBitStream(guid, 5, 3, 7, 2, 1, 6, 0, 4);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_ITEM_PUSH_RESULT)]
        public static void HandleItemPushResult(Packet packet)
        {
            var playerGUID = new byte[8];
            var guid2 = new byte[8];

            packet.Translator.ReadBit("Result in Combatlog");
            packet.Translator.ReadBit("Created");
            playerGUID[2] = packet.Translator.ReadBit();
            playerGUID[0] = packet.Translator.ReadBit();
            playerGUID[4] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("bit24");
            playerGUID[5] = packet.Translator.ReadBit();
            playerGUID[1] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();
            playerGUID[6] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            playerGUID[7] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            playerGUID[3] = packet.Translator.ReadBit();

            packet.Translator.ReadBit("From NPC");
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadInt32("Suffix Factor");
            packet.Translator.ReadXORByte(playerGUID, 1);
            packet.Translator.ReadInt32("Int14");
            packet.Translator.ReadUInt32("Count");
            packet.Translator.ReadInt32("Int54");
            packet.Translator.ReadInt32("Random Property ID");
            packet.Translator.ReadXORByte(playerGUID, 3);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(playerGUID, 5);
            packet.Translator.ReadUInt32("Unk Uint32");
            packet.Translator.ReadXORByte(playerGUID, 2);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(playerGUID, 7);
            packet.Translator.ReadByte("Slot");
            packet.Translator.ReadUInt32<ItemId>("Entry");
            packet.Translator.ReadInt32("Int40");
            packet.Translator.ReadXORByte(playerGUID, 0);
            packet.Translator.ReadXORByte(playerGUID, 4);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadUInt32("Count of Items in inventory");
            packet.Translator.ReadInt32("Item Slot");
            packet.Translator.ReadXORByte(playerGUID, 6);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid2, 4);

            packet.Translator.WriteGuid("Player GUID", playerGUID);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.CMSG_AUTOSTORE_LOOT_ITEM)]
        public static void HandleAutoStoreLootItem510(Packet packet)
        {
            var counter = packet.Translator.ReadBits("Count", 23);

            var guid = new byte[counter][];

            for (var i = 0; i < counter; ++i)
            {
                guid[i] = new byte[8];
                packet.Translator.StartBitStream(guid[i], 2, 1, 5, 7, 4, 3, 0, 6);
            }

            packet.Translator.ResetBitReader();

            for (var i = 0; i < counter; ++i)
            {
                packet.Translator.ReadXORByte(guid[i], 0);
                packet.Translator.ReadXORByte(guid[i], 3);
                packet.Translator.ReadByte("Slot", i);
                packet.Translator.ReadXORByte(guid[i], 7);
                packet.Translator.ReadXORByte(guid[i], 2);
                packet.Translator.ReadXORByte(guid[i], 4);
                packet.Translator.ReadXORByte(guid[i], 1);
                packet.Translator.ReadXORByte(guid[i], 6);
                packet.Translator.ReadXORByte(guid[i], 5);

                packet.Translator.WriteGuid("Looter GUID", guid[i], i);
            }
        }

        [Parser(Opcode.CMSG_BUY_BACK_ITEM)]
        public static void HandleBuyBackItem(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadUInt32("Slot");

            packet.Translator.StartBitStream(guid, 3, 5, 0, 7, 2, 6, 1, 4);
            packet.Translator.ParseBitStream(guid, 1, 7, 6, 0, 5, 3, 4, 2);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_INVENTORY_CHANGE_FAILURE)]
        public static void HandleInventoryChangeFailure(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var guid3 = new byte[8];
            var guid4 = new byte[8];

            guid2[0] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            guid1[4] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            guid1[5] = packet.Translator.ReadBit();
            guid1[2] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid1, 7);
            var result = packet.Translator.ReadByteE<InventoryResult>("Result");
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadByte("Bag");
            packet.Translator.ReadXORByte(guid2, 7);
            if (result == InventoryResult.Ok)
                return;

            packet.Translator.ReadInt32("Int30");
            packet.Translator.ReadXORByte(guid2, 2);

            if (result == InventoryResult.ItemMaxLimitCategoryCountExceeded ||
                result == InventoryResult.ItemMaxLimitCategorySocketedExceeded ||
                result == InventoryResult.ItemMaxLimitCategoryEquippedExceeded)
                packet.Translator.ReadUInt32("Limit Category");

            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadInt32("Int14");
            packet.Translator.ReadXORByte(guid2, 6);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);

            if (result == InventoryResult.EventAutoEquipBindConfirm)
            {
                guid4[6] = packet.Translator.ReadBit();
                guid3[7] = packet.Translator.ReadBit();
                guid3[3] = packet.Translator.ReadBit();
                guid3[0] = packet.Translator.ReadBit();
                guid4[7] = packet.Translator.ReadBit();
                guid3[5] = packet.Translator.ReadBit();
                guid4[5] = packet.Translator.ReadBit();
                guid3[2] = packet.Translator.ReadBit();
                guid3[1] = packet.Translator.ReadBit();
                guid4[3] = packet.Translator.ReadBit();
                guid3[4] = packet.Translator.ReadBit();
                guid4[0] = packet.Translator.ReadBit();
                guid4[2] = packet.Translator.ReadBit();
                guid4[1] = packet.Translator.ReadBit();
                guid3[6] = packet.Translator.ReadBit();
                guid4[4] = packet.Translator.ReadBit();

                packet.Translator.ReadXORByte(guid4, 6);
                packet.Translator.ReadXORByte(guid4, 2);
                packet.Translator.ReadXORByte(guid4, 7);
                packet.Translator.ReadXORByte(guid4, 0);
                packet.Translator.ReadXORByte(guid3, 3);
                packet.Translator.ReadXORByte(guid3, 6);
                packet.Translator.ReadXORByte(guid3, 2);
                packet.Translator.ReadXORByte(guid3, 5);
                packet.Translator.ReadXORByte(guid3, 4);
                packet.Translator.ReadXORByte(guid4, 5);
                packet.Translator.ReadXORByte(guid3, 7);
                packet.Translator.ReadXORByte(guid3, 0);
                packet.Translator.ReadXORByte(guid3, 1);
                packet.Translator.ReadXORByte(guid4, 4);
                packet.Translator.ReadXORByte(guid4, 1);
                packet.Translator.ReadXORByte(guid4, 3);

                packet.Translator.WriteGuid("Guid3", guid3);
                packet.Translator.WriteGuid("Guid4", guid4);
            }
        }

        [Parser(Opcode.CMSG_AUTO_EQUIP_ITEM)]
        public static void HandleAutoEquipItem(Packet packet)
        {
            packet.Translator.ReadByte("Bag");
            packet.Translator.ReadByte("Slot");

            var bits14 = (int)packet.Translator.ReadBits(2);

            var hasSlot = new bool[bits14];
            var hasBag = new bool[bits14];

            for (var i = 0; i < bits14; ++i)
            {
                hasSlot[i] = !packet.Translator.ReadBit();
                hasBag[i] = !packet.Translator.ReadBit();
            }

            for (var i = 0; i < bits14; ++i)
            {
                if (hasBag[i])
                    packet.Translator.ReadByte("Bag", i);
                if (hasSlot[i])
                    packet.Translator.ReadByte("Slot", i);
            }
        }

        [Parser(Opcode.CMSG_REPAIR_ITEM)]
        public static void HandleRepairItem(Packet packet)
        {
            var vendorGUID = new byte[8];
            var itemGUID = new byte[8];

            vendorGUID[3] = packet.Translator.ReadBit();
            itemGUID[3] = packet.Translator.ReadBit();
            itemGUID[6] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Use guild money");
            vendorGUID[2] = packet.Translator.ReadBit();
            itemGUID[0] = packet.Translator.ReadBit();
            vendorGUID[7] = packet.Translator.ReadBit();
            vendorGUID[6] = packet.Translator.ReadBit();
            vendorGUID[0] = packet.Translator.ReadBit();
            itemGUID[7] = packet.Translator.ReadBit();
            itemGUID[4] = packet.Translator.ReadBit();
            vendorGUID[5] = packet.Translator.ReadBit();
            itemGUID[5] = packet.Translator.ReadBit();
            vendorGUID[4] = packet.Translator.ReadBit();
            itemGUID[2] = packet.Translator.ReadBit();
            vendorGUID[1] = packet.Translator.ReadBit();
            itemGUID[1] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(itemGUID, 6);
            packet.Translator.ReadXORByte(vendorGUID, 1);
            packet.Translator.ReadXORByte(vendorGUID, 0);
            packet.Translator.ReadXORByte(vendorGUID, 2);
            packet.Translator.ReadXORByte(itemGUID, 2);
            packet.Translator.ReadXORByte(itemGUID, 5);
            packet.Translator.ReadXORByte(itemGUID, 1);
            packet.Translator.ReadXORByte(vendorGUID, 6);
            packet.Translator.ReadXORByte(vendorGUID, 7);
            packet.Translator.ReadXORByte(itemGUID, 3);
            packet.Translator.ReadXORByte(itemGUID, 7);
            packet.Translator.ReadXORByte(vendorGUID, 3);
            packet.Translator.ReadXORByte(vendorGUID, 4);
            packet.Translator.ReadXORByte(vendorGUID, 5);
            packet.Translator.ReadXORByte(itemGUID, 0);
            packet.Translator.ReadXORByte(itemGUID, 4);

            packet.Translator.WriteGuid("Vendor GUID", vendorGUID);
            packet.Translator.WriteGuid("Item GUID", itemGUID);
        }

        [Parser(Opcode.CMSG_SELL_ITEM)]
        public static void HandleSellItem(Packet packet)
        {
            var vendorGUID = new byte[8];
            var itemGUID = new byte[8];

            packet.Translator.ReadInt32("Count");

            itemGUID[7] = packet.Translator.ReadBit();
            vendorGUID[0] = packet.Translator.ReadBit();
            vendorGUID[3] = packet.Translator.ReadBit();
            itemGUID[3] = packet.Translator.ReadBit();
            vendorGUID[7] = packet.Translator.ReadBit();
            vendorGUID[6] = packet.Translator.ReadBit();
            vendorGUID[5] = packet.Translator.ReadBit();
            vendorGUID[2] = packet.Translator.ReadBit();
            itemGUID[4] = packet.Translator.ReadBit();
            itemGUID[6] = packet.Translator.ReadBit();
            itemGUID[5] = packet.Translator.ReadBit();
            itemGUID[2] = packet.Translator.ReadBit();
            vendorGUID[1] = packet.Translator.ReadBit();
            vendorGUID[4] = packet.Translator.ReadBit();
            itemGUID[0] = packet.Translator.ReadBit();
            itemGUID[1] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(vendorGUID, 6);
            packet.Translator.ReadXORByte(vendorGUID, 2);
            packet.Translator.ReadXORByte(itemGUID, 1);
            packet.Translator.ReadXORByte(vendorGUID, 0);
            packet.Translator.ReadXORByte(vendorGUID, 7);
            packet.Translator.ReadXORByte(itemGUID, 6);
            packet.Translator.ReadXORByte(itemGUID, 0);
            packet.Translator.ReadXORByte(itemGUID, 7);
            packet.Translator.ReadXORByte(vendorGUID, 1);
            packet.Translator.ReadXORByte(vendorGUID, 5);
            packet.Translator.ReadXORByte(itemGUID, 5);
            packet.Translator.ReadXORByte(itemGUID, 3);
            packet.Translator.ReadXORByte(itemGUID, 4);
            packet.Translator.ReadXORByte(vendorGUID, 4);
            packet.Translator.ReadXORByte(vendorGUID, 3);
            packet.Translator.ReadXORByte(itemGUID, 2);

            packet.Translator.WriteGuid("Vendor GUID", vendorGUID);
            packet.Translator.WriteGuid("Item GUID", itemGUID);
        }

        [Parser(Opcode.CMSG_SWAP_INV_ITEM)]
        public static void HandleSwapItem(Packet packet)
        {
            packet.Translator.ReadByte("Bag");
            packet.Translator.ReadByte("Destination Bag");
            packet.Translator.ReadByte("Slot");
            packet.Translator.ReadByte("Destination Slot");

            var bits14 = (int)packet.Translator.ReadBits(2);

            var hasSlot = new bool[bits14];
            var hasBag = new bool[bits14];

            for (var i = 0; i < bits14; ++i)
            {
                hasSlot[i] = !packet.Translator.ReadBit();
                hasBag[i] = !packet.Translator.ReadBit();
            }

            for (var i = 0; i < bits14; ++i)
            {
                if (hasBag[i])
                    packet.Translator.ReadByte("Bag", i);
                if (hasSlot[i])
                    packet.Translator.ReadByte("Slot", i);
            }
        }
        [Parser(Opcode.CMSG_SWAP_ITEM)]
        public static void HandleSwapItem2(Packet packet)
        {
            packet.Translator.ReadByte("Bag");
            packet.Translator.ReadByte("Slot");
            packet.Translator.ReadByte("Destination Bag");
            packet.Translator.ReadByte("Destination Slot");

            var bits14 = (int)packet.Translator.ReadBits(2);

            var hasSlot = new bool[bits14];
            var hasBag = new bool[bits14];

            for (var i = 0; i < bits14; ++i)
            {
                hasSlot[i] = !packet.Translator.ReadBit();
                hasBag[i] = !packet.Translator.ReadBit();
            }

            for (var i = 0; i < bits14; ++i)
            {
                if (hasSlot[i])
                    packet.Translator.ReadByte("Bag", i);
                if (hasBag[i])
                    packet.Translator.ReadByte("Slot", i);
            }
        }

        [Parser(Opcode.CMSG_AUTO_STORE_BAG_ITEM)]
        public static void HandleSplitItem(Packet packet)
        {
            packet.Translator.ReadSByte("Bag");
            packet.Translator.ReadByte("Destination Bag");
            packet.Translator.ReadByte("Slot");
            packet.Translator.ReadByte("Destination Slot");
        }
    }
}
