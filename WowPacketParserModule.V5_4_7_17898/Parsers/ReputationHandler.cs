using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class ReputationHandler
    {
        [Parser(Opcode.CMSG_RESET_FACTION_CHEAT)]
        public static void HandleResetFactionCheat(Packet packet)
        {
            packet.ReadUInt32("Unk Int");
            packet.ReadUInt32("Faction Id");
        }

        [Parser(Opcode.SMSG_INITIALIZE_FACTIONS)]
        public static void HandleInitializeFactions(Packet packet)
        {
            for (var i = 0; i < 256; i++)
            {
                packet.ReadEnum<FactionFlag>("Faction Flags", TypeCode.Byte, i);
                packet.ReadEnum<ReputationRank>("Faction Standing", TypeCode.UInt32, i);
            }

            for (var i = 0; i < 256; i++)
                packet.ReadBit("Count", i);
        }
    }
}
