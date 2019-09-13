using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V1_13_2_31446.Parsers
{
    public static class ReputationHandler
    {
        public static ushort FactionCount = 300;

        [Parser(Opcode.SMSG_INITIALIZE_FACTIONS, ClientVersionBuild.V7_2_0_23826)]
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
