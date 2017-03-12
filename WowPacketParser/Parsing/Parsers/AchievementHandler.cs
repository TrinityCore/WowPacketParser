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

        [Parser(Opcode.CMSG_QUERY_INSPECT_ACHIEVEMENTS)]
        public static void HandleInspectAchievementData(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
        }

        [Parser(Opcode.SMSG_RESPOND_INSPECT_ACHIEVEMENTS, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleInspectAchievementDataResponse(Packet packet)
        {
            packet.ReadPackedGuid("Player GUID");
            Messages.Submessages.AllAchievements.Read3(packet);
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
                    Messages.Client.ClientAllAchievementData.HandleAllAchievementData434(packet2);
                else if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                    Messages.Client.ClientAllAchievementData.HandleAllAchievementData422(packet2);
                else if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                    Messages.Client.ClientAllAchievementData.HandleAllAchievementData406(packet2);
                else
                    Messages.Client.ClientAllAchievementData.HandleAllAchievementData(packet2);
        }
    }
}
