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
