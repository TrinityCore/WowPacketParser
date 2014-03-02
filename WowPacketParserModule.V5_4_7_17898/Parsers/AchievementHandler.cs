using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class AchievementHandler
    {
        [Parser(Opcode.SMSG_CRITERIA_UPDATE_ACCOUNT)]
        public static void HandleCriteriaUpdateAccount(Packet packet)
        {
            var accountId = new byte[8];
            var counter = new byte[8];

            accountId[1] = packet.ReadBit();
            counter[5] = packet.ReadBit();
            accountId[0] = packet.ReadBit();
            counter[4] = packet.ReadBit();

            packet.ReadBits("Flags", 4); // some flag... & 1 -> delete

            counter[3] = packet.ReadBit();
            counter[7] = packet.ReadBit();
            counter[2] = packet.ReadBit();
            accountId[3] = packet.ReadBit();
            counter[1] = packet.ReadBit();
            counter[6] = packet.ReadBit();
            accountId[2] = packet.ReadBit();
            accountId[6] = packet.ReadBit();
            counter[0] = packet.ReadBit();
            accountId[4] = packet.ReadBit();
            accountId[5] = packet.ReadBit();
            accountId[7] = packet.ReadBit();

            packet.ReadXORByte(accountId, 0);
            packet.ReadXORByte(counter, 1);
            packet.ReadXORByte(counter, 3);
            packet.ReadUInt32("Timer 1");
            packet.ReadXORByte(accountId, 4);
            packet.ReadXORByte(counter, 0);
            packet.ReadXORByte(accountId, 7);
            packet.ReadXORByte(accountId, 6);
            packet.ReadXORByte(counter, 2);
            packet.ReadUInt32("Timer 2");
            packet.ReadXORByte(counter, 7);
            packet.ReadXORByte(accountId, 5);
            packet.ReadXORByte(accountId, 1);
            packet.ReadXORByte(counter, 5);
            packet.ReadXORByte(accountId, 3);
            packet.ReadInt32("Criteria ID");
            packet.ReadPackedTime("Time");
            packet.ReadXORByte(accountId, 2);
            packet.ReadXORByte(counter, 4);
            packet.ReadXORByte(counter, 6);

            packet.WriteLine("Account: {0}", BitConverter.ToUInt64(accountId, 0));
            packet.WriteLine("Counter: {0}", BitConverter.ToInt64(counter, 0));
        }

        [Parser(Opcode.SMSG_CRITERIA_UPDATE_PLAYER)]
        public static void HandleCriteriaPlayer(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadPackedTime("Time");

            packet.ReadInt32("Timer 1");
            packet.ReadInt32("Timer 2");
            packet.ReadInt64("Counter");
            packet.ReadInt32("Criteria ID");
            packet.ReadInt32("Flags");

            packet.StartBitStream(guid, 2, 4, 0, 6, 3, 7, 5, 1);
            packet.ParseBitStream(guid, 4, 2, 6, 1, 7, 3, 0, 5);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA_PLAYER)]
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

                counter[i][3] = packet.ReadBit();
                counter[i][6] = packet.ReadBit();
                guid2[i][0] = packet.ReadBit();
                guid2[i][7] = packet.ReadBit();
                counter[i][7] = packet.ReadBit();
                counter[i][0] = packet.ReadBit();
                counter[i][5] = packet.ReadBit();

                flags[i] = (byte)(packet.ReadBits(4) & 0xFFu);

                counter[i][4] = packet.ReadBit();
                guid2[i][3] = packet.ReadBit();
                guid2[i][6] = packet.ReadBit();
                guid2[i][4] = packet.ReadBit();
                guid2[i][5] = packet.ReadBit();
                guid2[i][1] = packet.ReadBit();
                counter[i][2] = packet.ReadBit();
                counter[i][1] = packet.ReadBit();
                guid2[i][2] = packet.ReadBit();
            }

            var bits10 = packet.ReadBits("Achievement count", 20);

            var guid1 = new byte[bits10][];
            for (var i = 0; i < bits10; ++i)
            {
                guid1[i] = new byte[8];
                packet.StartBitStream(guid1[i], 4, 7, 2, 3, 1, 5, 6, 0);
            }

            for (var i = 0; i < bits10; ++i)
            {
                packet.ReadXORByte(guid1[i], 6);
                packet.ReadXORByte(guid1[i], 5);
                packet.ReadXORByte(guid1[i], 3);
                packet.ReadXORByte(guid1[i], 0);
                packet.ReadXORByte(guid1[i], 2);
                packet.ReadXORByte(guid1[i], 1);
                packet.ReadUInt32("Achievement Id", i);
                packet.ReadInt32("Realm Id", i);
                packet.ReadInt32("Realm Id", i);
                packet.ReadXORByte(guid1[i], 4);
                packet.ReadPackedTime("Time", i);
                packet.ReadXORByte(guid1[i], 7);

                packet.WriteGuid("Guid3", guid1[i], i);
            }

            for (var i = 0; i < bits20; ++i)
            {
                packet.ReadPackedTime("Time", i);

                packet.ReadXORByte(counter[i], 2);

                packet.ReadXORByte(guid2[i], 2);
                packet.ReadXORByte(guid2[i], 3);

                packet.ReadInt32("Criteria ID", i);

                packet.ReadXORByte(guid2[i], 5);

                packet.ReadXORByte(counter[i], 4);
                packet.ReadXORByte(counter[i], 7);
                packet.ReadXORByte(counter[i], 0);

                packet.ReadXORByte(guid2[i], 0);
                packet.ReadUInt32("Timer 1", i);
                packet.ReadXORByte(counter[i], 1);
                packet.ReadXORByte(counter[i], 3);
                packet.ReadXORByte(counter[i], 6);
                packet.ReadXORByte(counter[i], 5);

                packet.ReadXORByte(guid2[i], 4);
                packet.ReadXORByte(guid2[i], 6);
                packet.ReadXORByte(guid2[i], 1);
                packet.ReadXORByte(guid2[i], 7);

                packet.ReadUInt32("Timer 2", i);

                packet.WriteGuid("Guid2", guid2[i], i);

                packet.WriteLine("[{0}] Criteria Flags: {1}", i, flags[i]);
                packet.WriteLine("[{0}] Criteria Counter: {1}", i, BitConverter.ToUInt64(counter[i], 0));
                packet.WriteGuid("Criteria GUID", guid2[i], i);
            }
        }
    }
}
