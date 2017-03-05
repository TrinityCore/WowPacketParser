using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class VoidStorageHandler
    {
        [Parser(Opcode.SMSG_VOID_ITEM_SWAP_RESPONSE)] // 4.3.4
        public static void HandleVoidItemSwapResponse(Packet packet)
        {
            var usedDestSlot = !packet.Translator.ReadBit("Used Dest Slot (Inv)");
            var usedSrcSlot = !packet.Translator.ReadBit("Used Src Slot (Inv)"); // always set?

            byte[] itemId1 = null;
            if (usedSrcSlot)
                itemId1 = packet.Translator.StartBitStream(5, 2, 1, 4, 0, 6, 7, 3);

            packet.Translator.ReadBit("Unk Bit 3 (Inv)");

            byte[] itemId2 = null;
            if (usedDestSlot)
                itemId2 = packet.Translator.StartBitStream(7, 3, 4, 0, 5, 1, 2, 6);

            packet.Translator.ReadBit("Unk Bit 4 (Inv)");

            // flushbits

            if (usedDestSlot)
            {
                packet.Translator.ParseBitStream(itemId2, 4, 6, 5, 2, 3, 1, 7, 0);
                packet.Translator.WriteGuid("Item Id 2", itemId2);
            }

            if (usedSrcSlot)
            {
                packet.Translator.ParseBitStream(itemId1, 6, 3, 5, 0, 1, 2, 4, 7);
                packet.Translator.WriteGuid("Item Id 1", itemId1);
            }

            if (usedSrcSlot)
                packet.Translator.ReadInt32("New Slot for Src Item");

            if (usedDestSlot)
                packet.Translator.ReadInt32("New Slot for Dest Item");
        }

        [Parser(Opcode.SMSG_VOID_STORAGE_CONTENTS)] // 4.3.4
        public static void HandleVoidStorageContents(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 8);

            var id = new byte[count][];
            var guid = new byte[count][];
            for (int i = 0; i < count; ++i)
            {
                id[i] = new byte[8];
                guid[i] = new byte[8];

                guid[i][3] = packet.Translator.ReadBit();
                id[i][5] = packet.Translator.ReadBit();
                guid[i][6] = packet.Translator.ReadBit();
                guid[i][1] = packet.Translator.ReadBit();
                id[i][1] = packet.Translator.ReadBit();
                id[i][3] = packet.Translator.ReadBit();
                id[i][6] = packet.Translator.ReadBit();
                guid[i][5] = packet.Translator.ReadBit();
                guid[i][2] = packet.Translator.ReadBit();
                id[i][2] = packet.Translator.ReadBit();
                guid[i][4] = packet.Translator.ReadBit();
                id[i][0] = packet.Translator.ReadBit();
                id[i][4] = packet.Translator.ReadBit();
                id[i][7] = packet.Translator.ReadBit();
                guid[i][0] = packet.Translator.ReadBit();
                guid[i][7] = packet.Translator.ReadBit();
            }

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadXORByte(guid[i], 3);

                packet.Translator.ReadInt32("Item Suffix Factor", i);

                packet.Translator.ReadXORByte(guid[i], 4);

                packet.Translator.ReadInt32("Item Slot", i);

                packet.Translator.ReadXORByte(id[i], 0);
                packet.Translator.ReadXORByte(id[i], 6);
                packet.Translator.ReadXORByte(guid[i], 0);
                packet.Translator.ReadXORByte(guid[i], 1);

                packet.Translator.ReadInt32("Item Random Property ID", i);

                packet.Translator.ReadXORByte(id[i], 4);
                packet.Translator.ReadXORByte(id[i], 5);
                packet.Translator.ReadXORByte(id[i], 2);
                packet.Translator.ReadXORByte(guid[i], 2);
                packet.Translator.ReadXORByte(guid[i], 6);
                packet.Translator.ReadXORByte(id[i], 1);
                packet.Translator.ReadXORByte(id[i], 3);
                packet.Translator.ReadXORByte(guid[i], 5);
                packet.Translator.ReadXORByte(guid[i], 7);

                packet.Translator.ReadUInt32<ItemId>("Item Entry", i);

                packet.Translator.ReadXORByte(id[i], 7);

                packet.Translator.WriteGuid("Item Id", id[i], i);
                packet.Translator.WriteGuid("Item Player Creator Guid", guid[i], i);
            }
        }

        [Parser(Opcode.SMSG_VOID_STORAGE_TRANSFER_CHANGES)] // 4.3.4
        public static void HandleVoidStorageTransferChanges(Packet packet)
        {
            var count1 = packet.Translator.ReadBits("Count 1", 5);
            var count2 = packet.Translator.ReadBits("Count 2", 5);

            var id1 = new byte[count1][];
            var guid = new byte[count1][];
            for (int i = 0; i < count1; ++i)
            {
                id1[i] = new byte[8];
                guid[i] = new byte[8];

                guid[i][7] = packet.Translator.ReadBit();
                id1[i][7] = packet.Translator.ReadBit();
                id1[i][4] = packet.Translator.ReadBit();
                guid[i][6] = packet.Translator.ReadBit();
                guid[i][5] = packet.Translator.ReadBit();
                id1[i][3] = packet.Translator.ReadBit();
                id1[i][5] = packet.Translator.ReadBit();
                guid[i][4] = packet.Translator.ReadBit();
                guid[i][2] = packet.Translator.ReadBit();
                guid[i][0] = packet.Translator.ReadBit();
                guid[i][3] = packet.Translator.ReadBit();
                guid[i][1] = packet.Translator.ReadBit();
                id1[i][2] = packet.Translator.ReadBit();
                id1[i][0] = packet.Translator.ReadBit();
                id1[i][1] = packet.Translator.ReadBit();
                id1[i][6] = packet.Translator.ReadBit();
            }

            var id2 = new byte[count2][];
            for (int i = 0; i < count2; ++i)
                id2[i] = packet.Translator.StartBitStream(1, 7, 3, 5, 6, 2, 4, 0);

            for (int i = 0; i < count2; ++i)
            {
                packet.Translator.ParseBitStream(id2[i], 3, 1, 0, 2, 7, 5, 6, 4);
                packet.Translator.WriteGuid("Item Id 2", id2[i], i);
            }

            for (int i = 0; i < count1; ++i)
            {
                packet.Translator.ReadInt32("Item Suffix Factor", i);

                packet.Translator.ReadXORByte(id1[i], 6);
                packet.Translator.ReadXORByte(id1[i], 4);
                packet.Translator.ReadXORByte(guid[i], 4);
                packet.Translator.ReadXORByte(id1[i], 2);
                packet.Translator.ReadXORByte(guid[i], 1);
                packet.Translator.ReadXORByte(guid[i], 3);
                packet.Translator.ReadXORByte(id1[i], 3);
                packet.Translator.ReadXORByte(guid[i], 0);
                packet.Translator.ReadXORByte(id1[i], 0);
                packet.Translator.ReadXORByte(guid[i], 6);
                packet.Translator.ReadXORByte(id1[i], 5);
                packet.Translator.ReadXORByte(guid[i], 5);
                packet.Translator.ReadXORByte(guid[i], 7);

                packet.Translator.ReadUInt32<ItemId>("Item Entry", i);

                packet.Translator.ReadXORByte(id1[i], 1);

                packet.Translator.ReadInt32("Item Slot", i);

                packet.Translator.ReadXORByte(guid[i], 2);
                packet.Translator.ReadXORByte(id1[i], 7);

                packet.Translator.ReadInt32("Item Random Property ID", i);

                packet.Translator.WriteGuid("Item Id 1", id1[i], i);
                packet.Translator.WriteGuid("Item Player Creator Guid", guid[i], i);
            }
        }

        [Parser(Opcode.SMSG_VOID_TRANSFER_RESULT)]
        public static void HandleVoidTransferResults(Packet packet)
        {
            packet.Translator.ReadUInt32E<VoidTransferError>("Error");
        }

        [Parser(Opcode.CMSG_VOID_STORAGE_TRANSFER)] // 4.3.4
        public static void HandleVoidStorageTransfer(Packet packet)
        {
            var npcGuid = new byte[8];
            npcGuid[1] = packet.Translator.ReadBit();

            var count1 = packet.Translator.ReadBits("Count 1", 26);

            var itemsGuid = new byte[count1][];
            for (int i = 0; i < count1; ++i)
                itemsGuid[i] = packet.Translator.StartBitStream(4, 6, 7, 0, 1, 5, 3, 2);

            npcGuid[2] = packet.Translator.ReadBit();
            npcGuid[0] = packet.Translator.ReadBit();
            npcGuid[3] = packet.Translator.ReadBit();
            npcGuid[5] = packet.Translator.ReadBit();
            npcGuid[6] = packet.Translator.ReadBit();
            npcGuid[4] = packet.Translator.ReadBit();

            var count2 = packet.Translator.ReadBits("Count 2", 26);

            var itemsId = new byte[count2][];
            for (int i = 0; i < count2; ++i)
                itemsId[i] = packet.Translator.StartBitStream(4, 7, 1, 0, 2, 3, 5, 6);

            npcGuid[7] = packet.Translator.ReadBit();

            // FlushBits

            for (int i = 0; i < count1; ++i)
            {
                packet.Translator.ParseBitStream(itemsGuid[i], 6, 1, 0, 2, 4, 5, 3, 7);
                packet.Translator.WriteGuid("Item Guid", itemsGuid[i], i);
            }

            packet.Translator.ReadXORByte(npcGuid, 5);
            packet.Translator.ReadXORByte(npcGuid, 6);

            for (int i = 0; i < count2; ++i)
            {
                packet.Translator.ParseBitStream(itemsId[i], 3, 1, 0, 6, 2, 7, 5, 4);
                packet.Translator.WriteGuid("Item Id:", itemsId[i], i);
            }

            packet.Translator.ReadXORByte(npcGuid, 1);
            packet.Translator.ReadXORByte(npcGuid, 4);
            packet.Translator.ReadXORByte(npcGuid, 7);
            packet.Translator.ReadXORByte(npcGuid, 3);
            packet.Translator.ReadXORByte(npcGuid, 2);
            packet.Translator.ReadXORByte(npcGuid, 0);

            packet.Translator.WriteGuid("NPC Guid", npcGuid);
        }

        [Parser(Opcode.CMSG_SWAP_VOID_ITEM)] // 4.3.4
        public static void HandleVoidSwapItem(Packet packet)
        {
            packet.Translator.ReadInt32("New Slot");

            var guid = new byte[8];
            var itemId = new byte[8];

            guid[2] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            itemId[2] = packet.Translator.ReadBit();
            itemId[6] = packet.Translator.ReadBit();
            itemId[5] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            itemId[3] = packet.Translator.ReadBit();
            itemId[7] = packet.Translator.ReadBit();
            itemId[0] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            itemId[1] = packet.Translator.ReadBit();
            itemId[4] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(itemId, 3);
            packet.Translator.ReadXORByte(itemId, 2);
            packet.Translator.ReadXORByte(itemId, 4);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(itemId, 6);
            packet.Translator.ReadXORByte(itemId, 1);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(itemId, 5);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(itemId, 0);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(itemId, 7);

            packet.Translator.WriteGuid("NPC Guid", guid);
            packet.Translator.WriteGuid("Item Id", itemId);
        }

        [Parser(Opcode.CMSG_QUERY_VOID_STORAGE)] // 4.3.4
        public static void HandleVoidStorageQuery(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(4, 0, 5, 7, 6, 3, 1, 2);
            packet.Translator.ParseBitStream(guid, 5, 6, 3, 7, 1, 0, 4, 2);
            packet.Translator.WriteGuid("NPC Guid", guid);
        }

        [Parser(Opcode.SMSG_VOID_STORAGE_FAILED)]
        public static void HandleVoidStorageFailed(Packet packet)
        {
            packet.Translator.ReadByte("Unk Byte");
        }

        [Parser(Opcode.CMSG_UNLOCK_VOID_STORAGE)] // 4.3.4
        public static void HandleVoidStorageUnlock(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(4, 5, 3, 0, 2, 1, 7, 6);
            packet.Translator.ParseBitStream(guid, 7, 1, 2, 3, 5, 0, 6, 4);
            packet.Translator.WriteGuid("NPC Guid", guid);
        }
    }
}
