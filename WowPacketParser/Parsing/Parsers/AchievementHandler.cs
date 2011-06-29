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
            var id = packet.ReadInt32();
            Console.WriteLine("ID: " + id);
        }

        [Parser(Opcode.SMSG_SERVER_FIRST_ACHIEVEMENT)]
        public static void HandleServerFirstAchievement(Packet packet)
        {
            var name = packet.ReadCString();
            Console.WriteLine("Player Name: " + name);

            var guid = packet.ReadGuid();
            Console.WriteLine("Player GUID: " + guid);

            var achId = packet.ReadInt32();
            Console.WriteLine("Achievement: " + achId);

            var linkName = packet.ReadInt32();
            Console.WriteLine("Linked Name: " + linkName);
        }

        [Parser(Opcode.SMSG_ACHIEVEMENT_EARNED)]
        public static void HandleAchievementEarned(Packet packet)
        {
            var guid = packet.ReadPackedGuid();
            Console.WriteLine("Player GUID: " + guid);

            var achId = packet.ReadInt32();
            Console.WriteLine("Achievement: " + achId);

            var time = packet.ReadPackedTime();
            Console.WriteLine("Time: " + time);

            var unkInt = packet.ReadInt32();
            Console.WriteLine("Unk Int32: " + unkInt);
        }

        [Parser(Opcode.SMSG_CRITERIA_UPDATE)]
        public static void HandleCriteriaUpdate(Packet packet)
        {
            var id = packet.ReadInt32();
            Console.WriteLine("Criteria ID: " + id);

            var counter = packet.ReadPackedGuid();
            Console.WriteLine("Criteria Counter: " + counter.Full);

            var guid = packet.ReadPackedGuid();
            Console.WriteLine("Player GUID: " + guid);

            var unk = packet.ReadInt32();
            Console.WriteLine("Unk Int32: " + unk);

            var time = packet.ReadPackedTime();
            Console.WriteLine("Time: " + time);

            for (var i = 0; i < 2; i++)
            {
                var timer = packet.ReadInt32();
                Console.WriteLine("Timer " + i + ": " + timer);
            }
        }

        public static void ReadAllAchievementData(Packet packet)
        {
            while (true)
            {

                var id = packet.ReadInt32();

                if (id == -1)
                    break;

                Console.WriteLine("Achievement ID: " + id);

                var time = packet.ReadPackedTime();
                Console.WriteLine("Achievement Time: " + time);
            }

            while (true)
            {
                var id = packet.ReadInt32();

                if (id == -1)
                    break;

                Console.WriteLine("Criteria ID: " + id);

                var counter = packet.ReadPackedGuid();
                Console.WriteLine("Criteria Counter: " + counter.Full);

                var guid = packet.ReadPackedGuid();
                Console.WriteLine("Player GUID: " + guid);

                var unk = packet.ReadInt32();
                Console.WriteLine("Unk Int32: " + unk);

                var time = packet.ReadPackedTime();
                Console.WriteLine("Criteria Time: " + time);

                for (var i = 0; i < 2; i++)
                {
                    var timer = packet.ReadInt32();
                    Console.WriteLine("Timer " + i + ": " + timer);
                }
            }
        }

        [Parser(Opcode.SMSG_ALL_ACHIEVEMENT_DATA)]
        public static void HandleAllAchievementData(Packet packet)
        {
            ReadAllAchievementData(packet);
        }

        [Parser(Opcode.CMSG_QUERY_INSPECT_ACHIEVEMENTS)]
        public static void HandleInspectAchievementData(Packet packet)
        {
            var guid = packet.ReadPackedGuid();
            Console.WriteLine("GUID: " + guid);
        }

        [Parser(Opcode.SMSG_RESPOND_INSPECT_ACHIEVEMENTS)]
        public static void HandleInspectAchievementDataResponse(Packet packet)
        {
            var guid = packet.ReadPackedGuid();
            Console.WriteLine("Player GUID: " + guid);

            ReadAllAchievementData(packet);
        }
    }
}
