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

        [Parser(Opcode.CMSG_UNK_0123)]
        public static void HandleUnk0123(Packet packet)
        {
            packet.ReadGuid("Item");
        }

        [Parser(Opcode.CMSG_UNK_0882)]
        public static void HandleUnk0882(Packet packet)
        {
            packet.ReadInt32("RealmID");
            var guid = packet.StartBitStream(0, 2, 5, 1, 6, 4, 3, 7);
            packet.ParseBitStream(guid, 7, 3, 1, 5, 4, 0, 2, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_UNK_1886)]
        public static void HandleUnk1886(Packet packet)
        {
            packet.ReadInt32("unk28");
            packet.ReadInt32("unk24");
            var guid = packet.StartBitStream(2, 3, 5, 1, 4, 7, 0, 6);
            packet.ParseBitStream(guid, 5, 3, 7, 1, 4, 0, 6, 2);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_UNK_19C3)]
        public static void HandleUnk19C3(Packet packet)
        {
            var guid = packet.StartBitStream(4, 3, 6, 1, 0, 2, 5, 7);
            packet.ParseBitStream(guid, 0, 5, 1, 4, 2, 6, 7, 3);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_UNK_1D36)]
        public static void HandleUnk1D36(Packet packet)
        {
            packet.ReadInt32("unk");
        }

        [Parser(Opcode.SMSG_UNK_0002)]
        public static void HandleSUnk0002(Packet packet)
        {
            packet.ReadInt32("unk");
        }

        [Parser(Opcode.SMSG_UNK_000F)]
        public static void HandleSUnk000F(Packet packet)
        {
            var guid = packet.StartBitStream(3, 2, 6, 0, 4, 5, 7, 1);
            packet.ParseBitStream(guid, 6, 1, 5, 7, 3, 4, 2, 0);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_0170)]
        public static void HandleSUnk0170(Packet packet)
        {
            var count = packet.ReadBits("Count", 19);
            for (var i = 0; i < count; i++)
                packet.ReadBitVisible("unk20", i);
            for (var i = 0; i < count; i++)
            {
                packet.ReadInt32Visible("unk28", i);
                packet.ReadInt32Visible("unk36", i);
                packet.ReadInt32Visible("unk24", i);
                packet.ReadInt32Visible("unk20", i);
                packet.ReadInt32Visible("unk32", i);
            }
        }

        [Parser(Opcode.SMSG_UNK_021A)]
        public static void HandleSUnk021A(Packet packet)
        {
            var guid = packet.StartBitStream(2, 3, 7, 5, 4, 6, 0, 1);
            packet.ParseBitStream(guid, 2, 1, 3, 0, 7, 4, 6, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_0354)]
        public static void HandleSUnk0354(Packet packet)
        {
            packet.ReadInt32("Unk");
        }

        [Parser(Opcode.SMSG_UNK_0364)]
        public static void HandleSUnk0364(Packet packet)
        {
            var count = packet.ReadBits("Count", 22);
            for (var i = 0; i < count; i++)
                packet.ReadInt32Visible("Unk20", i);
        }

        [Parser(Opcode.SMSG_UNK_041F)]
        public static void HandleSUnk041F(Packet packet)
        {
            var guid = packet.StartBitStream(4, 2, 3, 6, 0, 5, 7, 1);
            var count = packet.ReadBits("count", 3);
            for (var i = 0; i < count; i++)
            {
                packet.ReadInt32("unk48", i); // 48
                packet.ReadInt32("unk28", i); // 28
                packet.ReadInt32("unk44", i); // 44
                packet.ReadInt32("unk32", i); // 32
                packet.ReadInt32("unk36", i); // 36
                packet.ReadInt32("unk40", i); // 40
                packet.ReadInt32("unk52", i); // 52
                packet.ReadByte("unk28", i); // 28
            }
            packet.ParseBitStream(guid, 1, 7, 3, 2, 0, 5, 6, 4);
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

        [Parser(Opcode.SMSG_UNK_04AA)]
        public static void HandleSUnk04AA(Packet packet)
        {
            packet.ReadSingle("unk1");
            packet.ReadInt32("unk2");
        }

        [Parser(Opcode.SMSG_UNK_060A)]
        public static void HandleSUnk060A(Packet packet)
        {
            var count16 = packet.ReadBits("count16", 20);
            var guid = new byte[count16][];
            for (var i = 0; i < count16; i++)
                guid[i] = packet.StartBitStream(1, 5, 0, 4, 6, 3, 7, 2);
            for (var i = 0; i < count16; i++)
            {
                packet.ParseBitStream(guid[i], 0, 3, 7);
                packet.ReadByte("unk36", i);
                packet.ReadSingle("unk32", i);
                packet.ParseBitStream(guid[i], 4, 2, 1, 5);
                packet.ReadSingle("unk28", i);
                packet.ReadByte("unk37", i);
                packet.ParseBitStream(guid[i], 6);
                packet.WriteGuid("Guid", guid[i]);
            }
        }

        [Parser(Opcode.SMSG_UNK_0612)]
        public static void HandleSUnk0612(Packet packet)
        {
            packet.ReadInt64("unk16");
            packet.ReadInt32("unk28");
            packet.ReadInt32("unk24");
        }

        [Parser(Opcode.SMSG_UNK_061E)]
        public static void HandleSUnk061E(Packet packet)
        {
            var guid = new byte[8];
            var guid2 = new byte[8];

            guid[4] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var unk50 = packet.ReadBit("unk50");
            guid[7] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[3] = packet.ReadBit();

            packet.ReadSingle("unk28h");
            packet.ParseBitStream(guid, 2, 6, 5);
            packet.ParseBitStream(guid2, 2);
            packet.ParseBitStream(guid, 1);
            packet.ReadSingle("unk20h");
            packet.ParseBitStream(guid, 3);
            packet.ReadInt16("unk56");
            packet.ParseBitStream(guid2, 4, 7);
            packet.ReadSingle("unk2ch");
            packet.ReadSingle("unk24h");
            packet.ParseBitStream(guid, 4);
            packet.ParseBitStream(guid2, 5);
            packet.ReadInt32("unk24");
            packet.ParseBitStream(guid2, 1);
            packet.ParseBitStream(guid, 7);
            packet.ReadInt16("unk48");
            packet.ParseBitStream(guid2, 0, 6);
            packet.ParseBitStream(guid, 0);
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

        [Parser(Opcode.SMSG_UNK_080A)]
        public static void HandleSUnk080A(Packet packet)
        {
            packet.ReadBit("unk16");
            packet.ReadBit("unk18");
            packet.ReadBit("unk17");
            packet.ReadBit("unk20");
            packet.ReadBit("unk19");
        }

        [Parser(Opcode.SMSG_UNK_086A)]
        public static void HandleSUnk086A(Packet packet)
        {
            var guid = packet.StartBitStream(0, 7, 3, 1, 6, 5, 2, 4);
            packet.ParseBitStream(guid, 7, 6, 4, 3, 2, 0, 1);
            packet.ReadInt32("MCounter");
            packet.ParseBitStream(guid, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_09E2)]
        public static void HandleSUnk09E2(Packet packet)
        {
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

        [Parser(Opcode.SMSG_UNK_0B22)]
        public static void HandleSUnk0B22(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_UNK_0C32)]
        public static void HandleSUnk0C32(Packet packet)
        {
            packet.ReadInt32("unk");
        }

        [Parser(Opcode.SMSG_UNK_0CAE)]
        public static void HandleSUnk0CAE(Packet packet)
        {
            var guid = packet.StartBitStream(4, 3, 0, 2, 1, 6, 5, 7);
            packet.ParseBitStream(guid, 1, 4, 5, 6, 2, 0, 3, 7);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_0D51)]
        public static void HandleSUnk0D51(Packet packet)
        {
            var count = packet.ReadBits("count", 22);
            for (var i = 0; i < count; i++)
                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID", i);
        }

        [Parser(Opcode.SMSG_UNK_0D52)]
        public static void HandleSUnk0D52(Packet packet)
        {
            var guid = new byte[8];
            guid[6] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[0] = packet.ReadBit();

            var count = packet.ReadBits("count", 24);
            var guid32 = new byte[count][];
            for (var i = 0; i < count; i++)
                guid32[i] = packet.StartBitStream(4, 7, 1, 3, 0, 5, 2, 6);

            guid[3] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[7] = packet.ReadBit();

            for (var i = 0; i < count; i++)
            {
                packet.ParseBitStream(guid32[i], 2, 6, 1, 0, 5, 7, 4, 3);
                packet.WriteGuid("Guid32", guid32[i]);
            }

            packet.ParseBitStream(guid, 4, 0, 5, 6);
            packet.ReadInt32("Spell");
            packet.ParseBitStream(guid, 1, 7, 3, 2);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_0E1B)]
        public static void HandleSUnk0E1B(Packet packet)
        {
            packet.ReadInt32("Int28");
            packet.ReadInt32("Int16");
            packet.ReadInt32("Int20");
            packet.ReadInt32("Int24");
        }

        [Parser(Opcode.SMSG_UNK_0E2A)]
        public static void HandleSUnk0E2A(Packet packet)
        {
            packet.ReadInt32("Int20");
            packet.ReadInt32("Int16");
        }

        [Parser(Opcode.SMSG_UNK_0E3A)]
        public static void HandleSUnk0E3A(Packet packet)
        {
            var guid = packet.StartBitStream(6, 3, 0, 7, 1, 2, 5, 4);
            packet.ParseBitStream(guid, 3, 6, 7, 5, 1, 4, 2, 0);
            packet.WriteGuid("Guid", guid);
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
            packet.ReadInt32("Unk"); // 24
            packet.ParseBitStream(guid, 3, 1, 2, 4, 6, 0);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_11E1)]
        public static void HandleSUnk11E1(Packet packet)
        {
            packet.ReadBits("Unk16", 2);
        }

        [Parser(Opcode.SMSG_UNK_12BB)]
        public static void HandleSUnk12BB(Packet packet)
        {
            var guid = new byte[8];
            packet.ReadByte("unk65"); // 65
            packet.ReadByte("unk16"); // 16
            var count36 = packet.ReadBits("count36", 21);
            var guid40 = new byte[count36][];
            for (var i = 0; i < count36; i++)
            {
                packet.ReadBit("unk48", i); // 48
                guid40[i] = packet.StartBitStream(3, 0, 5, 2, 7, 1, 4, 6);
            }
            guid[3] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var count20 = packet.ReadBits("count20", 22);
            guid[0] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var unk64 = packet.ReadBit("unk64"); // 64
            packet.ParseBitStream(guid, 0);
            for (var i = 0; i < count36; i++)
            {
                packet.ReadByte("unk56", i); // 56
                packet.ParseBitStream(guid40[i], 3, 6);
                packet.ReadInt32("unk88", i); // 88
                packet.ParseBitStream(guid40[i], 2, 4, 0, 1, 5, 7);
                packet.WriteGuid("Guid40", guid40[i]);
            }
            packet.ParseBitStream(guid, 1, 7, 6, 4, 3, 2, 5);
            packet.WriteGuid("Guid", guid);
            for (var i = 0; i < count20; i++)
                packet.ReadInt32("unk24", i);
        }

        [Parser(Opcode.SMSG_UNK_12BE)]
        public static void HandleSUnk12BE(Packet packet)
        {
            var guid = new byte[8];
            var guid2 = new byte[8];

            guid[4] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            var unk48 = packet.ReadBit("unk48");
            guid2[6] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var unk20 = packet.ReadBit("unk20");
            guid2[3] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[3] = packet.ReadBit();

            packet.ParseBitStream(guid2, 1, 6, 5, 7);
            if (unk20)
                packet.ReadSingle("unk32");
            if (unk48)
                packet.ReadSingle("unk44");
            packet.ParseBitStream(guid, 5, 7);
            packet.ParseBitStream(guid2, 4, 0);
            packet.ReadInt32("unk160");
            packet.ParseBitStream(guid2, 2);
            packet.ParseBitStream(guid, 0);
            packet.ParseBitStream(guid2, 3);
            packet.ParseBitStream(guid, 3, 1, 4, 6, 2);

            packet.WriteGuid("Guid", guid);
            packet.WriteGuid("Guid2", guid2);
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

        [Parser(Opcode.SMSG_UNK_14E2)]
        public static void HandleSUnk14E2(Packet packet)
        {
            var count = packet.ReadBits("Count", 19);
            var len = new uint[count];
            for (var i = 0; i < count; i++)
                len[i] = packet.ReadBits(8);
            for (var i = 0; i < count; i++)
            {
                packet.ReadInt32("unk68", i);
                packet.ReadInt64("unk20", i);
                packet.ReadInt32("unk84", i);
                packet.ReadInt32("unk52", i);
                packet.ReadWoWString("str", len[i], i);
            }
        }

        [Parser(Opcode.SMSG_UNK_1553)]
        public static void HandleSUnk1553(Packet packet)
        {
            var guid = packet.StartBitStream(2, 4, 0, 3, 6, 7, 5, 1);
            packet.ParseBitStream(guid, 1, 3, 5);
            packet.ReadByte("unk24");
            packet.ParseBitStream(guid, 0, 2, 7, 6, 4);
            packet.ReadByte("unk25");
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_15A9)] // CA3771
        public static void HandleSUnk15A9(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_UNK_1960)]
        public static void HandleSUnk1960(Packet packet)
        {
            var guid = new byte[8];
            var guid2 = new byte[8];

            guid[0] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid2[2] = packet.ReadBit();

            packet.ParseBitStream(guid2, 3, 0, 2);
            packet.ParseBitStream(guid, 5, 4, 7, 3, 0);
            packet.ParseBitStream(guid2, 4);
            packet.ParseBitStream(guid, 1);
            packet.ParseBitStream(guid2, 1);
            packet.ParseBitStream(guid, 6);
            packet.ParseBitStream(guid2, 7, 6);
            packet.ParseBitStream(guid, 2);
            packet.ParseBitStream(guid2, 5);

            packet.WriteGuid("Guid", guid);
            packet.WriteGuid("Guid2", guid2);
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

        [Parser(Opcode.SMSG_UNK_1A1E)]
        public static void HandleSUnk1A1E(Packet packet)
        {
            packet.ReadInt32("unk24");
            packet.ReadInt16("unk28");
            packet.ReadInt16("unk30");
            packet.ReadByte("unk32");

            var guid = packet.StartBitStream(2, 1, 6, 4, 5, 3, 7, 0);
            packet.ParseBitStream(guid, 1, 3, 6, 7, 2, 4, 5, 0);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_1A1F)]
        public static void HandleSUnk1A1F(Packet packet)
        {
            var guid = new byte[8];
            guid[6] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            var unk32 = packet.ReadBit("unk32");
            guid[3] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[4] = packet.ReadBit();


            packet.ParseBitStream(guid, 0, 3, 6);
            packet.ReadInt32("unk64");
            packet.ParseBitStream(guid, 5, 1, 4, 2, 7);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_1A2B)]
        public static void HandleSUnk1A2B(Packet packet)
        {
            var guid = packet.StartBitStream(2, 3, 0, 5, 7, 6, 1, 4);
            packet.ParseBitStream(guid, 6, 5, 4);
            packet.ReadInt32("unk32");
            packet.ParseBitStream(guid, 7, 1);
            packet.ReadInt32("unk24");
            packet.ParseBitStream(guid, 3);
            packet.ReadInt32("unk28");
            packet.ParseBitStream(guid, 2, 0);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_1C0E)]
        public static void HandleSUnk1C0E(Packet packet)
        {
            packet.ReadBits("unk24", 3);
            var guid = packet.StartBitStream(7, 4, 2, 1, 3, 5, 6, 0);
            packet.ParseBitStream(guid, 7, 5, 1, 4, 6, 2, 0, 3);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_1C33)]
        public static void HandleSUnk1C33(Packet packet)
        {
            packet.ReadInt32("CriteriaID");
        }

        [Parser(Opcode.SMSG_UNK_1E1B)]
        public static void HandleSUnk1E1B(Packet packet)
        {
            var guid = new byte[8];
            var guid2 = new byte[8];

            guid[5] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            var unk5264 = packet.ReadBit("unk5264");
            guid[1] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            var unk5272 = packet.ReadBit("unk5272");
            guid2[1] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            var unk96 = 0u;
            var unk80 = 0u;
            var unk5168 = new bool[1048576];
            var unk5180 = new bool[1048576];
            var unk5260 = false;
            var unk352 = new bool[1048576];
            var unk368 = new bool[1048576];
            var unk1398 = new uint[1048576];
            var unk5500 = new bool[1048576];
            var unk372 = new uint[1048576];
            var unk885 = new uint[1048576];
            var unk360 = new bool[1048576];
            var unk5508 = new bool[1048576];
            var unk5520 = new bool[1048576];
            var unk5176 = new uint[1048576];
            var unk641 = 0u;
            var unk108 = false;
            var unk128 = 0u;
            var unk116 = false;
            var unk5256 = false;
            var unk1154 = 0u;
            var unk124 = false;
            if (unk5264)
            {
                unk96 = packet.ReadBits("unk96", 2); // 96
                unk80 = packet.ReadBits("unk80", 20); // 80
                for (var i = 0; i < unk80; i++) // 80
                {
                    unk5168[i] = packet.ReadBit("unk5168", i); // 5168
                    if (unk5168[i]) // 5168
                    {
                        unk352[i] = packet.ReadBit("unk84*4+16", i); // 352
                        unk1398[i] = packet.ReadBits("unk84*4+1062", 13, i); // 1398
                        unk5500[i] = packet.ReadBit("unk84*4+5164", i); // 5500
                        unk368[i] = packet.ReadBit("unk84*4+32", i); // 368
                        unk372[i] = packet.ReadBits("unk84*4+36", 10, i); // 372
                        unk885[i] = packet.ReadBits("unk84*4+549", 10, i); // 885
                        unk360[i] = packet.ReadBit("unk84*4+24", i); // 360
                    }
                    unk5508[i] = packet.ReadBit("unk84*4+5172", i); // 5508
                    unk5520[i] = packet.ReadBit("unk84*4+5184", i); // 5520
                    unk5180[i] = packet.ReadBit("unk84*4+5180", i); // 5180
                    if (unk5180[i]) // 5180
                        unk5176[i] = packet.ReadBits("unk84*4+5176", 4, i); // 5176
                }
                unk5260 = packet.ReadBit("unk5260"); // 5260
                if (unk5260) // 5260
                {
                    unk641 = packet.ReadBits("unk641", 10); // 641
                    unk108 = packet.ReadBit("unk108"); // 108
                    unk128 = packet.ReadBits("unk128", 10); // 128
                    unk116 = packet.ReadBit("unk116"); // 116
                    unk5256 = packet.ReadBit("unk5256"); // 5256
                    unk1154 = packet.ReadBits("unk1154", 13); // 1154
                    unk124 = packet.ReadBit("unk124"); // 124
                }
            }

            guid[7] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid[4] = packet.ReadBit();

            if (unk5264) // 5264
            {
                for (var i = 0; i < unk80; i++) // 80
                {
                    packet.ReadInt32("unk84", i); // 84
                    if (unk5168[i]) // 5168
                    {
                        if (unk352[i]) // 352
                            packet.ReadInt32("unk348", i); // 348
                        if (unk368[i]) // 368
                            packet.ReadInt32("unk344", i); // 344
                        packet.ReadWoWString("str", unk372[i], i); // 372
                        if (unk5500[i]) // 5500
                            packet.ReadInt32("unk5496", i); // 5496
                        packet.ReadWoWString("str2", unk1398[i], i); // 1398
                        packet.ReadWoWString("str3", unk885[i], i); // 885
                        if (unk360[i]) // 360
                            packet.ReadInt32("unk356", i); // 356
                    }
                    packet.ReadInt32("unk344", i); // 344
                    packet.ReadInt32("unk340", i); // 340
                }
                packet.ReadInt32("unk56"); // 56
                packet.ReadInt64("unk72"); // 72
                if (unk5260) // 5260
                {
                    if (unk5256) // 5256
                        packet.ReadInt32("unk5252"); // 5252
                    packet.ReadWoWString("str1", unk1154);
                    packet.ReadWoWString("str2", unk128);
                    packet.ReadWoWString("str3", unk641);
                    if (unk116)
                        packet.ReadInt32("unk112"); // 112
                    if (unk124)
                        packet.ReadInt32("unk120"); // 120
                    if (unk108)
                        packet.ReadInt32("unk104"); // 104
                }
                packet.ReadInt64("unk64"); // 64
                packet.ReadByte("unk97"); // 97
                packet.ReadInt32("unk100"); // 100
            }
            packet.ReadInt32("unk28"); // 28
            packet.ParseBitStream(guid2, 4);
            packet.ReadInt64("unk48"); // 48
            packet.ParseBitStream(guid2, 1, 5);
            packet.ParseBitStream(guid, 2, 4, 1, 0);
            packet.ReadInt32("unk44");
            packet.ParseBitStream(guid, 7);
            packet.ParseBitStream(guid2, 0, 7);
            packet.ReadInt32("unk40");
            packet.ReadInt32("unk24");
            packet.ParseBitStream(guid2, 6);
            packet.ParseBitStream(guid, 5, 6, 3);
            packet.ParseBitStream(guid2, 3, 2);
            packet.WriteGuid("Guid", guid);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNK_1E2E)]
        public static void HandleSUnk1E2E(Packet packet)
        {
            var guid = packet.StartBitStream(2, 1, 0, 4, 7, 3, 6, 5);
            packet.ParseBitStream(guid, 4, 1);
            packet.ReadInt32("unk24");
            packet.ParseBitStream(guid, 3, 6, 7, 5, 0);
            packet.ReadInt32("unk28");
            packet.ParseBitStream(guid, 2);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_1E3E)]
        public static void HandleSUnk1E3E(Packet packet)
        {
            packet.ReadInt32("unkInt32");
        }

        [Parser(Opcode.CMSG_NULL_0023)]
        [Parser(Opcode.CMSG_NULL_0060)]
        [Parser(Opcode.CMSG_NULL_0082)]
        [Parser(Opcode.CMSG_NULL_0141)]
        [Parser(Opcode.CMSG_NULL_01C0)]
        [Parser(Opcode.CMSG_NULL_0276)]
        [Parser(Opcode.CMSG_NULL_02D6)]
        [Parser(Opcode.CMSG_NULL_02DA)]
        [Parser(Opcode.CMSG_NULL_032D)]
        [Parser(Opcode.CMSG_NULL_033D)]
        [Parser(Opcode.CMSG_NULL_0365)]
        [Parser(Opcode.CMSG_NULL_0374)]
        [Parser(Opcode.CMSG_NULL_03C4)]
        [Parser(Opcode.CMSG_NULL_0558)]
        [Parser(Opcode.CMSG_NULL_05E1)]
        [Parser(Opcode.CMSG_NULL_0644)]
        [Parser(Opcode.CMSG_NULL_06D4)]
        [Parser(Opcode.CMSG_NULL_06E4)]
        [Parser(Opcode.CMSG_NULL_06F5)]
        [Parser(Opcode.CMSG_NULL_077B)]
        [Parser(Opcode.CMSG_NULL_0813)]
        [Parser(Opcode.CMSG_NULL_0826)]
        [Parser(Opcode.CMSG_NULL_0A22)]
        [Parser(Opcode.CMSG_NULL_0A23)]
        [Parser(Opcode.CMSG_NULL_0A82)]
        [Parser(Opcode.CMSG_NULL_0A87)]
        [Parser(Opcode.CMSG_NULL_0C62)]
        [Parser(Opcode.CMSG_NULL_1124)]
        [Parser(Opcode.CMSG_NULL_1203)]
        [Parser(Opcode.CMSG_NULL_1207)]
        [Parser(Opcode.CMSG_NULL_1272)]
        [Parser(Opcode.CMSG_NULL_135B)]
        [Parser(Opcode.CMSG_NULL_1452)]
        [Parser(Opcode.CMSG_NULL_147B)]
        [Parser(Opcode.CMSG_NULL_14DB)]
        [Parser(Opcode.CMSG_NULL_14E0)]
        [Parser(Opcode.CMSG_NULL_15A8)]
        [Parser(Opcode.CMSG_NULL_15E2)]
        [Parser(Opcode.CMSG_NULL_18A2)]
        [Parser(Opcode.CMSG_NULL_1A23)]
        [Parser(Opcode.CMSG_NULL_1A87)]
        [Parser(Opcode.CMSG_NULL_1C45)]
        [Parser(Opcode.CMSG_NULL_1C5A)]
        [Parser(Opcode.CMSG_NULL_1CE3)]
        [Parser(Opcode.CMSG_NULL_1D61)]
        [Parser(Opcode.CMSG_NULL_1DC3)]
        [Parser(Opcode.CMSG_NULL_1F34)]
        [Parser(Opcode.CMSG_NULL_1F89)]
        [Parser(Opcode.CMSG_NULL_1F8E)]
        [Parser(Opcode.CMSG_NULL_1F9E)]
        [Parser(Opcode.CMSG_NULL_1F9F)]
        [Parser(Opcode.CMSG_NULL_1FBE)]

        [Parser(Opcode.SMSG_NULL_04BB)]
        [Parser(Opcode.SMSG_NULL_0C59)]
        [Parser(Opcode.SMSG_NULL_0C9A)]
        [Parser(Opcode.SMSG_NULL_0E2B)]
        [Parser(Opcode.SMSG_NULL_0E8B)]
        [Parser(Opcode.SMSG_NULL_0FE1)]
        [Parser(Opcode.SMSG_NULL_1A2A)]
        public static void HandleUnkNull(Packet packet)
        {
        }
    }
}
