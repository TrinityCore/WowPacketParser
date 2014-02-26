using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class BattlePetHandler
    {
        [Parser(Opcode.SMSG_BATTLE_PET_DELETED)]
        public static void HandleBattlePetDeleted(Packet packet)
        {
            var guid = new byte[8];
            packet.StartBitStream(guid, 2, 3, 4, 7, 1, 0, 5, 6);
            packet.ParseBitStream(guid, 7, 3, 0, 4, 2, 5, 1, 6);
            packet.WriteGuid("GUID", guid);
        }
        

        [Parser(Opcode.SMSG_PET_BATTLE_FIRST_ROUND)]
        public static void HandleBattlePetFirstRound(Packet packet)
        {
            var bit4 = false;
            var bit6 = false;
            var bit8 = false;
            var bit9 = false;
            var bitA = false;
            var bitB = false;
            var bitC = false;
            var bit10 = false;
            var bit14 = false;
            var bit18 = false;
            var bit1C = false;
            var bit20 = false;
            var bit24 = false;
            var bit28 = false;
            var bit2C = false;
            var bit30 = false;
            var bit34 = false;
            var bit38 = false;
            var bitEB = false;

            var bits10 = 0;
            var bits18 = 0;
            var bits28 = 0;
            var bits38 = 0;
            var bitsED = 0;

            bits18 = (int)packet.ReadBits(22);
            for (var i = 0; i < bits18; ++i)
            {
                bitEB = !packet.ReadBit();
                bitA = !packet.ReadBit();
                bit4 = !packet.ReadBit();
                bitB = !packet.ReadBit();
                bit6 = !packet.ReadBit();
                bitC = !packet.ReadBit();
                bit8 = !packet.ReadBit();
                bits10 = (int)packet.ReadBits(25);

                for (var j = 0; j < bits10; ++j)
                {
                    bit1C = !packet.ReadBit();
                    bitsED = (int)packet.ReadBits(3);

                    switch (bitsED)
                    {
                        case 0:
                            bit28 = !packet.ReadBit(); break;
                        case 1:
                            {
                                bitC = !packet.ReadBit();
                                bit14 = !packet.ReadBit();
                                bit10 = !packet.ReadBit();
                                bit8 = !packet.ReadBit();
                                break;
                            }
                        case 2:
                            {
                                bit34 = !packet.ReadBit();
                                bit2C = !packet.ReadBit();
                                bit30 = !packet.ReadBit();
                                break;
                            }
                        case 3:
                            {
                                bit18 = !packet.ReadBit();
                                bit1C = !packet.ReadBit();
                                break;
                            }
                        case 4:
                            bit24 = !packet.ReadBit(); break;
                        case 5:
                            bit38 = !packet.ReadBit(); break;
                        case 7:
                            bit20 = !packet.ReadBit(); break;
                    }
                }
            }

            bits28 = (int)packet.ReadBits(3);
            bit14 = !packet.ReadBit();
            bits38 = (int)packet.ReadBits(20);

            for (var i = 0; i < bits38; ++i)
            {
                bit9 = !packet.ReadBit();
            }

            packet.ResetBitReader();

            for (var i = 0; i < bits38; ++i)
            {
                if (bit9)
                {
                    packet.ReadByte("Byte9");
                }
                packet.ReadInt16("IntED");
                packet.ReadByte("ByteED");
                packet.ReadInt32("Int3C");
                packet.ReadInt16("IntED");
            }

            for (var i = 0; i < bits18; ++i)
            {
                if (bit8)
                    packet.ReadInt16("Int8");

                for (var j = 0; j < bits10; ++j)
                {
                    switch (bitsED)
                    {
                        case 0:
                            {
                                if (bit28)
                                    packet.ReadInt32("Int28");
                                break;
                            }
                        case 1:
                            { 
                                if (bit14)
                                    packet.ReadInt32("Int14");
                                if (bit10)
                                    packet.ReadInt32("Int10");
                                if (bitC)
                                    packet.ReadInt32("IntC");
                                if (bit8)
                                    packet.ReadInt32("Int8");
                                break;
                            }
                        case 2:
                            {
                                if (bit30)
                                    packet.ReadInt32("Int30");
                                if (bit2C)
                                    packet.ReadInt32("Int2C");
                                if (bit34)
                                    packet.ReadInt32("Int34");
                                break;
                            }
                        case 3:
                            {
                                if (bit18)
                                    packet.ReadInt32("Int18");
                                if (bit1C)
                                    packet.ReadInt32("Int1C");
                                break;
                            }
                        case 4:
                            {
                                if (bit24)
                                    packet.ReadInt32("Int24");
                                break;
                            }
                        case 5:
                            {
                                if (bit38)
                                    packet.ReadInt32("Int38");
                                break;
                            }
                        case 7:
                            {
                                if (bit20)
                                    packet.ReadInt32("Int20");
                                break;
                            }
                    }

                    if (bit14)
                        packet.ReadByte("Byte14");
                }

                if (bit6)
                    packet.ReadInt16("Int6");
                if (bitB)
                    packet.ReadByte("ByteB");
                if (bitC)
                    packet.ReadByte("ByteC");
                if (bitEB)
                    packet.ReadInt32("IntEB");
                if (bitA)
                    packet.ReadByte("ByteA");
                if (bit4)
                    packet.ReadInt16("Int4");
            }

            if (bit14)
                packet.ReadByte("Byte14");

            for (var i = 0; i < 2; ++i)
            {
                packet.ReadInt16("Int4C");
                packet.ReadByte("Byte4A");
                packet.ReadByte("Byte4A");
            }

            packet.ReadInt32("Int10");

            for (var i = 0; i < bits28; ++i)
            {
                packet.ReadByte("Byte2C");
            }
        }
        [Parser(Opcode.SMSG_BATTLE_PET_ERROR)]
        public static void HandleBattlePetError(Packet packet)
        {
            var bits14 = 0;

            var bit10 = !packet.ReadBit();
            var bit14 = !packet.ReadBit();

            if (bit14)
                bits14 = (int)packet.ReadBits(4);

            if (bit10)
                packet.ReadInt32("Int10");
        }

        [Parser(Opcode.SMSG_BATTLE_PET_TRAP_LEVEL)]
        public static void HandleBattlePetTrapLevel(Packet packet)
        {
            packet.ReadInt16("Int10");
        }

        [Parser(Opcode.SMSG_BATTLE_PET_UPDATES)]
        public static void HandleBattlePetUpdates(Packet packet)
        {
            var bits10 = 0;

            bits10 = (int)packet.ReadBits(22);

            for (var i = 0; i < bits10; ++i)
            {
                    packet.ReadInt32("IntED");
            }
        }

        [Parser(Opcode.SMSG_BATTLE_PET_JOURNAL)]
        public static void HandleBattlePetJournal(Packet packet)
        {
            byte[][] guid1;
            byte[][] guid2;
            byte[][] guid3;

            var bits36 = packet.ReadBits(25);
            var bits20 = packet.ReadBits(19);
            var bits44 = new uint[bits20];
            var bit137 = new bool[bits20];
            var bit20 = new bool[bits20];
            var bit26 = new bool[bits20];
            var bit128 = new bool[bits20];
            var bit136 = new bool[bits20];

            guid1 = new byte[bits20][];
            guid2 = new byte[bits20][];
            for (var i = 0; i < bits20; ++i)
            {
                guid1[i] = new byte[8];

                bits44[i] = packet.ReadBits(7);
                bit137[i] = !packet.ReadBit();
                guid1[i][3] = packet.ReadBit();
                bit20[i] = !packet.ReadBit();
                guid1[i][5] = packet.ReadBit();
                guid1[i][0] = packet.ReadBit();
                bit26[i] = !packet.ReadBit();
                bit128[i] = packet.ReadBit();
                guid1[i][2] = packet.ReadBit(); // v7 0-7

                if (bit128[i])
                {
                    guid2[i] = new byte[8];
                    packet.StartBitStream(guid2[i], 7, 1, 6, 2, 4, 3, 0, 5); // 112-119
                }

                bit136[i] = packet.ReadBit();
                guid1[i][7] = packet.ReadBit();
                guid1[i][4] = packet.ReadBit();
                guid1[i][6] = packet.ReadBit();
                guid1[i][1] = packet.ReadBit();
            }


            guid3 = new byte[bits36][];

            var bit8 = new bool[bits36];
            var bit13 = new bool[bits36];
            var bit12 = new bool[bits36];

            for (var i = 0; i < bits36; ++i)
            {
                bit8[i] = !packet.ReadBit();
                bit13[i] = packet.ReadBit();
                bit12[i] = !packet.ReadBit();

                packet.ReadBit(); // fake bit

                guid3[i] = new byte[8];
                packet.StartBitStream(guid3[i], 3, 5, 0, 4, 1, 6, 2, 7); //v11 0-7
            }

            packet.ReadBit("Unk bit");

            for (var i = 0; i < bits36; ++i)
            {
                packet.ReadXORByte(guid3[i], 2);
                packet.ReadXORByte(guid3[i], 7);
                packet.ReadXORByte(guid3[i], 3);
                packet.ReadXORByte(guid3[i], 5);
                packet.ReadXORByte(guid3[i], 1);
                packet.ReadXORByte(guid3[i], 0);
                packet.ReadXORByte(guid3[i], 6);
                packet.ReadXORByte(guid3[i], 4);

                if (bit8[i])
                    packet.ReadInt32("Int-8", i);
                if (bit12[i])
                    packet.ReadByte("Byte12", i);

                packet.WriteGuid("Guid3", guid3[i], i);
            }

            for (var i = 0; i < bits20; ++i)
            {
                if (bit26[i])
                    packet.ReadInt16("Int-26", i);

                packet.ReadInt32("Health", i);
                packet.ReadInt32("Speed", i);

                if (bit20[i])
                    packet.ReadInt16("Breed at this pet", i);

                if (bit128[i])
                {
                    packet.ReadXORByte(guid2[i], 2);
                    packet.ReadInt32("Int-124", i);
                    packet.ReadXORByte(guid2[i], 5);
                    packet.ReadXORByte(guid2[i], 6);
                    packet.ReadInt32("Int-120", i);
                    packet.ReadXORByte(guid2[i], 0);
                    packet.ReadXORByte(guid2[i], 4);
                    packet.ReadXORByte(guid2[i], 3);
                    packet.ReadXORByte(guid2[i], 1);
                    packet.ReadXORByte(guid2[i], 7);
                    packet.WriteGuid("Guid2", guid2[i], i);
                }
                if (bit137[i])
                    packet.ReadByte("Quality", i);

                packet.ReadXORByte(guid1[i], 5);
                packet.ReadInt16("Actual Experiance at Level", i);
                packet.ReadXORByte(guid1[i], 2);
                packet.ReadXORByte(guid1[i], 1);
                packet.ReadXORByte(guid1[i], 6);
                packet.ReadInt32("Max Health", i);
                packet.ReadInt32("Int-16", i);
                packet.ReadInt32("Int-8", i);
                packet.ReadInt32("Entry", i);
                packet.ReadInt32("Power", i);
                packet.ReadXORByte(guid1[i], 3);
                packet.ReadInt16("Level", i);
                packet.ReadXORByte(guid1[i], 0);
                packet.ReadXORByte(guid1[i], 4);
                packet.ReadWoWString("Custom Name", bits44[i], i);
                packet.ReadXORByte(guid1[i], 7);
                packet.WriteGuid("Guid1", guid1[i], i);
            }

            packet.ReadInt16("Int-26");
        }
    }
}
