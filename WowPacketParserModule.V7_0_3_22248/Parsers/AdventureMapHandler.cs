using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class AdventureMapHandler
    {
        [Parser(Opcode.CMSG_CHECK_IS_ADVENTURE_MAP_POI_VALID)]
        public static void HandleCheckIsAdventureMapPOIValid(Packet packet)
        {
            packet.ReadUInt32("AdventureMapPoiID");
        }

        [Parser(Opcode.SMSG_PLAYER_IS_ADVENTURE_MAP_POI_VALID)]
        public static void HandlePlayerIsAdventureMapPOIValid(Packet packet)
        {
            packet.ReadInt32("AdventureMapPoiID");
            packet.ResetBitReader();
            packet.ReadBit("IsVisible");
        }
    }
}
