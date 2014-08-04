using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class DuelHandler
    {
        [Parser(Opcode.CMSG_DUEL_PROPOSED)]
        public static void HandleClientDuelProposed(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_DUEL_RESPONSE)]
        public static void HandleClientDuelResponse(Packet packet)
        {
            packet.ReadToEnd();
        }
    }
}
