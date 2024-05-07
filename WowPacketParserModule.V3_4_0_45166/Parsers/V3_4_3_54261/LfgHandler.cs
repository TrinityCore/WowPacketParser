using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V3_4_0_45166.Parsers.V3_4_3_54261
{
    public static class LfgHandler
    {
        [BuildMatch(ClientVersionBuild.V3_4_3_54261)]
        [Parser(Opcode.CMSG_DF_GET_SYSTEM_INFO, true)]
        public static void HandleLFGLockInfoRequest(Packet packet)
        {
            packet.ReadBit("Player");
            var hasPartyIndex = packet.ReadBit("HasPartyIndex");
            if (hasPartyIndex)
                packet.ReadByte("PartyIndex");
        }
    }
}
