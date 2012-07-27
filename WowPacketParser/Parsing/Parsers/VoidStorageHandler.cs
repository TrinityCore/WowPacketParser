using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class VoidStorageHandler
    {
        [Parser(Opcode.SMSG_VOID_ITEM_SWAP_RESPONSE)] // 4.3.4, not tested
        public static void HandleVoidItemSwapResponse(Packet packet)
        {
            var unkBit1 = !packet.ReadBit("Unk Bit 1 (Inv)");
            var unkBit2 = !packet.ReadBit("Unk Bit 2 (Inv)");

            byte[] guid1 = null;
            if (unkBit2)
                guid1 = packet.StartBitStream(5, 2, 1, 4, 0, 6, 7, 3);

            packet.ReadBit("Unk Bit 3 (Inv)");

            byte[] guid2 = null;
            if (unkBit1) // ? - *((_QWORD *)v4 + 2) != 0i64;
                guid2 = packet.StartBitStream(7, 3, 4, 0, 5, 1, 2, 6);

            var unkBit4 = !packet.ReadBit("Unk Bit 4 (Inv)");

            if (unkBit1) // ? - *((_QWORD *)v4 + 2) != 0i64;
            {
                packet.ParseBitStream(guid2, 4, 6, 5, 2, 3, 1, 7, 0);
                packet.WriteGuid("Unk Guid 2", guid2);
            }

            var unkBit5 = !packet.ReadBit("Unk Bit 5 (Inv)");

            if (unkBit2) // ? - *((_QWORD *)v4 + 3) != 0i64;
            {
                packet.ParseBitStream(guid1, 6, 3, 5, 0, 1, 2, 4, 7);
                packet.WriteGuid("Unk Guid 1", guid1);
            }

            if (unkBit4)
                packet.ReadInt32("Unk Int32");

            if (unkBit5)
                packet.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.SMSG_VOID_STORAGE_CONTENTS)] // 4.3.4
        public static void HandleVoidStorageContents(Packet packet)
        {
            var count = packet.ReadBits("Count", 8);

            var id = new byte[count][];
            var guid = new byte[count][];
            for (int i = 0; i < count; ++i)
            {
                id[i] = new byte[8];
                guid[i] = new byte[8];

                guid[i][3] = packet.ReadBit().ToByte();
                id[i][5] = packet.ReadBit().ToByte();
                guid[i][6] = packet.ReadBit().ToByte();
                guid[i][1] = packet.ReadBit().ToByte();
                id[i][1] = packet.ReadBit().ToByte();
                id[i][3] = packet.ReadBit().ToByte();
                id[i][6] = packet.ReadBit().ToByte();
                guid[i][5] = packet.ReadBit().ToByte();
                guid[i][2] = packet.ReadBit().ToByte();
                id[i][2] = packet.ReadBit().ToByte();
                guid[i][4] = packet.ReadBit().ToByte();
                id[i][0] = packet.ReadBit().ToByte();
                id[i][4] = packet.ReadBit().ToByte();
                id[i][7] = packet.ReadBit().ToByte();
                guid[i][0] = packet.ReadBit().ToByte();
                guid[i][7] = packet.ReadBit().ToByte();
            }

            for (int i = 0; i < count; ++i)
            {
                if (guid[i][3] != 0) guid[i][3] ^= packet.ReadByte();

                packet.ReadInt32("Unk Int32 1", i);

                if (guid[i][4] != 0) guid[i][4] ^= packet.ReadByte();

                packet.ReadInt32("Item Slot", i); // not confirmed

                if (id[i][0] != 0) id[i][0] ^= packet.ReadByte();
                if (id[i][6] != 0) id[i][6] ^= packet.ReadByte();
                if (guid[i][0] != 0) guid[i][0] ^= packet.ReadByte();
                if (guid[i][1] != 0) guid[i][1] ^= packet.ReadByte();

                packet.ReadInt32("Unk Int32 3", i);

                if (id[i][4] != 0) id[i][4] ^= packet.ReadByte();
                if (id[i][5] != 0) id[i][5] ^= packet.ReadByte();
                if (id[i][2] != 0) id[i][2] ^= packet.ReadByte();
                if (guid[i][2] != 0) guid[i][2] ^= packet.ReadByte();
                if (guid[i][6] != 0) guid[i][6] ^= packet.ReadByte();
                if (id[i][1] != 0) id[i][1] ^= packet.ReadByte();
                if (id[i][3] != 0) id[i][3] ^= packet.ReadByte();
                if (guid[i][5] != 0) guid[i][5] ^= packet.ReadByte();
                if (guid[i][7] != 0) guid[i][7] ^= packet.ReadByte();

                packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Item Entry", i);

                if (id[i][7] != 0) id[i][7] ^= packet.ReadByte();

                packet.WriteLine("[{1}] Item Id: {0}", BitConverter.ToUInt64(id[i], 0), i); // not confirmed
                packet.WriteGuid("Item Player Creator Guid", guid[i], i);
            }
        }

        [Parser(Opcode.SMSG_VOID_STORAGE_TRANSFER_CHANGES)] // 4.3.4
        public static void HandleVoidStorageTransferChanges(Packet packet)
        {
            var count1 = packet.ReadBits("Count 1", 5);
            var count2 = packet.ReadBits("Count 2", 5);

            var id1 = new byte[count1][];
            var guid = new byte[count1][];
            for (int i = 0; i < count1; ++i)
            {
                id1[i] = new byte[8];
                guid[i] = new byte[8];

                guid[i][7] = packet.ReadBit().ToByte();
                id1[i][7] = packet.ReadBit().ToByte();
                id1[i][4] = packet.ReadBit().ToByte();
                guid[i][6] = packet.ReadBit().ToByte();
                guid[i][5] = packet.ReadBit().ToByte();
                id1[i][3] = packet.ReadBit().ToByte();
                id1[i][5] = packet.ReadBit().ToByte();
                guid[i][4] = packet.ReadBit().ToByte();
                guid[i][2] = packet.ReadBit().ToByte();
                guid[i][0] = packet.ReadBit().ToByte();
                guid[i][3] = packet.ReadBit().ToByte();
                guid[i][1] = packet.ReadBit().ToByte();
                id1[i][2] = packet.ReadBit().ToByte();
                id1[i][0] = packet.ReadBit().ToByte();
                id1[i][1] = packet.ReadBit().ToByte();
                id1[i][6] = packet.ReadBit().ToByte();
            }

            var id2 = new byte[count2][];
            for (int i = 0; i < count2; ++i)
                id2[i] = packet.StartBitStream(1, 7, 3, 5, 6, 2, 4, 0);

            for (int i = 0; i < count2; ++i)
            {
                packet.ParseBitStream(id2[i], 3, 1, 0, 2, 7, 5, 6, 4);
                packet.WriteLine("[{1}] Item Id 2: {0}", BitConverter.ToUInt64(id2[i], 0), i); // not confirmed
            }

            for (int i = 0; i < count1; ++i)
            {
                packet.ReadInt32("Unk Int32 1", i);

                if (id1[i][6] != 0) id1[i][6] ^= packet.ReadByte();
                if (id1[i][4] != 0) id1[i][4] ^= packet.ReadByte();
                if (guid[i][4] != 0) guid[i][4] ^= packet.ReadByte();
                if (id1[i][2] != 0) id1[i][2] ^= packet.ReadByte();
                if (guid[i][1] != 0) guid[i][1] ^= packet.ReadByte();
                if (guid[i][3] != 0) guid[i][3] ^= packet.ReadByte();
                if (id1[i][3] != 0) id1[i][3] ^= packet.ReadByte();
                if (guid[i][0] != 0) guid[i][0] ^= packet.ReadByte();
                if (id1[i][0] != 0) id1[i][0] ^= packet.ReadByte();
                if (guid[i][6] != 0) guid[i][6] ^= packet.ReadByte();
                if (id1[i][5] != 0) id1[i][5] ^= packet.ReadByte();
                if (guid[i][5] != 0) guid[i][5] ^= packet.ReadByte();
                if (guid[i][7] != 0) guid[i][7] ^= packet.ReadByte();

                packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Item Entry", i);

                if (id1[i][1] != 0) id1[i][1] ^= packet.ReadByte();

                packet.ReadInt32("Item Slot", i); // not confirmed

                if (guid[i][2] != 0) guid[i][2] ^= packet.ReadByte();
                if (id1[i][7] != 0) id1[i][7] ^= packet.ReadByte();

                packet.ReadInt32("Unk Int32 3", i);

                packet.WriteLine("[{1}] Item Id 1: {0}", BitConverter.ToUInt64(id1[i], 0), i); // not confirmed
                packet.WriteGuid("Item Player Creator Guid", guid[i], i);
            }
        }

        [Parser(Opcode.SMSG_VOID_TRANSFER_RESULT)]
        public static void HandleVoidTransferResults(Packet packet)
        {
            packet.ReadEnum<VoidTransferError>("Error", TypeCode.UInt32);
        }

        [Parser(Opcode.CMSG_VOID_STORAGE_TRANSFER)] // 4.3.4
        public static void HandleVoidStorageTransfer(Packet packet)
        {
            var npcGuid = new byte[8];
            npcGuid[1] = packet.ReadBit().ToByte();

            var count1 = packet.ReadBits("Count 1", 26);

            var itemsGuid = new byte[count1][];
            for (int i = 0; i < count1; ++i)
                itemsGuid[i] = packet.StartBitStream(4, 6, 7, 0, 1, 5, 3, 2);

            npcGuid[2] = packet.ReadBit().ToByte();
            npcGuid[0] = packet.ReadBit().ToByte();
            npcGuid[3] = packet.ReadBit().ToByte();
            npcGuid[5] = packet.ReadBit().ToByte();
            npcGuid[6] = packet.ReadBit().ToByte();
            npcGuid[4] = packet.ReadBit().ToByte();

            var count2 = packet.ReadBits("Count 2", 26);

            var itemsId = new byte[count2][];
            for (int i = 0; i < count2; ++i)
                itemsId[i] = packet.StartBitStream(4, 7, 1, 0, 2, 3, 5, 6);

            npcGuid[7] = packet.ReadBit().ToByte();

            // FlushBits

            for (int i = 0; i < count1; ++i)
            {
                packet.ParseBitStream(itemsGuid[i], 6, 1, 0, 2, 4, 5, 3, 7);
                packet.WriteGuid("Item Guid", itemsGuid[i], i);
            }

            if (npcGuid[5] != 0) npcGuid[5] ^= packet.ReadByte();
            if (npcGuid[6] != 0) npcGuid[6] ^= packet.ReadByte();

            for (int i = 0; i < count2; ++i)
            {
                packet.ParseBitStream(itemsId[i], 3, 1, 0, 6, 2, 7, 5, 4);
                packet.WriteLine("[{1}] Item Id: {0}", BitConverter.ToUInt64(itemsId[i], 0), i);
            }

            if (npcGuid[1] != 0) npcGuid[1] ^= packet.ReadByte();
            if (npcGuid[4] != 0) npcGuid[4] ^= packet.ReadByte();
            if (npcGuid[7] != 0) npcGuid[7] ^= packet.ReadByte();
            if (npcGuid[3] != 0) npcGuid[3] ^= packet.ReadByte();
            if (npcGuid[2] != 0) npcGuid[2] ^= packet.ReadByte();
            if (npcGuid[0] != 0) npcGuid[0] ^= packet.ReadByte();

            packet.WriteGuid("NPC Guid", npcGuid);
        }

        [Parser(Opcode.CMSG_VOID_SWAP_ITEM)] // 4.3.4, not tested
        public static void HandleVoidSwapItem(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid1[2] = packet.ReadBit().ToByte();
            guid1[4] = packet.ReadBit().ToByte();
            guid1[0] = packet.ReadBit().ToByte();
            guid2[2] = packet.ReadBit().ToByte();
            guid2[6] = packet.ReadBit().ToByte();
            guid2[5] = packet.ReadBit().ToByte();
            guid1[1] = packet.ReadBit().ToByte();
            guid1[7] = packet.ReadBit().ToByte();
            guid2[3] = packet.ReadBit().ToByte();
            guid2[7] = packet.ReadBit().ToByte();
            guid2[0] = packet.ReadBit().ToByte();
            guid1[6] = packet.ReadBit().ToByte();
            guid1[5] = packet.ReadBit().ToByte();
            guid1[3] = packet.ReadBit().ToByte();
            guid2[1] = packet.ReadBit().ToByte();
            guid2[4] = packet.ReadBit().ToByte();

            if (guid1[1] != 0) guid1[1] ^= packet.ReadByte();
            if (guid2[3] != 0) guid2[3] ^= packet.ReadByte();
            if (guid2[2] != 0) guid2[2] ^= packet.ReadByte();
            if (guid2[4] != 0) guid2[4] ^= packet.ReadByte();
            if (guid1[3] != 0) guid1[3] ^= packet.ReadByte();
            if (guid1[0] != 0) guid1[0] ^= packet.ReadByte();
            if (guid2[6] != 0) guid2[6] ^= packet.ReadByte();
            if (guid2[1] != 0) guid2[1] ^= packet.ReadByte();
            if (guid1[5] != 0) guid1[5] ^= packet.ReadByte();
            if (guid2[5] != 0) guid2[5] ^= packet.ReadByte();
            if (guid1[6] != 0) guid1[6] ^= packet.ReadByte();
            if (guid2[0] != 0) guid2[0] ^= packet.ReadByte();
            if (guid1[2] != 0) guid1[2] ^= packet.ReadByte();
            if (guid1[7] != 0) guid1[7] ^= packet.ReadByte();
            if (guid1[4] != 0) guid1[4] ^= packet.ReadByte();
            if (guid2[7] != 0) guid2[7] ^= packet.ReadByte();
        }

        [Parser(Opcode.CMSG_VOID_STORAGE_QUERY)] // 4.3.4
        public static void HandleVoidStorageQuery(Packet packet)
        {
            var guid = packet.StartBitStream(4, 0, 5, 7, 6, 3, 1, 2);
            packet.ParseBitStream(guid, 5, 6, 3, 7, 1, 0, 4, 2);
            packet.WriteGuid("NPC Guid", guid);
        }

        [Parser(Opcode.SMSG_VOID_STORAGE_FAILED)]
        public static void HandleVoidStorageFailed(Packet packet)
        {
            packet.ReadByte("Unk Byte");
        }

        [Parser(Opcode.CMSG_VOID_STORAGE_UNLOCK)] // 4.3.4, not tested
        public static void HandleVoidStorageUnlock(Packet packet)
        {
            var guid = packet.StartBitStream(4, 5, 3, 0, 2, 1, 7, 6);
            packet.ParseBitStream(guid, 7, 1, 2, 3, 5, 0, 6, 4);
            packet.WriteGuid("Unk Guid", guid); // probably vaultkeeper guid
        }
    }
}
