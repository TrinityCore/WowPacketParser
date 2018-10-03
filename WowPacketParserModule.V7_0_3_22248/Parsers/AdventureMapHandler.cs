using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class AdventureMapHandler
    {
        [Parser(Opcode.CMSG_ADVENTURE_MAP_POI_QUERY)]
        public static void HandleAdcentureMapPoiQuery(Packet packet)
        {
            packet.ReadUInt32("AdventureMapPoiID");
        }

        [Parser(Opcode.SMSG_ADVENTURE_MAP_POI_QUERY_RESPONSE)]
        public static void HandleAdcentureMapPoiQueryResponse(Packet packet)
        {
            packet.ReadInt32("AdventureMapPoiID");
            packet.ResetBitReader();
            packet.ReadBit("IsVisible");
        }
    }
}
