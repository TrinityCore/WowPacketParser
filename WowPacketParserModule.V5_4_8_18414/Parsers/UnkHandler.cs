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

        [Parser(Opcode.CMSG_UNK_02A3)]
        public static void HandleUnk02A3(Packet packet)
        {
            packet.ReadInt32("unk96"); // 96
            var guid = packet.StartBitStream(0, 2, 1, 6, 5, 4, 7, 3);
            packet.ParseBitStream(guid, 6, 0, 3, 5, 4, 2, 1, 7);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_UNK_06C9)]
        public static void HandleUnk06C9(Packet packet)
        {
            packet.ReadInt32("unk16"); // 16
        }

        [Parser(Opcode.CMSG_UNK_0882)]
        public static void HandleUnk0882(Packet packet)
        {
            packet.ReadInt32("RealmID");
            var guid = packet.StartBitStream(0, 2, 5, 1, 6, 4, 3, 7);
            packet.ParseBitStream(guid, 7, 3, 1, 5, 4, 0, 2, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_UNK_0A16)]
        public static void HandleUnk0A16(Packet packet)
        {
            var guid = packet.StartBitStream(7, 6, 3, 0, 4, 1, 5, 2);
            packet.ParseBitStream(guid, 1, 6, 0, 5, 3, 2, 4, 7);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_UNK_10D3)]
        public static void HandleUnk10D3(Packet packet)
        {
            packet.ReadInt32("unk20"); // 20
            packet.ReadInt32("unk16"); // 16
        }

        [Parser(Opcode.CMSG_UNK_12B3)]
        public static void HandleUnk12B3(Packet packet)
        {
            // Str: DUNGEONS\TEXTURES\EFFECTS\BLUE_REFLECT..tga - (null)
            packet.ReadWoWString("Str", packet.ReadBits(9));
        }

        [Parser(Opcode.CMSG_UNK_1341)]
        public static void HandleUnk1341(Packet packet)
        {
            packet.ReadBoolean("Unk");
        }

        [Parser(Opcode.CMSG_UNK_15DB)]
        public static void HandleUnk15DB(Packet packet)
        {
            packet.ReadInt32("unk16"); // 16
            packet.ReadInt32("unk20"); // 20
            packet.ReadInt32("unk24"); // 24
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

        [Parser(Opcode.SMSG_UNK_003A)]
        public static void HandleSUnk003A(Packet packet)
        {
            var guid = packet.StartBitStream(5, 7, 0, 3, 2, 1, 4, 6);
            packet.ParseBitStream(guid, 7, 2, 0, 4, 5, 6, 1, 3);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_0061)]
        public static void HandleSUnk0061(Packet packet)
        {
            var guid = new byte[8];
            guid[1] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            var count = packet.ReadBits("count", 21);
            var unk92 = new bool[count];
            var unk112 = new bool[count];
            var unk120 = new bool[count];
            var unk136 = new bool[count];
            var unk168 = new bool[count];
            for (var i = 0; i < count; i++)
            {
                unk112[i] = packet.ReadBit("unk112", i); // 112
                unk136[i] = packet.ReadBit("unk136", i); // 136
                unk120[i] = packet.ReadBit("unk120", i); // 120
                if (unk136[i])
                    packet.ReadBits("unk132", 2, i); // 132
                unk92[i] = packet.ReadBit("unk92", i); // 92
                unk168[i] = packet.ReadBit("unk168", i); // 168
                if (unk168[i])
                    packet.ReadBits("unk164", 2, i);
            }
            guid[0] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[6] = packet.ReadBit();

            for (var i = 0; i < count; i++)
            {
                if (unk168[i])
                {
                    packet.ReadSingle("unk160", i); // 160
                    packet.ReadSingle("unk144", i); // 144
                    packet.ReadSingle("unk148", i); // 148
                    packet.ReadInt32("unk156", i); // 156
                    packet.ReadInt32("unk140", i); // 140
                    packet.ReadSingle("unk152", i); // 152
                }
                if (unk112[i])
                {
                    packet.ReadSingle("unk108", i); // 108
                    packet.ReadSingle("unk104", i); // 104
                    packet.ReadSingle("unk100", i); // 100
                    packet.ReadSingle("unk96", i); // 96
                }
                if (unk136[i])
                {
                    packet.ReadSingle("unk128", i); // 128
                    packet.ReadSingle("unk124", i); // 124
                }
                packet.ReadInt32("unk84", i); // 84
                if (unk120[i])
                    packet.ReadInt32("unk116", i); // 116
                packet.ReadInt16("unk80", i); // 80
                if (unk92[i])
                    packet.ReadSingle("unk88", i); // 88
            }

            packet.ParseBitStream(guid, 1, 5, 4, 6, 7, 0, 2, 3);
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

        [Parser(Opcode.SMSG_UNK_042E)]
        public static void HandleSUnk042E(Packet packet)
        {
            /*
             ServerToClient: SMSG_UNK_042E (0x042E) Length: 15
             unk24: 0 (0x0000)
             Spell ID: 41536 (0xA240)
             Guid: Full: 0xF13044CD00000FA8 Type: Unit Entry: 17613 Low: 4008
             */
            var guid = packet.StartBitStream(3, 0, 4, 7, 6, 1, 5, 2);
            packet.ParseBitStream(guid, 1, 2, 0, 3, 4);
            packet.ReadInt32("unk24"); // 24
            packet.ParseBitStream(guid, 5, 6, 7);
            packet.ReadEntry<Int32>(StoreNameType.Spell, "Spell ID"); // 28
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_043E)]
        public static void HandleSUnk043E(Packet packet)
        {
            /*
             ServerToClient: SMSG_UNK_043E (0x043E) Length: 9
             unk24: 1575 (0x0627)
             Guid: Full: 0xF130D54400000251 Type: Unit Entry: 54596 Low: 593
             */
            var guid = packet.StartBitStream(3, 1, 7, 6, 0, 4, 5, 2);
            packet.ParseBitStream(guid, 3, 6, 1, 4);
            packet.ReadInt16("unk24"); // 24
            packet.ParseBitStream(guid, 2, 7, 5, 0);
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

        [Parser(Opcode.SMSG_UNK_049A)]
        public static void HandleSUnk049A(Packet packet)
        {
            // after SMSG_ACTIVATETAXIREPLY
            var count = packet.ReadBits("count", 22);
            for (var i = 0; i < count; i++)
            {
                packet.ReadBits("unk24", 8, i); // 24
                packet.ReadBits("unk28", 8, i); // 28
            }
            for (var i = 0; i < count; i++)
            {
                packet.ReadByte("unk21", i);
                packet.ReadByte("unk20", i);
            }
        }

        [Parser(Opcode.SMSG_UNK_04AA)]
        public static void HandleSUnk04AA(Packet packet)
        {
            packet.ReadSingle("unk1");
            packet.ReadInt32("unk2");
        }

        [Parser(Opcode.SMSG_UNK_04D9)]
        public static void HandleSUnk04D9(Packet packet)
        {
            var guid24 = new byte[8];
            var guid64 = new byte[8];

            guid24[4] = packet.ReadBit();
            guid64[3] = packet.ReadBit();
            guid64[6] = packet.ReadBit();
            guid24[0] = packet.ReadBit();
            guid64[7] = packet.ReadBit();
            guid24[1] = packet.ReadBit();
            guid24[5] = packet.ReadBit();
            guid64[2] = packet.ReadBit();
            guid64[1] = packet.ReadBit();
            guid24[7] = packet.ReadBit();
            guid64[4] = packet.ReadBit();
            guid64[0] = packet.ReadBit();
            guid24[2] = packet.ReadBit();
            guid64[5] = packet.ReadBit();
            guid24[3] = packet.ReadBit();
            var count44 = packet.ReadBits("count44", 22);
            guid24[6] = packet.ReadBit();

            packet.ReadByte("unk60"); // 60
            packet.ReadInt32("unk144"); // 144
            packet.ReadByte("unk32"); // 32

            packet.ParseBitStream(guid64, 6, 4);
            packet.ParseBitStream(guid24, 7);
            packet.ParseBitStream(guid64, 1);
            packet.ParseBitStream(guid24, 3);

            packet.ReadByte("unk16"); // 16

            packet.ParseBitStream(guid24, 2, 0);

            packet.ReadByte("unk61"); // 61
            packet.ReadByte("unk63"); // 63

            packet.ParseBitStream(guid64, 7);

            for (var i = 0; i < count44; i++)
                packet.ReadInt32("unk192", i); // 192

            packet.ParseBitStream(guid24, 4);

            packet.ReadByte("unk62"); // 62
            packet.ReadByte("unk72"); // 72
            packet.ReadByte("unk40"); // 40

            packet.ParseBitStream(guid24, 5);
            packet.ParseBitStream(guid64, 3, 2);
            packet.ParseBitStream(guid24, 1);
            packet.ParseBitStream(guid64, 0, 5);
            packet.ParseBitStream(guid24, 6);

            packet.WriteGuid("Guid24", guid24);
            packet.WriteGuid("Guid64", guid64);
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

        [Parser(Opcode.SMSG_UNK_080E)]
        public static void HandleSUnk080E(Packet packet)
        {
            var hasData = !packet.ReadBit("!hasData"); // 16
            packet.ReadByte("unk20"); // 20
            if (hasData)
                packet.ReadInt32("unk16");
        }

        [Parser(Opcode.SMSG_UNK_08FB)]
        public static void HandleSUnk08FB(Packet packet)
        {
            var guid = new byte[8];
            var guid2 = new byte[8];

            guid2[5] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            packet.ReadBit("unk40"); // 40
            guid2[7] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[0] = packet.ReadBit();

            packet.ParseBitStream(guid, 4, 3);
            packet.ParseBitStream(guid2, 2);
            packet.ParseBitStream(guid, 6, 5);
            packet.ParseBitStream(guid2, 3);
            packet.ParseBitStream(guid, 7, 2, 0);
            packet.ParseBitStream(guid2, 7, 5, 0, 1, 4);
            packet.ReadEntry<Int32>(StoreNameType.Spell, "Spell ID"); // 16
            packet.ParseBitStream(guid, 1);
            packet.ParseBitStream(guid2, 6);

            packet.WriteGuid("Guid", guid);
            packet.WriteGuid("Guid2", guid2);
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

        [Parser(Opcode.SMSG_UNK_0C13)]
        public static void HandleSUnk0C13(Packet packet)
        {
            packet.ReadEntry<UInt32>(StoreNameType.Spell, "Spell ID"); // 36
            packet.ReadInt32("unk32"); // 32
            packet.ReadInt32("Cooldown1"); // 24
            packet.ReadInt32("Cooldown2"); // 28

            var guid = new byte[8];

            guid[5] = packet.ReadBit();
            packet.ReadBits("unk44", 8); // 44
            guid[7] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            packet.ReadBits("unk40", 8); // 40
            guid[0] = packet.ReadBit();

            packet.ParseBitStream(guid, 4, 2, 5, 6, 0, 7, 1, 3);

            packet.WriteGuid("Guid", guid);
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

        [Parser(Opcode.SMSG_UNK_0CAF)]
        public static void HandleSUnk0CAF(Packet packet)
        {
            packet.ReadBit("unk16"); // 16
            packet.ReadBit("unk17"); // 17
            packet.ReadInt32("unk24"); // 24
            packet.ReadByte("unk28"); // 28
            packet.ReadInt32("unk20"); // 20
            packet.ReadInt32("unk32"); // 32
        }

        [Parser(Opcode.SMSG_UNK_0D51)]
        public static void HandleSUnk0D51(Packet packet)
        {
            var count = packet.ReadBits("count", 22);
            for (var i = 0; i < count; i++)
                packet.ReadEntry<Int32>(StoreNameType.Spell, "Spell ID", i);
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

        [Parser(Opcode.SMSG_UNK_11E1)]
        public static void HandleSUnk11E1(Packet packet)
        {
            packet.ReadBits("Unk16", 2);
        }

        [Parser(Opcode.SMSG_UNK_1203)]
        public static void HandleSUnk1203(Packet packet)
        {
            /*
             ѕ≥сл€
             ServerToClient: SMSG_CHANNEL_START (0x10F9) Length: 16
             Spell ID: 115175 (0x1C1E7)
             йде
             ServerToClient: SMSG_UNK_1203 (0x1203) Length: 12
             unk20: False
             Spell ID: 117899 (0x1CC8B)
             */

            var guid = new byte[8];
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            packet.ReadBit("unk20"); // 20
            guid[3] = packet.ReadBit();

            packet.ParseBitStream(guid, 4, 1);

            packet.ReadEntry<Int32>(StoreNameType.Spell, "Spell ID"); // 64

            packet.ParseBitStream(guid, 2, 3, 7, 5, 0, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_1206)]
        public static void HandleSUnk1206(Packet packet)
        {
            var guid = new byte[8];
            var guid2 = new byte[8];

            guid2[1] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[2] = packet.ReadBit();

            packet.ParseBitStream(guid, 5, 4);
            packet.ParseBitStream(guid2, 3, 0, 5);
            packet.ParseBitStream(guid, 0);
            packet.ParseBitStream(guid2, 2);
            packet.ParseBitStream(guid, 1, 7, 6, 2);
            packet.ParseBitStream(guid2, 1, 6);

            packet.ReadByte("unk32"); // 32

            packet.ParseBitStream(guid2, 4, 7);
            packet.ParseBitStream(guid, 3);

            packet.WriteGuid("Guid", guid);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNK_1223)]
        public static void HandleSUnk1223(Packet packet)
        {
            packet.ReadInt32("unk");
        }

        [Parser(Opcode.SMSG_UNK_129E)]
        public static void HandleSUnk129E(Packet packet)
        {
            packet.ReadInt32("unk44"); // 44
            packet.ReadInt32("unk16"); // 16
            packet.ReadInt32("unk24"); // 24
            var unk32 = packet.ReadBit("unk32"); // 32
            var unk20 = packet.ReadBit("unk20"); // 20
            var unk40 = packet.ReadBit("unk40"); // 40
            if (unk32)
                packet.ReadInt32("unk28"); // 28
            if (unk40)
                packet.ReadInt32("unk36"); // 36
        }

        [Parser(Opcode.SMSG_UNK_12A3)]
        public static void HandleSUnk12A3(Packet packet)
        {
            packet.ReadInt32("unk40"); // 40
            packet.ReadInt32("unk48"); // 48
            packet.ReadInt32("unk44"); // 44

            var guid16 = new byte[8];
            var guid24 = new byte[8];
            var guid32 = new byte[8];

            guid24[6] = packet.ReadBit();
            guid24[7] = packet.ReadBit();
            guid16[6] = packet.ReadBit();
            guid16[4] = packet.ReadBit();
            guid24[5] = packet.ReadBit();
            guid32[7] = packet.ReadBit();
            guid32[2] = packet.ReadBit();
            guid32[3] = packet.ReadBit();
            guid24[4] = packet.ReadBit();
            guid24[3] = packet.ReadBit();
            guid32[6] = packet.ReadBit();
            guid16[1] = packet.ReadBit();
            guid24[2] = packet.ReadBit();
            guid16[5] = packet.ReadBit();
            guid32[4] = packet.ReadBit();
            guid16[0] = packet.ReadBit();
            guid32[1] = packet.ReadBit();
            guid24[0] = packet.ReadBit();
            guid16[3] = packet.ReadBit();
            guid16[7] = packet.ReadBit();
            guid32[5] = packet.ReadBit();
            guid32[0] = packet.ReadBit();
            guid16[2] = packet.ReadBit();
            guid24[1] = packet.ReadBit();

            packet.ReadXORByte(guid24, 0);
            packet.ReadXORByte(guid16, 2);
            packet.ReadXORByte(guid32, 7);
            packet.ReadXORByte(guid24, 1);
            packet.ReadXORByte(guid16, 4);
            packet.ReadXORByte(guid32, 5);
            packet.ReadXORByte(guid24, 4);
            packet.ReadXORByte(guid32, 2);
            packet.ReadXORByte(guid16, 6);
            packet.ReadXORByte(guid16, 0);
            packet.ReadXORByte(guid32, 0);
            packet.ReadXORByte(guid32, 4);
            packet.ReadXORByte(guid24, 3);
            packet.ReadXORByte(guid16, 5);
            packet.ReadXORByte(guid32, 1);
            packet.ReadXORByte(guid16, 3);
            packet.ReadXORByte(guid16, 7);
            packet.ReadXORByte(guid24, 7);
            packet.ReadXORByte(guid32, 3);
            packet.ReadXORByte(guid24, 6);
            packet.ReadXORByte(guid24, 2);
            packet.ReadXORByte(guid24, 5);
            packet.ReadXORByte(guid32, 6);
            packet.ReadXORByte(guid16, 1);

            packet.WriteGuid("Guid16", guid16);
            packet.WriteGuid("Guid24", guid24);
            packet.WriteGuid("Guid32", guid32);
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

        [Parser(Opcode.SMSG_UNK_149B)]
        public static void HandleSUnk149B(Packet packet)
        {
            packet.ReadInt32("unk28"); // 28
            packet.ReadEntry<Int32>(StoreNameType.Spell, "Spell ID"); // 20
            packet.ReadByte("unk32"); // 32
            var unk24 = -packet.ReadBit("-unk24"); // 24
            var unk16 = -packet.ReadBit("-unk16"); // 16
            if (unk16 != -1)
                packet.ReadInt32("unk16"); // 16
            if (unk24 != -1)
                packet.ReadInt32("unk24"); // 24
        }

        [Parser(Opcode.SMSG_UNK_149F)]
        public static void HandleSUnk149F(Packet packet)
        {
            var guid = packet.StartBitStream(5, 7, 2, 1, 4, 0, 3, 6);
            packet.ParseBitStream(guid, 5, 7, 4, 6, 2, 1, 3, 0);
            packet.ReadInt32("unk24"); // 24
            packet.WriteGuid("Guid", guid);
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

        [Parser(Opcode.SMSG_UNK_163F)]
        public static void HandleSUnk163F(Packet packet)
        {
            var guid = packet.StartBitStream(5, 0, 6, 3, 7, 2, 4, 1);
            packet.ReadInt32("unk24"); // 24
            packet.ParseBitStream(guid, 2, 5, 4, 0, 1, 7, 3, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_182E)]
        public static void HandleSUnk182E(Packet packet)
        {
            packet.ReadInt32("unk36"); // 36
            packet.ReadInt32("unk32"); // 32
            packet.ReadInt32("unk40"); // 40
            var count44 = packet.ReadBits("count44", 19); // 44
            var count240 = new uint[count44];
            var guids44 = new byte[count44][][];
            for (var i = 0; i < count44; i++)
            {
                count240[i] = packet.ReadBits("unk240", 20, i); // 240
                guids44[i] = new byte[count240[i]][];
                for (var j = 0; j < count240[i]; j++)
                    guids44[i][j] = packet.StartBitStream(5, 2, 3, 0, 7, 1, 4, 6);
            }
            var count16 = packet.ReadBits("count16", 19); // 16
            var count212 = new uint[count16];
            var guids16 = new byte[count16][][];
            for (var i = 0; i < count16; i++)
            {
                count212[i] = packet.ReadBits("count212", 20, i); // 212
                guids16[i] = new byte[count212[i]][];
                for (var j = 0; j < count212[i]; j++)
                    guids16[i][j] = packet.StartBitStream(5, 3, 0, 1, 2, 4, 6, 7);
            }
            for (var i = 0; i < count16; i++)
            {
                for (var j = 0; j < count212[i]; j++)
                {
                    packet.ParseBitStream(guids16[i][j], 4, 3, 6, 7, 2, 1);
                    packet.ReadInt32("RealmID", i, j); // 276
                    packet.ReadInt32("unk292", i, j); // 292
                    packet.ReadInt32("RealmID", i, j); // 260
                    packet.ParseBitStream(guids16[i][j], 0, 5);
                    packet.WriteGuid("Guids16", guids16[i][j], i, j);
                }
                packet.ReadPackedTime("Time", i);
                packet.ReadInt32("unk20", i); // 20
                packet.ReadInt32("unk36", i); // 36
                packet.ReadInt32("unk52", i); // 52
                packet.ReadInt32("unk196", i); // 196
            }
            for (var i = 0; i < count44; i++)
            {
                for (var j = 0; j < count240[i]; j++)
                {
                    packet.ParseBitStream(guids44[i][j], 3);
                    packet.ReadInt32("unk320", i, j); // 320
                    packet.ReadInt32("unk304", i, j); // 304
                    packet.ParseBitStream(guids44[i][j], 1, 5, 0);
                    packet.ReadInt32("unk288", i, j); // 288
                    packet.ParseBitStream(guids44[i][j], 4, 7, 6, 2);
                    packet.WriteGuid("Guids44", guids44[i][j], i, j);
                }
                packet.ReadInt32("unk64", i); // 64
                packet.ReadInt32("unk224", i); // 224
                packet.ReadInt32("unk48", i); // 48
                packet.ReadInt32("unk80", i); // 80
                packet.ReadPackedTime("Time", i);
            }
        }

        [Parser(Opcode.SMSG_UNK_1943)]
        public static void HandleSUnk1943(Packet packet)
        {
            /*
             «€вл€Їтьс€ при п≥двищенн≥ рангу бонус спела (53040 - 74496)
             */
            var count16 = packet.ReadBits("count16", 22);
            var count32 = packet.ReadBits("count32", 22);
            for (var i = 0; i < count32; i++)
                packet.ReadEntry<UInt32>(StoreNameType.Spell, "New Spell ID", i); // 36
            for (var i = 0; i < count16; i++)
                packet.ReadEntry<UInt32>(StoreNameType.Spell, "Old Spell ID", i); // 20
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

        [Parser(Opcode.SMSG_UNK_1ABB)]
        public static void HandleSUnk1ABB(Packet packet)
        {
            /* пакет з€вл€Їтьс€ при рухов≥ паровим танком новолун≥€.
             якщо unk28=110, п≥сл€ нього йде пакет
             ServerToClient: SMSG_DESTROY_OBJECT (0x14C2) Length: 9
             Despawn Animation: False
             Object Guid: Full: 0xF141743FAE000E1D Type: Pet Low: 2919239197 Name: 0
             якщо unk28=2201,  п≥сл€ нього йде пакет
             ServerToClient: SMSG_CANCEL_AUTO_REPEAT (0x1E0F) Length: 8
             Guid: Full: 0xF150D53C000573CC Type: Vehicle Entry: 54588 Low: 357324
             */
            packet.ReadInt32("unk28"); // 28  110 / 2201
            packet.ReadSingle("X"); // 16
            packet.ReadSingle("Z"); // 24
            packet.ReadSingle("Y"); // 20
        }

        [Parser(Opcode.SMSG_UNK_1C0E)]
        public static void HandleSUnk1C0E(Packet packet)
        {
            packet.ReadBits("unk24", 3);
            var guid = packet.StartBitStream(7, 4, 2, 1, 3, 5, 6, 0);
            packet.ParseBitStream(guid, 7, 5, 1, 4, 6, 2, 0, 3);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_1DBE)]
        public static void HandleSUnk1DBE(Packet packet)
        {
            var guid48 = new byte[8];
            guid48[2] = packet.ReadBit();
            guid48[3] = packet.ReadBit();
            packet.ReadBits("unk40", 2); // 40
            guid48[7] = packet.ReadBit();
            guid48[5] = packet.ReadBit();
            guid48[0] = packet.ReadBit();
            guid48[1] = packet.ReadBit();
            guid48[6] = packet.ReadBit();
            guid48[4] = packet.ReadBit();

            packet.ParseBitStream(guid48, 6);
            packet.ReadSingle("unk24"); // 24
            packet.ParseBitStream(guid48, 4);
            packet.ReadSingle("unk28"); // 28
            packet.ReadInt32("unk44"); // 44
            packet.ReadInt32("unk16"); // 16
            packet.ParseBitStream(guid48, 5);
            packet.ReadSingle("unk36"); // 36
            packet.ParseBitStream(guid48, 0, 7, 1, 3, 2);
            packet.ReadInt32("unk32"); // 32
            packet.ReadSingle("unk20"); // 20

            packet.WriteGuid("Guid", guid48);
        }

        [Parser(Opcode.SMSG_CRITERIA_DELETED)]
        public static void HandleCriteriaDeleted(Packet packet)
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

        [Parser(Opcode.SMSG_UNK_1E9E)]
        public static void HandleSUnk1E9E(Packet packet)
        {
            var guid = packet.StartBitStream(0, 4, 2, 6, 5, 1, 3, 7);
            packet.ParseBitStream(guid, 3);
            packet.ReadInt32("unk24"); // 24
            packet.ReadInt32("unk28"); // 28
            packet.ParseBitStream(guid, 4, 6, 7, 0, 2, 5, 1);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_0332)]
        public static void HandleCoded(Packet packet)
        {
            packet.ReadToEnd();
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
        [Parser(Opcode.CMSG_NULL_0360)]
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
