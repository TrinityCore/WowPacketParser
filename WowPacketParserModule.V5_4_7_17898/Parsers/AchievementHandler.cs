using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class AchievementHandler
    {
        [Parser(Opcode.SMSG_ACCOUNT_CRITERIA_UPDATE)]
        public static void HandleCriteriaUpdateAccount(Packet packet)
        {
            var accountId = new byte[8];
            var counter = new byte[8];

            accountId[1] = packet.Translator.ReadBit();
            counter[5] = packet.Translator.ReadBit();
            accountId[0] = packet.Translator.ReadBit();
            counter[4] = packet.Translator.ReadBit();

            packet.Translator.ReadBits("Flags", 4); // some flag... & 1 -> delete

            counter[3] = packet.Translator.ReadBit();
            counter[7] = packet.Translator.ReadBit();
            counter[2] = packet.Translator.ReadBit();
            accountId[3] = packet.Translator.ReadBit();
            counter[1] = packet.Translator.ReadBit();
            counter[6] = packet.Translator.ReadBit();
            accountId[2] = packet.Translator.ReadBit();
            accountId[6] = packet.Translator.ReadBit();
            counter[0] = packet.Translator.ReadBit();
            accountId[4] = packet.Translator.ReadBit();
            accountId[5] = packet.Translator.ReadBit();
            accountId[7] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(accountId, 0);
            packet.Translator.ReadXORByte(counter, 1);
            packet.Translator.ReadXORByte(counter, 3);
            packet.Translator.ReadUInt32("Timer 1");
            packet.Translator.ReadXORByte(accountId, 4);
            packet.Translator.ReadXORByte(counter, 0);
            packet.Translator.ReadXORByte(accountId, 7);
            packet.Translator.ReadXORByte(accountId, 6);
            packet.Translator.ReadXORByte(counter, 2);
            packet.Translator.ReadUInt32("Timer 2");
            packet.Translator.ReadXORByte(counter, 7);
            packet.Translator.ReadXORByte(accountId, 5);
            packet.Translator.ReadXORByte(accountId, 1);
            packet.Translator.ReadXORByte(counter, 5);
            packet.Translator.ReadXORByte(accountId, 3);
            packet.Translator.ReadInt32("Criteria ID");
            packet.Translator.ReadPackedTime("Time");
            packet.Translator.ReadXORByte(accountId, 2);
            packet.Translator.ReadXORByte(counter, 4);
            packet.Translator.ReadXORByte(counter, 6);

            packet.AddValue("Account", BitConverter.ToUInt64(accountId, 0));
            packet.AddValue("Counter", BitConverter.ToInt64(counter, 0));
        }

        [Parser(Opcode.SMSG_CRITERIA_UPDATE)]
        public static void HandleCriteriaPlayer(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadPackedTime("Time");

            packet.Translator.ReadInt32("Timer 1");
            packet.Translator.ReadInt32("Timer 2");
            packet.Translator.ReadInt64("Counter");
            packet.Translator.ReadInt32("Criteria ID");
            packet.Translator.ReadInt32("Flags");

            packet.Translator.StartBitStream(guid, 2, 4, 0, 6, 3, 7, 5, 1);
            packet.Translator.ParseBitStream(guid, 4, 2, 6, 1, 7, 3, 0, 5);

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

                counter[i][3] = packet.Translator.ReadBit();
                counter[i][6] = packet.Translator.ReadBit();
                guid2[i][0] = packet.Translator.ReadBit();
                guid2[i][7] = packet.Translator.ReadBit();
                counter[i][7] = packet.Translator.ReadBit();
                counter[i][0] = packet.Translator.ReadBit();
                counter[i][5] = packet.Translator.ReadBit();

                flags[i] = (byte)(packet.Translator.ReadBits(4) & 0xFFu);

                counter[i][4] = packet.Translator.ReadBit();
                guid2[i][3] = packet.Translator.ReadBit();
                guid2[i][6] = packet.Translator.ReadBit();
                guid2[i][4] = packet.Translator.ReadBit();
                guid2[i][5] = packet.Translator.ReadBit();
                guid2[i][1] = packet.Translator.ReadBit();
                counter[i][2] = packet.Translator.ReadBit();
                counter[i][1] = packet.Translator.ReadBit();
                guid2[i][2] = packet.Translator.ReadBit();
            }

            var bits10 = packet.Translator.ReadBits("Achievement count", 20);

            var guid1 = new byte[bits10][];
            for (var i = 0; i < bits10; ++i)
            {
                guid1[i] = new byte[8];
                packet.Translator.StartBitStream(guid1[i], 4, 7, 2, 3, 1, 5, 6, 0);
            }

            for (var i = 0; i < bits10; ++i)
            {
                packet.Translator.ReadXORByte(guid1[i], 6);
                packet.Translator.ReadXORByte(guid1[i], 5);
                packet.Translator.ReadXORByte(guid1[i], 3);
                packet.Translator.ReadXORByte(guid1[i], 0);
                packet.Translator.ReadXORByte(guid1[i], 2);
                packet.Translator.ReadXORByte(guid1[i], 1);
                packet.Translator.ReadInt32<AchievementId>("Achievement Id", i);
                packet.Translator.ReadInt32("Realm Id", i);
                packet.Translator.ReadInt32("Realm Id", i);
                packet.Translator.ReadXORByte(guid1[i], 4);
                packet.Translator.ReadPackedTime("Time", i);
                packet.Translator.ReadXORByte(guid1[i], 7);

                packet.Translator.WriteGuid("Guid3", guid1[i], i);
            }

            for (var i = 0; i < bits20; ++i)
            {
                packet.Translator.ReadPackedTime("Time", i);

                packet.Translator.ReadXORByte(counter[i], 2);

                packet.Translator.ReadXORByte(guid2[i], 2);
                packet.Translator.ReadXORByte(guid2[i], 3);

                packet.Translator.ReadInt32("Criteria ID", i);

                packet.Translator.ReadXORByte(guid2[i], 5);

                packet.Translator.ReadXORByte(counter[i], 4);
                packet.Translator.ReadXORByte(counter[i], 7);
                packet.Translator.ReadXORByte(counter[i], 0);

                packet.Translator.ReadXORByte(guid2[i], 0);
                packet.Translator.ReadUInt32("Timer 1", i);
                packet.Translator.ReadXORByte(counter[i], 1);
                packet.Translator.ReadXORByte(counter[i], 3);
                packet.Translator.ReadXORByte(counter[i], 6);
                packet.Translator.ReadXORByte(counter[i], 5);

                packet.Translator.ReadXORByte(guid2[i], 4);
                packet.Translator.ReadXORByte(guid2[i], 6);
                packet.Translator.ReadXORByte(guid2[i], 1);
                packet.Translator.ReadXORByte(guid2[i], 7);

                packet.Translator.ReadUInt32("Timer 2", i);

                packet.Translator.WriteGuid("Guid2", guid2[i], i);

                packet.AddValue("Criteria Flags", flags[i], i);
                packet.AddValue("Criteria Counter", BitConverter.ToUInt64(counter[i], 0), i);
                packet.Translator.WriteGuid("Criteria GUID", guid2[i], i);
            }
        }

        [Parser(Opcode.SMSG_ALL_ACCOUNT_CRITERIA)]
        public static void HandleAllAchievementCriteriaDataAccount(Packet packet)
        {
            var count = packet.Translator.ReadBits("Criteria count", 19);

            var counter = new byte[count][];
            var accountId = new byte[count][];
            var flags = new byte[count];

            for (var i = 0; i < count; ++i)
            {
                counter[i] = new byte[8];
                accountId[i] = new byte[8];

                counter[i][2] = packet.Translator.ReadBit();
                accountId[i][0] = packet.Translator.ReadBit();
                counter[i][0] = packet.Translator.ReadBit();
                accountId[i][1] = packet.Translator.ReadBit();

                flags[i] = (byte)(packet.Translator.ReadBits(4) & 0xFFu);

                accountId[i][5] = packet.Translator.ReadBit();
                accountId[i][7] = packet.Translator.ReadBit();
                counter[i][7] = packet.Translator.ReadBit();
                counter[i][6] = packet.Translator.ReadBit();
                accountId[i][4] = packet.Translator.ReadBit();
                accountId[i][2] = packet.Translator.ReadBit();
                counter[i][4] = packet.Translator.ReadBit();
                accountId[i][3] = packet.Translator.ReadBit();
                accountId[i][6] = packet.Translator.ReadBit();
                counter[i][3] = packet.Translator.ReadBit();
                counter[i][5] = packet.Translator.ReadBit();
                counter[i][1] = packet.Translator.ReadBit();
            }

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadXORByte(accountId[i], 0);
                packet.Translator.ReadXORByte(counter[i], 4);
                packet.Translator.ReadXORByte(accountId[i], 2);
                packet.Translator.ReadXORByte(counter[i], 0);
                packet.Translator.ReadPackedTime("Time", i);
                packet.Translator.ReadXORByte(accountId[i], 6);
                packet.Translator.ReadInt32("Criteria ID", i);
                packet.Translator.ReadXORByte(accountId[i], 5);
                packet.Translator.ReadXORByte(counter[i], 1);
                packet.Translator.ReadXORByte(counter[i], 2);
                packet.Translator.ReadXORByte(counter[i], 3);
                packet.Translator.ReadXORByte(accountId[i], 4);
                packet.Translator.ReadXORByte(accountId[i], 1);
                packet.Translator.ReadXORByte(accountId[i], 7);
                packet.Translator.ReadXORByte(counter[i], 5);
                packet.Translator.ReadUInt32("Timer 1", i);
                packet.Translator.ReadXORByte(counter[i], 7);
                packet.Translator.ReadXORByte(accountId[i], 3);
                packet.Translator.ReadUInt32("Timer 2", i);
                packet.Translator.ReadXORByte(counter[i], 6);

                packet.AddValue("Criteria Flags", flags[i], i);
                packet.AddValue("Criteria Counter", BitConverter.ToUInt64(counter[i], 0), i);
                packet.AddValue("Account", i, BitConverter.ToUInt64(accountId[i], 0), i);
            }
        }

        [Parser(Opcode.CMSG_QUERY_INSPECT_ACHIEVEMENTS)]
        public static void HandleInspectAchievementData(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 2, 5, 0, 7, 3, 4, 6, 1);
            packet.Translator.ParseBitStream(guid, 2, 5, 4, 6, 1, 3, 0, 7);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_RESPOND_INSPECT_ACHIEVEMENTS)]
        public static void HandleInspectAchievementDataResponse(Packet packet)
        {
            var guid = new byte[8];

            var criterias = packet.Translator.ReadBits("Criterias", 19);
            var achievements = packet.Translator.ReadBits("Achievements", 20);

            var flags = new uint[criterias];
            var counter = new byte[criterias][];
            var guid2 = new byte[criterias][];

            for (var i = 0; i < criterias; ++i)
            {
                counter[i] = new byte[8];
                guid2[i] = new byte[8];

                counter[i][5] = packet.Translator.ReadBit();
                flags[i] = packet.Translator.ReadBits(4);
                guid2[i][4] = packet.Translator.ReadBit();
                counter[i][6] = packet.Translator.ReadBit();
                guid2[i][6] = packet.Translator.ReadBit();
                guid2[i][1] = packet.Translator.ReadBit();
                counter[i][0] = packet.Translator.ReadBit();
                guid2[i][5] = packet.Translator.ReadBit();
                counter[i][7] = packet.Translator.ReadBit();
                guid2[i][2] = packet.Translator.ReadBit();
                counter[i][1] = packet.Translator.ReadBit();
                guid2[i][7] = packet.Translator.ReadBit();
                counter[i][2] = packet.Translator.ReadBit();
                counter[i][3] = packet.Translator.ReadBit();
                guid2[i][0] = packet.Translator.ReadBit();
                counter[i][4] = packet.Translator.ReadBit();
                guid2[i][3] = packet.Translator.ReadBit();
            }

            guid[1] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();

            var guid5 = new byte[achievements][];

            for (var i = 0; i < achievements; ++i)
            {
                guid5[i] = new byte[8];

                guid5[i][4] = packet.Translator.ReadBit();
                guid5[i][3] = packet.Translator.ReadBit();
                guid5[i][0] = packet.Translator.ReadBit();
                guid5[i][6] = packet.Translator.ReadBit();
                guid5[i][2] = packet.Translator.ReadBit();
                guid5[i][5] = packet.Translator.ReadBit();
                guid5[i][1] = packet.Translator.ReadBit();
                guid5[i][7] = packet.Translator.ReadBit();
            }

            guid[3] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 2);

            for (var i = 0; i < achievements; ++i)
            {
                packet.Translator.ReadXORByte(guid5[i], 3);
                packet.Translator.ReadXORByte(guid5[i], 0);
                packet.Translator.ReadInt32("Realm Id", i);
                packet.Translator.ReadInt32("Realm Id", i);
                packet.Translator.ReadXORByte(guid5[i], 4);
                packet.Translator.ReadInt32<AchievementId>("Achievement Id", i);
                packet.Translator.ReadPackedTime("Achievement Time", i);
                packet.Translator.ReadXORByte(guid5[i], 7);
                packet.Translator.ReadXORByte(guid5[i], 5);
                packet.Translator.ReadXORByte(guid5[i], 1);
                packet.Translator.ReadXORByte(guid5[i], 6);
                packet.Translator.ReadXORByte(guid5[i], 2);

                packet.Translator.WriteGuid("Player GUID", guid2[i], i);
            }

            for (var i = 0; i < criterias; ++i)
            {
                packet.Translator.ReadUInt32("Criteria Timer 2", i);
                packet.Translator.ReadXORByte(guid2[i], 3);
                packet.Translator.ReadXORByte(counter[i], 5);
                packet.Translator.ReadXORByte(guid2[i], 6);
                packet.Translator.ReadXORByte(guid2[i], 4);
                packet.Translator.ReadXORByte(counter[i], 4);
                packet.Translator.ReadXORByte(counter[i], 7);
                packet.Translator.ReadUInt32("Criteria Id", i);
                packet.Translator.ReadXORByte(counter[i], 0);
                packet.Translator.ReadXORByte(counter[i], 1);
                packet.Translator.ReadXORByte(guid2[i], 0);
                packet.Translator.ReadXORByte(guid2[i], 1);
                packet.Translator.ReadUInt32("Criteria Timer 1", i);
                packet.Translator.ReadXORByte(guid2[i], 2);
                packet.Translator.ReadXORByte(guid2[i], 7);
                packet.Translator.ReadXORByte(guid2[i], 5);
                packet.Translator.ReadXORByte(counter[i], 2);
                packet.Translator.ReadPackedTime("Criteria Time", i);
                packet.Translator.ReadXORByte(counter[i], 6);
                packet.Translator.ReadXORByte(counter[i], 3);

                packet.AddValue("Criteria Flags", flags[i], i);
                packet.AddValue("Criteria Counter", BitConverter.ToUInt64(counter[i], 0), i);
                packet.Translator.WriteGuid("Criteria GUID", guid2[i], i);
            }

            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 7);

            packet.Translator.WriteGuid("GUID", guid);
        }
    }
}
