using System;
using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAllAchievementData
    {
        public AllAchievements Data;

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleAllAchievementData(Packet packet)
        {
            AllAchievements.Read3(packet);
        }

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA, ClientVersionBuild.V4_0_6a_13623, ClientVersionBuild.V4_1_0_13914)]
        public static void HandleAllAchievementData406(Packet packet)
        {
            var achievements = packet.ReadUInt32("Achievement count");
            var criterias = packet.ReadUInt32("Criterias count");

            for (var i = 0; i < achievements; ++i)
                packet.ReadInt32<AchievementId>("Achievement Id", i);

            for (var i = 0; i < achievements; ++i)
                packet.ReadPackedTime("Achievement Time", i);

            for (var i = 0; i < criterias; ++i)
                packet.ReadUInt64("Counter", i);

            for (var i = 0; i < criterias; ++i)
                packet.ReadUInt32("Criteria Timer 1", i);

            for (var i = 0; i < criterias; ++i)
                packet.ReadPackedTime("Criteria Time", i);

            for (var i = 0; i < criterias; ++i)
                packet.ReadGuid("Player GUID", i);

            for (var i = 0; i < criterias; ++i)
                packet.ReadUInt32("Criteria Timer 2", i);

            for (var i = 0; i < criterias; ++i)
                packet.ReadBits("Flag", 2, i);

            for (var i = 0; i < criterias; ++i)
                packet.ReadUInt32("Criteria Id", i);
        }

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleAllAchievementData422(Packet packet)
        {
            var criterias = packet.ReadUInt32("Criterias Count");
            for (var i = 0; i < criterias; ++i)
                packet.ReadBits("Flag", 2, 0, i);

            for (var i = 0; i < criterias; ++i)
                packet.ReadUInt64("Counter", 0, i);

            var achievements = packet.ReadUInt32("Achievement Count");
            for (var i = 0; i < achievements; ++i)
                packet.ReadPackedTime("Achievement Time", 1, i);

            for (var i = 0; i < criterias; ++i)
                packet.ReadGuid("Player GUID", 0, i);

            for (var i = 0; i < criterias; ++i)
                packet.ReadPackedTime("Criteria Time", 0, i);

            for (var i = 0; i < criterias; ++i)
                packet.ReadUInt32("Timer 1", 0, i);

            for (var i = 0; i < achievements; ++i)
                packet.ReadInt32<AchievementId>("Achievement Id", 1, i);

            for (var i = 0; i < criterias; ++i)
                packet.ReadUInt32("Criteria Id", 0, i);

            for (var i = 0; i < criterias; ++i)
                packet.ReadUInt32("Timer 2", 0, i);
        }

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleAllAchievementData434(Packet packet)
        {
            var criterias = packet.ReadBits("Criteria count", 21);
            var counter = new byte[criterias][];
            var guid = new byte[criterias][];
            var flags = new byte[criterias];
            for (var i = 0; i < criterias; ++i)
            {
                counter[i] = new byte[8];
                guid[i] = new byte[8];

                guid[i][4] = packet.ReadBit();
                counter[i][3] = packet.ReadBit();
                guid[i][5] = packet.ReadBit();
                counter[i][0] = packet.ReadBit();
                counter[i][6] = packet.ReadBit();
                guid[i][3] = packet.ReadBit();
                guid[i][0] = packet.ReadBit();
                counter[i][4] = packet.ReadBit();
                guid[i][2] = packet.ReadBit();
                counter[i][7] = packet.ReadBit();
                guid[i][7] = packet.ReadBit();
                flags[i] = (byte)(packet.ReadBits(2) & 0xFFu);
                guid[i][6] = packet.ReadBit();
                counter[i][2] = packet.ReadBit();
                counter[i][1] = packet.ReadBit();
                counter[i][5] = packet.ReadBit();
                guid[i][1] = packet.ReadBit();
            }

            var achievements = packet.ReadBits("Achievement count", 23);
            for (var i = 0; i < criterias; ++i)
            {
                packet.ReadXORByte(guid[i], 3);
                packet.ReadXORByte(counter[i], 5);
                packet.ReadXORByte(counter[i], 6);
                packet.ReadXORByte(guid[i], 4);
                packet.ReadXORByte(guid[i], 6);
                packet.ReadXORByte(counter[i], 2);

                packet.ReadUInt32("Criteria Timer 2", i);

                packet.ReadXORByte(guid[i], 2);

                packet.ReadUInt32("Criteria Id", i);

                packet.ReadXORByte(guid[i], 5);
                packet.ReadXORByte(counter[i], 0);
                packet.ReadXORByte(counter[i], 3);
                packet.ReadXORByte(counter[i], 1);
                packet.ReadXORByte(counter[i], 4);
                packet.ReadXORByte(guid[i], 0);
                packet.ReadXORByte(guid[i], 7);
                packet.ReadXORByte(counter[i], 7);

                packet.ReadUInt32("Criteria Timer 1", i);
                packet.ReadPackedTime("Criteria Date", i);

                packet.ReadXORByte(guid[i], 1);

                packet.AddValue("Criteria Flags", flags[i], i);
                packet.AddValue("Criteria Counter", BitConverter.ToUInt64(counter[i], 0), i);
                packet.WriteGuid("Criteria GUID", guid[i], i);
            }

            for (var i = 0; i < achievements; ++i)
            {
                packet.ReadInt32<AchievementId>("Achievement Id", i);
                packet.ReadPackedTime("Achievement Date", i);
            }
        }

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA, ClientVersionBuild.V5_1_0_16309, ClientVersionBuild.V5_4_0_17359)]
        public static void HandleAllAchievementData510(Packet packet)
        {
            var criterias = packet.ReadBits("Criteria count", 21);
            var counter = new byte[criterias][];
            var guid = new byte[criterias][];
            var flags = new byte[criterias];

            for (var i = 0; i < criterias; ++i)
            {
                counter[i] = new byte[8];
                guid[i] = new byte[8];

                guid[i][5] = packet.ReadBit();
                counter[i][2] = packet.ReadBit();
                guid[i][3] = packet.ReadBit();
                counter[i][1] = packet.ReadBit();
                guid[i][2] = packet.ReadBit();
                guid[i][7] = packet.ReadBit();
                guid[i][4] = packet.ReadBit();
                counter[i][7] = packet.ReadBit();
                guid[i][6] = packet.ReadBit();
                flags[i] = (byte)(packet.ReadBits(4) & 0xFFu);
                counter[i][5] = packet.ReadBit();
                guid[i][1] = packet.ReadBit();
                counter[i][6] = packet.ReadBit();
                counter[i][3] = packet.ReadBit();
                counter[i][4] = packet.ReadBit();
                guid[i][0] = packet.ReadBit();
                counter[i][0] = packet.ReadBit();
            }

            var achievements = packet.ReadBits("Achievement count", 22);
            var achievementGuid = new byte[achievements][];
            for (var i = 0; i < achievements; ++i)
            {
                achievementGuid[i] = new byte[8];

                achievementGuid[i][6] = packet.ReadBit();
                achievementGuid[i][3] = packet.ReadBit();
                achievementGuid[i][7] = packet.ReadBit();
                achievementGuid[i][5] = packet.ReadBit();
                achievementGuid[i][2] = packet.ReadBit();
                achievementGuid[i][1] = packet.ReadBit();
                achievementGuid[i][4] = packet.ReadBit();
                achievementGuid[i][0] = packet.ReadBit();
            }

            for (var i = 0; i < achievements; ++i)
            {
                packet.ReadUInt32("Realm Id", i);
                packet.ReadXORByte(achievementGuid[i], 7);
                packet.ReadXORByte(achievementGuid[i], 1);
                packet.ReadXORByte(achievementGuid[i], 0);
                packet.ReadXORByte(achievementGuid[i], 4);
                packet.ReadXORByte(achievementGuid[i], 6);
                packet.ReadXORByte(achievementGuid[i], 3);
                packet.ReadXORByte(achievementGuid[i], 2);
                packet.ReadPackedTime("Achievement Date", i);
                packet.ReadInt32<AchievementId>("Achievement Id", i);
                packet.ReadXORByte(achievementGuid[i], 5);
                packet.WriteGuid("Achievement Completer GUID", achievementGuid[i], i);
            }

            for (var i = 0; i < criterias; ++i)
            {
                packet.ReadXORByte(guid[i], 6);
                packet.ReadUInt32("Criteria Timer 1", i);
                packet.ReadXORByte(guid[i], 7);
                packet.ReadXORByte(guid[i], 4);
                packet.ReadXORByte(guid[i], 3);
                packet.ReadXORByte(counter[i], 4);
                packet.ReadXORByte(counter[i], 0);
                packet.ReadXORByte(guid[i], 5);
                packet.ReadXORByte(counter[i], 5);
                packet.ReadXORByte(counter[i], 7);
                packet.ReadXORByte(counter[i], 6);
                packet.ReadXORByte(guid[i], 1);
                packet.ReadPackedTime("Criteria Date", i);
                packet.ReadUInt32("Criteria Id", i);
                packet.ReadXORByte(guid[i], 0);
                packet.ReadUInt32("Criteria Timer 2", i);
                packet.ReadXORByte(counter[i], 3);
                packet.ReadXORByte(counter[i], 1);
                packet.ReadXORByte(counter[i], 2);
                packet.ReadXORByte(guid[i], 2);

                packet.AddValue("Criteria Flags", flags[i], i);
                packet.AddValue("Criteria Counter", BitConverter.ToUInt64(counter[i], 0), i);
                packet.WriteGuid("Criteria Completer GUID", guid[i], i);
            }
        }

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA, ClientVersionBuild.V5_4_0_17359, ClientVersionBuild.V5_4_2_17658)]
        public static void HandleAllAchievementData540(Packet packet)
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

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA, ClientVersionBuild.V5_4_2_17658, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleAllAchievementDataPlayer542(Packet packet)
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

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_8_18291)]
        public static void HandleAllAchievementDataPlayer547(Packet packet)
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
                packet.ReadInt32<AchievementId>("Achievement Id", i);
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

                packet.AddValue("Criteria Flags", flags[i], i);
                packet.AddValue("Criteria Counter", BitConverter.ToUInt64(counter[i], 0), i);
                packet.WriteGuid("Criteria GUID", guid2[i], i);
            }
        }

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA, ClientVersionBuild.V5_4_8_18291, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleAllAchievementDataPlayer548(Packet packet)
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

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleAllAchievementDataPlayer(Packet packet)
        {
            AllAchievements.Read6(packet, "Data");
        }
    }
}
