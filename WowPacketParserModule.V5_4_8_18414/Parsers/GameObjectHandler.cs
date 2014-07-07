using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class GameObjectHandler
    {
        [Parser(Opcode.CMSG_GAMEOBJECT_QUERY)]
        public static void HandleGameObjectQuery(Packet packet)
        {
            var entry = packet.ReadUInt32("Entry");

            var guid = packet.StartBitStream(5, 3, 6, 2, 7, 1, 0, 4);
            packet.ParseBitStream(guid, 1, 5, 3, 4, 6, 2, 7, 0);
            packet.WriteGuid("GameObject Guid", guid);
        }

        [Parser(Opcode.CMSG_GAMEOBJ_REPORT_USE)]
        public static void HandleGOReportUse(Packet packet)
        {
            var guid = packet.StartBitStream(4, 7, 5, 3, 6, 1, 2, 0);
            packet.ParseBitStream(guid, 7, 1, 6, 5, 0, 3, 2, 4);
            packet.WriteGuid("GameObject Guid", guid);
        }

        [Parser(Opcode.CMSG_GAMEOBJ_USE)]
        public static void HandleGOUse(Packet packet)
        {
            var guid = packet.StartBitStream(6, 1, 3, 4, 0, 5, 7, 2);
            packet.ParseBitStream(guid, 0, 1, 6, 2, 3, 4, 5, 7);
            packet.WriteGuid("GameObject Guid", guid);
        }
    }
}
