using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class HotfixHandler
    {
        [Parser(Opcode.SMSG_AVAILABLE_HOTFIXES)]
        public static void HandleAvailableHotfixes(Packet packet)
        {
            packet.ReadUInt32("VirtualRealmAddress");
            var hotfixCount = packet.ReadUInt32("HotfixCount");

            for (var i = 0u; i < hotfixCount; ++i)
            {
                packet.ReadUInt32("UniqueID", i, "HotfixUniqueID");
                packet.ReadInt32("PushID", i, "HotfixUniqueID");
            }
        }

        [Parser(Opcode.CMSG_HOTFIX_REQUEST)]
        public static void HandleHotfixRequest553(Packet packet)
        {
            packet.ReadUInt32("CurrentBuild");
            packet.ReadUInt32("InternalBuild");
            var hotfixCount = packet.ReadUInt32("HotfixCount");
            for (var i = 0u; i < hotfixCount; ++i)
                packet.ReadInt32("HotfixID", i);
        }

        [Parser(Opcode.CMSG_DB_QUERY_BULK)]
        public static void HandleDbQueryBulk(Packet packet)
        {
            packet.ReadInt32E<DB2Hash>("TableHash");

            var count = packet.ReadBits("Count", 13);
            for (var i = 0; i < count; ++i)
                packet.ReadInt32("RecordID", i);
        }
    }
}
