using System;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using Guid = WowPacketParser.Misc.Guid;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_1_17538.Parsers
{
    public static class SessionHandler
    {
        [Parser(Opcode.CMSG_PLAYER_LOGIN)]
        public static void HandlePlayerLogin(Packet packet)
        {
            packet.ReadSingle("Unk Float");
            var guid = packet.StartBitStream(6, 7, 1, 5, 2, 4, 3, 0);
            packet.ParseBitStream(guid, 7, 6, 0, 1, 4, 3, 2, 5);
            CoreParsers.SessionHandler.LoginGuid = new Guid(BitConverter.ToUInt64(guid, 0));
            packet.WriteGuid("Guid", guid);
        }
     }
}
