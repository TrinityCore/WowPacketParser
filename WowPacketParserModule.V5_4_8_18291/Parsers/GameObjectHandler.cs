using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class GameObjectHandler
    {

        [Parser(Opcode.CMSG_GAMEOBJ_REPORT_USE)]
        public static void HandleGameObjectReportUse(Packet packet)
        {
            var GUID = new byte[8];
            GUID = packet.StartBitStream(4, 7, 5, 3, 6, 1, 2, 0);
            packet.ParseBitStream(GUID, 7, 1, 6, 5, 0, 3, 2, 4);
            packet.WriteGuid("GUID", GUID);
        }

        [Parser(Opcode.CMSG_GAMEOBJ_USE)]
        public static void HandleGameObjectuse(Packet packet)
        {
            var GUID = new byte[8];
            GUID = packet.StartBitStream(6, 1, 3, 4, 0, 5, 7, 2);
            packet.ParseBitStream(GUID, 0, 1, 6, 2, 3, 4, 5, 7);
            packet.WriteGuid("GUID", GUID);
        }
		
    }
}
