using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class ReputationHandler
    {
        public static ushort FactionCount = 300;

        [Parser(Opcode.SMSG_SET_FORCED_REACTIONS)]
        public static void HandleForcedReactions(Packet packet)
        {
            var counter = packet.ReadUInt32("ForcedReactionCount");

            for (var i = 0; i < counter; i++)
            {
                packet.ReadUInt32("Faction");
                packet.ReadUInt32("Reaction");
            }
        }

        [Parser(Opcode.SMSG_FACTION_BONUS_INFO, ClientVersionBuild.V7_2_0_23826)]
        public static void HandleFactionBonusInfo(Packet packet)
        {
            for (var i = 0; i < FactionCount; i++)
                packet.ReadBit("FactionHasBonus", i);
        }

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
