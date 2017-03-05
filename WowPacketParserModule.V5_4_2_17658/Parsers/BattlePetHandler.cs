using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class BattlePetHandler
    {
        [Parser(Opcode.SMSG_BATTLE_PET_JOURNAL)]
        public static void HandleBattlePetJournal(Packet packet)
        {
            byte[][] guid1;
            byte[][] guid2;
            byte[][] guid3;

            var bit34 = packet.Translator.ReadBit();
            var bits24 = packet.Translator.ReadBits(25);

            var bit8 = new bool[bits24];
            var bit0C = new bool[bits24];

            guid1 = new byte[bits24][];
            for (var i = 0; i < bits24; ++i)
            {
                guid1[i] = new byte[8];

                packet.Translator.ReadBit(); // fake bit

                packet.Translator.ReadBit("Unk bit");

                packet.Translator.StartBitStream(guid1[i], 7, 5, 3, 0, 4, 1, 6, 2);

                bit8[i] = !packet.Translator.ReadBit();
                bit0C[i] = !packet.Translator.ReadBit();
            }

            var bits10 = packet.Translator.ReadBits(19);

            guid2 = new byte[bits10][];
            guid3 = new byte[bits10][];

            var bit88 = new bool[bits10];
            var bits2C = new uint[bits10];
            var bit80 = new bool[bits10];
            var bit14 = new bool[bits10];
            var bit1A = new bool[bits10];
            var bit89 = new bool[bits10];

            for (var i = 0; i < bits10; ++i)
            {
                bit88[i] = packet.Translator.ReadBit();
                bits2C[i] = packet.Translator.ReadBits(7);
                bit80[i] = packet.Translator.ReadBit();

                if (bit80[i])
                {
                    guid2[i] = new byte[8];
                    packet.Translator.StartBitStream(guid2[i], 4, 5, 6, 2, 0, 7, 1, 3);
                }

                bit14[i] = !packet.Translator.ReadBit();

                guid3[i] = new byte[8];

                guid3[i][7] = packet.Translator.ReadBit();
                guid3[i][0] = packet.Translator.ReadBit();
                guid3[i][5] = packet.Translator.ReadBit();
                guid3[i][4] = packet.Translator.ReadBit();
                bit1A[i] = !packet.Translator.ReadBit();
                guid3[i][3] = packet.Translator.ReadBit();
                bit89[i] = !packet.Translator.ReadBit();
                guid3[i][1] = packet.Translator.ReadBit();
                guid3[i][6] = packet.Translator.ReadBit();
                guid3[i][2] = packet.Translator.ReadBit();
            }

            for (var i = 0; i < bits10; ++i)
            {
                packet.Translator.ReadXORByte(guid3[i], 6);
                packet.Translator.ReadXORByte(guid3[i], 1);

                packet.Translator.ReadInt32("IntED", i);

                if (bit80[i])
                {
                    packet.Translator.ReadInt32("IntED", i);

                    packet.Translator.ReadXORByte(guid2[i], 2);
                    packet.Translator.ReadXORByte(guid2[i], 0);
                    packet.Translator.ReadXORByte(guid2[i], 7);
                    packet.Translator.ReadXORByte(guid2[i], 3);
                    packet.Translator.ReadXORByte(guid2[i], 4);

                    packet.Translator.ReadInt32("IntED", i);

                    packet.Translator.ReadXORByte(guid2[i], 7);
                    packet.Translator.ReadXORByte(guid2[i], 5);
                    packet.Translator.ReadXORByte(guid2[i], 1);

                    packet.Translator.WriteGuid("Guid2D", guid2[i], i);
                }

                if (bit14[i])
                    packet.Translator.ReadInt16("IntED", i);

                packet.Translator.ReadInt32("Entry", i);
                packet.Translator.ReadInt32("IntED", i);
                packet.Translator.ReadInt32("IntED", i);
                packet.Translator.ReadInt32("IntED", i);
                packet.Translator.ReadInt32("IntED", i);
                packet.Translator.ReadInt32("IntED", i);

                packet.Translator.ReadXORByte(guid3[i], 0);

                packet.Translator.ReadInt16("IntED", i);
                packet.Translator.ReadInt16("IntED", i);

                packet.Translator.ReadWoWString("Custom Name", bits2C[i], i);

                packet.Translator.ReadXORByte(guid3[i], 7);

                if (bit89[i])
                    packet.Translator.ReadByte("ByteED", i);

                packet.Translator.ReadXORByte(guid3[i], 2);
                packet.Translator.ReadXORByte(guid3[i], 3);

                if (bit1A[i])
                    packet.Translator.ReadInt16("IntED", i);

                packet.Translator.ReadXORByte(guid3[i], 5);
                packet.Translator.ReadXORByte(guid3[i], 4);

                packet.Translator.WriteGuid("Guid3", guid3[i], i);
            }

            for (var i = 0; i < bits24; ++i)
            {
                packet.Translator.ParseBitStream(guid1[i], 4, 3, 0, 1, 5, 7, 2, 6);

                if (bit8[i])
                    packet.Translator.ReadInt32("Int28", i);

                if (bit0C[i])
                    packet.Translator.ReadByte("Byte28", i);

                packet.Translator.WriteGuid("Guid1", guid1[i], i);
            }

            packet.Translator.ReadInt16("Int20");
        }
    }
}
