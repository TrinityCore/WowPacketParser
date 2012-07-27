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
            packet.ReadInt32("Achievement");
            packet.ReadInt32("Linked Name");
        }

        [Parser(Opcode.SMSG_ACHIEVEMENT_EARNED)]
        public static void HandleAchievementEarned(Packet packet)
        {
            packet.ReadPackedGuid("Player GUID");
            packet.ReadInt32("Achievement");
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

        public static void ReadAllAchievementData(ref Packet packet)
        {
            while (true)
            {
                var id = packet.ReadInt32();

                if (id == -1)
                    break;

                packet.WriteLine("Achievement ID: " + id);

                packet.ReadPackedTime("Achievement Time");
            }

            while (true)
            {
                var id = packet.ReadInt32();

                if (id == -1)
                    break;

                packet.WriteLine("Criteria ID: " + id);

                var counter = packet.ReadPackedGuid();
                packet.WriteLine("Criteria Counter: " + counter.Full);

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
            ReadAllAchievementData(ref packet);
        }

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA, ClientVersionBuild.V4_0_6a_13623, ClientVersionBuild.V4_1_0_13914)]
        public static void HandleAllAchievementData406(Packet packet)
        {
            var achievements = packet.ReadUInt32("Achievement count");
            var criterias = packet.ReadUInt32("Criterias count");

            for (var i = 0; i < achievements; ++i)
                packet.ReadUInt32("Achievement Id", i);

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

        [Parser(Opcode.SMSG_RESPOND_INSPECT_ACHIEVEMENTS)]
        public static void HandleInspectAchievementDataResponse(Packet packet)
        {
            packet.ReadPackedGuid("Player GUID");
            ReadAllAchievementData(ref packet);
        }

        [Parser(Opcode.SMSG_GUILD_ACHIEVEMENT_DATA, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildAchievementData(Packet packet)
        {
            var cnt = packet.ReadUInt32("Count");
            for (var i = 0; i < cnt; ++i)
                packet.ReadPackedTime("Date", i);

            for (var i = 0; i < cnt; ++i)
                packet.ReadUInt32("Achievement Id", i);
        }

        [Parser(Opcode.SMSG_GUILD_ACHIEVEMENT_DATA, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildAchievementData434(Packet packet)
        {
            var count = packet.ReadBits("Count", 23);
            for (var i = 0; i < count; ++i)
            {
                packet.ReadPackedTime("Date", i);
                packet.ReadUInt32("Achievement Id", i);
            }
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
                packet.ReadUInt32("Achievement Id", 1, i);

            for (var i = 0; i < criterias; ++i)
                packet.ReadUInt32("Criteria Id", 0, i);

            for (var i = 0; i < criterias; ++i)
                packet.ReadUInt32("Timer 2", 0, i);
        }

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA, ClientVersionBuild.V4_3_4_15595)]
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

                guid[i][4] = (byte)(packet.ReadBit() ? 1 : 0);
                counter[i][3] = (byte)(packet.ReadBit() ? 1 : 0);
                guid[i][5] = (byte)(packet.ReadBit() ? 1 : 0);
                counter[i][0] = (byte)(packet.ReadBit() ? 1 : 0);
                counter[i][6] = (byte)(packet.ReadBit() ? 1 : 0);
                guid[i][3] = (byte)(packet.ReadBit() ? 1 : 0);
                guid[i][0] = (byte)(packet.ReadBit() ? 1 : 0);
                counter[i][4] = (byte)(packet.ReadBit() ? 1 : 0);
                guid[i][2] = (byte)(packet.ReadBit() ? 1 : 0);
                counter[i][7] = (byte)(packet.ReadBit() ? 1 : 0);
                guid[i][7] = (byte)(packet.ReadBit() ? 1 : 0);
                flags[i] = (byte)(packet.ReadBits(2) & 0xFFu);
                guid[i][6] = (byte)(packet.ReadBit() ? 1 : 0);
                counter[i][2] = (byte)(packet.ReadBit() ? 1 : 0);
                counter[i][1] = (byte)(packet.ReadBit() ? 1 : 0);
                counter[i][5] = (byte)(packet.ReadBit() ? 1 : 0);
                guid[i][1] = (byte)(packet.ReadBit() ? 1 : 0);
            }

            var achievements = packet.ReadBits("Achievement count", 23);
            for (var i = 0; i < criterias; ++i)
            {
                if (guid[i][3] != 0) guid[i][3] ^= packet.ReadByte();
                if (counter[i][5] != 0) counter[i][5] ^= packet.ReadByte();
                if (counter[i][6] != 0) counter[i][6] ^= packet.ReadByte();
                if (guid[i][4] != 0) guid[i][4] ^= packet.ReadByte();
                if (guid[i][6] != 0) guid[i][6] ^= packet.ReadByte();
                if (counter[i][2] != 0) counter[i][2] ^= packet.ReadByte();

                packet.ReadUInt32("Criteria Timer 2", i);

                if (guid[i][2] != 0) guid[i][2] ^= packet.ReadByte();

                packet.ReadUInt32("Criteria Id", i);

                if (guid[i][5] != 0) guid[i][5] ^= packet.ReadByte();
                if (counter[i][0] != 0) counter[i][0] ^= packet.ReadByte();
                if (counter[i][3] != 0) counter[i][3] ^= packet.ReadByte();
                if (counter[i][1] != 0) counter[i][1] ^= packet.ReadByte();
                if (counter[i][4] != 0) counter[i][4] ^= packet.ReadByte();
                if (guid[i][0] != 0) guid[i][0] ^= packet.ReadByte();
                if (guid[i][7] != 0) guid[i][7] ^= packet.ReadByte();
                if (counter[i][7] != 0) counter[i][7] ^= packet.ReadByte();

                packet.ReadUInt32("Criteria Timer 1", i);
                packet.ReadPackedTime("Criteria Date", i);

                if (guid[i][1] != 0) guid[i][1] ^= packet.ReadByte();

                packet.WriteLine("[{0}] Criteria Flags: {1}", i, flags[i]);
                packet.WriteLine("[{0}] Criteria Counter: {1}", i, BitConverter.ToUInt64(counter[i], 0));
                packet.WriteGuid("Criteria GUID", guid[i], i);
            }

            for (var i = 0; i < achievements; ++i)
            {
                packet.ReadUInt32("Achievement Id", i);
                packet.ReadPackedTime("Achievement Date", i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_CRITERIA_DATA, ClientVersionBuild.V4_0_6a_13623, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildCriteriaData(Packet packet)
        {
            var criterias = packet.ReadUInt32("Criterias");

            for (var i = 0; i < criterias; ++i)
                packet.ReadGuid("Player GUID", i);

            for (var i = 0; i < criterias; ++i)
                packet.ReadUInt32("Criteria Timer 1", i);

            for (var i = 0; i < criterias; ++i)
                packet.ReadUInt32("Criteria Timer 2", i);

            for (var i = 0; i < criterias; ++i)
                packet.ReadUInt64("Counter", i);
            
            for (var i = 0; i < criterias; ++i)
                packet.ReadPackedTime("Criteria Time", i);                

            for (var i = 0; i < criterias; ++i)
                packet.ReadUInt32("Criteria Id", i);

            for (var i = 0; i < criterias; ++i)
                packet.ReadUInt32("Flag", i);
        }

        [Parser(Opcode.SMSG_GUILD_CRITERIA_DATA, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildCriteriaData434(Packet packet)
        {
            var count = packet.ReadBits("Count", 21);
            var counter = new byte[count][];
            var guid = new byte[count][];

            for (var i = 0; i < count; ++i)
            {
                counter[i] = new byte[8];
                guid[i] = new byte[8];

                counter[i][4] = (byte)(packet.ReadBit() ? 1 : 0);
                counter[i][1] = (byte)(packet.ReadBit() ? 1 : 0);
                guid[i][2] = (byte)(packet.ReadBit() ? 1 : 0);
                counter[i][3] = (byte)(packet.ReadBit() ? 1 : 0);
                guid[i][1] = (byte)(packet.ReadBit() ? 1 : 0);
                counter[i][5] = (byte)(packet.ReadBit() ? 1 : 0);
                counter[i][0] = (byte)(packet.ReadBit() ? 1 : 0);
                guid[i][3] = (byte)(packet.ReadBit() ? 1 : 0);
                counter[i][2] = (byte)(packet.ReadBit() ? 1 : 0);
                guid[i][7] = (byte)(packet.ReadBit() ? 1 : 0);
                guid[i][5] = (byte)(packet.ReadBit() ? 1 : 0);
                guid[i][0] = (byte)(packet.ReadBit() ? 1 : 0);
                counter[i][6] = (byte)(packet.ReadBit() ? 1 : 0);
                guid[i][6] = (byte)(packet.ReadBit() ? 1 : 0);
                counter[i][7] = (byte)(packet.ReadBit() ? 1 : 0);
                guid[i][4] = (byte)(packet.ReadBit() ? 1 : 0);
            }

            for (var i = 0; i < count; ++i)
            {
                if (guid[i][5] != 0) guid[i][5] ^= packet.ReadByte();

                packet.ReadTime("Unk time 1", i);

                if (counter[i][3] != 0) counter[i][3] ^= packet.ReadByte();
                if (counter[i][7] != 0) counter[i][7] ^= packet.ReadByte();

                packet.ReadTime("Unk time 2", i);

                if (counter[i][6] != 0) counter[i][6] ^= packet.ReadByte();
                if (guid[i][4] != 0) guid[i][4] ^= packet.ReadByte();
                if (guid[i][1] != 0) guid[i][1] ^= packet.ReadByte();
                if (counter[i][4] != 0) counter[i][4] ^= packet.ReadByte();
                if (guid[i][3] != 0) guid[i][3] ^= packet.ReadByte();
                if (counter[i][0] != 0) counter[i][0] ^= packet.ReadByte();
                if (guid[i][2] != 0) guid[i][2] ^= packet.ReadByte();
                if (counter[i][1] != 0) counter[i][1] ^= packet.ReadByte();
                if (guid[i][6] != 0) guid[i][6] ^= packet.ReadByte();

                packet.ReadTime("Criteria Date", i);
                packet.ReadUInt32("Criteria id", i);

                if (counter[i][5] != 0) counter[i][5] ^= packet.ReadByte();

                packet.ReadUInt32("Unk", i);

                if (guid[i][7] != 0) guid[i][7] ^= packet.ReadByte();
                if (counter[i][2] != 0) counter[i][2] ^= packet.ReadByte();
                if (guid[i][0] != 0) guid[i][0] ^= packet.ReadByte();

                packet.WriteGuid("Criteria GUID", guid[i], i);
                packet.WriteLine("[{0}] Criteria counter: {1}", i, BitConverter.ToUInt64(counter[i], 0));
            }
        }
    }
}
