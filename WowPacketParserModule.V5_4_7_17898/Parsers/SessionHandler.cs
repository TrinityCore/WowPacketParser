using System;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using Guid = WowPacketParser.Misc.Guid;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class SessionHandler
    {
        [Parser(Opcode.CMSG_PLAYER_LOGIN)]
        public static void HandlePlayerLogin(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadSingle("Unk Float");

            packet.StartBitStream(guid, 7, 6, 0, 4, 5, 2, 3, 1);
            packet.ParseBitStream(guid, 5, 0, 1, 6, 7, 2, 3, 4);

            CoreParsers.SessionHandler.LoginGuid = new Guid(BitConverter.ToUInt64(guid, 0));
            packet.WriteGuid("Guid", guid);
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

        [Parser(Opcode.SMSG_SEND_SERVER_LOCATION)]
        public static void HandleSendServerLocation(Packet packet)
        {
            var len1 = packet.ReadBits(7);
            var len2 = packet.ReadBits(7);
            packet.ReadWoWString("Server Location", len2);
            packet.ReadWoWString("Server Location", len1);
        }

        [Parser(Opcode.CMSG_REDIRECT_AUTH_PROOF)]
        public static void HandleRedirectAuthProof(Packet packet)
        {
            var sha = new byte[20];
            packet.ReadInt64("Int64 Unk1");
            packet.ReadInt64("Int64 Unk2");
            sha[1] = packet.ReadByte();
            sha[14] = packet.ReadByte();
            sha[9] = packet.ReadByte();
            sha[18] = packet.ReadByte();
            sha[17] = packet.ReadByte();
            sha[8] = packet.ReadByte();
            sha[6] = packet.ReadByte();
            sha[10] = packet.ReadByte();
            sha[3] = packet.ReadByte();
            sha[16] = packet.ReadByte();
            sha[4] = packet.ReadByte();
            sha[0] = packet.ReadByte();
            sha[15] = packet.ReadByte();
            sha[2] = packet.ReadByte();
            sha[19] = packet.ReadByte();
            sha[12] = packet.ReadByte();
            sha[13] = packet.ReadByte();
            sha[5] = packet.ReadByte();
            sha[11] = packet.ReadByte();
            sha[7] = packet.ReadByte();

            packet.WriteLine("SHA-1 Hash: " + Utilities.ByteArrayToHexString(sha));
        }


        [Parser(Opcode.SMSG_REDIRECT_CLIENT)]
        public static void HandleRedirectClient(Packet packet)
        {
            packet.ReadUInt64("Unk Long");
            var hash = packet.ReadBytes(0x100);
            packet.WriteLine("RSA Hash: {0}", Utilities.ByteArrayToHexString(hash));
            packet.ReadByte("Unk Byte");
            packet.ReadUInt32("Token");
        }
    }
}