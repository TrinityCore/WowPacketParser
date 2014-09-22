using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_18019.Parsers
{
    public static class ReputationHandler
    {
        [Parser(Opcode.SMSG_INITIALIZE_FACTIONS)]
        public static void HandleInitializeFactions(Packet packet)
        {
            var count = 256;
            for (var i = 0; i < count; i++)
            {
                packet.ReadEnum<FactionFlag>("Faction Flags", TypeCode.Byte, i);
                packet.ReadEnum<ReputationRank>("Faction Standing", TypeCode.UInt32, i);
            }
            for (var i = 0; i < count; i++)
            {
                packet.ReadBit();
            }
        }
    }
}
