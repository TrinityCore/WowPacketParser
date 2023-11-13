using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class SessionHandler
    {
        [Parser(Opcode.SMSG_SET_TIME_ZONE_INFORMATION)]
        public static void HandleSetTimeZoneInformation(Packet packet)
        {
            var len1 = packet.ReadBits(7);
            var len2 = packet.ReadBits(7);
            var len3 = packet.ReadBits(7);

            packet.ReadWoWString("ServerTimeTZ", len1);
            packet.ReadWoWString("GameTimeTZ", len2);
            packet.ReadWoWString("ServerRegionalTZ", len3);
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V3_4_3_51505)]
        public static void HandlePlayerLogin(Packet packet)
        {
            var guid = packet.ReadPackedGuid128("Guid");
            packet.ReadSingle("FarClip");
            packet.Holder.PlayerLogin = new() { PlayerGuid = guid };
            WowPacketParser.Parsing.Parsers.SessionHandler.LoginGuid = guid;
        }
    }
}
