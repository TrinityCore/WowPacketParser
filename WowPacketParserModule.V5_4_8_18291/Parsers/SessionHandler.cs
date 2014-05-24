using System;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using Guid = WowPacketParser.Misc.Guid;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class SessionHandler
    {
        [Parser(Opcode.SMSG_LOGIN_VERIFY_WORLD)]
        public static void HandleLoginVerifyWorld(Packet packet)
        {
            packet.ReadSingle("X");
            packet.ReadSingle("O");
            packet.ReadSingle("Y");
            packet.ReadUInt32("Map");
            packet.ReadSingle("Z");
        }
    }
}
