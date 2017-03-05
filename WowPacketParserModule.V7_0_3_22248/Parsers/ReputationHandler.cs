using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class ReputationHandler
    {
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
    }
}
