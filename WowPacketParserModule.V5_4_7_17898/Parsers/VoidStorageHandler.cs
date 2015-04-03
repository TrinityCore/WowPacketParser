using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class VoidStorageHandler
    {
        [Parser(Opcode.SMSG_VOID_ITEM_SWAP_RESPONSE)]
        public static void HandleVoidItemSwapResponse(Packet packet)
        {
            packet.ReadBit("Unk Bit 3 (Inv)");
            var usedDestSlot = false;
            var usedSrcSlot = false;

            byte[] itemId2 = null;
            if (usedDestSlot)
                itemId2 = packet.StartBitStream(2, 0, 6, 4, 7, 1, 5, 3);

            packet.ReadBit("Unk Bit 4 (Inv)");

            byte[] itemId1 = null;
            if (usedSrcSlot)
                itemId1 = packet.StartBitStream(1, 5, 2, 3, 4, 7, 0, 6);

            usedDestSlot = !packet.ReadBit("Used Dest Slot (Inv)");
            usedSrcSlot = !packet.ReadBit("Used Src Slot (Inv)"); // always set?

            // flushbits

            if (usedDestSlot)
            {
                packet.ParseBitStream(itemId2, 5, 1, 3, 4, 6, 0, 2, 7);
                packet.WriteGuid("Item Id 2", itemId2);
            }

            if (usedDestSlot)
                packet.ReadInt32("New Slot for Dest Item");

            if (usedSrcSlot)
            {
                packet.ParseBitStream(itemId1, 3, 4, 5, 2, 1, 0, 6, 7);
                packet.WriteGuid("Item Id 1", itemId1);
            }

            if (usedSrcSlot)
                packet.ReadInt32("New Slot for Src Item");
        }

        [Parser(Opcode.SMSG_VOID_STORAGE_CONTENTS)]
        public static void HandleVoidStorageContents(Packet packet)
        {
            var count = packet.ReadBits("Count", 7);

            var id = new byte[count][];
            var guid = new byte[count][];

            for (int i = 0; i < count; ++i)
            {
                id[i] = new byte[8];
                guid[i] = new byte[8];

                guid[i][2] = packet.ReadBit();
                id[i][2] = packet.ReadBit();
                id[i][5] = packet.ReadBit();
                id[i][7] = packet.ReadBit();
                guid[i][0] = packet.ReadBit();
                id[i][3] = packet.ReadBit();
                id[i][1] = packet.ReadBit();
                guid[i][5] = packet.ReadBit();
                id[i][0] = packet.ReadBit();
                guid[i][7] = packet.ReadBit();
                id[i][6] = packet.ReadBit();
                id[i][4] = packet.ReadBit();
                guid[i][4] = packet.ReadBit();
                guid[i][3] = packet.ReadBit();
                guid[i][1] = packet.ReadBit();
                guid[i][6] = packet.ReadBit();
            }

            for (int i = 0; i < count; ++i)
            {
                packet.ReadXORByte(id[i], 4);
                packet.ReadXORByte(guid[i], 2);
                packet.ReadXORByte(guid[i], 3);
                packet.ReadXORByte(guid[i], 1);
                packet.ReadXORByte(guid[i], 4);
                packet.ReadXORByte(guid[i], 0);
                packet.ReadXORByte(id[i], 3);
                packet.ReadXORByte(id[i], 1);
                packet.ReadXORByte(id[i], 6);
                packet.ReadInt32("Item Slot", i);
                packet.ReadInt32("Item Suffix Factor", i);
                packet.ReadXORByte(guid[i], 5);
                packet.ReadXORByte(id[i], 5);
                packet.ReadXORByte(id[i], 2);
                packet.ReadInt32("Item Random Property ID", i);
                packet.ReadXORByte(id[i], 7);
                packet.ReadXORByte(guid[i], 7);
                packet.ReadXORByte(guid[i], 6);
                packet.ReadXORByte(id[i], 0);
                packet.ReadUInt32<ItemId>("Item Entry", i);
                packet.ReadInt32("New Unk", i);
                packet.WriteGuid("Item Id", id[i], i);
                packet.WriteGuid("Item Player Creator Guid", guid[i], i);
            }
        }
        [Parser(Opcode.SMSG_VOID_TRANSFER_RESULT)]
        public static void HandleVoidTransferResults(Packet packet)
        {
            packet.ReadUInt32E<VoidTransferError>("Error");
        }

        [Parser(Opcode.SMSG_VOID_STORAGE_TRANSFER_CHANGES)]
        public static void HandleVoidStorageTransferChanges(Packet packet)
        {
            var count2 = packet.ReadBits("Count 2", 4); //32

            var id2 = new byte[count2][];
            for (int i = 0; i < count2; ++i)
                id2[i] = packet.StartBitStream(0, 6, 2, 3, 1, 5, 4, 7);

            var count1 = packet.ReadBits("Count 1", 4); //16
            var id1 = new byte[count1][];
            var guid = new byte[count1][];

            for (int i = 0; i < count1; ++i)
            {
                id1[i] = new byte[8];
                guid[i] = new byte[8];

                id1[i][7] = packet.ReadBit();
                id1[i][0] = packet.ReadBit();
                guid[i][5] = packet.ReadBit();
                id1[i][2] = packet.ReadBit();
                guid[i][0] = packet.ReadBit();
                guid[i][4] = packet.ReadBit();
                guid[i][7] = packet.ReadBit();
                guid[i][1] = packet.ReadBit();
                id1[i][4] = packet.ReadBit();
                guid[i][6] = packet.ReadBit();
                guid[i][3] = packet.ReadBit();
                id1[i][1] = packet.ReadBit();
                id1[i][5] = packet.ReadBit();
                guid[i][2] = packet.ReadBit();
                id1[i][6] = packet.ReadBit();
                id1[i][3] = packet.ReadBit();
            }

            for (int i = 0; i < count1; ++i)
            {
                packet.ReadInt32("New Unk", i); //32
                packet.ReadInt32("Item Random Property ID", i); //24

                packet.ReadXORByte(guid[i], 3);
                packet.ReadXORByte(id1[i], 6);
                packet.ReadXORByte(id1[i], 1);
                packet.ReadXORByte(guid[i], 7);
                packet.ReadXORByte(id1[i], 5);
                packet.ReadXORByte(id1[i], 7);
                packet.ReadXORByte(id1[i], 0);
                packet.ReadXORByte(guid[i], 6);
                packet.ReadXORByte(guid[i], 2);
                packet.ReadXORByte(guid[i], 4);
                packet.ReadInt32("Item Suffix Factor", i); //20
                packet.ReadXORByte(id1[i], 2);
                packet.ReadXORByte(guid[i], 0);
                packet.ReadXORByte(id1[i], 3);
                packet.ReadInt32("Item Slot", i); //28
                packet.ReadXORByte(guid[i], 5);
                packet.ReadXORByte(guid[i], 1);
                packet.ReadXORByte(id1[i], 4);
                packet.ReadUInt32<ItemId>("Item Entry", i); //16

                packet.WriteGuid("Item Id 1", id1[i], i);
                packet.WriteGuid("Item Player Creator Guid", guid[i], i);
            }

            for (int i = 0; i < count2; ++i)
            {
                packet.ParseBitStream(id2[i], 0, 1, 5, 7, 3, 6, 4, 2);
                packet.WriteGuid("Item Id 2", id2[i], i);
            }
        }
        [Parser(Opcode.SMSG_VOID_STORAGE_FAILED)]
        public static void HandleVoidStorageFailed(Packet packet)
        {
            packet.ReadByte("Unk Byte");
        }

        [Parser(Opcode.CMSG_UNLOCK_VOID_STORAGE)]
        public static void HandleVoidStorageUnlock(Packet packet)
        {
            var guid = packet.StartBitStream(5, 1, 3, 0, 4, 6, 7, 2);
            packet.ParseBitStream(guid, 3, 7, 1, 4, 5, 2, 0, 6);
            packet.WriteGuid("NPC Guid", guid);
        }

        [Parser(Opcode.CMSG_VOID_STORAGE_QUERY)]
        public static void HandleVoidStorageQuery(Packet packet)
        {
            var guid = packet.StartBitStream(6, 3, 4, 0, 5, 1, 7, 2);
            packet.ParseBitStream(guid, 6, 0, 4, 7, 2, 3, 1, 5);
            packet.WriteGuid("NPC Guid", guid);
        }

        [Parser(Opcode.CMSG_VOID_STORAGE_TRANSFER)]
        public static void HandleVoidStorageTransfer(Packet packet)
        {
            var npcGuid = new byte[8];

            npcGuid[2] = packet.ReadBit();
            npcGuid[7] = packet.ReadBit();
            var count1 = packet.ReadBits("Count 1", 24); // 5 or 20
            npcGuid[0] = packet.ReadBit();
            npcGuid[6] = packet.ReadBit();
            npcGuid[5] = packet.ReadBit();

            var itemsGuid = new byte[count1][];
            for (int i = 0; i < count1; ++i)
                itemsGuid[i] = packet.StartBitStream(7, 2, 0, 4, 6, 1, 5, 3); // v2+6 0-7

            npcGuid[3] = packet.ReadBit();
            npcGuid[1] = packet.ReadBit();
            npcGuid[4] = packet.ReadBit();

            var count2 = packet.ReadBits("Count 2", 24); //9 or 36

            var itemsId = new byte[count2][];
            for (int i = 0; i < count2; ++i)
                itemsId[i] = packet.StartBitStream(5, 7, 1, 6, 4, 3, 2, 0); //v2+10


            // FlushBits

            for (int i = 0; i < count2; ++i)
            {
                packet.ParseBitStream(itemsId[i], 4, 3, 0, 7, 2, 6, 5, 1);
                packet.WriteGuid("Item Id", itemsId[i], i);
            }

            packet.ReadXORByte(npcGuid, 3);

            for (int i = 0; i < count1; ++i)
            {
                packet.ParseBitStream(itemsGuid[i], 5, 4, 0, 7, 3, 2, 6, 1);
                packet.WriteGuid("Item Guid", itemsGuid[i], i);
            }

            packet.ReadXORByte(npcGuid, 2);
            packet.ReadXORByte(npcGuid, 1);
            packet.ReadXORByte(npcGuid, 6);
            packet.ReadXORByte(npcGuid, 4);
            packet.ReadXORByte(npcGuid, 7);
            packet.ReadXORByte(npcGuid, 0);
            packet.ReadXORByte(npcGuid, 5);

            packet.WriteGuid("NPC Guid", npcGuid);
        }

        [Parser(Opcode.CMSG_SWAP_VOID_ITEM)]
        public static void HandleVoidSwapItem(Packet packet)
        {
            packet.ReadInt32("New Slot");

            var guid = new byte[8];
            var itemId = new byte[8];

            guid[0] = packet.ReadBit();
            itemId[3] = packet.ReadBit();
            itemId[2] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            itemId[5] = packet.ReadBit();
            itemId[6] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            itemId[7] = packet.ReadBit();
            itemId[1] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            itemId[4] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            itemId[0] = packet.ReadBit();

            packet.ReadXORByte(itemId, 7);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(itemId, 6);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(itemId, 1);
            packet.ReadXORByte(itemId, 4);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(itemId, 5);
            packet.ReadXORByte(itemId, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(itemId, 3);
            packet.ReadXORByte(itemId, 0);

            packet.WriteGuid("NPC Guid", guid);
            packet.WriteGuid("Item Id", itemId);
        }
    }
}