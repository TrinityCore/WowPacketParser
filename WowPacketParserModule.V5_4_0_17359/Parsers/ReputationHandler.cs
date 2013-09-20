using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class ReputationHandler
    {
        [Parser(Opcode.SMSG_INITIALIZE_FACTIONS)]
        public static void HandleInitializeFactions(Packet packet)
        {
            for (var i = 0; i < 256; i++)
            {
                packet.ReadEnum<ReputationRank>("Faction Standing", TypeCode.UInt32, i);
                packet.ReadEnum<FactionFlag>("Faction Flags", TypeCode.Byte, i);
            }

            for (var i = 0; i < 256; i++)
                packet.ReadBit("Count", i);
        }
    }
}
