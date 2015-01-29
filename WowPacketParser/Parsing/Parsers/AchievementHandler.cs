using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class AchievementHandler
    {
        [Parser(Opcode.SMSG_ACHIEVEMENT_DELETED)]
        [Parser(Opcode.SMSG_CRITERIA_DELETED)]
        public static void HandleDeleted(Packet packet)
        {
            packet.ReadInt32("ID");
        }

        [Parser(Opcode.SMSG_SERVER_FIRST_ACHIEVEMENT)]
        public static void HandleServerFirstAchievement(Packet packet)
        {
            var name = packet.ReadCString("Player Name");
            var guid = packet.ReadGuid("Player GUID");
            StoreGetters.AddName(guid, name);
            packet.ReadInt32<AchievementId>("Achievement Id");
            packet.ReadInt32("Linked Name");
        }

        [Parser(Opcode.SMSG_ACHIEVEMENT_EARNED)]
        public static void HandleAchievementEarned(Packet packet)
        {
            packet.ReadPackedGuid("Player GUID");
            packet.ReadInt32<AchievementId>("Achievement Id");
            packet.ReadPackedTime("Time");
            packet.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.SMSG_CRITERIA_UPDATE)]
        public static void HandleCriteriaUpdate(Packet packet)
        {
            packet.ReadInt32("Criteria ID");
            packet.ReadPackedGuid("Criteria Counter");
            packet.ReadPackedGuid("Player GUID");
            packet.ReadInt32("Unk Int32"); // some flag... & 1 -> delete
            packet.ReadPackedTime("Time");

            for (var i = 0; i < 2; i++)
                packet.ReadInt32("Timer " + i);
        }

        public static void ReadAllAchievementData(Packet packet)
        {
            while (true)
            {
                var id = packet.ReadInt32<AchievementId>("Achievement Id");

                if (id == -1)
                    break;

                packet.ReadPackedTime("Achievement Time");
            }

            while (true)
            {
                var id = packet.ReadInt32("Criteria ID");

                if (id == -1)
                    break;

                packet.ReadPackedUInt64("Criteria Counter");
                packet.ReadPackedGuid("Player GUID");
                packet.ReadInt32("Unk Int32"); // Unk flag, same as in SMSG_CRITERIA_UPDATE
                packet.ReadPackedTime("Criteria Time");

                for (var i = 0; i < 2; i++)
                    packet.ReadInt32("Timer " + i);
            }
        }

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleAllAchievementData(Packet packet)
        {
            ReadAllAchievementData(packet);
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

        [Parser(Opcode.CMSG_QUERY_INSPECT_ACHIEVEMENTS)]
        public static void HandleInspectAchievementData(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
        }

        [Parser(Opcode.SMSG_RESPOND_INSPECT_ACHIEVEMENTS, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleInspectAchievementDataResponse(Packet packet)
        {
            packet.ReadPackedGuid("Player GUID");
            ReadAllAchievementData(packet);
        }

        [Parser(Opcode.SMSG_RESPOND_INSPECT_ACHIEVEMENTS, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleInspectAchievementDataResponse434(Packet packet)
        {
            var guid = new byte[8];
            guid[7] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var achievements = packet.ReadBits("Achievements", 23);
            guid[0] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            var criterias = packet.ReadBits("Criterias", 21);
            guid[2] = packet.ReadBit();

            var counter = new byte[criterias][];
            var guid2 = new byte[criterias][];

            for (var i = 0; i < criterias; ++i)
            {
                counter[i] = new byte[8];
                guid2[i] = new byte[8];

                guid2[i][5] = packet.ReadBit();
                guid2[i][3] = packet.ReadBit();
                counter[i][1] = packet.ReadBit();
                counter[i][4] = packet.ReadBit();
                counter[i][2] = packet.ReadBit();
                guid2[i][6] = packet.ReadBit();
                counter[i][0] = packet.ReadBit();
                guid2[i][4] = packet.ReadBit();
                guid2[i][1] = packet.ReadBit();
                guid2[i][2] = packet.ReadBit();
                counter[i][3] = packet.ReadBit();
                counter[i][7] = packet.ReadBit();
                packet.ReadBits("Criteria flags", 2, i);
                guid2[i][0] = packet.ReadBit();
                counter[i][5] = packet.ReadBit();
                counter[i][6] = packet.ReadBit();
                guid2[i][7] = packet.ReadBit();
            }

            guid[6] = packet.ReadBit();
            guid[5] = packet.ReadBit();

            for (var i = 0; i < criterias; ++i)
            {
                packet.ReadXORByte(counter[i], 3);
                packet.ReadXORByte(guid2[i], 4);

                packet.ReadUInt32("Criteria Timer 2", i);

                packet.ReadXORByte(counter[i], 1);

                packet.ReadPackedTime("Criteria Time", i);

                packet.ReadXORByte(guid2[i], 3);
                packet.ReadXORByte(guid2[i], 7);
                packet.ReadXORByte(counter[i], 5);
                packet.ReadXORByte(guid2[i], 0);
                packet.ReadXORByte(counter[i], 4);
                packet.ReadXORByte(counter[i], 2);
                packet.ReadXORByte(counter[i], 6);
                packet.ReadXORByte(counter[i], 7);
                packet.ReadXORByte(guid2[i], 6);
                packet.ReadUInt32("Criteria Id", i);
                packet.ReadUInt32("Criteria Timer 1", i);
                packet.ReadXORByte(guid2[i], 1);
                packet.ReadXORByte(guid2[i], 5);
                packet.ReadXORByte(counter[i], 0);
                packet.ReadXORByte(guid2[i], 2);

                packet.AddValue("Criteria Counter", BitConverter.ToUInt64(counter[i], 0), i);
                packet.WriteGuid("GUID2", guid2[i], i);
            }

            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);

            for (var i = 0; i < achievements; ++i)
            {
                packet.ReadInt32<AchievementId>("Achievement Id", i);
                packet.ReadPackedTime("Achievement Time", i);
            }

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_COMPRESSED_ACHIEVEMENT_DATA)]
        public static void HandleCompressedAllAchievementData(Packet packet)
        {
            using (var packet2 = packet.Inflate(packet.ReadInt32()))
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_4_15595))
                    HandleAllAchievementData434(packet2);
                else if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                    HandleAllAchievementData422(packet2);
                else if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                    HandleAllAchievementData406(packet2);
                else
                    HandleAllAchievementData(packet2);
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

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA, ClientVersionBuild.V5_1_0_16309)]
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
    }
}
