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

namespace WowPacketParserModule.V4_4_0_53627.Parsers
{
    public static class HotfixHandler
    {
        [Parser(Opcode.SMSG_AVAILABLE_HOTFIXES, ClientVersionBuild.V9_1_5_40772)]
        public static void HandleAvailableHotfixes915(Packet packet)
        {
            packet.ReadInt32("VirtualRealmAddress");
            var hotfixCount = packet.ReadUInt32("HotfixCount");
            for (var i = 0u; i < hotfixCount; ++i)
            {
                packet.ReadInt32("PushID", i, "HotfixUniqueID");
                packet.ReadUInt32("UniqueID", i, "HotfixUniqueID");
            }
        }
    }
}
