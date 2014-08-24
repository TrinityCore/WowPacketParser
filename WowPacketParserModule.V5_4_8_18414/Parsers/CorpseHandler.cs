using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class CorpseHandler
    {
        [Parser(Opcode.CMSG_CORPSE_QUERY)]
        public static void HandleCorpseQuery(Packet packet)
        {
        }
    }
}
