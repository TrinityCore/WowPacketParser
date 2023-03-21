using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V10_0_0_46181.Parsers
{
    public static class BattlegroundHandler
    {
        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN_RATED_SOLO_SHUFFLE)]
        public static void HandleBattlemasterJoinRatedSoloShuffle(Packet packet)
        {
            packet.ReadByteE<LfgRoleFlag>("RolesMask");
        }
    }
}
