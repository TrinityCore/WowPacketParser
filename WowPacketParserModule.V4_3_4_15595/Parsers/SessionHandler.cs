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
            packet.ReadUInt32("DosChallenge1");
            packet.ReadUInt32("DosChallenge2");
            packet.ReadUInt32("DosChallenge3");
            packet.ReadUInt32("DosChallenge4");
            packet.ReadUInt32("DosChallenge5");
            packet.ReadUInt32("DosChallenge6");
            packet.ReadUInt32("DosChallenge7");
            packet.ReadUInt32("DosChallenge8");
            packet.ReadInt32("Challenge");
            packet.ReadByte("DosZeroBits");
        }

        [Parser(Opcode.CMSG_AUTH_SESSION)]
        public static void HandleAuthSession(Packet packet)
        {
            var sha = new byte[20];
            packet.ReadUInt32("LoginServerID");
            packet.ReadUInt32("BattlegroupID");
            packet.ReadByte("LoginServerType");
            sha[10] = packet.ReadByte();
            sha[18] = packet.ReadByte();
            sha[12] = packet.ReadByte();
            sha[5] = packet.ReadByte();
            packet.ReadInt64("DosResponse");
            sha[15] = packet.ReadByte();
            sha[9] = packet.ReadByte();
            sha[19] = packet.ReadByte();
            sha[4] = packet.ReadByte();
            sha[7] = packet.ReadByte();
            sha[16] = packet.ReadByte();
            sha[3] = packet.ReadByte();
            packet.ReadInt16E<ClientVersionBuild>("Build");
            sha[8] = packet.ReadByte();
            packet.ReadUInt32("RealmID");
            packet.ReadByte("BuildType");
            sha[17] = packet.ReadByte();
            sha[6] = packet.ReadByte();
            sha[0] = packet.ReadByte();
            sha[1] = packet.ReadByte();
            sha[11] = packet.ReadByte();
            packet.ReadUInt32("LocalChallenge");
            sha[2] = packet.ReadByte();
            packet.ReadUInt32("RegionID");
            sha[14] = packet.ReadByte();
            sha[13] = packet.ReadByte();

            var addons = new Packet(packet.ReadBytes(packet.ReadInt32()), packet.Opcode, packet.Time, packet.Direction,
                packet.Number, packet.Writer, packet.FileName);
            CoreParsers.AddonHandler.ReadClientAddonsList(addons);
            addons.ClosePacket(false);

            packet.ReadBit("UseIPv6");
            var size = (int)packet.ReadBits(12);
            packet.ReadBytesString("Account", size);
            packet.AddValue("Digest", Utilities.ByteArrayToHexString(sha));
        }

        [Parser(Opcode.SMSG_AUTH_RESPONSE)]
        public static void HandleAuthResponse(Packet packet)
        {
            var isQueued = packet.ReadBit("HasWaitInfo");
            if (isQueued)
                packet.ReadBit("WaitInfoHasFCM");

            var hasAccountInfo = packet.ReadBit("HasSuccessInfo");

            if (hasAccountInfo)
            {
                packet.ReadInt32("TimeRemain");
                packet.ReadByteE<ClientType>("ActiveExpansionLevel");
                packet.ReadInt32("TimeSecondsUntilPCKick");
                packet.ReadByteE<ClientType>("AccountExpansionLevel");
                packet.ReadInt32("TimeRested");
                packet.ReadByteE<BillingFlag>("TimeOptions");
            }

            packet.ReadByteE<ResponseCode>("Result");

            if (isQueued)
                packet.ReadInt32("WaitInfoWaitCount");
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN)]
        public static void HandlePlayerLogin(Packet packet)
        {
            var guid = packet.StartBitStream(2, 3, 0, 6, 4, 5, 1, 7);
            packet.ParseBitStream(guid, 2, 7, 0, 3, 5, 6, 1, 4);
            packet.Holder.PlayerLogin = new() { PlayerGuid = packet.WriteGuid("Guid", guid) };
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
            packet.ReadUInt64("Key");
            packet.ReadUInt64("DosResponse");
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
