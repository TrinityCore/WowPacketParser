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

                packet.Writer.WriteLine("Achievement ID: " + id);

                packet.ReadPackedTime("Achievement Time");
            }

            while (true)
            {
                var id = packet.ReadInt32();

                if (id == -1)
                    break;

                packet.Writer.WriteLine("Criteria ID: " + id);

                var counter = packet.ReadPackedGuid();
                packet.Writer.WriteLine("Criteria Counter: " + counter.Full);

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
    }
}
