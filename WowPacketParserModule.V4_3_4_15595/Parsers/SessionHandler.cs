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
            packet.Translator.ReadUInt32("Key pt1");
            packet.Translator.ReadUInt32("Key pt2");
            packet.Translator.ReadUInt32("Key pt3");
            packet.Translator.ReadUInt32("Key pt4");
            packet.Translator.ReadUInt32("Key pt5");
            packet.Translator.ReadUInt32("Key pt6");
            packet.Translator.ReadUInt32("Key pt7");
            packet.Translator.ReadUInt32("Key pt8");
            packet.Translator.ReadInt32("Server Seed");
            packet.Translator.ReadByte("Unk Byte");
        }

        [Parser(Opcode.CMSG_AUTH_SESSION)]
        public static void HandleAuthSession(Packet packet)
        {
            var sha = new byte[20];
            packet.Translator.ReadUInt32("UInt32 1");
            packet.Translator.ReadUInt32("UInt32 2");
            packet.Translator.ReadByte("Unk Byte");
            sha[10] = packet.Translator.ReadByte();
            sha[18] = packet.Translator.ReadByte();
            sha[12] = packet.Translator.ReadByte();
            sha[5] = packet.Translator.ReadByte();
            packet.Translator.ReadInt64("Int64");
            sha[15] = packet.Translator.ReadByte();
            sha[9] = packet.Translator.ReadByte();
            sha[19] = packet.Translator.ReadByte();
            sha[4] = packet.Translator.ReadByte();
            sha[7] = packet.Translator.ReadByte();
            sha[16] = packet.Translator.ReadByte();
            sha[3] = packet.Translator.ReadByte();
            packet.Translator.ReadInt16E<ClientVersionBuild>("Client Build");
            sha[8] = packet.Translator.ReadByte();
            packet.Translator.ReadUInt32("UInt32 3");
            packet.Translator.ReadByte("Unk Byte");
            sha[17] = packet.Translator.ReadByte();
            sha[6] = packet.Translator.ReadByte();
            sha[0] = packet.Translator.ReadByte();
            sha[1] = packet.Translator.ReadByte();
            sha[11] = packet.Translator.ReadByte();
            packet.Translator.ReadUInt32("Client seed");
            sha[2] = packet.Translator.ReadByte();
            packet.Translator.ReadUInt32("UInt32 4");
            sha[14] = packet.Translator.ReadByte();
            sha[13] = packet.Translator.ReadByte();

            var addons = new Packet(packet.Translator.ReadBytes(packet.Translator.ReadInt32()), packet.Opcode, packet.Time, packet.Direction,
                packet.Number, packet.Formatter, packet.FileName);
            CoreParsers.AddonHandler.ReadClientAddonsList(addons);
            addons.ClosePacket(false);

            packet.Translator.ReadBit("Unk bit");
            var size = (int)packet.Translator.ReadBits(12);
            packet.Translator.ReadBytesString("Account name", size);
            packet.AddValue("Proof SHA-1 Hash", Utilities.ByteArrayToHexString(sha));
        }

        [Parser(Opcode.SMSG_AUTH_RESPONSE)]
        public static void HandleAuthResponse(Packet packet)
        {
            var isQueued = packet.Translator.ReadBit("Queued");
            var hasAccountInfo = packet.Translator.ReadBit("Has account info");

            if (isQueued)
                packet.Translator.ReadBit("UnkBit");

            if (hasAccountInfo)
            {
                packet.Translator.ReadInt32("Billing Time Remaining");
                packet.Translator.ReadByteE<ClientType>("Player Expansion");
                packet.Translator.ReadInt32("Unknown UInt32");
                packet.Translator.ReadByteE<ClientType>("Account Expansion");
                packet.Translator.ReadInt32("Billing Time Rested");
                packet.Translator.ReadByteE<BillingFlag>("Billing Flags");
            }

            packet.Translator.ReadByteE<ResponseCode>("Auth Code");

            if (isQueued)
                packet.Translator.ReadInt32("Queue Position");
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN)]
        public static void HandlePlayerLogin(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(2, 3, 0, 6, 4, 5, 1, 7);
            packet.Translator.ParseBitStream(guid, 2, 7, 0, 3, 5, 6, 1, 4);
            packet.Translator.WriteGuid("Guid", guid);
            CoreParsers.SessionHandler.LoginGuid = new WowGuid64(BitConverter.ToUInt64(guid, 0));
        }

        [Parser(Opcode.SMSG_CONNECT_TO)]
        public static void HandleRedirectClient(Packet packet)
        {
            packet.Translator.ReadUInt64("Key");
            packet.Translator.ReadUInt32("Serial");
            packet.Translator.ReadBytes("RSA Hash", 0x100);
            packet.Translator.ReadByte("Con"); // 1 == Connecting to world server
        }

        [Parser(Opcode.CMSG_AUTH_CONTINUED_SESSION)]
        public static void HandleRedirectionAuthProof(Packet packet)
        {
            var bytes = new byte[20];
            packet.Translator.ReadUInt64("Unk Long");
            packet.Translator.ReadUInt64("+ 4");
            bytes[5] = packet.Translator.ReadByte();
            bytes[2] = packet.Translator.ReadByte();
            bytes[6] = packet.Translator.ReadByte();
            bytes[10] = packet.Translator.ReadByte();
            bytes[8] = packet.Translator.ReadByte();
            bytes[17] = packet.Translator.ReadByte();
            bytes[11] = packet.Translator.ReadByte();
            bytes[15] = packet.Translator.ReadByte();
            bytes[7] = packet.Translator.ReadByte();
            bytes[1] = packet.Translator.ReadByte();
            bytes[4] = packet.Translator.ReadByte();
            bytes[16] = packet.Translator.ReadByte();
            bytes[0] = packet.Translator.ReadByte();
            bytes[12] = packet.Translator.ReadByte();
            bytes[14] = packet.Translator.ReadByte();
            bytes[13] = packet.Translator.ReadByte();
            bytes[18] = packet.Translator.ReadByte();
            bytes[9] = packet.Translator.ReadByte();
            bytes[19] = packet.Translator.ReadByte();
            bytes[3] = packet.Translator.ReadByte();
            packet.AddValue("Proof SHA-1 Hash", Utilities.ByteArrayToHexString(bytes));
        }
    }
}
