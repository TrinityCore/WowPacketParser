using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class AdventureMapHandler
    {
        [Parser(Opcode.SMSG_PLAYER_IS_ADVENTURE_MAP_POI_VALID, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePlayerIsAdventureMapPOIValid(Packet packet)
        {
            packet.ReadInt32("AdventureMapPoiID");
            packet.ResetBitReader();
            packet.ReadBit("IsVisible");
        }

        [Parser(Opcode.CMSG_CHECK_IS_ADVENTURE_MAP_POI_VALID, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleCheckIsAdventureMapPOIValid(Packet packet)
        {
            packet.ReadUInt32("AdventureMapPoiID");
        }
    }
}
