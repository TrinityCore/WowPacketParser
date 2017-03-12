using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class AchievementHandler
    {
        [Parser(Opcode.SMSG_CRITERIA_UPDATE)]
        public static void HandleCriteriaPlayer(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 4, 6, 2, 3, 7, 1, 5, 0);

            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 2);
            packet.ReadInt32("Criteria ID");
            packet.ReadInt32("Flags");
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 1);
            packet.ReadPackedTime("Time");
            packet.ReadXORByte(guid, 4);
            packet.ReadInt32("Timer 1");
            packet.ReadInt32("Timer 2");
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 0);
            packet.ReadInt64("Counter");

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA)]
        public static void HandleAllAchievementDataPlayer(Packet packet)
        {
            var bits20 = packet.ReadBits("Criteria count", 19);

            var counter = new byte[bits20][];
            var guid2 = new byte[bits20][];
            var flags = new byte[bits20];

            for (var i = 0; i < bits20; ++i)
            {
                counter[i] = new byte[8];
                guid2[i] = new byte[8];

                guid2[i][3] = packet.ReadBit();
                counter[i][3] = packet.ReadBit();
                counter[i][6] = packet.ReadBit();
                guid2[i][0] = packet.ReadBit();
                counter[i][7] = packet.ReadBit();
                guid2[i][1] = packet.ReadBit();
                guid2[i][5] = packet.ReadBit();
                counter[i][2] = packet.ReadBit();
                counter[i][1] = packet.ReadBit();
                guid2[i][7] = packet.ReadBit();
                counter[i][4] = packet.ReadBit();
                counter[i][0] = packet.ReadBit();
                guid2[i][2] = packet.ReadBit();
                counter[i][5] = packet.ReadBit();
                guid2[i][4] = packet.ReadBit();
                flags[i] = (byte)(packet.ReadBits(4) & 0xFFu);
                guid2[i][6] = packet.ReadBit();
            }

            var bits10 = packet.ReadBits("Achievement count", 20);

            var guid1 = new byte[bits10][];
            for (var i = 0; i < bits10; ++i)
            {
                guid1[i] = new byte[8];
                packet.StartBitStream(guid1[i], 0, 7, 1, 5, 2, 4, 6, 3);
            }

            for (var i = 0; i < bits10; ++i)
            {
                packet.ReadInt32<AchievementId>("Achievement Id", i);
                packet.ReadInt32("Realm Id", i);
                packet.ReadXORByte(guid1[i], 5);
                packet.ReadXORByte(guid1[i], 7);
                packet.ReadInt32("Realm Id", i);
                packet.ReadPackedTime("Time", i);
                packet.ReadXORByte(guid1[i], 0);
                packet.ReadXORByte(guid1[i], 4);
                packet.ReadXORByte(guid1[i], 1);
                packet.ReadXORByte(guid1[i], 6);
                packet.ReadXORByte(guid1[i], 2);
                packet.ReadXORByte(guid1[i], 3);

                packet.WriteGuid("Guid3", guid1[i], i);
            }

            for (var i = 0; i < bits20; ++i)
            {
                packet.ReadXORByte(guid2[i], 7);
                packet.ReadUInt32("Timer 1", i);
                packet.ReadXORByte(guid2[i], 6);
                packet.ReadXORByte(counter[i], 1);
                packet.ReadInt32("Criteria ID", i);
                packet.ReadXORByte(guid2[i], 4);
                packet.ReadXORByte(counter[i], 0);
                packet.ReadXORByte(counter[i], 4);
                packet.ReadXORByte(counter[i], 6);
                packet.ReadXORByte(guid2[i], 1);
                packet.ReadXORByte(guid2[i], 5);
                packet.ReadXORByte(counter[i], 7);
                packet.ReadXORByte(counter[i], 2);
                packet.ReadXORByte(guid2[i], 2);
                packet.ReadXORByte(guid2[i], 0);
                packet.ReadXORByte(counter[i], 3);
                packet.ReadUInt32("Timer 2", i);
                packet.ReadXORByte(guid2[i], 3);
                packet.ReadXORByte(counter[i], 5);
                packet.ReadPackedTime("Time", i);

                packet.WriteGuid("Guid2", guid2[i], i);

                packet.AddValue("Criteria Flags", flags[i], i);
                packet.AddValue("Criteria Counter", BitConverter.ToUInt64(guid2[i], 0), i); // guid and counter is switched, was lazy to remake it
                packet.WriteGuid("Criteria GUID", counter[i], i);
            }
        }

        [Parser(Opcode.SMSG_ACHIEVEMENT_EARNED)]
        public static void HandleAchievementEarned(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[6] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            packet.ReadBit("unk");
            guid2[7] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid2[5] = packet.ReadBit();

            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid1, 6);
            packet.ReadPackedTime("Time");
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 7);
            packet.ReadInt32<AchievementId>("Achievement Id");
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid1, 5);
            packet.ReadInt32("Realm Id");
            packet.ReadInt32("Realm Id");
            packet.ReadXORByte(guid2, 2);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid1);
        }
    }
}
