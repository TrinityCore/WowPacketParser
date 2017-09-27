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
