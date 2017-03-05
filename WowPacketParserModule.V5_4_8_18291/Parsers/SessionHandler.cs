using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class SessionHandler
    {
        [Parser(Opcode.SMSG_MOTD)]
        public static void HandleMessageOfTheDay(Packet packet)
        {
            var lineCount = packet.Translator.ReadBits("Line Count", 4);
            var lineLength = new int[lineCount];

            for (var i = 0; i < lineCount; i++)
                lineLength[i] = (int)packet.Translator.ReadBits(7);

            for (var i = 0; i < lineCount; i++)
                packet.Translator.ReadWoWString("Line", lineLength[i], i);
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN)]
        public static void HandlePlayerLogin(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadSingle("Unk Float");

            packet.Translator.StartBitStream(guid, 1, 4, 7, 3, 2, 6, 5, 0);
            packet.Translator.ParseBitStream(guid, 5, 1, 0, 6, 2, 4, 7, 3);

            CoreParsers.SessionHandler.LoginGuid = new WowGuid64(BitConverter.ToUInt64(guid, 0));
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SET_TIME_ZONE_INFORMATION)]
        public static void HandleSetTimeZoneInformation(Packet packet)
        {
            var len1 = packet.Translator.ReadBits(7);
            var len2 = packet.Translator.ReadBits(7);
            packet.Translator.ReadWoWString("Server Location", len2);
            packet.Translator.ReadWoWString("Server Location", len1);
        }

        [Parser(Opcode.SMSG_LOGOUT_COMPLETE)]
        public static void HandleLogoutComplete(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadBit(); // fake bit

            packet.Translator.StartBitStream(guid, 3, 2, 1, 4, 6, 7, 5, 0);
            packet.Translator.ParseBitStream(guid, 6, 4, 1, 2, 7, 3, 0, 5);

            packet.Translator.WriteGuid("Guid", guid);

            CoreParsers.SessionHandler.LoginGuid = new WowGuid64(BitConverter.ToUInt64(guid, 0));
        }
    }
}
