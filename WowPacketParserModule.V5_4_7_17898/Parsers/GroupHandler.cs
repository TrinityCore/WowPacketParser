using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class GroupHandler
    {
        [Parser(Opcode.CMSG_REQUEST_PARTY_MEMBER_STATS)]
        public static void HandleRequestPartyMemberStats(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadByte("Online?");

            packet.StartBitStream(guid, 5, 3, 4, 1, 6, 0, 2, 7);
            packet.ParseBitStream(guid, 0, 3, 1, 2, 7, 5, 4, 6);

            packet.WriteGuid("Guid", guid);
        }
    }
}
