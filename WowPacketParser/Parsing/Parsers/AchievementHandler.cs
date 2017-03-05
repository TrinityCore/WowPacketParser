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
            packet.Translator.ReadInt32("ID");
        }

        [Parser(Opcode.SMSG_SERVER_FIRST_ACHIEVEMENT)]
        public static void HandleServerFirstAchievement(Packet packet)
        {
            var name = packet.Translator.ReadCString("Player Name");
            var guid = packet.Translator.ReadGuid("Player GUID");
            StoreGetters.AddName(guid, name);
            packet.Translator.ReadInt32<AchievementId>("Achievement Id");
            packet.Translator.ReadInt32("Linked Name");
        }

        [Parser(Opcode.SMSG_ACHIEVEMENT_EARNED)]
        public static void HandleAchievementEarned(Packet packet)
        {
            packet.Translator.ReadPackedGuid("Player GUID");
            packet.Translator.ReadInt32<AchievementId>("Achievement Id");
            packet.Translator.ReadPackedTime("Time");
            packet.Translator.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.SMSG_CRITERIA_UPDATE)]
        public static void HandleCriteriaUpdate(Packet packet)
        {
            packet.Translator.ReadInt32("Criteria ID");
            packet.Translator.ReadPackedGuid("Criteria Counter");
            packet.Translator.ReadPackedGuid("Player GUID");
            packet.Translator.ReadInt32("Unk Int32"); // some flag... & 1 -> delete
            packet.Translator.ReadPackedTime("Time");

            for (var i = 0; i < 2; i++)
                packet.Translator.ReadInt32("Timer " + i);
        }

        public static void ReadAllAchievementData(Packet packet)
        {
            while (true)
            {
                var id = packet.Translator.ReadInt32<AchievementId>("Achievement Id");

                if (id == -1)
                    break;

                packet.Translator.ReadPackedTime("Achievement Time");
            }

            while (true)
            {
                var id = packet.Translator.ReadInt32("Criteria ID");

                if (id == -1)
                    break;

                packet.Translator.ReadPackedUInt64("Criteria Counter");
                packet.Translator.ReadPackedGuid("Player GUID");
                packet.Translator.ReadInt32("Unk Int32"); // Unk flag, same as in SMSG_CRITERIA_UPDATE
                packet.Translator.ReadPackedTime("Criteria Time");

                for (var i = 0; i < 2; i++)
                    packet.Translator.ReadInt32("Timer " + i);
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
            var achievements = packet.Translator.ReadUInt32("Achievement count");
            var criterias = packet.Translator.ReadUInt32("Criterias count");

            for (var i = 0; i < achievements; ++i)
                packet.Translator.ReadInt32<AchievementId>("Achievement Id", i);

            for (var i = 0; i < achievements; ++i)
                packet.Translator.ReadPackedTime("Achievement Time", i);

            for (var i = 0; i < criterias; ++i)
                packet.Translator.ReadUInt64("Counter", i);

            for (var i = 0; i < criterias; ++i)
                packet.Translator.ReadUInt32("Criteria Timer 1", i);

            for (var i = 0; i < criterias; ++i)
                packet.Translator.ReadPackedTime("Criteria Time", i);

            for (var i = 0; i < criterias; ++i)
                packet.Translator.ReadGuid("Player GUID", i);

            for (var i = 0; i < criterias; ++i)
                packet.Translator.ReadUInt32("Criteria Timer 2", i);

            for (var i = 0; i < criterias; ++i)
                packet.Translator.ReadBits("Flag", 2, i);

            for (var i = 0; i < criterias; ++i)
                packet.Translator.ReadUInt32("Criteria Id", i);
        }

        [Parser(Opcode.CMSG_QUERY_INSPECT_ACHIEVEMENTS)]
        public static void HandleInspectAchievementData(Packet packet)
        {
            packet.Translator.ReadPackedGuid("GUID");
        }

        [Parser(Opcode.SMSG_RESPOND_INSPECT_ACHIEVEMENTS, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleInspectAchievementDataResponse(Packet packet)
        {
            packet.Translator.ReadPackedGuid("Player GUID");
            ReadAllAchievementData(packet);
        }

        [Parser(Opcode.SMSG_RESPOND_INSPECT_ACHIEVEMENTS, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleInspectAchievementDataResponse434(Packet packet)
        {
            var guid = new byte[8];
            guid[7] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            var achievements = packet.Translator.ReadBits("Achievements", 23);
            guid[0] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            var criterias = packet.Translator.ReadBits("Criterias", 21);
            guid[2] = packet.Translator.ReadBit();

            var counter = new byte[criterias][];
            var guid2 = new byte[criterias][];

            for (var i = 0; i < criterias; ++i)
            {
                counter[i] = new byte[8];
                guid2[i] = new byte[8];

                guid2[i][5] = packet.Translator.ReadBit();
                guid2[i][3] = packet.Translator.ReadBit();
                counter[i][1] = packet.Translator.ReadBit();
                counter[i][4] = packet.Translator.ReadBit();
                counter[i][2] = packet.Translator.ReadBit();
                guid2[i][6] = packet.Translator.ReadBit();
                counter[i][0] = packet.Translator.ReadBit();
                guid2[i][4] = packet.Translator.ReadBit();
                guid2[i][1] = packet.Translator.ReadBit();
                guid2[i][2] = packet.Translator.ReadBit();
                counter[i][3] = packet.Translator.ReadBit();
                counter[i][7] = packet.Translator.ReadBit();
                packet.Translator.ReadBits("Criteria flags", 2, i);
                guid2[i][0] = packet.Translator.ReadBit();
                counter[i][5] = packet.Translator.ReadBit();
                counter[i][6] = packet.Translator.ReadBit();
                guid2[i][7] = packet.Translator.ReadBit();
            }

            guid[6] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();

            for (var i = 0; i < criterias; ++i)
            {
                packet.Translator.ReadXORByte(counter[i], 3);
                packet.Translator.ReadXORByte(guid2[i], 4);

                packet.Translator.ReadUInt32("Criteria Timer 2", i);

                packet.Translator.ReadXORByte(counter[i], 1);

                packet.Translator.ReadPackedTime("Criteria Time", i);

                packet.Translator.ReadXORByte(guid2[i], 3);
                packet.Translator.ReadXORByte(guid2[i], 7);
                packet.Translator.ReadXORByte(counter[i], 5);
                packet.Translator.ReadXORByte(guid2[i], 0);
                packet.Translator.ReadXORByte(counter[i], 4);
                packet.Translator.ReadXORByte(counter[i], 2);
                packet.Translator.ReadXORByte(counter[i], 6);
                packet.Translator.ReadXORByte(counter[i], 7);
                packet.Translator.ReadXORByte(guid2[i], 6);
                packet.Translator.ReadUInt32("Criteria Id", i);
                packet.Translator.ReadUInt32("Criteria Timer 1", i);
                packet.Translator.ReadXORByte(guid2[i], 1);
                packet.Translator.ReadXORByte(guid2[i], 5);
                packet.Translator.ReadXORByte(counter[i], 0);
                packet.Translator.ReadXORByte(guid2[i], 2);

                packet.AddValue("Criteria Counter", BitConverter.ToUInt64(counter[i], 0), i);
                packet.Translator.WriteGuid("GUID2", guid2[i], i);
            }

            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 2);

            for (var i = 0; i < achievements; ++i)
            {
                packet.Translator.ReadInt32<AchievementId>("Achievement Id", i);
                packet.Translator.ReadPackedTime("Achievement Time", i);
            }

            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_COMPRESSED_ACHIEVEMENT_DATA)]
        public static void HandleCompressedAllAchievementData(Packet packet)
        {
            using (var packet2 = packet.Inflate(packet.Translator.ReadInt32()))
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
            var criterias = packet.Translator.ReadUInt32("Criterias Count");
            for (var i = 0; i < criterias; ++i)
                packet.Translator.ReadBits("Flag", 2, 0, i);

            for (var i = 0; i < criterias; ++i)
                packet.Translator.ReadUInt64("Counter", 0, i);

            var achievements = packet.Translator.ReadUInt32("Achievement Count");
            for (var i = 0; i < achievements; ++i)
                packet.Translator.ReadPackedTime("Achievement Time", 1, i);

            for (var i = 0; i < criterias; ++i)
                packet.Translator.ReadGuid("Player GUID", 0, i);

            for (var i = 0; i < criterias; ++i)
                packet.Translator.ReadPackedTime("Criteria Time", 0, i);

            for (var i = 0; i < criterias; ++i)
                packet.Translator.ReadUInt32("Timer 1", 0, i);

            for (var i = 0; i < achievements; ++i)
                packet.Translator.ReadInt32<AchievementId>("Achievement Id", 1, i);

            for (var i = 0; i < criterias; ++i)
                packet.Translator.ReadUInt32("Criteria Id", 0, i);

            for (var i = 0; i < criterias; ++i)
                packet.Translator.ReadUInt32("Timer 2", 0, i);
        }

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleAllAchievementData434(Packet packet)
        {
            var criterias = packet.Translator.ReadBits("Criteria count", 21);
            var counter = new byte[criterias][];
            var guid = new byte[criterias][];
            var flags = new byte[criterias];
            for (var i = 0; i < criterias; ++i)
            {
                counter[i] = new byte[8];
                guid[i] = new byte[8];

                guid[i][4] = packet.Translator.ReadBit();
                counter[i][3] = packet.Translator.ReadBit();
                guid[i][5] = packet.Translator.ReadBit();
                counter[i][0] = packet.Translator.ReadBit();
                counter[i][6] = packet.Translator.ReadBit();
                guid[i][3] = packet.Translator.ReadBit();
                guid[i][0] = packet.Translator.ReadBit();
                counter[i][4] = packet.Translator.ReadBit();
                guid[i][2] = packet.Translator.ReadBit();
                counter[i][7] = packet.Translator.ReadBit();
                guid[i][7] = packet.Translator.ReadBit();
                flags[i] = (byte)(packet.Translator.ReadBits(2) & 0xFFu);
                guid[i][6] = packet.Translator.ReadBit();
                counter[i][2] = packet.Translator.ReadBit();
                counter[i][1] = packet.Translator.ReadBit();
                counter[i][5] = packet.Translator.ReadBit();
                guid[i][1] = packet.Translator.ReadBit();
            }

            var achievements = packet.Translator.ReadBits("Achievement count", 23);
            for (var i = 0; i < criterias; ++i)
            {
                packet.Translator.ReadXORByte(guid[i], 3);
                packet.Translator.ReadXORByte(counter[i], 5);
                packet.Translator.ReadXORByte(counter[i], 6);
                packet.Translator.ReadXORByte(guid[i], 4);
                packet.Translator.ReadXORByte(guid[i], 6);
                packet.Translator.ReadXORByte(counter[i], 2);

                packet.Translator.ReadUInt32("Criteria Timer 2", i);

                packet.Translator.ReadXORByte(guid[i], 2);

                packet.Translator.ReadUInt32("Criteria Id", i);

                packet.Translator.ReadXORByte(guid[i], 5);
                packet.Translator.ReadXORByte(counter[i], 0);
                packet.Translator.ReadXORByte(counter[i], 3);
                packet.Translator.ReadXORByte(counter[i], 1);
                packet.Translator.ReadXORByte(counter[i], 4);
                packet.Translator.ReadXORByte(guid[i], 0);
                packet.Translator.ReadXORByte(guid[i], 7);
                packet.Translator.ReadXORByte(counter[i], 7);

                packet.Translator.ReadUInt32("Criteria Timer 1", i);
                packet.Translator.ReadPackedTime("Criteria Date", i);

                packet.Translator.ReadXORByte(guid[i], 1);

                packet.AddValue("Criteria Flags", flags[i], i);
                packet.AddValue("Criteria Counter", BitConverter.ToUInt64(counter[i], 0), i);
                packet.Translator.WriteGuid("Criteria GUID", guid[i], i);
            }

            for (var i = 0; i < achievements; ++i)
            {
                packet.Translator.ReadInt32<AchievementId>("Achievement Id", i);
                packet.Translator.ReadPackedTime("Achievement Date", i);
            }
        }

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleAllAchievementData510(Packet packet)
        {
            var criterias = packet.Translator.ReadBits("Criteria count", 21);
            var counter = new byte[criterias][];
            var guid = new byte[criterias][];
            var flags = new byte[criterias];

            for (var i = 0; i < criterias; ++i)
            {
                counter[i] = new byte[8];
                guid[i] = new byte[8];

                guid[i][5] = packet.Translator.ReadBit();
                counter[i][2] = packet.Translator.ReadBit();
                guid[i][3] = packet.Translator.ReadBit();
                counter[i][1] = packet.Translator.ReadBit();
                guid[i][2] = packet.Translator.ReadBit();
                guid[i][7] = packet.Translator.ReadBit();
                guid[i][4] = packet.Translator.ReadBit();
                counter[i][7] = packet.Translator.ReadBit();
                guid[i][6] = packet.Translator.ReadBit();
                flags[i] = (byte)(packet.Translator.ReadBits(4) & 0xFFu);
                counter[i][5] = packet.Translator.ReadBit();
                guid[i][1] = packet.Translator.ReadBit();
                counter[i][6] = packet.Translator.ReadBit();
                counter[i][3] = packet.Translator.ReadBit();
                counter[i][4] = packet.Translator.ReadBit();
                guid[i][0] = packet.Translator.ReadBit();
                counter[i][0] = packet.Translator.ReadBit();
            }

            var achievements = packet.Translator.ReadBits("Achievement count", 22);
            var achievementGuid = new byte[achievements][];
            for (var i = 0; i < achievements; ++i)
            {
                achievementGuid[i] = new byte[8];

                achievementGuid[i][6] = packet.Translator.ReadBit();
                achievementGuid[i][3] = packet.Translator.ReadBit();
                achievementGuid[i][7] = packet.Translator.ReadBit();
                achievementGuid[i][5] = packet.Translator.ReadBit();
                achievementGuid[i][2] = packet.Translator.ReadBit();
                achievementGuid[i][1] = packet.Translator.ReadBit();
                achievementGuid[i][4] = packet.Translator.ReadBit();
                achievementGuid[i][0] = packet.Translator.ReadBit();
            }

            for (var i = 0; i < achievements; ++i)
            {
                packet.Translator.ReadUInt32("Realm Id", i);
                packet.Translator.ReadXORByte(achievementGuid[i], 7);
                packet.Translator.ReadXORByte(achievementGuid[i], 1);
                packet.Translator.ReadXORByte(achievementGuid[i], 0);
                packet.Translator.ReadXORByte(achievementGuid[i], 4);
                packet.Translator.ReadXORByte(achievementGuid[i], 6);
                packet.Translator.ReadXORByte(achievementGuid[i], 3);
                packet.Translator.ReadXORByte(achievementGuid[i], 2);
                packet.Translator.ReadPackedTime("Achievement Date", i);
                packet.Translator.ReadInt32<AchievementId>("Achievement Id", i);
                packet.Translator.ReadXORByte(achievementGuid[i], 5);
                packet.Translator.WriteGuid("Achievement Completer GUID", achievementGuid[i], i);
            }

            for (var i = 0; i < criterias; ++i)
            {
                packet.Translator.ReadXORByte(guid[i], 6);
                packet.Translator.ReadUInt32("Criteria Timer 1", i);
                packet.Translator.ReadXORByte(guid[i], 7);
                packet.Translator.ReadXORByte(guid[i], 4);
                packet.Translator.ReadXORByte(guid[i], 3);
                packet.Translator.ReadXORByte(counter[i], 4);
                packet.Translator.ReadXORByte(counter[i], 0);
                packet.Translator.ReadXORByte(guid[i], 5);
                packet.Translator.ReadXORByte(counter[i], 5);
                packet.Translator.ReadXORByte(counter[i], 7);
                packet.Translator.ReadXORByte(counter[i], 6);
                packet.Translator.ReadXORByte(guid[i], 1);
                packet.Translator.ReadPackedTime("Criteria Date", i);
                packet.Translator.ReadUInt32("Criteria Id", i);
                packet.Translator.ReadXORByte(guid[i], 0);
                packet.Translator.ReadUInt32("Criteria Timer 2", i);
                packet.Translator.ReadXORByte(counter[i], 3);
                packet.Translator.ReadXORByte(counter[i], 1);
                packet.Translator.ReadXORByte(counter[i], 2);
                packet.Translator.ReadXORByte(guid[i], 2);

                packet.AddValue("Criteria Flags", flags[i], i);
                packet.AddValue("Criteria Counter", BitConverter.ToUInt64(counter[i], 0), i);
                packet.Translator.WriteGuid("Criteria Completer GUID", guid[i], i);
            }
        }
    }
}
