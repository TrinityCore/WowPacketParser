using System;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using Guid = WowPacketParser.Misc.Guid;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class SessionHandler
    {
        [Parser(Opcode.CMSG_PLAYER_LOGIN)]
        public static void HandlePlayerLogin(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadSingle("Unk Float");

            packet.StartBitStream(guid, 7, 2, 5, 4, 3, 0, 6, 1);
            packet.ParseBitStream(guid, 7, 1, 5, 0, 3, 6, 2, 4);

            CoreParsers.SessionHandler.LoginGuid = new Guid(BitConverter.ToUInt64(guid, 0));
            packet.WriteGuid("Guid", guid);
        }
    }
}