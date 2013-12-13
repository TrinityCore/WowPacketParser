using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class QueryHandler
    {
        [Parser(Opcode.CMSG_CREATURE_QUERY)]
        public static void HandleCreatureQuery(Packet packet)
        {
            packet.ReadInt32("Entry");
        }

        [Parser(Opcode.CMSG_DB_QUERY_BULK)]
        public static void HandleDBQueryBulk(Packet packet)
        {
            packet.ReadEnum<DB2Hash>("DB2 File", TypeCode.Int32);
            var count = packet.ReadBits(21);

            var guids = new byte[count][];
            for (var i = 0; i < count; ++i)
            {
                guids[i] = new byte[8];
                packet.StartBitStream(guids[i], 3, 7, 5, 6, 2, 0, 4, 1);
            }

            packet.ResetBitReader();
            for (var i = 0; i < count; ++i)
            {
                packet.ReadXORBytes(guids[i], 5, 1, 4, 6, 7, 2, 0, 3);
                packet.ReadInt32("Entry", i);
                packet.WriteGuid("Guid", guids[i], i);
            }
        }
    }
}
