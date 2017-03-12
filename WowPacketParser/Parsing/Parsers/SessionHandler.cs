using System;
using System.Collections.Generic;
using System.Text;
using Ionic.Zlib;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class SessionHandler
    {
        public static WowGuid LoginGuid;
        public static Dictionary<int, ZlibCodec> ZStreams = new Dictionary<int, ZlibCodec>();

        [Parser(Opcode.SMSG_AUTH_CHALLENGE, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_1a_13205)]
        public static void HandleServerAuthChallenge(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
                packet.ReadInt32("Shuffle Count");

            packet.ReadUInt32("Server Seed");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
            {
                var stateCount = ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3_11685) ? 8 : 4;
                for (var i = 0; i < stateCount; i++)
                    packet.ReadInt32("Server State", i);
            }
        }

        [Parser(Opcode.SMSG_AUTH_CHALLENGE, ClientVersionBuild.V4_0_1a_13205, ClientVersionBuild.V4_0_3_13329)]
        public static void HandleServerAuthChallenge401(Packet packet)
        {
            packet.ReadUInt32("Key pt3");
            packet.ReadUInt32("Key pt5");
            packet.ReadByte("Unk Byte");
            packet.ReadUInt32("Server Seed");
            packet.ReadUInt32("Key pt7");
            packet.ReadUInt32("Key pt6");
            packet.ReadUInt32("Key pt1");
            packet.ReadUInt32("Key pt2");
            packet.ReadUInt32("Key pt8");
            packet.ReadUInt32("Key pt4");
        }

        [Parser(Opcode.SMSG_AUTH_CHALLENGE, ClientVersionBuild.V4_0_3_13329, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleServerAuthChallenge403(Packet packet)
        {
            packet.ReadUInt32("Key pt5");
            packet.ReadUInt32("Key pt8");
            packet.ReadUInt32("Server Seed");
            packet.ReadUInt32("Key pt1");
            packet.ReadByte("Unk Byte");
            packet.ReadUInt32("Key pt7");
            packet.ReadUInt32("Key pt4");
            packet.ReadUInt32("Key pt3");
            packet.ReadUInt32("Key pt6");
            packet.ReadUInt32("Key pt2");
        }

        [Parser(Opcode.SMSG_AUTH_CHALLENGE, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleServerAuthChallenge422(Packet packet)
        {
            packet.ReadUInt32("Unk1");
            packet.ReadUInt32("Unk2");
            packet.ReadUInt32("Unk3");
            packet.ReadUInt32("Unk4");
            packet.ReadUInt32("Server Seed");
            packet.ReadByte("Unk Byte");
            packet.ReadUInt32("Unk5");
            packet.ReadUInt32("Unk6");
            packet.ReadUInt32("Unk7");
            packet.ReadUInt32("Unk8");
        }

        [Parser(Opcode.SMSG_AUTH_CHALLENGE, ClientVersionBuild.V5_0_5_16048)]
        public static void HandleServerAuthChallenge505(Packet packet)
        {
            packet.ReadUInt32("Server Seed");
            packet.ReadUInt32("Key pt1");
            packet.ReadUInt32("Key pt2");
            packet.ReadUInt32("Key pt3");
            packet.ReadUInt32("Key pt4");
            packet.ReadUInt32("Key pt5");
            packet.ReadUInt32("Key pt6");
            packet.ReadUInt32("Key pt7");
            packet.ReadUInt32("Key pt8");
            packet.ReadByte("Unk Byte");
        }

        [Parser(Opcode.SMSG_AUTH_RESPONSE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleAuthResponse(Packet packet)
        {
            var code = packet.ReadByteE<ResponseCode>("Auth Code");

            switch (code)
            {
                case ResponseCode.AUTH_OK:
                {
                    ReadAuthResponseInfo(packet);
                    break;
                }
                case ResponseCode.AUTH_WAIT_QUEUE:
                {
                    if (packet.Length <= 6)
                    {
                        ReadQueuePositionInfo(packet);
                        break;
                    }

                    ReadAuthResponseInfo(packet);
                    ReadQueuePositionInfo(packet);
                    break;
                }
            }
        }

        [Parser(Opcode.SMSG_AUTH_RESPONSE, ClientVersionBuild.V5_0_5_16048)]
        public static void HandleAuthResponse505(Packet packet)
        {
            var hasAccountData = packet.ReadBit("Has Account Data");
            var count = 0u;
            var count1 = 0u;

            if (hasAccountData)
            {
                packet.ReadBit("Unk 2");
                count = packet.ReadBits("Class Activation Count", 25);
                packet.ReadBits("Unk", 22);
                count1 = packet.ReadBits("Race Activation Count", 25);
            }

            var isQueued = packet.ReadBit("Is In Queue");
            if (isQueued)
            {
                packet.ReadBit("Unk 3");
                packet.ReadUInt32("Queue Position");
            }

            if (hasAccountData)
            {
                packet.ReadByte("Unk 5");
                packet.ReadByteE<ClientType>("Player Expansion");

                for (var i = 0; i < count; ++i)
                {
                    packet.ReadByteE<Class>("Class", i);
                    packet.ReadByteE<ClientType>("Class Expansion", i);
                }

                packet.ReadUInt32("Unk 8");
                packet.ReadUInt32("Unk 9");
                packet.ReadUInt32("Unk 10");

                for (var i = 0; i < count1; ++i)
                {
                    packet.ReadByteE<Race>("Race", i);
                    packet.ReadByteE<ClientType>("Race Expansion", i);
                }

                packet.ReadByteE<ClientType>("Account Expansion");
            }

            packet.ReadByteE<ResponseCode>("Auth Code");
        }

        public static void ReadAuthResponseInfo(Packet packet)
        {
            packet.ReadInt32("Billing Time Remaining");
            packet.ReadByteE<BillingFlag>("Billing Flags");
            packet.ReadInt32("Billing Time Rested");

            // Unknown, these two show the same as expansion payed for.
            // Eg. If account only has payed for Wotlk expansion it will show 2 for both.
            packet.ReadByteE<ClientType>("Account Expansion");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_3_13329))
                packet.ReadByteE<ClientType>("Account Expansion");
        }

        public static void ReadQueuePositionInfo(Packet packet)
        {
            packet.ReadInt32("Queue Position");
            packet.ReadBool("Realm Has Free Character Migration");
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandlePlayerLogin(Packet packet)
        {
            var guid = packet.ReadGuid("GUID");
            LoginGuid = guid;
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandlePlayerLogin422(Packet packet)
        {
            var guid = packet.StartBitStream(0, 4, 7, 1, 3, 2, 5, 6);
            packet.ParseBitStream(guid, 5, 0, 3, 4, 7, 2, 6, 1);
            packet.WriteGuid("Guid", guid);
            LoginGuid = new WowGuid64(BitConverter.ToUInt64(guid, 0));
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_3_15354)]
        public static void HandlePlayerLogin430(Packet packet)
        {
            var guid = packet.StartBitStream(0, 5, 3, 4, 7, 6, 2, 1);
            packet.ParseBitStream(guid, 4, 1, 7, 2, 6, 5, 3, 0);

            packet.WriteGuid("Guid", guid);
            LoginGuid = new WowGuid64(BitConverter.ToUInt64(guid, 0));
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V4_3_3_15354, ClientVersionBuild.V4_3_4_15595)]
        public static void HandlePlayerLogin433(Packet packet)
        {
            var guid = packet.StartBitStream(6, 7, 4, 5, 0, 1, 3, 2);
            packet.ParseBitStream(guid, 1, 4, 7, 2, 3, 6, 0, 5);
            packet.WriteGuid("Guid", guid);
            LoginGuid = new WowGuid64(BitConverter.ToUInt64(guid, 0));
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V5_1_0_16309)]
        public static void HandlePlayerLogin510(Packet packet)
        {
            var guid = packet.StartBitStream(1, 5, 0, 2, 7, 6, 3, 4);
            packet.ParseBitStream(guid, 6, 4, 3, 5, 0, 2, 7, 1);
            packet.WriteGuid("Guid", guid);
            packet.ReadSingle("Unk Float");
            LoginGuid = new WowGuid64(BitConverter.ToUInt64(guid, 0));
        }

        [Parser(Opcode.SMSG_CHARACTER_LOGIN_FAILED)]
        public static void HandleLoginFailed(Packet packet)
        {
            packet.ReadByteE<ResponseCode>("Fail reason");
        }

        [Parser(Opcode.SMSG_LOGOUT_RESPONSE)]
        public static void HandlePlayerLogoutResponse(Packet packet)
        {
            packet.ReadInt32("Reason");
            packet.ReadBool("Instant");
            // From TC:
            // Reason 1: IsInCombat
            // Reason 2: InDuel or frozen by GM
            // Reason 3: Jumping or Falling
        }

        [Parser(Opcode.SMSG_LOGOUT_COMPLETE)]
        public static void HandleLogoutComplete(Packet packet)
        {
            LoginGuid = new WowGuid64(0);
        }

        [Parser(Opcode.SMSG_CONNECT_TO, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleRedirectClient(Packet packet)
        {
            packet.ReadIPAddress("IP Address");
            packet.ReadUInt16("Port");
            packet.ReadInt32("Token");
            packet.ReadBytes("Address SHA-1 Hash", 20);
        }

        [Parser(Opcode.SMSG_CONNECT_TO, ClientVersionBuild.V4_0_6a_13623, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleRedirectClient406(Packet packet)
        {
            packet.ReadInt64("Int 64");
            packet.ReadBytes("RSA Hash", 256);
            packet.ReadByte("Byte");
            packet.ReadInt32("Int32");
        }

        [Parser(Opcode.SMSG_CONNECT_TO, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleRedirectClient422(Packet packet)
        {
            packet.ReadBytes("RSA Hash", 255);
            packet.ReadInt16("Int 16");
            packet.ReadInt32E<UnknownFlags>("Unknown int32 flag");
            packet.ReadInt64("Int 64");
        }

        [Parser(Opcode.CMSG_CONNECT_TO_FAILED, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_1_13164)]
        public static void HandleRedirectFailed(Packet packet)
        {
            packet.ReadInt32("Serial");
            if (ClientVersion.AddedInVersion(ClientType.Cataclysm))
                packet.ReadByte("Con");
        }

        [Parser(Opcode.SMSG_KICK_REASON)]
        public static void HandleKickReason(Packet packet)
        {
            packet.ReadByteE<KickReason>("Reason");

            if (!packet.CanRead())
                return;

            packet.ReadCString("Unk String");
        }

        [Parser(Opcode.SMSG_MOTD)]
        public static void HandleMessageOfTheDay(Packet packet)
        {
            var lineCount = packet.ReadInt32("Line Count");

            for (var i = 0; i < lineCount; i++)
                packet.ReadCString("Line", i);
        }

        [Parser(Opcode.SMSG_RESET_COMPRESSION_CONTEXT)]
        public static void HandleResetCompressionContext(Packet packet)
        {
            packet.ReadInt32("Unk?");
            ZStreams[packet.ConnectionIndex] = new ZlibCodec(CompressionMode.Decompress);
        }
    }
}
