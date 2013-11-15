using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParserModule.V5_4_1_17538.Enums;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_4_1_17538.Parsers
{
    public static class QueryHandler
    {
        [Parser(Opcode.SMSG_REALM_QUERY_RESPONSE)]
        public static void HandleRealmQueryResponse(Packet packet)
        {

            var byte20 = packet.ReadByte("byte20");
            packet.ReadUInt32("Realm Id");

            var bits22 = packet.ReadBits(8);
            packet.ReadBit();
            var bits278 = packet.ReadBits(8);

            packet.ReadWoWString("Realmname (without white char)", bits278);
            packet.ReadWoWString("Realmname", bits22);
        }
    }
}
