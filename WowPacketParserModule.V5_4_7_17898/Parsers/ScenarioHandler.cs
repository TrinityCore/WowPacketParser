using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class ScenarioHandler
    {
        [Parser(Opcode.SMSG_SCENARIO_STATE)]
        public static void HandleScenarioState(Packet packet)
        {
            var bits10 = 0;
            var bits18 = 0;

            packet.Translator.ReadInt32("Int34");
            packet.Translator.ReadInt32("Step");
            packet.Translator.ReadInt32("Scenario ID");
            packet.Translator.ReadInt32("Int30");
            packet.Translator.ReadInt32("Int38");
            packet.Translator.ReadInt32("Int3C");
            packet.Translator.ReadInt32("Int24");
            var bit40 = packet.Translator.ReadBit();
            bits10 = (int)packet.Translator.ReadBits(19);

            var guid1 = new byte[bits10][];
            var guid2 = new byte[bits10][];

            for (var i = 0; i < bits10; ++i)
            {
                guid1[i] = new byte[8];
                guid2[i] = new byte[8];

                guid1[i][2] = packet.Translator.ReadBit();
                bits18 = (int)packet.Translator.ReadBits(4);
                guid1[i][6] = packet.Translator.ReadBit();
                guid2[i][2] = packet.Translator.ReadBit();
                guid2[i][7] = packet.Translator.ReadBit();
                guid1[i][1] = packet.Translator.ReadBit();
                guid2[i][3] = packet.Translator.ReadBit();
                guid2[i][5] = packet.Translator.ReadBit();
                guid1[i][7] = packet.Translator.ReadBit();
                guid1[i][3] = packet.Translator.ReadBit();
                guid1[i][5] = packet.Translator.ReadBit();
                guid2[i][4] = packet.Translator.ReadBit();
                guid2[i][0] = packet.Translator.ReadBit();
                guid2[i][1] = packet.Translator.ReadBit();
                guid1[i][4] = packet.Translator.ReadBit();
                guid2[i][6] = packet.Translator.ReadBit();
                guid1[i][0] = packet.Translator.ReadBit();
            }

            var bit20 = packet.Translator.ReadBit();

            for (var i = 0; i < bits10; ++i)
            {
                packet.Translator.ReadXORByte(guid1[i], 6);
                packet.Translator.ReadXORByte(guid2[i], 3);
                packet.Translator.ReadXORByte(guid1[i], 3);
                packet.Translator.ReadInt32("Int54");
                packet.Translator.ReadInt32("Int60");
                packet.Translator.ReadXORByte(guid1[i], 0);
                packet.Translator.ReadXORByte(guid2[i], 7);
                packet.Translator.ReadXORByte(guid1[i], 2);
                packet.Translator.ReadXORByte(guid2[i], 1);
                packet.Translator.ReadXORByte(guid2[i], 6);
                packet.Translator.ReadXORByte(guid2[i], 0);
                packet.Translator.ReadXORByte(guid1[i], 1);
                packet.Translator.ReadXORByte(guid2[i], 2);
                packet.Translator.ReadXORByte(guid1[i], 5);
                packet.Translator.ReadXORByte(guid2[i], 4);
                packet.Translator.ReadPackedTime("Time", i);
                packet.Translator.ReadXORByte(guid1[i], 4);
                packet.Translator.ReadXORByte(guid2[i], 5);
                packet.Translator.ReadInt32("Int14");
                packet.Translator.ReadXORByte(guid1[i], 7);
                packet.Translator.WriteGuid("Guid1", guid1[i], i);
                packet.Translator.WriteGuid("Guid2", guid2[i], i);
            }
        }

        [Parser(Opcode.SMSG_SCENARIO_PROGRESS_UPDATE)]
        public static void HandleScenarioProgressUpdate(Packet packet)
        {
            var guid3 = new byte[8];
            var guid4 = new byte[8];

            var bits28 = 0;

            guid4[7] = packet.Translator.ReadBit();
            guid4[0] = packet.Translator.ReadBit();
            guid4[4] = packet.Translator.ReadBit();
            bits28 = (int)packet.Translator.ReadBits(4);
            guid3[3] = packet.Translator.ReadBit();
            guid3[4] = packet.Translator.ReadBit();
            guid3[0] = packet.Translator.ReadBit();
            guid4[6] = packet.Translator.ReadBit();
            guid3[2] = packet.Translator.ReadBit();
            guid4[3] = packet.Translator.ReadBit();
            guid3[7] = packet.Translator.ReadBit();
            guid4[5] = packet.Translator.ReadBit();
            guid3[6] = packet.Translator.ReadBit();
            guid3[5] = packet.Translator.ReadBit();
            guid3[1] = packet.Translator.ReadBit();
            guid4[2] = packet.Translator.ReadBit();
            guid4[1] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(guid4, 5);
            packet.Translator.ReadXORByte(guid3, 2);
            packet.Translator.ReadXORByte(guid3, 6);
            packet.Translator.ReadXORByte(guid4, 4);
            packet.Translator.ReadXORByte(guid3, 4);
            packet.Translator.ReadXORByte(guid4, 6);
            packet.Translator.ReadXORByte(guid3, 3);
            packet.Translator.ReadPackedTime("Time");
            packet.Translator.ReadXORByte(guid4, 7);
            packet.Translator.ReadXORByte(guid3, 5);
            packet.Translator.ReadXORByte(guid3, 7);
            packet.Translator.ReadXORByte(guid3, 0);
            packet.Translator.ReadXORByte(guid4, 1);
            packet.Translator.ReadXORByte(guid3, 1);
            packet.Translator.ReadXORByte(guid4, 2);
            packet.Translator.ReadXORByte(guid4, 3);
            packet.Translator.ReadInt32("Int50");
            packet.Translator.ReadInt32("Int4C");
            packet.Translator.ReadInt32("Criteria ID");
            packet.Translator.ReadXORByte(guid4, 0);

            packet.Translator.WriteGuid("Guid3", guid3);
            packet.Translator.WriteGuid("Guid4", guid4);

        }

        [Parser(Opcode.SMSG_SCENARIO_POIS)]
        public static void HandleScenarioPoi(Packet packet)
        {
            var bits20 = packet.Translator.ReadBits(21);

            var bits4 = new uint[bits20];
            var bits34 = new uint[bits20][];

            for (var i = 0; i < bits20; ++i)
            {
                bits4[i] = packet.Translator.ReadBits(19);

                bits34[i] = new uint[bits4[i]];

                for (var j = 0; j < bits4[i]; ++j)
                    bits34[i][j] = packet.Translator.ReadBits(21);
            }

            for (var i = 0; i < bits20; ++i)
            {
                for (var j = 0; j < bits4[i]; ++j)
                {
                    packet.Translator.ReadInt32<MapId>("Map Id");
                    packet.Translator.ReadInt32("Int10", i, j);
                    packet.Translator.ReadInt32("Int16", i, j);
                    packet.Translator.ReadInt32("World Effect ID", i, j);
                    packet.Translator.ReadInt32("World Map Area ID", i, j);

                    for (var k = 0; k < bits34[i][j]; ++k)
                    {
                        packet.Translator.ReadInt32("Point X", i, j, k);
                        packet.Translator.ReadInt32("Point Y", i, j, k);
                    }

                    packet.Translator.ReadInt32("Int12", i, j);
                    packet.Translator.ReadInt32("Int20", i, j);
                    packet.Translator.ReadInt32("Player Condition ID", i, j);
                }
                packet.Translator.ReadInt32("Criteria Tree ID", i);
            }
        }
    }
}
