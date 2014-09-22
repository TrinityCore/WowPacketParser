using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class CorpseHandler
    {
        [Parser(Opcode.SMSG_CORPSE_RECLAIM_DELAY)]
        public static void HandleCorpseReclaimDelay(Packet packet)
        {
            var hasDelay = !packet.ReadBit();
            if (hasDelay)
                packet.ReadInt32("Delay");
        }
    }
}
