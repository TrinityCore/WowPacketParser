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
                packet.Translator.ReadInt32("Shuffle Count");

            packet.Translator.ReadUInt32("Server Seed");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
            {
                var stateCount = ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3_11685) ? 8 : 4;
                for (var i = 0; i < stateCount; i++)
                    packet.Translator.ReadInt32("Server State", i);
            }
        }

        [Parser(Opcode.SMSG_AUTH_CHALLENGE, ClientVersionBuild.V4_0_1a_13205, ClientVersionBuild.V4_0_3_13329)]
        public static void HandleServerAuthChallenge401(Packet packet)
        {
            packet.Translator.ReadUInt32("Key pt3");
            packet.Translator.ReadUInt32("Key pt5");
            packet.Translator.ReadByte("Unk Byte");
            packet.Translator.ReadUInt32("Server Seed");
            packet.Translator.ReadUInt32("Key pt7");
            packet.Translator.ReadUInt32("Key pt6");
            packet.Translator.ReadUInt32("Key pt1");
            packet.Translator.ReadUInt32("Key pt2");
            packet.Translator.ReadUInt32("Key pt8");
            packet.Translator.ReadUInt32("Key pt4");
        }

        [Parser(Opcode.SMSG_AUTH_CHALLENGE, ClientVersionBuild.V4_0_3_13329, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleServerAuthChallenge403(Packet packet)
        {
            packet.Translator.ReadUInt32("Key pt5");
            packet.Translator.ReadUInt32("Key pt8");
            packet.Translator.ReadUInt32("Server Seed");
            packet.Translator.ReadUInt32("Key pt1");
            packet.Translator.ReadByte("Unk Byte");
            packet.Translator.ReadUInt32("Key pt7");
            packet.Translator.ReadUInt32("Key pt4");
            packet.Translator.ReadUInt32("Key pt3");
            packet.Translator.ReadUInt32("Key pt6");
            packet.Translator.ReadUInt32("Key pt2");
        }

        [Parser(Opcode.SMSG_AUTH_CHALLENGE, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleServerAuthChallenge422(Packet packet)
        {
            packet.Translator.ReadUInt32("Unk1");
            packet.Translator.ReadUInt32("Unk2");
            packet.Translator.ReadUInt32("Unk3");
            packet.Translator.ReadUInt32("Unk4");
            packet.Translator.ReadUInt32("Server Seed");
            packet.Translator.ReadByte("Unk Byte");
            packet.Translator.ReadUInt32("Unk5");
            packet.Translator.ReadUInt32("Unk6");
            packet.Translator.ReadUInt32("Unk7");
            packet.Translator.ReadUInt32("Unk8");
        }

        [Parser(Opcode.SMSG_AUTH_CHALLENGE, ClientVersionBuild.V5_0_5_16048)]
        public static void HandleServerAuthChallenge505(Packet packet)
        {
            packet.Translator.ReadUInt32("Server Seed");
            packet.Translator.ReadUInt32("Key pt1");
            packet.Translator.ReadUInt32("Key pt2");
            packet.Translator.ReadUInt32("Key pt3");
            packet.Translator.ReadUInt32("Key pt4");
            packet.Translator.ReadUInt32("Key pt5");
            packet.Translator.ReadUInt32("Key pt6");
            packet.Translator.ReadUInt32("Key pt7");
            packet.Translator.ReadUInt32("Key pt8");
            packet.Translator.ReadByte("Unk Byte");
        }

        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleAuthSession(Packet packet)
        {
            // Do not overwrite version after Handler was initialized
            packet.Translator.ReadInt32E<ClientVersionBuild>("Client Build");

            packet.Translator.ReadInt32("Unk Int32 1");
            packet.Translator.ReadCString("Account");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                packet.Translator.ReadInt32("Unk Int32 2");

            packet.Translator.ReadUInt32("Client Seed");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_5a_12340))
            {
                // Some numbers about selected realm
                packet.Translator.ReadInt32("Unk Int32 3");
                packet.Translator.ReadInt32("Unk Int32 4");
                packet.Translator.ReadInt32("Unk Int32 5");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
                packet.Translator.ReadInt64("Unk Int64");

            packet.Translator.ReadBytes("Proof SHA-1 Hash", 20);

            AddonHandler.ReadClientAddonsList(packet);
        }

        //[Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V4_2_0_14333)]
        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleAuthSession422(Packet packet)
        {
            packet.Translator.ReadByte("Byte");
            packet.Translator.ReadByte("Byte");
            packet.Translator.ReadInt32("Int32");
            packet.Translator.ReadInt32("Int32");
            packet.Translator.ReadByte("Byte");
            packet.Translator.ReadByte("Byte");
            packet.Translator.ReadByte("Byte");

            packet.Translator.ReadByte("Byte");
            packet.Translator.ReadByte("Byte");
            packet.Translator.ReadByte("Byte");
            packet.Translator.ReadInt16E<ClientVersionBuild>("Client Build");

            packet.Translator.ReadByte("Byte");
            packet.Translator.ReadByte("Byte");
            packet.Translator.ReadByte("Byte");
            packet.Translator.ReadByte("Byte");

            packet.Translator.ReadInt32("Int32");
            packet.Translator.ReadByte("Byte");
            packet.Translator.ReadByte("Byte");
            packet.Translator.ReadByte("Byte");
            packet.Translator.ReadByte("Byte");

            packet.Translator.ReadInt32("Int32");
            packet.Translator.ReadByte("Byte");

            packet.Translator.ReadInt32("Int32");
            packet.Translator.ReadByte("Byte");

            packet.Translator.ReadInt32("Int32");
            packet.Translator.ReadByte("Byte");

            packet.Translator.ReadInt32("Int32");
            packet.Translator.ReadInt32("Int32");

            packet.Translator.ReadCString("Account name");
            packet.Translator.ReadInt32("Int32");

            AddonHandler.ReadClientAddonsList(packet);
        }

        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_2_15211)]
        public static void HandleAuthSession430(Packet packet)
        {
            packet.Translator.ReadInt32("Int32");
            packet.Translator.ReadByte("Digest (1)");
            packet.Translator.ReadInt64("Int64");
            packet.Translator.ReadInt32("Int32");
            packet.Translator.ReadByte("Digest (2)");
            packet.Translator.ReadInt32("Int32");
            packet.Translator.ReadByte("Digest (3)");

            packet.Translator.ReadInt32("Int32");
            for (var i = 0; i < 7; i++)
                packet.Translator.ReadByte("Digest (4)", i);

            packet.Translator.ReadInt16E<ClientVersionBuild>("Client Build");

            for (var i = 0; i < 8; i++)
                packet.Translator.ReadByte("Digest (5)", i);

            packet.Translator.ReadByte("Unk Byte");
            packet.Translator.ReadByte("Unk Byte");

            packet.Translator.ReadUInt32("Client Seed");

            for (var i = 0; i < 2; i++)
                packet.Translator.ReadByte("Digest (6)", i);

            var pkt = new Packet(packet.Translator.ReadBytes(packet.Translator.ReadInt32()), packet.Opcode, packet.Time, packet.Direction,
                packet.Number, packet.Formatter, packet.FileName);
            AddonHandler.ReadClientAddonsList(pkt);
            pkt.ClosePacket(false);

            packet.Translator.ReadByte("Mask"); // TODO: Seems to affect how the size is read
            var size = (packet.Translator.ReadByte() >> 4);
            packet.AddValue("Size", size);
            packet.AddValue("Account name", Encoding.UTF8.GetString(packet.Translator.ReadBytes(size)));
        }

        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V4_3_2_15211, ClientVersionBuild.V4_3_3_15354)]
        public static void HandleAuthSession432(Packet packet)
        {
            var sha = new byte[20];
            packet.Translator.ReadInt32("Int32");
            sha[12] = packet.Translator.ReadByte();
            packet.Translator.ReadInt32("Int32");
            packet.Translator.ReadInt32("Int32");
            sha[0] = packet.Translator.ReadByte();
            sha[2] = packet.Translator.ReadByte();
            sha[18] = packet.Translator.ReadByte();
            sha[7] = packet.Translator.ReadByte();
            sha[9] = packet.Translator.ReadByte();
            sha[19] = packet.Translator.ReadByte();
            sha[17] = packet.Translator.ReadByte();
            sha[6] = packet.Translator.ReadByte();
            sha[11] = packet.Translator.ReadByte();

            packet.Translator.ReadInt16E<ClientVersionBuild>("Client Build");

            sha[15] = packet.Translator.ReadByte();

            packet.Translator.ReadInt64("Int64");
            packet.Translator.ReadByte("Unk Byte");
            packet.Translator.ReadByte("Unk Byte");
            sha[3] = packet.Translator.ReadByte();
            sha[10] = packet.Translator.ReadByte();

            packet.Translator.ReadUInt32("Client Seed");

            sha[16] = packet.Translator.ReadByte();
            sha[4] = packet.Translator.ReadByte();
            packet.Translator.ReadInt32("Int32");
            sha[14] = packet.Translator.ReadByte();
            sha[8] = packet.Translator.ReadByte();
            sha[5] = packet.Translator.ReadByte();
            sha[1] = packet.Translator.ReadByte();
            sha[13] = packet.Translator.ReadByte();

            var pkt = new Packet(packet.Translator.ReadBytes(packet.Translator.ReadInt32()), packet.Opcode, packet.Time, packet.Direction,
                packet.Number, packet.Formatter, packet.FileName);
            AddonHandler.ReadClientAddonsList(pkt);
            pkt.ClosePacket(false);

            var highBits = packet.Translator.ReadByte() << 5;
            var lowBits = packet.Translator.ReadByte() >> 3;
            var size = lowBits | highBits;
            packet.AddValue("Size", size);
            packet.AddValue("Account name", Encoding.UTF8.GetString(packet.Translator.ReadBytes(size)));
            packet.AddValue("Proof SHA-1 Hash", Utilities.ByteArrayToHexString(sha));
        }

        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V5_0_5_16048)]
        public static void HandleAuthSession505(Packet packet)
        {
            var sha = new byte[20];
            packet.Translator.ReadUInt32("UInt32 2");//18
            sha[2] = packet.Translator.ReadByte();//24
            sha[15] = packet.Translator.ReadByte();//37
            sha[12] = packet.Translator.ReadByte();//34
            sha[11] = packet.Translator.ReadByte();//33
            sha[10] = packet.Translator.ReadByte();//32
            sha[9] = packet.Translator.ReadByte();//31
            packet.Translator.ReadByte("Unk Byte");//20
            packet.Translator.ReadUInt32("Client seed");//14
            sha[16] = packet.Translator.ReadByte();//38
            sha[5] = packet.Translator.ReadByte();//27
            packet.Translator.ReadInt16E<ClientVersionBuild>("Client Build");//34
            packet.Translator.ReadUInt32("UInt32 4");//16
            sha[18] = packet.Translator.ReadByte();//40
            sha[0] = packet.Translator.ReadByte();//22
            sha[13] = packet.Translator.ReadByte();//35
            sha[3] = packet.Translator.ReadByte();//25
            sha[14] = packet.Translator.ReadByte();//36
            packet.Translator.ReadByte("Unk Byte");//21
            sha[8] = packet.Translator.ReadByte();//30
            sha[7] = packet.Translator.ReadByte();//29
            packet.Translator.ReadUInt32("UInt32 3");//15
            sha[4] = packet.Translator.ReadByte();//26
            packet.Translator.ReadInt64("Int64");//12,13
            sha[17] = packet.Translator.ReadByte();//39
            sha[19] = packet.Translator.ReadByte();//41
            packet.Translator.ReadUInt32("UInt32 1");//4
            sha[6] = packet.Translator.ReadByte();//28
            sha[1] = packet.Translator.ReadByte();//23

            var addons = new Packet(packet.Translator.ReadBytes(packet.Translator.ReadInt32()), packet.Opcode, packet.Time, packet.Direction,
                packet.Number, packet.Formatter, packet.FileName);
            AddonHandler.ReadClientAddonsList(addons);
            addons.ClosePacket(false);

            packet.Translator.ReadBit("Unk bit");
            var size = (int)packet.Translator.ReadBits(12);
            packet.AddValue("Account name", Encoding.UTF8.GetString(packet.Translator.ReadBytes(size)));
            packet.AddValue("Proof SHA-1 Hash", Utilities.ByteArrayToHexString(sha));
        }

        [Parser(Opcode.SMSG_AUTH_RESPONSE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleAuthResponse(Packet packet)
        {
            var code = packet.Translator.ReadByteE<ResponseCode>("Auth Code");

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
            var hasAccountData = packet.Translator.ReadBit("Has Account Data");
            var count = 0u;
            var count1 = 0u;

            if (hasAccountData)
            {
                packet.Translator.ReadBit("Unk 2");
                count = packet.Translator.ReadBits("Class Activation Count", 25);
                packet.Translator.ReadBits("Unk", 22);
                count1 = packet.Translator.ReadBits("Race Activation Count", 25);
            }

            var isQueued = packet.Translator.ReadBit("Is In Queue");
            if (isQueued)
            {
                packet.Translator.ReadBit("Unk 3");
                packet.Translator.ReadUInt32("Queue Position");
            }

            if (hasAccountData)
            {
                packet.Translator.ReadByte("Unk 5");
                packet.Translator.ReadByteE<ClientType>("Player Expansion");

                for (var i = 0; i < count; ++i)
                {
                    packet.Translator.ReadByteE<Class>("Class", i);
                    packet.Translator.ReadByteE<ClientType>("Class Expansion", i);
                }

                packet.Translator.ReadUInt32("Unk 8");
                packet.Translator.ReadUInt32("Unk 9");
                packet.Translator.ReadUInt32("Unk 10");

                for (var i = 0; i < count1; ++i)
                {
                    packet.Translator.ReadByteE<Race>("Race", i);
                    packet.Translator.ReadByteE<ClientType>("Race Expansion", i);
                }

                packet.Translator.ReadByteE<ClientType>("Account Expansion");
            }

            packet.Translator.ReadByteE<ResponseCode>("Auth Code");
        }

        public static void ReadAuthResponseInfo(Packet packet)
        {
            packet.Translator.ReadInt32("Billing Time Remaining");
            packet.Translator.ReadByteE<BillingFlag>("Billing Flags");
            packet.Translator.ReadInt32("Billing Time Rested");

            // Unknown, these two show the same as expansion payed for.
            // Eg. If account only has payed for Wotlk expansion it will show 2 for both.
            packet.Translator.ReadByteE<ClientType>("Account Expansion");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_3_13329))
                packet.Translator.ReadByteE<ClientType>("Account Expansion");
        }

        public static void ReadQueuePositionInfo(Packet packet)
        {
            packet.Translator.ReadInt32("Queue Position");
            packet.Translator.ReadBool("Realm Has Free Character Migration");
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandlePlayerLogin(Packet packet)
        {
            var guid = packet.Translator.ReadGuid("GUID");
            LoginGuid = guid;
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandlePlayerLogin422(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(0, 4, 7, 1, 3, 2, 5, 6);
            packet.Translator.ParseBitStream(guid, 5, 0, 3, 4, 7, 2, 6, 1);
            packet.Translator.WriteGuid("Guid", guid);
            LoginGuid = new WowGuid64(BitConverter.ToUInt64(guid, 0));
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_3_15354)]
        public static void HandlePlayerLogin430(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(0, 5, 3, 4, 7, 6, 2, 1);
            packet.Translator.ParseBitStream(guid, 4, 1, 7, 2, 6, 5, 3, 0);

            packet.Translator.WriteGuid("Guid", guid);
            LoginGuid = new WowGuid64(BitConverter.ToUInt64(guid, 0));
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V4_3_3_15354, ClientVersionBuild.V4_3_4_15595)]
        public static void HandlePlayerLogin433(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(6, 7, 4, 5, 0, 1, 3, 2);
            packet.Translator.ParseBitStream(guid, 1, 4, 7, 2, 3, 6, 0, 5);
            packet.Translator.WriteGuid("Guid", guid);
            LoginGuid = new WowGuid64(BitConverter.ToUInt64(guid, 0));
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V5_1_0_16309)]
        public static void HandlePlayerLogin510(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(1, 5, 0, 2, 7, 6, 3, 4);
            packet.Translator.ParseBitStream(guid, 6, 4, 3, 5, 0, 2, 7, 1);
            packet.Translator.WriteGuid("Guid", guid);
            packet.Translator.ReadSingle("Unk Float");
            LoginGuid = new WowGuid64(BitConverter.ToUInt64(guid, 0));
        }

        [Parser(Opcode.SMSG_CHARACTER_LOGIN_FAILED)]
        public static void HandleLoginFailed(Packet packet)
        {
            packet.Translator.ReadByteE<ResponseCode>("Fail reason");
        }

        [Parser(Opcode.SMSG_LOGOUT_RESPONSE)]
        public static void HandlePlayerLogoutResponse(Packet packet)
        {
            packet.Translator.ReadInt32("Reason");
            packet.Translator.ReadBool("Instant");
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
            packet.Translator.ReadIPAddress("IP Address");
            packet.Translator.ReadUInt16("Port");
            packet.Translator.ReadInt32("Token");
            packet.Translator.ReadBytes("Address SHA-1 Hash", 20);
        }

        [Parser(Opcode.SMSG_CONNECT_TO, ClientVersionBuild.V4_0_6a_13623, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleRedirectClient406(Packet packet)
        {
            packet.Translator.ReadInt64("Int 64");
            packet.Translator.ReadBytes("RSA Hash", 256);
            packet.Translator.ReadByte("Byte");
            packet.Translator.ReadInt32("Int32");
        }

        [Parser(Opcode.SMSG_CONNECT_TO, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleRedirectClient422(Packet packet)
        {
            packet.Translator.ReadBytes("RSA Hash", 255);
            packet.Translator.ReadInt16("Int 16");
            packet.Translator.ReadInt32E<UnknownFlags>("Unknown int32 flag");
            packet.Translator.ReadInt64("Int 64");
        }

        [Parser(Opcode.CMSG_CONNECT_TO_FAILED, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_1_13164)]
        public static void HandleRedirectFailed(Packet packet)
        {
            packet.Translator.ReadInt32("Serial");
            if (ClientVersion.AddedInVersion(ClientType.Cataclysm))
                packet.Translator.ReadByte("Con");
        }

        [Parser(Opcode.CMSG_AUTH_CONTINUED_SESSION, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleRedirectionAuthProof(Packet packet)
        {
            packet.Translator.ReadCString("Account");
            packet.Translator.ReadInt64("Unk Int64"); // Key or DosResponse
            packet.Translator.ReadBytes("Proof SHA-1 Hash", 20);
        }

        [Parser(Opcode.CMSG_AUTH_CONTINUED_SESSION, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleRedirectionAuthProof422(Packet packet)
        {
            var bytes = new byte[20];
            bytes[0] = packet.Translator.ReadByte();
            bytes[12] = packet.Translator.ReadByte();
            bytes[3] = packet.Translator.ReadByte();
            bytes[17] = packet.Translator.ReadByte();
            bytes[11] = packet.Translator.ReadByte();
            bytes[13] = packet.Translator.ReadByte();
            bytes[5] = packet.Translator.ReadByte();
            bytes[9] = packet.Translator.ReadByte();
            bytes[6] = packet.Translator.ReadByte();
            bytes[19] = packet.Translator.ReadByte();
            bytes[15] = packet.Translator.ReadByte();
            bytes[18] = packet.Translator.ReadByte();
            bytes[8] = packet.Translator.ReadByte();
            packet.Translator.ReadInt64("Unk long 1"); // Key or DosResponse
            bytes[2] = packet.Translator.ReadByte();
            bytes[1] = packet.Translator.ReadByte();
            packet.Translator.ReadInt64("Unk long 2"); // Key or DosResponse
            bytes[7] = packet.Translator.ReadByte();
            bytes[4] = packet.Translator.ReadByte();
            bytes[16] = packet.Translator.ReadByte();
            bytes[14] = packet.Translator.ReadByte();
            bytes[10] = packet.Translator.ReadByte();
            packet.AddValue("Proof RSA Hash", Utilities.ByteArrayToHexString(bytes));
        }

        [Parser(Opcode.SMSG_KICK_REASON)]
        public static void HandleKickReason(Packet packet)
        {
            packet.Translator.ReadByteE<KickReason>("Reason");

            if (!packet.CanRead())
                return;

            packet.Translator.ReadCString("Unk String");
        }

        [Parser(Opcode.SMSG_MOTD)]
        public static void HandleMessageOfTheDay(Packet packet)
        {
            var lineCount = packet.Translator.ReadInt32("Line Count");

            for (var i = 0; i < lineCount; i++)
                packet.Translator.ReadCString("Line", i);
        }

        [Parser(Opcode.SMSG_RESET_COMPRESSION_CONTEXT)]
        public static void HandleResetCompressionContext(Packet packet)
        {
            packet.Translator.ReadInt32("Unk?");
            ZStreams[packet.ConnectionIndex] = new ZlibCodec(CompressionMode.Decompress);
        }
    }
}
