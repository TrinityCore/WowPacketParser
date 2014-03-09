using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class LootHandler
    {
        [Parser(Opcode.CMSG_LOOT)]
        public static void HandleLoot(Packet packet)
        {
            var guid = packet.StartBitStream(6, 4, 2, 7, 5, 3, 0, 1);
            packet.ParseBitStream(guid, 3, 2, 1, 6, 0, 5, 7, 4);
            packet.WriteGuid("GUID", guid);
        }
    }
}
