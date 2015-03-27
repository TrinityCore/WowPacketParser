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

        [Parser(Opcode.CMSG_AUTH_SESSION)]
        public static void HandleAuthSession(Packet packet)
        {
            var sha = new byte[20];
            packet.ReadUInt32("UInt32 1");
            packet.ReadUInt32("UInt32 2");
            packet.ReadByte("Unk Byte");
            sha[10] = packet.ReadByte();
            sha[18] = packet.ReadByte();
            sha[12] = packet.ReadByte();
            sha[5] = packet.ReadByte();
            packet.ReadInt64("Int64");
            sha[15] = packet.ReadByte();
            sha[9] = packet.ReadByte();
            sha[19] = packet.ReadByte();
            sha[4] = packet.ReadByte();
            sha[7] = packet.ReadByte();
            sha[16] = packet.ReadByte();
            sha[3] = packet.ReadByte();
            packet.ReadInt16E<ClientVersionBuild>("Client Build");
            sha[8] = packet.ReadByte();
            packet.ReadUInt32("UInt32 3");
            packet.ReadByte("Unk Byte");
            sha[17] = packet.ReadByte();
            sha[6] = packet.ReadByte();
            sha[0] = packet.ReadByte();
            sha[1] = packet.ReadByte();
            sha[11] = packet.ReadByte();
            packet.ReadUInt32("Client seed");
            sha[2] = packet.ReadByte();
            packet.ReadUInt32("UInt32 4");
            sha[14] = packet.ReadByte();
            sha[13] = packet.ReadByte();

            var addons = new Packet(packet.ReadBytes(packet.ReadInt32()), packet.Opcode, packet.Time, packet.Direction,
                packet.Number, packet.Writer, packet.FileName);
            CoreParsers.AddonHandler.ReadClientAddonsList(addons);
            addons.ClosePacket(false);

            packet.ReadBit("Unk bit");
            var size = (int)packet.ReadBits(12);
            packet.ReadBytesString("Account name", size);
            packet.AddValue("Proof SHA-1 Hash", Utilities.ByteArrayToHexString(sha));
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

        [Parser(Opcode.CMSG_AUTH_CONTINUED_SESSION)]
        public static void HandleRedirectionAuthProof(Packet packet)
        {
            var bytes = new byte[20];
            packet.ReadUInt64("Unk Long");
            packet.ReadUInt64("+ 4");
            bytes[5] = packet.ReadByte();
            bytes[2] = packet.ReadByte();
            bytes[6] = packet.ReadByte();
            bytes[10] = packet.ReadByte();
            bytes[8] = packet.ReadByte();
            bytes[17] = packet.ReadByte();
            bytes[11] = packet.ReadByte();
            bytes[15] = packet.ReadByte();
            bytes[7] = packet.ReadByte();
            bytes[1] = packet.ReadByte();
            bytes[4] = packet.ReadByte();
            bytes[16] = packet.ReadByte();
            bytes[0] = packet.ReadByte();
            bytes[12] = packet.ReadByte();
            bytes[14] = packet.ReadByte();
            bytes[13] = packet.ReadByte();
            bytes[18] = packet.ReadByte();
            bytes[9] = packet.ReadByte();
            bytes[19] = packet.ReadByte();
            bytes[3] = packet.ReadByte();
            packet.AddValue("Proof SHA-1 Hash", Utilities.ByteArrayToHexString(bytes));
        }
    }
}
