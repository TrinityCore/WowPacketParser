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

            packet.ReadInt32("Int34");
            packet.ReadInt32("Step");
            packet.ReadInt32("Scenario ID");
            packet.ReadInt32("Int30");
            packet.ReadInt32("Int38");
            packet.ReadInt32("Int3C");
            packet.ReadInt32("Int24");
            var bit40 = packet.ReadBit();
            bits10 = (int)packet.ReadBits(19);

            var guid1 = new byte[bits10][];
            var guid2 = new byte[bits10][];

            for (var i = 0; i < bits10; ++i)
            {
                guid1[i] = new byte[8];
                guid2[i] = new byte[8];

                guid1[i][2] = packet.ReadBit();
                bits18 = (int)packet.ReadBits(4);
                guid1[i][6] = packet.ReadBit();
                guid2[i][2] = packet.ReadBit();
                guid2[i][7] = packet.ReadBit();
                guid1[i][1] = packet.ReadBit();
                guid2[i][3] = packet.ReadBit();
                guid2[i][5] = packet.ReadBit();
                guid1[i][7] = packet.ReadBit();
                guid1[i][3] = packet.ReadBit();
                guid1[i][5] = packet.ReadBit();
                guid2[i][4] = packet.ReadBit();
                guid2[i][0] = packet.ReadBit();
                guid2[i][1] = packet.ReadBit();
                guid1[i][4] = packet.ReadBit();
                guid2[i][6] = packet.ReadBit();
                guid1[i][0] = packet.ReadBit();
            }

            var bit20 = packet.ReadBit();

            for (var i = 0; i < bits10; ++i)
            {
                packet.ReadXORByte(guid1[i], 6);
                packet.ReadXORByte(guid2[i], 3);
                packet.ReadXORByte(guid1[i], 3);
                packet.ReadInt32("Int54");
                packet.ReadInt32("Int60");
                packet.ReadXORByte(guid1[i], 0);
                packet.ReadXORByte(guid2[i], 7);
                packet.ReadXORByte(guid1[i], 2);
                packet.ReadXORByte(guid2[i], 1);
                packet.ReadXORByte(guid2[i], 6);
                packet.ReadXORByte(guid2[i], 0);
                packet.ReadXORByte(guid1[i], 1);
                packet.ReadXORByte(guid2[i], 2);
                packet.ReadXORByte(guid1[i], 5);
                packet.ReadXORByte(guid2[i], 4);
                packet.ReadPackedTime("Time", i);
                packet.ReadXORByte(guid1[i], 4);
                packet.ReadXORByte(guid2[i], 5);
                packet.ReadInt32("Int14");
                packet.ReadXORByte(guid1[i], 7);
                packet.WriteGuid("Guid1", guid1[i], i);
                packet.WriteGuid("Guid2", guid2[i], i);
            }
        }

        [Parser(Opcode.SMSG_SCENARIO_PROGRESS_UPDATE)]
        public static void HandleScenarioProgressUpdate(Packet packet)
        {
            var guid3 = new byte[8];
            var guid4 = new byte[8];

            var bits28 = 0;

            guid4[7] = packet.ReadBit();
            guid4[0] = packet.ReadBit();
            guid4[4] = packet.ReadBit();
            bits28 = (int)packet.ReadBits(4);
            guid3[3] = packet.ReadBit();
            guid3[4] = packet.ReadBit();
            guid3[0] = packet.ReadBit();
            guid4[6] = packet.ReadBit();
            guid3[2] = packet.ReadBit();
            guid4[3] = packet.ReadBit();
            guid3[7] = packet.ReadBit();
            guid4[5] = packet.ReadBit();
            guid3[6] = packet.ReadBit();
            guid3[5] = packet.ReadBit();
            guid3[1] = packet.ReadBit();
            guid4[2] = packet.ReadBit();
            guid4[1] = packet.ReadBit();
            packet.ReadXORByte(guid4, 5);
            packet.ReadXORByte(guid3, 2);
            packet.ReadXORByte(guid3, 6);
            packet.ReadXORByte(guid4, 4);
            packet.ReadXORByte(guid3, 4);
            packet.ReadXORByte(guid4, 6);
            packet.ReadXORByte(guid3, 3);
            packet.ReadPackedTime("Time");
            packet.ReadXORByte(guid4, 7);
            packet.ReadXORByte(guid3, 5);
            packet.ReadXORByte(guid3, 7);
            packet.ReadXORByte(guid3, 0);
            packet.ReadXORByte(guid4, 1);
            packet.ReadXORByte(guid3, 1);
            packet.ReadXORByte(guid4, 2);
            packet.ReadXORByte(guid4, 3);
            packet.ReadInt32("Int50");
            packet.ReadInt32("Int4C");
            packet.ReadInt32("Criteria ID");
            packet.ReadXORByte(guid4, 0);

            packet.WriteGuid("Guid3", guid3);
            packet.WriteGuid("Guid4", guid4);

        }

        [Parser(Opcode.SMSG_SCENARIO_POIS)]
        public static void HandleScenarioPoi(Packet packet)
        {
            var bits20 = packet.ReadBits(21);

            var bits4 = new uint[bits20];
            var bits34 = new uint[bits20][];

            for (var i = 0; i < bits20; ++i)
            {
                bits4[i] = packet.ReadBits(19);

                bits34[i] = new uint[bits4[i]];

                for (var j = 0; j < bits4[i]; ++j)
                    bits34[i][j] = packet.ReadBits(21);
            }

            for (var i = 0; i < bits20; ++i)
            {
                for (var j = 0; j < bits4[i]; ++j)
                {
                    packet.ReadInt32<MapId>("Map Id");
                    packet.ReadInt32("Int10", i, j);
                    packet.ReadInt32("Int16", i, j);
                    packet.ReadInt32("World Effect ID", i, j);
                    packet.ReadInt32("World Map Area ID", i, j);

                    for (var k = 0; k < bits34[i][j]; ++k)
                    {
                        packet.ReadInt32("Point X", i, j, k);
                        packet.ReadInt32("Point Y", i, j, k);
                    }

                    packet.ReadInt32("Int12", i, j);
                    packet.ReadInt32("Int20", i, j);
                    packet.ReadInt32("Player Condition ID", i, j);
                }
                packet.ReadInt32("Criteria Tree ID", i);
            }
        }
    }
}
