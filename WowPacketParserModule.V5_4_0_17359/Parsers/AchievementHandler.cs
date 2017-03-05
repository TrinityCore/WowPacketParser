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

            counter[5] = packet.Translator.ReadBit();

            packet.Translator.ReadBits("Flags", 4); // some flag... & 1 -> delete

            counter[3] = packet.Translator.ReadBit();
            counter[1] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            counter[4] = packet.Translator.ReadBit();
            counter[2] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            counter[6] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            counter[0] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            counter[7] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(counter, 6);
            packet.Translator.ReadXORByte(guid, 1);

            packet.Translator.ReadUInt32("Timer 1");

            packet.Translator.ReadXORByte(counter, 2);

            packet.Translator.ReadInt32("Criteria ID");

            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(counter, 0);
            packet.Translator.ReadXORByte(counter, 5);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadPackedTime("Time");

            packet.Translator.ReadXORByte(counter, 4);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 7);

            packet.Translator.ReadUInt32("Timer 2");

            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(counter, 7);
            packet.Translator.ReadXORByte(counter, 3);
            packet.Translator.ReadXORByte(counter, 1);

            packet.AddValue("Counter", BitConverter.ToInt64(counter, 0));
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_CRITERIA_UNKNOWN)]
        public static void HandleCriteriaUnknow(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadInt32("Criteria ID");
            packet.Translator.ReadInt32("Flags");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadPackedTime("Time");

            packet.Translator.ReadInt32("Timer 1");
            packet.Translator.ReadInt64("Counter");
            packet.Translator.ReadInt32("Timer 2");

            packet.Translator.StartBitStream(guid, 2, 4, 1, 5, 3, 6, 7, 0);
            packet.Translator.ParseBitStream(guid, 7, 0, 6, 5, 2, 1, 4, 3);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA)]
        public static void HandleAllAchievementData(Packet packet)
        {
            var bits10 = packet.Translator.ReadBits("Achievement count", 20);

            var guid1 = new byte[bits10][];
            for (var i = 0; i < bits10; ++i)
            {
                guid1[i] = new byte[8];
                packet.Translator.StartBitStream(guid1[i], 6, 1, 5, 3, 2, 7, 0, 4);
            }

            var bits20 = packet.Translator.ReadBits("Criteria count", 19);

            var counter = new byte[bits20][];
            var guid2 = new byte[bits20][];
            var flags = new byte[bits20];
            for (var i = 0; i < bits20; ++i)
            {
                counter[i] = new byte[8];
                guid2[i] = new byte[8];

                counter[i][5] = packet.Translator.ReadBit();
                packet.Translator.StartBitStream(guid2[i], 2, 4);
                packet.Translator.StartBitStream(counter[i], 1, 7);
                packet.Translator.StartBitStream(guid2[i], 0, 1);
                counter[i][3] = packet.Translator.ReadBit();
                guid2[i][5] = packet.Translator.ReadBit();

                flags[i] = (byte)(packet.Translator.ReadBits(4) & 0xFFu);

                packet.Translator.StartBitStream(counter[i], 0, 6, 4);
                packet.Translator.StartBitStream(guid2[i], 7, 3, 6);

                counter[i][2] = packet.Translator.ReadBit();
            }

            for (var i = 0; i < bits20; ++i)
            {
                packet.Translator.ReadXORByte(guid2[i], 1);

                packet.Translator.ReadUInt32("Criteria Id", i);

                packet.Translator.ReadXORByte(guid2[i], 7);
                packet.Translator.ReadXORByte(counter[i], 5);

                packet.Translator.ReadUInt32("Criteria Timer 2", i);

                packet.Translator.ReadXORByte(guid2[i], 6);
                packet.Translator.ReadXORByte(counter[i], 0);

                packet.Translator.ReadPackedTime("Criteria Date", i);

                packet.Translator.ReadXORByte(guid2[i], 2);
                packet.Translator.ReadXORByte(counter[i], 2);
                packet.Translator.ReadXORByte(counter[i], 1);

                packet.Translator.ReadUInt32("Criteria Timer 1", i);

                packet.Translator.ReadXORByte(guid2[i], 3);
                packet.Translator.ReadXORByte(guid2[i], 5);
                packet.Translator.ReadXORByte(counter[i], 3);
                packet.Translator.ReadXORByte(guid2[i], 4);
                packet.Translator.ReadXORByte(counter[i], 4);
                packet.Translator.ReadXORByte(counter[i], 7);
                packet.Translator.ReadXORByte(counter[i], 6);
                packet.Translator.ReadXORByte(guid2[i], 0);

                packet.AddValue("Criteria Flags", flags[i], i);
                packet.AddValue("Criteria Counter", BitConverter.ToUInt64(counter[i], 0), i);
                packet.Translator.WriteGuid("Criteria GUID", guid2[i], i);
            }

            for (var i = 0; i < bits10; ++i)
            {
                packet.Translator.ReadXORByte(guid1[i], 4);
                packet.Translator.ReadXORByte(guid1[i], 6);
                packet.Translator.ReadXORByte(guid1[i], 3);
                packet.Translator.ReadXORByte(guid1[i], 0);

                packet.Translator.ReadInt32("Realm Id");

                packet.Translator.ReadXORByte(guid1[i], 1);
                packet.Translator.ReadXORByte(guid1[i], 2);

                packet.Translator.ReadPackedTime("Achievement Date", i);

                packet.Translator.ReadXORByte(guid1[i], 7);
                packet.Translator.ReadXORByte(guid1[i], 5);

                packet.Translator.ReadInt32("Int14", i);
                packet.Translator.ReadInt32<AchievementId>("Achievement Id", i);

                packet.Translator.WriteGuid("GUID1", guid1[i], i);
            }
        }
    }
}
