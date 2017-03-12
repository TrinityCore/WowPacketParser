using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class AchievementHandler
    {
        [Parser(Opcode.SMSG_CRITERIA_UPDATE)]
        public static void HandleCriteriaPlayer(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 7, 2, 3, 6, 1, 5, 4, 0);

            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 7);

            packet.ReadInt64("Counter");
            packet.ReadInt32("Timer 1");

            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 5);

            packet.ReadInt32("Criteria ID");

            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 3);

            packet.ReadInt32("Flags");
            packet.ReadInt32("Timer 2");

            packet.ReadPackedTime("Time");
            packet.ReadXORByte(guid, 2);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA)]
        public static void HandleAllAchievementDataPlayer(Packet packet)
        {
            var bits10 = packet.ReadBits("Achievement count", 20);

            var guid1 = new byte[bits10][];
            for (var i = 0; i < bits10; ++i)
            {
                guid1[i] = new byte[8];
                packet.StartBitStream(guid1[i], 1, 6, 4, 7, 2, 5, 3, 0);
            }

            var bits20 = packet.ReadBits(19);

            var counter = new byte[bits20][];
            var guid2 = new byte[bits20][];
            var flags = new byte[bits20];
            for (var i = 0; i < bits20; ++i)
            {
                counter[i] = new byte[8];
                guid2[i] = new byte[8];

                counter[i][1] = packet.ReadBit();
                counter[i][3] = packet.ReadBit();
                guid2[i][1] = packet.ReadBit();
                guid2[i][7] = packet.ReadBit();

                flags[i] = (byte)(packet.ReadBits(4) & 0xFFu);

                counter[i][4] = packet.ReadBit();
                counter[i][2] = packet.ReadBit();
                guid2[i][6] = packet.ReadBit();
                counter[i][6] = packet.ReadBit();
                guid2[i][2] = packet.ReadBit();
                counter[i][7] = packet.ReadBit();
                guid2[i][5] = packet.ReadBit();
                counter[i][5] = packet.ReadBit();
                counter[i][0] = packet.ReadBit();
                guid2[i][4] = packet.ReadBit();
                guid2[i][3] = packet.ReadBit();
                guid2[i][0] = packet.ReadBit();
            }

            for (var i = 0; i < bits20; ++i)
            {
                packet.ReadXORByte(guid2[i], 2);
                packet.ReadXORByte(counter[i], 3);
                packet.ReadXORByte(guid2[i], 4);
                packet.ReadXORByte(counter[i], 2);
                packet.ReadXORByte(guid2[i], 6);
                packet.ReadXORByte(guid2[i], 1);
                packet.ReadXORByte(counter[i], 1);

                packet.ReadUInt32("Criteria Timer 1", i);

                packet.ReadXORByte(guid2[i], 3);

                packet.ReadUInt32("Criteria Timer 2", i);

                packet.ReadXORByte(counter[i], 7);
                packet.ReadXORByte(guid2[i], 7);
                packet.ReadXORByte(guid2[i], 0);
                packet.ReadXORByte(counter[i], 4);

                packet.ReadPackedTime("Time", i);

                packet.ReadXORByte(counter[i], 0);
                packet.ReadXORByte(counter[i], 5);
                packet.ReadXORByte(guid2[i], 5);
                packet.ReadXORByte(counter[i], 6);

                packet.ReadUInt32("Criteria Id", i);

                packet.AddValue("Criteria Flags", flags[i], i);
                packet.AddValue("Criteria Counter", BitConverter.ToUInt64(counter[i], 0), i);
                packet.WriteGuid("Criteria GUID", guid2[i], i);
            }

            for (var i = 0; i < bits10; ++i)
            {
                packet.ReadInt32<AchievementId>("Achievement Id");
                packet.ReadXORByte(guid1[i], 6);
                packet.ReadInt32("Realm Id", i);
                packet.ReadXORByte(guid1[i], 2);
                packet.ReadXORByte(guid1[i], 3);

                packet.ReadPackedTime("Time", i);

                packet.ReadInt32("Realm Id", i);
                packet.ReadXORByte(guid1[i], 0);
                packet.ReadXORByte(guid1[i], 7);
                packet.ReadXORByte(guid1[i], 5);
                packet.ReadXORByte(guid1[i], 1);
                packet.ReadXORByte(guid1[i], 4);

                packet.WriteGuid("Criteria GUID", guid1[i], i);
            }
        }
    }
}
