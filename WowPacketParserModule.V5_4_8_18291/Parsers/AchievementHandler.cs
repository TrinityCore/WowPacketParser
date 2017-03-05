using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class AchievementHandler
    {
        [Parser(Opcode.SMSG_ACCOUNT_CRITERIA_UPDATE)]
        public static void HandleCriteriaUpdateAccount(Packet packet)
        {
            var counter = new byte[8];
            var accountId = new byte[8];

            counter[4] = packet.Translator.ReadBit();
            accountId[2] = packet.Translator.ReadBit();
            counter[2] = packet.Translator.ReadBit();
            accountId[4] = packet.Translator.ReadBit();
            counter[0] = packet.Translator.ReadBit();
            counter[5] = packet.Translator.ReadBit();
            accountId[3] = packet.Translator.ReadBit();
            counter[3] = packet.Translator.ReadBit();
            accountId[6] = packet.Translator.ReadBit();
            counter[6] = packet.Translator.ReadBit();
            accountId[1] = packet.Translator.ReadBit();
            accountId[7] = packet.Translator.ReadBit();
            counter[1] = packet.Translator.ReadBit();

            packet.Translator.ReadBits("Flags", 4); // some flag... & 1 -> delete

            accountId[5] = packet.Translator.ReadBit();
            counter[7] = packet.Translator.ReadBit();
            accountId[0] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(accountId, 7);
            packet.Translator.ReadUInt32("Timer 2");
            packet.Translator.ReadInt32("Criteria ID");
            packet.Translator.ReadXORByte(counter, 7);
            packet.Translator.ReadUInt32("Timer 1");
            packet.Translator.ReadXORByte(accountId, 4);
            packet.Translator.ReadXORByte(accountId, 3);
            packet.Translator.ReadPackedTime("Time");
            packet.Translator.ReadXORByte(counter, 0);
            packet.Translator.ReadXORByte(counter, 1);
            packet.Translator.ReadXORByte(counter, 2);
            packet.Translator.ReadXORByte(counter, 3);
            packet.Translator.ReadXORByte(accountId, 1);
            packet.Translator.ReadXORByte(counter, 4);
            packet.Translator.ReadXORByte(counter, 5);
            packet.Translator.ReadXORByte(accountId, 5);
            packet.Translator.ReadXORByte(accountId, 2);
            packet.Translator.ReadXORByte(counter, 6);
            packet.Translator.ReadXORByte(accountId, 0);
            packet.Translator.ReadXORByte(accountId, 6);

            packet.AddValue("Account", BitConverter.ToUInt64(accountId, 0));
            packet.AddValue("Counter", BitConverter.ToInt64(counter, 0));
        }

        [Parser(Opcode.SMSG_CRITERIA_UPDATE)]
        public static void HandleCriteriaPlayer(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 4, 6, 2, 3, 7, 1, 5, 0);

            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadInt32("Criteria ID");
            packet.Translator.ReadInt32("Flags");
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadPackedTime("Time");
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadInt32("Timer 1");
            packet.Translator.ReadInt32("Timer 2");
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadInt64("Counter");

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA)]
        public static void HandleAllAchievementDataPlayer(Packet packet)
        {
            var bits20 = packet.Translator.ReadBits("Criteria count", 19);

            var counter = new byte[bits20][];
            var guid2 = new byte[bits20][];
            var flags = new byte[bits20];

            for (var i = 0; i < bits20; ++i)
            {
                counter[i] = new byte[8];
                guid2[i] = new byte[8];

                guid2[i][3] = packet.Translator.ReadBit();
                counter[i][3] = packet.Translator.ReadBit();
                counter[i][6] = packet.Translator.ReadBit();
                guid2[i][0] = packet.Translator.ReadBit();
                counter[i][7] = packet.Translator.ReadBit();
                guid2[i][1] = packet.Translator.ReadBit();
                guid2[i][5] = packet.Translator.ReadBit();
                counter[i][2] = packet.Translator.ReadBit();
                counter[i][1] = packet.Translator.ReadBit();
                guid2[i][7] = packet.Translator.ReadBit();
                counter[i][4] = packet.Translator.ReadBit();
                counter[i][0] = packet.Translator.ReadBit();
                guid2[i][2] = packet.Translator.ReadBit();
                counter[i][5] = packet.Translator.ReadBit();
                guid2[i][4] = packet.Translator.ReadBit();
                flags[i] = (byte)(packet.Translator.ReadBits(4) & 0xFFu);
                guid2[i][6] = packet.Translator.ReadBit();
            }

            var bits10 = packet.Translator.ReadBits("Achievement count", 20);

            var guid1 = new byte[bits10][];
            for (var i = 0; i < bits10; ++i)
            {
                guid1[i] = new byte[8];
                packet.Translator.StartBitStream(guid1[i], 0, 7, 1, 5, 2, 4, 6, 3);
            }

            for (var i = 0; i < bits10; ++i)
            {
                packet.Translator.ReadInt32<AchievementId>("Achievement Id", i);
                packet.Translator.ReadInt32("Realm Id", i);
                packet.Translator.ReadXORByte(guid1[i], 5);
                packet.Translator.ReadXORByte(guid1[i], 7);
                packet.Translator.ReadInt32("Realm Id", i);
                packet.Translator.ReadPackedTime("Time", i);
                packet.Translator.ReadXORByte(guid1[i], 0);
                packet.Translator.ReadXORByte(guid1[i], 4);
                packet.Translator.ReadXORByte(guid1[i], 1);
                packet.Translator.ReadXORByte(guid1[i], 6);
                packet.Translator.ReadXORByte(guid1[i], 2);
                packet.Translator.ReadXORByte(guid1[i], 3);

                packet.Translator.WriteGuid("Guid3", guid1[i], i);
            }

            for (var i = 0; i < bits20; ++i)
            {
                packet.Translator.ReadXORByte(guid2[i], 7);
                packet.Translator.ReadUInt32("Timer 1", i);
                packet.Translator.ReadXORByte(guid2[i], 6);
                packet.Translator.ReadXORByte(counter[i], 1);
                packet.Translator.ReadInt32("Criteria ID", i);
                packet.Translator.ReadXORByte(guid2[i], 4);
                packet.Translator.ReadXORByte(counter[i], 0);
                packet.Translator.ReadXORByte(counter[i], 4);
                packet.Translator.ReadXORByte(counter[i], 6);
                packet.Translator.ReadXORByte(guid2[i], 1);
                packet.Translator.ReadXORByte(guid2[i], 5);
                packet.Translator.ReadXORByte(counter[i], 7);
                packet.Translator.ReadXORByte(counter[i], 2);
                packet.Translator.ReadXORByte(guid2[i], 2);
                packet.Translator.ReadXORByte(guid2[i], 0);
                packet.Translator.ReadXORByte(counter[i], 3);
                packet.Translator.ReadUInt32("Timer 2", i);
                packet.Translator.ReadXORByte(guid2[i], 3);
                packet.Translator.ReadXORByte(counter[i], 5);
                packet.Translator.ReadPackedTime("Time", i);

                packet.Translator.WriteGuid("Guid2", guid2[i], i);

                packet.AddValue("Criteria Flags", flags[i], i);
                packet.AddValue("Criteria Counter", BitConverter.ToUInt64(guid2[i], 0), i); // guid and counter is switched, was lazy to remake it
                packet.Translator.WriteGuid("Criteria GUID", counter[i], i);
            }
        }

        [Parser(Opcode.SMSG_ACHIEVEMENT_EARNED)]
        public static void HandleAchievementEarned(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[6] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            guid1[4] = packet.Translator.ReadBit();
            guid1[5] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("unk");
            guid2[7] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            guid1[2] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadPackedTime("Time");
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadInt32<AchievementId>("Achievement Id");
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadInt32("Realm Id");
            packet.Translator.ReadInt32("Realm Id");
            packet.Translator.ReadXORByte(guid2, 2);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid1);
        }
    }
}
