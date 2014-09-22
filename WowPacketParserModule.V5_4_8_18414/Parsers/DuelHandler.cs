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
            var guid = new byte[8];

            packet.StartBitStream(guid, 1, 5, 4, 6, 3, 2, 7, 0);
            packet.ParseBitStream(guid, 4, 2, 5, 7, 1, 3, 6, 0);

            packet.WriteGuid("Opponent GUID", guid);
        }

        [Parser(Opcode.CMSG_DUEL_RESPONSE)]
        public static void HandleClientDuelResponse(Packet packet)
        {
            packet.ReadToEnd();
        }
    }
}
