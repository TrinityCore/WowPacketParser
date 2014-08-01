using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.V5_4_8_18414.Parsers
{
    public static class VoidStorageHandler
    {
        [Parser(Opcode.CMSG_VOID_STORAGE_QUERY)]
        public static void HandleVoidStorageQuery(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_VOID_STORAGE_TRANSFER)]
        public static void HandleVoidStorageTransfer(Packet packet)
        {
            var guid = new byte[8];
            guid[7] = packet.ReadBit();
            guid[4] = packet.ReadBit();

            var count1 = packet.ReadBits("count1", 24);
            var guid1 = new byte[count1][];
            for (var i = 0; i < count1; i++)
                guid1[i] = packet.StartBitStream(0, 3, 6, 5, 4, 2, 1, 7);

            var count2 = packet.ReadBits("count2", 24);
            var guid2 = new byte[count2][];
            for (var i = 0; i < count2; i++)
                guid2[i] = packet.StartBitStream(4, 0, 5, 7, 6, 1, 2, 3);

            guid[6] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[5] = packet.ReadBit();

            packet.ResetBitReader();

            for (var i = 0; i < count1; i++)
            {
                packet.ParseBitStream(guid1[i], 5, 6, 3, 4, 1, 7, 2, 0);
                packet.WriteGuid("Guid1", guid1[i], i);
            }

            packet.ParseBitStream(guid, 5);

            for (var i = 0; i < count2; i++)
            {
                packet.ParseBitStream(guid2[i], 0, 4, 1, 2, 6, 3, 7, 5);
                packet.WriteGuid("Guid2", guid2[i], i);
            }
            packet.ParseBitStream(guid, 1, 7, 4, 3, 2, 0, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_VOID_STORAGE_UNLOCK)]
        public static void HandleVoidStorageUnlock(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_VOID_SWAP_ITEM)]
        public static void HandleVoidSwapItem(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_VOID_ITEM_SWAP_RESPONSE)]
        public static void HandleVoidItemSwapResponse(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_VOID_STORAGE_CONTENTS)]
        public static void HandleVoidStorageContents(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_VOID_STORAGE_FAILED)]
        public static void HandleVoidStorageFailed(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_VOID_STORAGE_TRANSFER_CHANGES)]
        public static void HandleVoidStorageTransferChanges(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_VOID_TRANSFER_RESULT)]
        public static void HandleVoidTransferResults(Packet packet)
        {
            packet.ReadEnum<VoidTransferError>("Error", TypeCode.UInt32);
        }
    }
}
