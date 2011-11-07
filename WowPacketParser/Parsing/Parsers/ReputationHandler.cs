using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class ReputationHandler
    {
        [Parser(Opcode.SMSG_INITIALIZE_FACTIONS)]
        public static  void HandleInitializeFactions(Packet packet)
        {
            var flags = packet.ReadInt32();
            Console.WriteLine("Flags: 0x" + flags.ToString("X8"));

            for (var i = 0; i < 128; i++)
            {
                packet.ReadEnum<FactionFlag>("Faction Flags", TypeCode.Byte, i);
                packet.ReadInt32("Faction Standing", i);
            }
        }

        [Parser(Opcode.SMSG_SET_FORCED_REACTIONS)]
        public static  void HandleForcedReactions(Packet packet)
        {
            var counter = packet.ReadInt32("Factions");
            for (var i = 0; i < counter; i++)
            {
                packet.ReadUInt32("Faction Id");
                packet.ReadUInt32("Reputation Rank");
            }
        }

        [Parser(Opcode.SMSG_SET_FACTION_STANDING)]
        public static void HandleSetFactionStanding(Packet packet)
        {
            packet.ReadSingle("Reputation loss");
            packet.ReadBoolean("Unk bool"); // First login = false ?

            var count = packet.ReadInt32("Count");
            for (var i = 0; i < count; i++)
            {
                packet.ReadInt32("Faction List Id");
                packet.ReadInt32("Standing");
            }
        }
    }
}
