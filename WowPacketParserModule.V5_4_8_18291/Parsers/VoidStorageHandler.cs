using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.V5_4_8_18291.Parsers
{
    public static class VoidStorageHandler
    {

        [Parser(Opcode.CMSG_VOID_STORAGE_TRANSFER)]
        public static void HandleVoidStorageTransfer(Packet packet)
        {
            var npcGuid = new byte[8];

            npcGuid[7] = packet.ReadBit();
            npcGuid[4] = packet.ReadBit();

            var count1 = packet.ReadBits("Count 1", 24); // 5 or 20
            var itemsGuid = new byte[count1][];
            for (int i = 0; i < count1; ++i)
                itemsGuid[i] = packet.StartBitStream(0, 3, 6, 5, 4, 2, 1, 7); // v2+6 0-7

            var count2 = packet.ReadBits("Count 2", 24); //9 or 36
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
                packet.WriteLine("[{1}] Item Id: {0}", BitConverter.ToUInt64(itemsId[i], 0), i);
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
            packet.ReadEnum<VoidTransferError>("Error", TypeCode.UInt32);
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
                packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Item Entry", i);
                packet.ReadXORByte(itemId[i], 5);
                packet.ReadXORByte(itemId[i], 1);
                packet.ReadInt32("Item Slot", i);
                packet.ReadXORByte(itemId[i], 4);
                packet.ReadXORByte(creatorGuid[i], 1);

                packet.WriteLine("[{1}] Item Id: {0}", BitConverter.ToUInt64(itemId[i], 0), i);
                packet.WriteGuid("Item Player Creator Guid", creatorGuid[i], i);
            }
        }
    }
}