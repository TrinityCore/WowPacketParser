using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class InstanceHandler
    {
        [Parser(Opcode.MSG_SET_DUNGEON_DIFFICULTY)]
        [Parser(Opcode.MSG_SET_RAID_DIFFICULTY)]
        public static void HandleSetDifficulty(Packet packet)
        {
            var difficulty = (MapDifficulty)packet.ReadInt32();
            Console.WriteLine("Difficulty: " + difficulty);

            if (packet.GetDirection() != Direction.ServerToClient)
                return;

            var unkByte = packet.ReadInt32();
            Console.WriteLine("Unk Int32 1: " + unkByte);

            var inGroup = packet.ReadInt32();
            Console.WriteLine("In Group: " + inGroup);
        }

        [Parser(Opcode.SMSG_INSTANCE_DIFFICULTY)]
        public static void HandleInstanceDifficulty(Packet packet)
        {
            var diff = (MapDifficulty)packet.ReadInt32();
            Console.WriteLine("Instance Difficulty: " + diff);

            var unk2 = packet.ReadInt32();
            Console.WriteLine("Player Difficulty: " + unk2);
        }

        [Parser(Opcode.SMSG_CHANGEPLAYER_DIFFICULTY_RESULT)]
        public static void HandlePlayerChangeDifficulty(Packet packet)
        {
            var type = (DifficultyChangeType)packet.ReadInt32();
            Console.WriteLine("Change Type: " + type);

            switch (type)
            {
                case DifficultyChangeType.PlayerDifficulty1:
                {
                    var difficulty = packet.ReadByte();
                    Console.WriteLine("Player Difficulty: " + difficulty);
                    break;
                }
                case DifficultyChangeType.SpellDuration:
                {
                    var duration = packet.ReadInt32();
                    Console.WriteLine("Spell Duration: " + duration);
                    break;
                }
                case DifficultyChangeType.Time:
                {
                    var time = packet.ReadInt32();
                    Console.WriteLine("Time: " + time);
                    break;
                }
                case DifficultyChangeType.MapDifficulty:
                {
                    var difficulty = (MapDifficulty)packet.ReadInt32();
                    Console.WriteLine("Map Difficulty: " + difficulty);
                    break;
                }
            }
        }
    }
}
