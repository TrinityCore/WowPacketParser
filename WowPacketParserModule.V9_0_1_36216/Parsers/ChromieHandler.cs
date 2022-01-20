using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;


namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class ChromieHandler
    {
        [Parser(Opcode.SMSG_CHROMIE_TIME_OPEN_NPC)]
        public static void HandleChromieTimeOpenNpc(Packet packet)
        {
            packet.ReadPackedGuid128("GUID");
        }

        [Parser(Opcode.CMSG_CHROMIE_TIME_SELECT_EXPANSION)]
        public static void HandleChromieTimeSelectExpansion(Packet packet)
        {
            packet.ReadPackedGuid128("GUID");
            packet.ReadUInt32("Expansion");
        }
    }
}
