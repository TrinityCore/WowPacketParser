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

        [Parser(Opcode.SMSG_AUTH_CHALLENGE)]
        public static void HandleServerAuthChallenge(Packet packet)
        {
            packet.ReadByte("Unk Byte");
            packet.ReadUInt32("Key pt1");
            packet.ReadUInt32("Key pt2");
            packet.ReadUInt32("Key pt3");
            packet.ReadUInt32("Key pt4");
            packet.ReadUInt32("Key pt5");
            packet.ReadUInt32("Key pt6");
            packet.ReadUInt32("Key pt7");
            packet.ReadUInt32("Key pt8");
            packet.ReadUInt32("Server Seed");
        }

        [Parser(Opcode.SMSG_MOTD)]
        public static void HandleMessageOfTheDay(Packet packet)
        {
            var lineCount = packet.ReadBits("Line Count", 4);
            var lineLength = new int[lineCount];

            for (var i = 0; i < lineCount; i++)
                lineLength[i] = (int)packet.ReadBits(7);

            for (var i = 0; i < lineCount; i++)
                packet.ReadWoWString("Line", lineLength[i], i);
        }
     }
}
