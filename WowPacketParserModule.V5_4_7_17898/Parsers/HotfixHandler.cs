using System;
using System.Text;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class HotfixHandler
    {
        [Parser(Opcode.SMSG_HOTFIX_INFO)]
        public static void HandleHotfixInfo(Packet packet)
        {
            var count = packet.ReadBits("Count", 20);

            for (var i = 0; i < count; ++i)
            {
                packet.ReadTime("Hotfix date", i);
                packet.ReadEnum<DB2Hash>("Hotfix DB2 File", TypeCode.Int32, i);
                packet.ReadInt32("Hotfixed entry", i);
            }
        }
    }
}
