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
            packet.ReadUInt32E<UnknownFlags>("Mask");
            packet.ReadByteE<ItemClass>("Class");
        }

        [Parser(Opcode.SMSG_ITEM_ENCHANT_TIME_UPDATE)]
        public static void HandleItemEnchantTimeUpdate(Packet packet)
        {
            var itemGuid = new byte[8];
            var playerGuid = new byte[8];

            packet.ReadInt32("Slot");
            packet.ReadInt32("Duration");

            itemGuid[5] = packet.ReadBit();
            itemGuid[0] = packet.ReadBit();
            playerGuid[7] = packet.ReadBit();
            playerGuid[4] = packet.ReadBit();
            itemGuid[4] = packet.ReadBit();
            itemGuid[3] = packet.ReadBit();
            playerGuid[3] = packet.ReadBit();
            playerGuid[0] = packet.ReadBit();
            playerGuid[5] = packet.ReadBit();
            itemGuid[2] = packet.ReadBit();
            playerGuid[1] = packet.ReadBit();
            itemGuid[7] = packet.ReadBit();
            playerGuid[6] = packet.ReadBit();
            itemGuid[6] = packet.ReadBit();
            playerGuid[2] = packet.ReadBit();
            itemGuid[1] = packet.ReadBit();

            packet.ReadXORByte(itemGuid, 1);
            packet.ReadXORByte(playerGuid, 3);
            packet.ReadXORByte(itemGuid, 0);
            packet.ReadXORByte(itemGuid, 6);
            packet.ReadXORByte(playerGuid, 6);
            packet.ReadXORByte(playerGuid, 1);
            packet.ReadXORByte(playerGuid, 7);
            packet.ReadXORByte(itemGuid, 3);
            packet.ReadXORByte(playerGuid, 0);
            packet.ReadXORByte(itemGuid, 2);
            packet.ReadXORByte(itemGuid, 4);
            packet.ReadXORByte(itemGuid, 5);
            packet.ReadXORByte(playerGuid, 2);
            packet.ReadXORByte(itemGuid, 7);
            packet.ReadXORByte(playerGuid, 5);
            packet.ReadXORByte(playerGuid, 4);

            packet.WriteGuid("Player GUID", playerGuid);
            packet.WriteGuid("Item GUID", itemGuid);
        }

        [Parser(Opcode.CMSG_REFORGE_ITEM)]
        public static void HandleItemSendReforge(Packet packet)
        {
            packet.ReadInt32("Bag");
            packet.ReadInt32("Reforge Entry");
            packet.ReadInt32("Slot");

            var guid = packet.StartBitStream(3, 5, 4, 6, 1, 0, 7, 2);
            packet.ParseBitStream(guid, 2, 0, 6, 4, 3, 5, 1, 7);
            packet.WriteGuid("Reforger Guid", guid);

        }

        [Parser(Opcode.SMSG_REFORGE_RESULT)]
        public static void HandleItemReforgeResult(Packet packet)
        {
            packet.ReadBit("Successful");
        }

        [Parser(Opcode.CMSG_ITEM_UPGRADE)]
        public static void HandleItemSendUpgrade(Packet packet)
        {
            var itemGUID = new byte[8];
            var npcGUID = new byte[8];

            packet.ReadInt32("Bag");
            packet.ReadInt32("Slot");
            packet.ReadInt32("Reforge Entry");

            itemGUID[7] = packet.ReadBit();
            itemGUID[4] = packet.ReadBit();
            npcGUID[3] = packet.ReadBit();
            itemGUID[0] = packet.ReadBit();
            npcGUID[5] = packet.ReadBit();
            npcGUID[0] = packet.ReadBit();
            itemGUID[1] = packet.ReadBit();
            itemGUID[2] = packet.ReadBit();
            npcGUID[2] = packet.ReadBit();
            itemGUID[3] = packet.ReadBit();
            npcGUID[4] = packet.ReadBit();
            npcGUID[6] = packet.ReadBit();
            itemGUID[5] = packet.ReadBit();
            npcGUID[7] = packet.ReadBit();
            npcGUID[1] = packet.ReadBit();
            itemGUID[6] = packet.ReadBit();

            packet.ReadXORByte(itemGUID, 6);
            packet.ReadXORByte(itemGUID, 1);
            packet.ReadXORByte(npcGUID, 7);
            packet.ReadXORByte(itemGUID, 5);
            packet.ReadXORByte(itemGUID, 4);
            packet.ReadXORByte(npcGUID, 6);
            packet.ReadXORByte(itemGUID, 0);
            packet.ReadXORByte(npcGUID, 3);
            packet.ReadXORByte(itemGUID, 7);
            packet.ReadXORByte(npcGUID, 2);
            packet.ReadXORByte(npcGUID, 4);
            packet.ReadXORByte(npcGUID, 5);
            packet.ReadXORByte(itemGUID, 3);
            packet.ReadXORByte(npcGUID, 1);
            packet.ReadXORByte(npcGUID, 0);
            packet.ReadXORByte(itemGUID, 2);

            packet.WriteGuid("Item GUID", itemGUID);
            packet.WriteGuid("NPC GUID", npcGUID);
        }

        [Parser(Opcode.CMSG_GET_ITEM_PURCHASE_DATA)]
        public static void HandleItemRefundInfo(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 0, 4, 6, 3, 2, 1, 7, 5);
            packet.ParseBitStream(guid, 5, 3, 7, 2, 1, 6, 0, 4);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_ITEM_PUSH_RESULT)]
        public static void HandleItemPushResult(Packet packet)
        {
            var playerGUID = new byte[8];
            var guid2 = new byte[8];

            packet.ReadBit("Result in Combatlog");
            packet.ReadBit("Created");
            playerGUID[2] = packet.ReadBit();
            playerGUID[0] = packet.ReadBit();
            playerGUID[4] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            packet.ReadBit("bit24");
            playerGUID[5] = packet.ReadBit();
            playerGUID[1] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            playerGUID[6] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            playerGUID[7] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            playerGUID[3] = packet.ReadBit();

            packet.ReadBit("From NPC");
            packet.ReadXORByte(guid2, 6);
            packet.ReadInt32("Suffix Factor");
            packet.ReadXORByte(playerGUID, 1);
            packet.ReadInt32("Int14");
            packet.ReadUInt32("Count");
            packet.ReadInt32("Int54");
            packet.ReadInt32("Random Property ID");
            packet.ReadXORByte(playerGUID, 3);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(playerGUID, 5);
            packet.ReadUInt32("Unk Uint32");
            packet.ReadXORByte(playerGUID, 2);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(playerGUID, 7);
            packet.ReadByte("Slot");
            packet.ReadUInt32<ItemId>("Entry");
            packet.ReadInt32("Int40");
            packet.ReadXORByte(playerGUID, 0);
            packet.ReadXORByte(playerGUID, 4);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 2);
            packet.ReadUInt32("Count of Items in inventory");
            packet.ReadInt32("Item Slot");
            packet.ReadXORByte(playerGUID, 6);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 4);

            packet.WriteGuid("Player GUID", playerGUID);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.CMSG_AUTOSTORE_LOOT_ITEM)]
        public static void HandleAutoStoreLootItem510(Packet packet)
        {
            var counter = packet.ReadBits("Count", 23);

            var guid = new byte[counter][];

            for (var i = 0; i < counter; ++i)
            {
                guid[i] = new byte[8];
                packet.StartBitStream(guid[i], 2, 1, 5, 7, 4, 3, 0, 6);
            }

            packet.ResetBitReader();

            for (var i = 0; i < counter; ++i)
            {
                packet.ReadXORByte(guid[i], 0);
                packet.ReadXORByte(guid[i], 3);
                packet.ReadByte("Slot", i);
                packet.ReadXORByte(guid[i], 7);
                packet.ReadXORByte(guid[i], 2);
                packet.ReadXORByte(guid[i], 4);
                packet.ReadXORByte(guid[i], 1);
                packet.ReadXORByte(guid[i], 6);
                packet.ReadXORByte(guid[i], 5);

                packet.WriteGuid("Looter GUID", guid[i], i);
            }
        }

        [Parser(Opcode.CMSG_BUY_BACK_ITEM)]
        public static void HandleBuyBackItem(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadUInt32("Slot");

            packet.StartBitStream(guid, 3, 5, 0, 7, 2, 6, 1, 4);
            packet.ParseBitStream(guid, 1, 7, 6, 0, 5, 3, 4, 2);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_INVENTORY_CHANGE_FAILURE)]
        public static void HandleInventoryChangeFailure(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var guid3 = new byte[8];
            var guid4 = new byte[8];

            guid2[0] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid1[2] = packet.ReadBit();

            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 7);
            var result = packet.ReadByteE<InventoryResult>("Result");
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid1, 6);
            packet.ReadByte("Bag");
            packet.ReadXORByte(guid2, 7);
            if (result == InventoryResult.Ok)
                return;

            packet.ReadInt32("Int30");
            packet.ReadXORByte(guid2, 2);

            if (result == InventoryResult.ItemMaxLimitCategoryCountExceeded ||
                result == InventoryResult.ItemMaxLimitCategorySocketedExceeded ||
                result == InventoryResult.ItemMaxLimitCategoryEquippedExceeded)
                packet.ReadUInt32("Limit Category");

            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid1, 4);
            packet.ReadInt32("Int14");
            packet.ReadXORByte(guid2, 6);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);

            if (result == InventoryResult.EventAutoEquipBindConfirm)
            {
                guid4[6] = packet.ReadBit();
                guid3[7] = packet.ReadBit();
                guid3[3] = packet.ReadBit();
                guid3[0] = packet.ReadBit();
                guid4[7] = packet.ReadBit();
                guid3[5] = packet.ReadBit();
                guid4[5] = packet.ReadBit();
                guid3[2] = packet.ReadBit();
                guid3[1] = packet.ReadBit();
                guid4[3] = packet.ReadBit();
                guid3[4] = packet.ReadBit();
                guid4[0] = packet.ReadBit();
                guid4[2] = packet.ReadBit();
                guid4[1] = packet.ReadBit();
                guid3[6] = packet.ReadBit();
                guid4[4] = packet.ReadBit();

                packet.ReadXORByte(guid4, 6);
                packet.ReadXORByte(guid4, 2);
                packet.ReadXORByte(guid4, 7);
                packet.ReadXORByte(guid4, 0);
                packet.ReadXORByte(guid3, 3);
                packet.ReadXORByte(guid3, 6);
                packet.ReadXORByte(guid3, 2);
                packet.ReadXORByte(guid3, 5);
                packet.ReadXORByte(guid3, 4);
                packet.ReadXORByte(guid4, 5);
                packet.ReadXORByte(guid3, 7);
                packet.ReadXORByte(guid3, 0);
                packet.ReadXORByte(guid3, 1);
                packet.ReadXORByte(guid4, 4);
                packet.ReadXORByte(guid4, 1);
                packet.ReadXORByte(guid4, 3);

                packet.WriteGuid("Guid3", guid3);
                packet.WriteGuid("Guid4", guid4);
            }
        }

        [Parser(Opcode.CMSG_AUTO_EQUIP_ITEM)]
        public static void HandleAutoEquipItem(Packet packet)
        {
            packet.ReadByte("Bag");
            packet.ReadByte("Slot");

            var bits14 = (int)packet.ReadBits(2);

            var hasSlot = new bool[bits14];
            var hasBag = new bool[bits14];

            for (var i = 0; i < bits14; ++i)
            {
                hasSlot[i] = !packet.ReadBit();
                hasBag[i] = !packet.ReadBit();
            }

            for (var i = 0; i < bits14; ++i)
            {
                if (hasBag[i])
                    packet.ReadByte("Bag", i);
                if (hasSlot[i])
                    packet.ReadByte("Slot", i);
            }
        }

        [Parser(Opcode.CMSG_REPAIR_ITEM)]
        public static void HandleRepairItem(Packet packet)
        {
            var vendorGUID = new byte[8];
            var itemGUID = new byte[8];

            vendorGUID[3] = packet.ReadBit();
            itemGUID[3] = packet.ReadBit();
            itemGUID[6] = packet.ReadBit();
            packet.ReadBit("Use guild money");
            vendorGUID[2] = packet.ReadBit();
            itemGUID[0] = packet.ReadBit();
            vendorGUID[7] = packet.ReadBit();
            vendorGUID[6] = packet.ReadBit();
            vendorGUID[0] = packet.ReadBit();
            itemGUID[7] = packet.ReadBit();
            itemGUID[4] = packet.ReadBit();
            vendorGUID[5] = packet.ReadBit();
            itemGUID[5] = packet.ReadBit();
            vendorGUID[4] = packet.ReadBit();
            itemGUID[2] = packet.ReadBit();
            vendorGUID[1] = packet.ReadBit();
            itemGUID[1] = packet.ReadBit();

            packet.ReadXORByte(itemGUID, 6);
            packet.ReadXORByte(vendorGUID, 1);
            packet.ReadXORByte(vendorGUID, 0);
            packet.ReadXORByte(vendorGUID, 2);
            packet.ReadXORByte(itemGUID, 2);
            packet.ReadXORByte(itemGUID, 5);
            packet.ReadXORByte(itemGUID, 1);
            packet.ReadXORByte(vendorGUID, 6);
            packet.ReadXORByte(vendorGUID, 7);
            packet.ReadXORByte(itemGUID, 3);
            packet.ReadXORByte(itemGUID, 7);
            packet.ReadXORByte(vendorGUID, 3);
            packet.ReadXORByte(vendorGUID, 4);
            packet.ReadXORByte(vendorGUID, 5);
            packet.ReadXORByte(itemGUID, 0);
            packet.ReadXORByte(itemGUID, 4);

            packet.WriteGuid("Vendor GUID", vendorGUID);
            packet.WriteGuid("Item GUID", itemGUID);
        }

        [Parser(Opcode.CMSG_SELL_ITEM)]
        public static void HandleSellItem(Packet packet)
        {
            var vendorGUID = new byte[8];
            var itemGUID = new byte[8];

            packet.ReadInt32("Count");

            itemGUID[7] = packet.ReadBit();
            vendorGUID[0] = packet.ReadBit();
            vendorGUID[3] = packet.ReadBit();
            itemGUID[3] = packet.ReadBit();
            vendorGUID[7] = packet.ReadBit();
            vendorGUID[6] = packet.ReadBit();
            vendorGUID[5] = packet.ReadBit();
            vendorGUID[2] = packet.ReadBit();
            itemGUID[4] = packet.ReadBit();
            itemGUID[6] = packet.ReadBit();
            itemGUID[5] = packet.ReadBit();
            itemGUID[2] = packet.ReadBit();
            vendorGUID[1] = packet.ReadBit();
            vendorGUID[4] = packet.ReadBit();
            itemGUID[0] = packet.ReadBit();
            itemGUID[1] = packet.ReadBit();

            packet.ReadXORByte(vendorGUID, 6);
            packet.ReadXORByte(vendorGUID, 2);
            packet.ReadXORByte(itemGUID, 1);
            packet.ReadXORByte(vendorGUID, 0);
            packet.ReadXORByte(vendorGUID, 7);
            packet.ReadXORByte(itemGUID, 6);
            packet.ReadXORByte(itemGUID, 0);
            packet.ReadXORByte(itemGUID, 7);
            packet.ReadXORByte(vendorGUID, 1);
            packet.ReadXORByte(vendorGUID, 5);
            packet.ReadXORByte(itemGUID, 5);
            packet.ReadXORByte(itemGUID, 3);
            packet.ReadXORByte(itemGUID, 4);
            packet.ReadXORByte(vendorGUID, 4);
            packet.ReadXORByte(vendorGUID, 3);
            packet.ReadXORByte(itemGUID, 2);

            packet.WriteGuid("Vendor GUID", vendorGUID);
            packet.WriteGuid("Item GUID", itemGUID);
        }

        [Parser(Opcode.CMSG_SWAP_INV_ITEM)]
        public static void HandleSwapItem(Packet packet)
        {
            packet.ReadByte("Bag");
            packet.ReadByte("Destination Bag");
            packet.ReadByte("Slot");
            packet.ReadByte("Destination Slot");

            var bits14 = (int)packet.ReadBits(2);

            var hasSlot = new bool[bits14];
            var hasBag = new bool[bits14];

            for (var i = 0; i < bits14; ++i)
            {
                hasSlot[i] = !packet.ReadBit();
                hasBag[i] = !packet.ReadBit();
            }

            for (var i = 0; i < bits14; ++i)
            {
                if (hasBag[i])
                    packet.ReadByte("Bag", i);
                if (hasSlot[i])
                    packet.ReadByte("Slot", i);
            }
        }
        [Parser(Opcode.CMSG_SWAP_ITEM)]
        public static void HandleSwapItem2(Packet packet)
        {
            packet.ReadByte("Bag");
            packet.ReadByte("Slot");
            packet.ReadByte("Destination Bag");
            packet.ReadByte("Destination Slot");

            var bits14 = (int)packet.ReadBits(2);

            var hasSlot = new bool[bits14];
            var hasBag = new bool[bits14];

            for (var i = 0; i < bits14; ++i)
            {
                hasSlot[i] = !packet.ReadBit();
                hasBag[i] = !packet.ReadBit();
            }

            for (var i = 0; i < bits14; ++i)
            {
                if (hasSlot[i])
                    packet.ReadByte("Bag", i);
                if (hasBag[i])
                    packet.ReadByte("Slot", i);
            }
        }

        [Parser(Opcode.CMSG_AUTOSTORE_BAG_ITEM)]
        public static void HandleSplitItem(Packet packet)
        {
            packet.ReadSByte("Bag");
            packet.ReadByte("Destination Bag");
            packet.ReadByte("Slot");
            packet.ReadByte("Destination Slot");
        }
    }
}
