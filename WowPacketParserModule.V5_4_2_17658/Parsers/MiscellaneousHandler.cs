using System;
using System.Text;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class MiscellaneousHandler
    {
        [Parser(Opcode.SMSG_SEND_SERVER_LOCATION)]
        public static void HandleSendServerLocation(Packet packet)
        {
            var len1 = packet.ReadBits(7);
            var len2 = packet.ReadBits(7);
            packet.ReadWoWString("Server Location", len2);
            packet.ReadWoWString("Server Location", len1);
        }
    }
}
