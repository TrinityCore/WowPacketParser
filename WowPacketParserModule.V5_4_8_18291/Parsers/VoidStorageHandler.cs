using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class VoidStorageHandler
    {
        [Parser(Opcode.SMSG_VOID_ITEM_SWAP_RESPONSE)]
        public static void HandleVoidItemSwapResponse(Packet packet)
        {
            packet.ReadBit("Has Src Item id (Inv)");

            byte[] itemId2 = packet.StartBitStream(4, 1, 6, 0, 3, 7, 2, 5);

            packet.ReadBit("Has Dest Item id (Inv)");

            byte[] itemId1 = itemId1 = packet.StartBitStream(6, 0, 3, 2, 1, 5, 7, 4);

            var usedSrcSlot = !packet.ReadBit("Used Src Slot (Inv)"); // always set?
            var usedDestSlot = !packet.ReadBit("Used Dest Slot (Inv)");

            packet.ParseBitStream(itemId1, 3, 7, 2, 5, 0, 1, 4, 6);
            packet.WriteGuid("Dest Item Id", itemId1);

            packet.ParseBitStream(itemId2, 0, 2, 7, 5, 6, 4, 3, 1);
            packet.WriteGuid("Src Item Id", itemId2);

            if (usedSrcSlot)
                packet.ReadInt32("New Slot for Src Item");

            if (usedDestSlot)
                packet.ReadInt32("New Slot for Dest Item");
        }

        [Parser(Opcode.SMSG_VOID_STORAGE_TRANSFER_CHANGES)]
        public static void HandleVoidStorageTransferChanges(Packet packet)
        {
            var withdrawCount = packet.ReadBits("Withdraw Count", 4); //32

            var id2 = new byte[withdrawCount][];
            for (int i = 0; i < withdrawCount; ++i)
                id2[i] = packet.StartBitStream(1, 6, 7, 3, 2, 0, 4, 5);

            var depositCount = packet.ReadBits("Deposit Count", 4); //16
            var id1 = new byte[depositCount][];
            var guid = new byte[depositCount][];

            for (int i = 0; i < depositCount; ++i)
            {
                id1[i] = new byte[8];
                guid[i] = new byte[8];

                id1[i][0] = packet.ReadBit();
                guid[i][6] = packet.ReadBit();
                guid[i][4] = packet.ReadBit();
                id1[i][3] = packet.ReadBit();
                guid[i][3] = packet.ReadBit();
                id1[i][5] = packet.ReadBit();
                id1[i][7] = packet.ReadBit();
                guid[i][0] = packet.ReadBit();
                guid[i][5] = packet.ReadBit();
                guid[i][7] = packet.ReadBit();
                id1[i][6] = packet.ReadBit();
                id1[i][4] = packet.ReadBit();
                guid[i][1] = packet.ReadBit();
                id1[i][1] = packet.ReadBit();
                guid[i][2] = packet.ReadBit();
                id1[i][2] = packet.ReadBit();
            }

            for (int i = 0; i < depositCount; ++i)
            {
                packet.ReadInt32("Item Slot", i); //28
                packet.ReadXORByte(guid[i], 5);
                packet.ReadUInt32<ItemId>("Item Entry", i); //16
                packet.ReadXORByte(guid[i], 6);
                packet.ReadXORByte(guid[i], 3);
                packet.ReadInt32("Item Suffix Factor", i); //20
                packet.ReadXORByte(guid[i], 2);
                packet.ReadXORByte(id1[i], 5);
                packet.ReadInt32("Item Random Property ID", i); //24
                packet.ReadXORByte(id1[i], 3);
                packet.ReadXORByte(guid[i], 7);
                packet.ReadXORByte(guid[i], 4);
                packet.ReadXORByte(guid[i], 1);
                packet.ReadXORByte(id1[i], 0);
                packet.ReadXORByte(id1[i], 4);
                packet.ReadXORByte(id1[i], 6);
                packet.ReadInt32("New Unk", i); //32
                packet.ReadXORByte(id1[i], 1);
                packet.ReadXORByte(id1[i], 2);
                packet.ReadXORByte(guid[i], 0);
                packet.ReadXORByte(id1[i], 7);

                packet.WriteGuid("Item Id 1", id1[i], i);
                packet.WriteGuid("Item Player Creator Guid", guid[i], i);
            }

            for (int i = 0; i < withdrawCount; ++i)
            {
                packet.ParseBitStream(id2[i], 7, 3, 1, 5, 4, 0, 6, 2);
                packet.WriteGuid("Item Id 2: {0}", id2[i], i);
            }
        }

        [Parser(Opcode.CMSG_SWAP_VOID_ITEM)]
        public static void HandleVoidSwapItem(Packet packet)
        {
            packet.ReadInt32("New Slot");

            var itemId = new byte[8];
            var npcGuid = new byte[8];

            npcGuid[6] = packet.ReadBit();
            itemId[4] = packet.ReadBit();
            itemId[7] = packet.ReadBit();
            itemId[3] = packet.ReadBit();
            itemId[2] = packet.ReadBit();
            npcGuid[4] = packet.ReadBit();
            npcGuid[2] = packet.ReadBit();
            itemId[0] = packet.ReadBit();
            itemId[1] = packet.ReadBit();
            npcGuid[7] = packet.ReadBit();
            npcGuid[1] = packet.ReadBit();
            itemId[6] = packet.ReadBit();
            npcGuid[3] = packet.ReadBit();
            npcGuid[5] = packet.ReadBit();
            itemId[5] = packet.ReadBit();
            npcGuid[0] = packet.ReadBit();

            packet.ReadXORByte(npcGuid, 3);
            packet.ReadXORByte(npcGuid, 5);
            packet.ReadXORByte(itemId, 6);
            packet.ReadXORByte(npcGuid, 4);
            packet.ReadXORByte(itemId, 4);
            packet.ReadXORByte(npcGuid, 0);
            packet.ReadXORByte(itemId, 5);
            packet.ReadXORByte(itemId, 7);
            packet.ReadXORByte(npcGuid, 7);
            packet.ReadXORByte(npcGuid, 2);
            packet.ReadXORByte(npcGuid, 1);
            packet.ReadXORByte(itemId, 1);
            packet.ReadXORByte(itemId, 3);
            packet.ReadXORByte(npcGuid, 6);
            packet.ReadXORByte(itemId, 0);
            packet.ReadXORByte(itemId, 2);

            packet.WriteGuid("NPC Guid", npcGuid);
            packet.WriteGuid("Item Id", itemId);
        }

        [Parser(Opcode.CMSG_VOID_STORAGE_TRANSFER)]
        public static void HandleVoidStorageTransfer(Packet packet)
        {
            var npcGuid = new byte[8];

            npcGuid[7] = packet.ReadBit();
            npcGuid[4] = packet.ReadBit();

            var count1 = packet.ReadBits("Deposit Count", 24); // 5 or 20
            var itemsGuid = new byte[count1][];
            for (int i = 0; i < count1; ++i)
                itemsGuid[i] = packet.StartBitStream(0, 3, 6, 5, 4, 2, 1, 7); // v2+6 0-7

            var count2 = packet.ReadBits("Withdraw Count", 24); //9 or 36
            var itemsId = new byte[count2][];
            for (int i = 0; i < count2; ++i)
                itemsId[i] = packet.StartBitStream(4, 0, 5, 7, 6, 1, 2, 3); //v2+10

            npcGuid[6] = packet.ReadBit();
            npcGuid[0] = packet.ReadBit();
            npcGuid[3] = packet.ReadBit();
            npcGuid[1] = packet.ReadBit();
            npcGuid[2] = packet.ReadBit();
            npcGuid[5] = packet.ReadBit();

            // FlushBits

            for (int i = 0; i < count1; ++i)
            {
                packet.ParseBitStream(itemsGuid[i], 5, 6, 3, 4, 1, 7, 2, 0);
                packet.WriteGuid("Item Guid", itemsGuid[i], i);
            }

            packet.ReadXORByte(npcGuid, 5);

            for (int i = 0; i < count2; ++i)
            {
                packet.ParseBitStream(itemsId[i], 0, 4, 1, 2, 6, 3, 7, 5);
                packet.WriteGuid("Item Id", itemsId[i], i);
            }

            packet.ReadXORByte(npcGuid, 1);
            packet.ReadXORByte(npcGuid, 7);
            packet.ReadXORByte(npcGuid, 4);
            packet.ReadXORByte(npcGuid, 3);
            packet.ReadXORByte(npcGuid, 2);
            packet.ReadXORByte(npcGuid, 0);
            packet.ReadXORByte(npcGuid, 6);

            packet.WriteGuid("NPC Guid", npcGuid);
        }

        [Parser(Opcode.SMSG_VOID_TRANSFER_RESULT)]
        public static void HandleVoidTransferResults(Packet packet)
        {
            packet.ReadUInt32E<VoidTransferError>("Error");
        }

        [Parser(Opcode.SMSG_VOID_STORAGE_CONTENTS)]
        public static void HandleVoidStorageContents(Packet packet)
        {
            var count = packet.ReadBits("Count", 7);

            var itemId = new byte[count][];
            var creatorGuid = new byte[count][];

            for (int i = 0; i < count; ++i)
            {
                itemId[i] = new byte[8];
                creatorGuid[i] = new byte[8];

                creatorGuid[i][1] = packet.ReadBit();
                creatorGuid[i][3] = packet.ReadBit();
                itemId[i][1] = packet.ReadBit();
                creatorGuid[i][2] = packet.ReadBit();
                itemId[i][2] = packet.ReadBit();
                creatorGuid[i][5] = packet.ReadBit();
                creatorGuid[i][0] = packet.ReadBit();
                itemId[i][6] = packet.ReadBit();
                itemId[i][5] = packet.ReadBit();
                creatorGuid[i][4] = packet.ReadBit();
                itemId[i][7] = packet.ReadBit();
                itemId[i][3] = packet.ReadBit();
                itemId[i][4] = packet.ReadBit();
                itemId[i][0] = packet.ReadBit();
                creatorGuid[i][6] = packet.ReadBit();
                creatorGuid[i][7] = packet.ReadBit();
            }

            for (int i = 0; i < count; ++i)
            {
                packet.ReadXORByte(creatorGuid[i], 4);
                packet.ReadXORByte(creatorGuid[i], 7);
                packet.ReadXORByte(itemId[i], 6);
                packet.ReadXORByte(creatorGuid[i], 6);
                packet.ReadXORByte(itemId[i], 2);
                packet.ReadInt32("Item Suffix Factor", i);
                packet.ReadXORByte(itemId[i], 7);
                packet.ReadXORByte(itemId[i], 3);
                packet.ReadXORByte(creatorGuid[i], 0);
                packet.ReadInt32("Unk UInt32", i);
                packet.ReadXORByte(itemId[i], 0);
                packet.ReadInt32("Item Random Property ID", i);
                packet.ReadXORByte(creatorGuid[i], 2);
                packet.ReadXORByte(creatorGuid[i], 5);
                packet.ReadXORByte(creatorGuid[i], 3);
                packet.ReadUInt32<ItemId>("Item Entry", i);
                packet.ReadXORByte(itemId[i], 5);
                packet.ReadXORByte(itemId[i], 1);
                packet.ReadInt32("Item Slot", i);
                packet.ReadXORByte(itemId[i], 4);
                packet.ReadXORByte(creatorGuid[i], 1);

                packet.WriteGuid("Item Id", itemId[i], i);
                packet.WriteGuid("Item Player Creator Guid", creatorGuid[i], i);
            }
        }
    }
}