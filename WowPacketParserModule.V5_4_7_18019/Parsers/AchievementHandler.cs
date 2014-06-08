using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_18019.Parsers
{
    public static class AchievementHandler
    {
        [Parser(Opcode.CMSG_QUERY_INSPECT_ACHIEVEMENTS)]
        public static void HandleInspectAchievementData(Packet packet)
        {
            var guid = packet.StartBitStream(2, 5, 0, 7, 3, 4, 6, 1);
            packet.ParseBitStream(guid, 2, 5, 4, 6, 1, 3, 0, 7);

            packet.WriteGuid("Guid", guid);
        }
    }
}
