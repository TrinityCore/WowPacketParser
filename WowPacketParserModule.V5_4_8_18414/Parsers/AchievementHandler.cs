using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class AchievementHandler
    {
        [Parser(Opcode.CMSG_QUERY_INSPECT_ACHIEVEMENTS)]
        public static void HandleQueryInspectAchievemens(Packet packet)
        {
            var guid = packet.StartBitStream(2, 7, 1, 5, 4, 0, 3, 6);
            packet.ParseBitStream(guid, 7, 2, 0, 4, 1, 5, 6, 3);
            packet.WriteGuid("Guid", guid);
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
            packet.ReadInt32("Achievement");
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

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA_ACCOUNT)]
        public static void HandleAllAchievementDataAccount(Packet packet)
        {
            var count = packet.ReadBits("count", 19);
            var guid = new byte[count][];
            var guid2 = new byte[count][];
            var cnt = new uint[count];
            for (var i = 0; i < count; i++)
            {
                guid[i] = new byte[8];
                guid2[i] = new byte[8];

                guid[i][0] = packet.ReadBit();
                guid2[i][7] = packet.ReadBit();

                cnt[i] = packet.ReadBits(4);

                guid2[i][4] = packet.ReadBit();
                guid[i][6] = packet.ReadBit();
                guid2[i][5] = packet.ReadBit();
                guid2[i][2] = packet.ReadBit();
                guid2[i][0] = packet.ReadBit();
                guid2[i][6] = packet.ReadBit();
                guid[i][5] = packet.ReadBit();
                guid[i][7] = packet.ReadBit();
                guid2[i][1] = packet.ReadBit();
                guid2[i][3] = packet.ReadBit();
                guid[i][4] = packet.ReadBit();
                guid[i][3] = packet.ReadBit();
                guid[i][2] = packet.ReadBit();
                guid[i][1] = packet.ReadBit();
            }
            for (var i = 0; i < count; i++)
            {
                packet.ParseBitStream(guid[i], 2);
                packet.ParseBitStream(guid2[i], 2, 4);
                packet.ParseBitStream(guid[i], 4);
                packet.ParseBitStream(guid2[i], 6, 0);
                packet.ReadInt32("unk276", i); // 276
                packet.ParseBitStream(guid2[i], 1, 7);
                packet.ReadPackedTime("Time", i);
                packet.ParseBitStream(guid[i], 7, 6, 5, 1);
                packet.ParseBitStream(guid2[i], 3);
                packet.ReadInt32("unk260", i); // 260
                packet.ParseBitStream(guid2[i], 5);
                packet.ParseBitStream(guid[i], 3, 0);
                packet.ReadInt32("Criteria ID", i); // 20
                packet.WriteGuid("Guid", guid[i], i);
                packet.WriteGuid("Guid2", guid2[i], i);
            }
        }

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA_PLAYER)]
        public static void HandleAllAchievementDataPlayer(Packet packet)
        {
            var count = packet.ReadBits("Criteria count", 19);
            var guid = new byte[count][];
            var counter = new byte[count][];
            var flags = new uint[count];
            for (var i = 0; i < count; i++)
            {
                guid[i] = new byte[8];
                counter[i] = new byte[8];

                guid[i][3] = packet.ReadBit();
                counter[i][3] = packet.ReadBit();
                counter[i][6] = packet.ReadBit();
                guid[i][0] = packet.ReadBit();
                counter[i][7] = packet.ReadBit();
                guid[i][1] = packet.ReadBit();
                guid[i][5] = packet.ReadBit();
                counter[i][2] = packet.ReadBit();
                counter[i][1] = packet.ReadBit();
                guid[i][7] = packet.ReadBit();
                counter[i][4] = packet.ReadBit();
                counter[i][0] = packet.ReadBit();
                guid[i][2] = packet.ReadBit();
                counter[i][5] = packet.ReadBit();
                guid[i][4] = packet.ReadBit();
                flags[i] = packet.ReadBits(4);
                guid[i][6] = packet.ReadBit();
            }
            var cnt16 = packet.ReadBits("Achievement count", 20); // 16
            var guid3 = new byte[cnt16][];
            for (var i = 0; i < cnt16; i++)
            {
                guid3[i] = new byte[8];
                guid3[i] = packet.StartBitStream(0, 7, 1, 5, 2, 4, 6, 3);
            }
            for (var i = 0; i < cnt16; i++)
            {
                packet.ReadInt32("Achievement Id", i); // 20
                packet.ReadInt32("Realm Id", i); // 208
                packet.ParseBitStream(guid3[i], 5, 7);
                packet.ReadInt32("unk212", i); // 212
                packet.ReadPackedTime("Time", i);
                packet.ParseBitStream(guid3[i], 0, 4, 1, 6, 2, 3);
                packet.WriteGuid("guid3", guid3[i], i);
            }
            for (var i = 0; i < count; i++)
            {
                packet.ParseBitStream(guid[i], 7);
                packet.ReadInt32("Timer 1", i); // 292
                packet.ParseBitStream(guid[i], 6);
                packet.ParseBitStream(counter[i], 1);
                packet.ReadInt32("Criteria ID", i); // 36
                packet.ParseBitStream(guid[i], 4);
                packet.ParseBitStream(counter[i], 0, 4, 6);
                packet.ParseBitStream(guid[i], 1, 5);
                packet.ParseBitStream(counter[i], 7, 2);
                packet.ParseBitStream(guid[i], 2, 0);
                packet.ParseBitStream(counter[i], 3);
                packet.ReadInt32("Timer 2", i); // 236
                packet.ParseBitStream(guid[i], 3);
                packet.ParseBitStream(counter[i], 5);
                packet.ReadPackedTime("Time", i);

                packet.WriteGuid("Guid", guid[i], i);
                packet.WriteLine("[{0}] Counter: {1}", i, BitConverter.ToUInt64(counter[i], 0));
                packet.WriteLine("[{0}] Flags: {1}", i, flags[i]);
            }
        }

        [Parser(Opcode.SMSG_CRITERIA_UPDATE_ACCOUNT)]
        public static void HandleCriteriaUpdateAccount(Packet packet)
        {
            var counter = new byte[8];
            var accountId = new byte[8];

            counter[4] = packet.ReadBit();
            accountId[2] = packet.ReadBit();
            counter[2] = packet.ReadBit();
            accountId[4] = packet.ReadBit();
            counter[0] = packet.ReadBit();
            counter[5] = packet.ReadBit();
            accountId[3] = packet.ReadBit();
            counter[3] = packet.ReadBit();
            accountId[6] = packet.ReadBit();
            counter[6] = packet.ReadBit();
            accountId[1] = packet.ReadBit();
            accountId[7] = packet.ReadBit();
            counter[1] = packet.ReadBit();

            packet.ReadBits("Flags", 4); // some flag... & 1 -> delete

            accountId[5] = packet.ReadBit();
            counter[7] = packet.ReadBit();
            accountId[0] = packet.ReadBit();

            packet.ReadXORByte(accountId, 7);
            packet.ReadUInt32("Timer 2"); // 80
            packet.ReadInt32("Criteria ID"); // 16
            packet.ReadXORByte(counter, 7);
            packet.ReadUInt32("Timer 1"); // 76
            packet.ReadXORByte(accountId, 4);
            packet.ReadXORByte(accountId, 3);
            packet.ReadPackedTime("Time");
            packet.ReadXORByte(counter, 0);
            packet.ReadXORByte(counter, 1);
            packet.ReadXORByte(counter, 2);
            packet.ReadXORByte(counter, 3);
            packet.ReadXORByte(accountId, 1);
            packet.ReadXORByte(counter, 4);
            packet.ReadXORByte(counter, 5);
            packet.ReadXORByte(accountId, 5);
            packet.ReadXORByte(accountId, 2);
            packet.ReadXORByte(counter, 6);
            packet.ReadXORByte(accountId, 0);
            packet.ReadXORByte(accountId, 6);

            packet.WriteLine("Account: {0}", BitConverter.ToUInt64(accountId, 0));
            packet.WriteLine("Counter: {0}", BitConverter.ToInt64(counter, 0));
        }

        [Parser(Opcode.SMSG_CRITERIA_UPDATE_PLAYER)]
        public static void HandleCriteriaPlayer(Packet packet)
        {
            var guid = packet.StartBitStream(4, 6, 2, 3, 7, 1, 5, 0);
            packet.ParseBitStream(guid, 3, 6, 2);
            packet.ReadUInt32("Criteria ID"); // 72
            packet.ReadUInt32("Flags"); // 76
            packet.ParseBitStream(guid, 5, 1);
            packet.ReadPackedTime("Time"); // 28
            packet.ParseBitStream(guid, 4);
            packet.ReadUInt32("Timer 1"); // 24
            packet.ReadUInt32("Timer 2"); // 80
            packet.ParseBitStream(guid, 7, 0);
            packet.WriteGuid("Guid", guid);
            packet.ReadUInt64("Counter"); // 16
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_RESPOND_INSPECT_ACHIEVEMENTS)]
        public static void HandleRespondInspectAchievemens(Packet packet)
        {
            var guid = new byte[8];
            guid[3] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var achievements = packet.ReadBits("Achievements", 20);
            var criterias = packet.ReadBits("Criterias", 19);
            var guid8 = new byte[criterias][];
            var guid16 = new byte[criterias][];
            var flags = new uint[criterias];
            for (var i = 0; i < criterias; i++)
            {
                guid8[i] = new byte[8];
                guid16[i] = new byte[8];
                guid16[i][1] = packet.ReadBit();
                guid16[i][4] = packet.ReadBit();
                guid16[i][5] = packet.ReadBit();
                guid8[i][7] = packet.ReadBit();
                guid8[i][4] = packet.ReadBit();
                guid8[i][3] = packet.ReadBit();
                guid16[i][7] = packet.ReadBit();
                guid16[i][0] = packet.ReadBit();
                guid16[i][6] = packet.ReadBit();
                flags[i] = packet.ReadBits(4);
                guid16[i][2] = packet.ReadBit();
                guid8[i][5] = packet.ReadBit();
                guid8[i][6] = packet.ReadBit();
                guid8[i][0] = packet.ReadBit();
                guid8[i][2] = packet.ReadBit();
                guid8[i][1] = packet.ReadBit();
                guid16[i][3] = packet.ReadBit();
            }
            guid[5] = packet.ReadBit();
            var guid60 = new byte[achievements][];
            for (var i = 0; i < achievements; i++)
                guid60[i] = packet.StartBitStream(0, 2, 5, 4, 3, 6, 1, 7);

            guid[4] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[1] = packet.ReadBit();

            packet.ParseBitStream(guid, 5);
            for (var i = 0; i < achievements; i++)
            {
                packet.ParseBitStream(guid60[i], 1, 0);
                packet.ReadPackedTime("Time", i);
                packet.ReadInt32("RealmID", i); // 68
                packet.ReadInt32("Achievement ID", i); // 20
                packet.ParseBitStream(guid60[i], 7, 4, 6, 2, 3, 5);
                packet.ReadInt32("RealmID", i); // 72
                packet.WriteGuid("Player Guid", guid60[i], i);
            }

            for (var i = 0; i < criterias; i++)
            {
                packet.ParseBitStream(guid8[i], 4);
                packet.ReadInt32("Criteria Timer1", i); // 96
                packet.ParseBitStream(guid8[i], 1);
                packet.ParseBitStream(guid16[i], 1);
                packet.ParseBitStream(guid8[i], 7);
                packet.ReadInt32("Criteria ID", i); // 36
                packet.ParseBitStream(guid16[i], 3);
                packet.ParseBitStream(guid8[i], 3, 5, 2);
                packet.ParseBitStream(guid16[i], 4);
                packet.ParseBitStream(guid8[i], 0);
                packet.ParseBitStream(guid16[i], 0);
                packet.ReadInt32("Criteria Timer2", i); // 100
                packet.ParseBitStream(guid16[i], 7);
                packet.ReadPackedTime("Criteria Time", i);
                packet.ParseBitStream(guid8[i], 6);
                packet.ParseBitStream(guid16[i], 2, 6, 5);

                packet.WriteLine("[{0}] Criteria Flags: {1}", i, flags[i]);
                packet.WriteLine("[{0}] Criteria Counter: {1}", i, BitConverter.ToUInt64(guid8[i], 0));
                packet.WriteGuid("Criteria GUID", guid16[i], i);
            }
            packet.ParseBitStream(guid, 0, 3, 6, 2, 7, 4, 1);
            packet.WriteGuid("Target", guid);
        }
    }
}
