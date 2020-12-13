using System;
using System.Collections.Generic;
using System.Linq;
using WowPacketParser.DBC;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParserModule.V7_0_3_22248.Enums;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using MovementFlag = WowPacketParserModule.V6_0_2_19033.Enums.MovementFlag;
using SplineFlag = WowPacketParserModule.V7_0_3_22248.Enums.SplineFlag;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class MovementHandler
    {
        [HasSniffData]
        [Parser(Opcode.SMSG_NEW_WORLD)]
        public static void HandleNewWorld(Packet packet)
        {
            WowPacketParser.Parsing.Parsers.MovementHandler.CurrentMapId = (uint)packet.ReadInt32<MapId>("Map");
            packet.ReadVector4("Position");
            packet.ReadInt32("Unused901_1");
            packet.ReadInt32("Unused901_2");
            packet.ReadUInt32("Reason");
            packet.ReadVector3("MovementOffset");

            packet.AddSniffData(StoreNameType.Map, (int)WowPacketParser.Parsing.Parsers.MovementHandler.CurrentMapId, "NEW_WORLD");
        }
    }
}
