using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class ReputationHandler
    {
        public const int FactionCount = 400;

        [Parser(Opcode.SMSG_FACTION_BONUS_INFO)]
        public static void HandleFactionBonusInfo(Packet packet)
        {
            for (var i = 0; i < FactionCount; i++)
                packet.ReadBit("FactionHasBonus", i);
        }

        [Parser(Opcode.SMSG_INITIALIZE_FACTIONS)]
        public static void HandleInitializeFactions(Packet packet)
        {
            for (var i = 0; i < FactionCount; i++)
            {
                packet.ReadByteE<FactionFlag>("FactionFlags", i);
                packet.ReadUInt32E<ReputationRank>("FactionStandings", i);
            }

            for (var i = 0; i < FactionCount; i++)
                packet.ReadBit("FactionHasBonus", i);
        }
    }
}
