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
            packet.ReadCString("Player Name");
            packet.ReadGuid("Player GUID");
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
            packet.ReadInt32("Unk Int32");
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

                packet.ReadInt32("Unk Int32");

                packet.ReadPackedTime("Criteria Time");

                for (var i = 0; i < 2; i++)
                    packet.ReadInt32("Timer " + i);
            }
        }

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA)]
        public static void HandleAllAchievementData(Packet packet)
        {
            ReadAllAchievementData(ref packet);
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

        [Parser(Opcode.SMSG_GUILD_ACHIEVEMENT_DATA)]
        public static void HandleGuildAchievementData(Packet packet)
        {
            var cnt = packet.ReadUInt32("Count");
            for (var i = 0; i < cnt; ++i)
                packet.ReadPackedTime("Date", i);

            for (var i = 0; i < cnt; ++i)
                packet.ReadUInt32("Achievement Id", i);
        }

        [Parser(Opcode.SMSG_COMPRESSED_ACHIEVEMENT_DATA)]
        public static void HandleCompressedAllAchievementData(Packet packet)
        {
            using (var packet2 = packet.Inflate(packet.ReadInt32()))
                HandleAllAchievementData422(packet2);
        }

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleAllAchievementData422(Packet packet)
        {
            var count1 = packet.ReadUInt32("Count1");
            for (var i = 0; i < count1; ++i)
                packet.ReadBits("Flag", 2, 0, i);

            for (var i = 0; i < count1; ++i)
                packet.ReadUInt64("Counter", 0, i);

            var count2 = packet.ReadUInt32("Count2");
            for (var i = 0; i < count2; ++i)
                packet.ReadPackedTime("Achievement Time", 1, i);

            for (var i = 0; i < count1; ++i)
                packet.ReadGuid("Player GUID", 0, i);

            for (var i = 0; i < count1; ++i)
                packet.ReadPackedTime("Criteria Time", 0, i);

            for (var i = 0; i < count1; ++i)
                packet.ReadUInt32("Timer 1", 0, i);

            for (var i = 0; i < count2; ++i)
                packet.ReadUInt32("Achievement Id", 1, i);

            for (var i = 0; i < count1; ++i)
                packet.ReadUInt32("Criteria Id", 0, i);

            for (var i = 0; i < count1; ++i)
                packet.ReadUInt32("Timer 2", 0, i);
        }
    }
}
