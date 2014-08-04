using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParserModule.V5_4_8_18414.Enums;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class UnkHandler
    {
        [Parser(Opcode.CMSG_UNK_0002)]
        public static void HandleUnk0002(Packet packet)
        {
                packet.ReadBit("unk");
        }

        [Parser(Opcode.SMSG_UNK_0002)]
        public static void HandleSUnk0002(Packet packet)
        {
                packet.ReadInt32("unk");
        }

        [Parser(Opcode.SMSG_UNK_021A)]
        public static void HandleUnk021A(Packet packet)
        {
            var guid = packet.StartBitStream(2, 3, 7, 5, 4, 6, 0, 1);
            packet.ParseBitStream(guid, 2, 1, 3, 0, 7, 4, 6, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_048A)]
        public static void HandleSUnk048A(Packet packet)
        {
            var guid = new byte[8];
            var guid2 = new byte[8];

            guid2[7] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid[6] = packet.ReadBit();

            packet.ParseBitStream(guid2, 0, 5);
            packet.ParseBitStream(guid, 0, 2);
            packet.ParseBitStream(guid2, 7, 6, 1, 4);
            packet.ParseBitStream(guid, 4, 1);
            packet.ParseBitStream(guid2, 2);
            packet.ParseBitStream(guid, 6, 3, 5, 7);
            packet.ParseBitStream(guid2, 3);

            packet.WriteGuid("Guid", guid);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNK_068E)]
        public static void HandleSUnk068E(Packet packet)
        {
            packet.ReadInt32("unk");
        }

        [Parser(Opcode.SMSG_UNK_069F)]
        public static void HandleSUnk069F(Packet packet)
        {
            var guid = new byte[8];
            guid[3] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            packet.ReadBit("unk24");
            guid[2] = packet.ReadBit();
            guid[7] = packet.ReadBit();

            packet.ParseBitStream(guid, 5, 1, 6, 2, 3, 0, 4, 7);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_09F8)]
        public static void HandleSUnk09F8(Packet packet)
        {
            var guid = new byte[8];
            var guid2 = new byte[8];

            guid[6] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid2[3] = packet.ReadBit();

            packet.ParseBitStream(guid, 0);
            packet.ParseBitStream(guid2, 1);
            packet.ParseBitStream(guid, 3, 4, 5, 7);
            packet.ParseBitStream(guid2, 0);
            packet.ParseBitStream(guid, 6);
            packet.ParseBitStream(guid2, 2, 4);
            packet.ParseBitStream(guid, 1);
            packet.ReadInt32("unk32"); // 32
            packet.ParseBitStream(guid2, 3);
            packet.ParseBitStream(guid, 2);
            packet.ParseBitStream(guid2, 7, 6, 5);

            packet.WriteGuid("Guid", guid);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNK_0A27)]
        public static void HandleSUnk0A27(Packet packet)
        {
            var guid = packet.StartBitStream(3, 0, 7, 1, 5, 2, 6, 4);
            packet.ParseBitStream(guid, 3, 2, 1, 7, 6, 0, 4);
            packet.ReadInt32("Mcounter"); // 24
            packet.ParseBitStream(guid, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_0C32)]
        public static void HandleUnk0C32(Packet packet)
        {
            packet.ReadInt32("unk");
        }

        [Parser(Opcode.SMSG_UNK_0CAE)]
        public static void HandleUnk0CAE(Packet packet)
        {
            var guid = packet.StartBitStream(4, 3, 0, 2, 1, 6, 5, 7);
            packet.ParseBitStream(guid, 1, 4, 5, 6, 2, 0, 3, 7);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_0D51)]
        public static void HandleUnk0D51(Packet packet)
        {
            var count = packet.ReadBits("count", 22);
            for (var i = 0; i < count; i++)
                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID", i);
        }

        [Parser(Opcode.SMSG_UNK_0E2A)]
        public static void HandleSUnk0E2A(Packet packet)
        {
            packet.ReadInt32("Int20");
            packet.ReadInt32("Int16");
        }

        [Parser(Opcode.SMSG_UNK_0EBA)]
        public static void HandleSUnk0EBA(Packet packet)
        {
            for (var i = 0; i < 4; i++)
            {
                packet.ReadInt32("unk20", i);
                packet.ReadInt32("unk36", i);
                packet.ReadInt32("unk24", i);
                packet.ReadInt32("unk16", i);
                packet.ReadInt32("unk28", i);
                packet.ReadInt32("unk40", i);
                packet.ReadInt32("unk32", i);
                packet.ReadInt32("unk44", i);
            }
        }

        [Parser(Opcode.SMSG_UNK_103E)]
        public static void HandleSUnk103E(Packet packet)
        {
            packet.ReadInt32("Int32");
            packet.ReadInt32("Int28");
            packet.ReadInt32("Int24");
            packet.ReadInt64("QW16");
        }

        [Parser(Opcode.SMSG_UNK_109A)]
        public static void HandleSUnk109A(Packet packet)
        {
            var guid = packet.StartBitStream(6, 7, 2, 5, 3, 0, 1, 4);
            packet.ParseBitStream(guid, 2, 5, 6, 7, 1, 4, 3, 0);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_1163)]
        public static void HandleSUnk1163(Packet packet)
        {
            var guid = packet.StartBitStream(4, 7, 1, 5, 6, 0, 2, 3);
            packet.ParseBitStream(guid, 5, 7);
            packet.ReadInt32("Mcounter"); // 24
            packet.ParseBitStream(guid, 3, 1, 2, 4, 6, 0);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_11E1)]
        public static void HandleSUnk11E1(Packet packet)
        {
            packet.ReadBits("Unk16", 2);
        }

        [Parser(Opcode.SMSG_UNK_1443)]
        public static void HandleSUnk1443(Packet packet)
        {
            var guid = new byte[8];
            var guid2 = new byte[8];

            guid2[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid2[0] = packet.ReadBit();

            packet.ParseBitStream(guid, 6, 2);
            packet.ParseBitStream(guid2, 2, 5);
            packet.ParseBitStream(guid, 7, 5, 3, 1);
            packet.ParseBitStream(guid2, 3, 1);
            packet.ReadInt32("unk32"); // 32
            packet.ParseBitStream(guid, 4);
            packet.ParseBitStream(guid2, 4, 7, 0, 6);
            packet.ParseBitStream(guid, 0);

            packet.WriteGuid("Guid", guid);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNK_148F)]
        public static void HandleSUnk148F(Packet packet)
        {
            packet.ReadInt32("unk20");
            packet.ReadBit("unk16");
        }

        [Parser(Opcode.SMSG_UNK_159F)]
        public static void HandleSUnk159F(Packet packet)
        {
            var guid = packet.StartBitStream(6, 1, 3, 7, 4, 2, 5, 0);
            packet.ParseBitStream(guid, 5, 2, 1, 6, 0);
            packet.ReadInt32("Mcounter"); // 24
            packet.ParseBitStream(guid, 3, 4, 7);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_1961)]
        public static void HandleSUnk1961(Packet packet)
        {
            packet.ReadInt32("unk64");
            packet.ReadInt32("unk16");
            for (var i = 0; i < 5; i++)
                packet.ReadInt32("unk24", i);
            packet.ReadInt32("unk20");
            for (var i = 0; i < 5; i++)
                packet.ReadInt32("unk44", i);
        }
    }
}
