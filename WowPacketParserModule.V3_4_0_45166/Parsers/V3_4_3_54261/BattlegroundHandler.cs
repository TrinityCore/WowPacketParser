using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V3_4_0_45166.Parsers.V3_4_3_54261
{
    public static class BattlegroundHandler
    {
        [Parser(Opcode.SMSG_REQUEST_PVP_REWARDS_RESPONSE, true)]
        public static void HandleRequestPVPRewardsResponse(Packet packet)
        {
            //unknown structure
        }
    }
}
