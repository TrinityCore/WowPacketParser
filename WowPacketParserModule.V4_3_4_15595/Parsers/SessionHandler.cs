using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V4_3_4_15595.Parsers
{
    public static class SessionHandler
    {
        [Parser(Opcode.SMSG_AUTH_CHALLENGE)]
        public static void HandleServerAuthChallenge(Packet packet)
        {
            packet.ReadUInt32("Key pt1");
            packet.ReadUInt32("Key pt2");
            packet.ReadUInt32("Key pt3");
            packet.ReadUInt32("Key pt4");
            packet.ReadUInt32("Key pt5");
            packet.ReadUInt32("Key pt6");
            packet.ReadUInt32("Key pt7");
            packet.ReadUInt32("Key pt8");
            packet.ReadInt32("Server Seed");
            packet.ReadByte("Unk Byte");
        }

        [Parser(Opcode.SMSG_AUTH_RESPONSE)]
        public static void HandleAuthResponse(Packet packet)
        {
            var isQueued = packet.ReadBit("Queued");
            var hasAccountInfo = packet.ReadBit("Has account info");

            if (isQueued)
                packet.ReadBit("UnkBit");

            if (hasAccountInfo)
            {
                packet.ReadInt32("Billing Time Remaining");
                packet.ReadByteE<ClientType>("Player Expansion");
                packet.ReadInt32("Unknown UInt32");
                packet.ReadByteE<ClientType>("Account Expansion");
                packet.ReadInt32("Billing Time Rested");
                packet.ReadByteE<BillingFlag>("Billing Flags");
            }

            packet.ReadByteE<ResponseCode>("Auth Code");

            if (isQueued)
                packet.ReadInt32("Queue Position");
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN)]
        public static void HandlePlayerLogin(Packet packet)
        {
            var guid = packet.StartBitStream(2, 3, 0, 6, 4, 5, 1, 7);
            packet.ParseBitStream(guid, 2, 7, 0, 3, 5, 6, 1, 4);
            packet.WriteGuid("Guid", guid);
            CoreParsers.SessionHandler.LoginGuid = new WowGuid64(BitConverter.ToUInt64(guid, 0));
        }

        [Parser(Opcode.SMSG_CONNECT_TO)]
        public static void HandleRedirectClient(Packet packet)
        {
            packet.ReadUInt64("Key");
            packet.ReadUInt32("Serial");
            packet.ReadBytes("RSA Hash", 0x100);
            packet.ReadByte("Con"); // 1 == Connecting to world server
        }
        
    }
}
