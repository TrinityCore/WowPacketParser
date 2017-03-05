using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class AchievementHandler
    {
        [Parser(Opcode.SMSG_CRITERIA_UPDATE)]
        public static void HandleCriteriaUpdate(Packet packet)
        {
            var counter = new byte[8];
            var guid = new byte[8];

            counter[5] = packet.ReadBit();

            packet.ReadBits("Flags", 4); // some flag... & 1 -> delete

            counter[3] = packet.ReadBit();
            counter[1] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            counter[4] = packet.ReadBit();
            counter[2] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            counter[6] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            counter[0] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            counter[7] = packet.ReadBit();
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(counter, 6);
            packet.ReadXORByte(guid, 1);

            packet.ReadUInt32("Timer 1");

            packet.ReadXORByte(counter, 2);

            packet.ReadInt32("Criteria ID");

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(counter, 0);
            packet.ReadXORByte(counter, 5);

            packet.ResetBitReader();

            packet.ReadPackedTime("Time");

            packet.ReadXORByte(counter, 4);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 7);

            packet.ReadUInt32("Timer 2");

            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(counter, 7);
            packet.ReadXORByte(counter, 3);
            packet.ReadXORByte(counter, 1);

            packet.AddValue("Counter", BitConverter.ToInt64(counter, 0));
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_CRITERIA_UNKNOWN)]
        public static void HandleCriteriaUnknow(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32("Criteria ID");
            packet.ReadInt32("Flags");

            packet.ResetBitReader();

            packet.ReadPackedTime("Time");

            packet.ReadInt32("Timer 1");
            packet.ReadInt64("Counter");
            packet.ReadInt32("Timer 2");

            packet.StartBitStream(guid, 2, 4, 1, 5, 3, 6, 7, 0);
            packet.ParseBitStream(guid, 7, 0, 6, 5, 2, 1, 4, 3);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA)]
        public static void HandleAllAchievementData(Packet packet)
        {
            var bits10 = packet.ReadBits("Achievement count", 20);

            var guid1 = new byte[bits10][];
            for (var i = 0; i < bits10; ++i)
            {
                guid1[i] = new byte[8];
                packet.StartBitStream(guid1[i], 6, 1, 5, 3, 2, 7, 0, 4);
            }

            var bits20 = packet.ReadBits("Criteria count", 19);

            var counter = new byte[bits20][];
            var guid2 = new byte[bits20][];
            var flags = new byte[bits20];
            for (var i = 0; i < bits20; ++i)
            {
                counter[i] = new byte[8];
                guid2[i] = new byte[8];

                counter[i][5] = packet.ReadBit();
                packet.StartBitStream(guid2[i], 2, 4);
                packet.StartBitStream(counter[i], 1, 7);
                packet.StartBitStream(guid2[i], 0, 1);
                counter[i][3] = packet.ReadBit();
                guid2[i][5] = packet.ReadBit();

                flags[i] = (byte)(packet.ReadBits(4) & 0xFFu);

                packet.StartBitStream(counter[i], 0, 6, 4);
                packet.StartBitStream(guid2[i], 7, 3, 6);

                counter[i][2] = packet.ReadBit();
            }

            for (var i = 0; i < bits20; ++i)
            {
                packet.ReadXORByte(guid2[i], 1);

                packet.ReadUInt32("Criteria Id", i);

                packet.ReadXORByte(guid2[i], 7);
                packet.ReadXORByte(counter[i], 5);

                packet.ReadUInt32("Criteria Timer 2", i);

                packet.ReadXORByte(guid2[i], 6);
                packet.ReadXORByte(counter[i], 0);

                packet.ReadPackedTime("Criteria Date", i);

                packet.ReadXORByte(guid2[i], 2);
                packet.ReadXORByte(counter[i], 2);
                packet.ReadXORByte(counter[i], 1);

                packet.ReadUInt32("Criteria Timer 1", i);

                packet.ReadXORByte(guid2[i], 3);
                packet.ReadXORByte(guid2[i], 5);
                packet.ReadXORByte(counter[i], 3);
                packet.ReadXORByte(guid2[i], 4);
                packet.ReadXORByte(counter[i], 4);
                packet.ReadXORByte(counter[i], 7);
                packet.ReadXORByte(counter[i], 6);
                packet.ReadXORByte(guid2[i], 0);

                packet.AddValue("Criteria Flags", flags[i], i);
                packet.AddValue("Criteria Counter", BitConverter.ToUInt64(counter[i], 0), i);
                packet.WriteGuid("Criteria GUID", guid2[i], i);
            }

            for (var i = 0; i < bits10; ++i)
            {
                packet.ReadXORByte(guid1[i], 4);
                packet.ReadXORByte(guid1[i], 6);
                packet.ReadXORByte(guid1[i], 3);
                packet.ReadXORByte(guid1[i], 0);

                packet.ReadInt32("Realm Id");

                packet.ReadXORByte(guid1[i], 1);
                packet.ReadXORByte(guid1[i], 2);

                packet.ReadPackedTime("Achievement Date", i);

                packet.ReadXORByte(guid1[i], 7);
                packet.ReadXORByte(guid1[i], 5);

                packet.ReadInt32("Int14", i);
                packet.ReadInt32<AchievementId>("Achievement Id", i);

                packet.WriteGuid("GUID1", guid1[i], i);
            }
        }
    }
}
