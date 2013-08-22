using System;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using Guid=WowPacketParser.Misc.Guid;
using Ionic.Zlib;
using System.Collections.Generic;

namespace WowPacketParser.Parsing.Parsers
{
    public static class SessionHandler
    {
        public static Guid LoginGuid;
        public static Dictionary<int, ZlibCodec> z_streams = new Dictionary<int, ZlibCodec>();

        [Parser(Opcode.SMSG_AUTH_CHALLENGE, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_1a_13205)]
        public static void HandleServerAuthChallenge(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
                packet.ReadInt32("Shuffle Count");

            packet.ReadUInt32("Server Seed");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
            {
                var SStateCount = ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3_11685) ? 8 : 4;
                for (var i = 0; i < SStateCount; i++)
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

        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleAuthSession(Packet packet)
        {
            // Do not overwrite version after Handler was initialized
            packet.ReadEnum<ClientVersionBuild>("Client Build", TypeCode.Int32);

            packet.ReadInt32("Unk Int32 1");
            packet.ReadCString("Account");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                packet.ReadInt32("Unk Int32 2");

            packet.ReadUInt32("Client Seed");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_5a_12340))
            {
                // Some numbers about selected realm
                packet.ReadInt32("Unk Int32 3");
                packet.ReadInt32("Unk Int32 4");
                packet.ReadInt32("Unk Int32 5");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
                packet.ReadInt64("Unk Int64");

            packet.WriteLine("Proof SHA-1 Hash: " + Utilities.ByteArrayToHexString(packet.ReadBytes(20)));

            AddonHandler.ReadClientAddonsList(ref packet);
        }

        //[Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V4_2_0_14333)]
        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleAuthSession422(Packet packet)
        {
            packet.ReadByte("Byte");
            packet.ReadByte("Byte");
            packet.ReadInt32("Int32");
            packet.ReadInt32("Int32");
            packet.ReadByte("Byte");
            packet.ReadByte("Byte");
            packet.ReadByte("Byte");

            packet.ReadByte("Byte");
            packet.ReadByte("Byte");
            packet.ReadByte("Byte");
            packet.ReadEnum<ClientVersionBuild>("Client Build", TypeCode.Int16);

            packet.ReadByte("Byte");
            packet.ReadByte("Byte");
            packet.ReadByte("Byte");
            packet.ReadByte("Byte");

            packet.ReadInt32("Int32");
            packet.ReadByte("Byte");
            packet.ReadByte("Byte");
            packet.ReadByte("Byte");
            packet.ReadByte("Byte");

            packet.ReadInt32("Int32");
            packet.ReadByte("Byte");

            packet.ReadInt32("Int32");
            packet.ReadByte("Byte");

            packet.ReadInt32("Int32");
            packet.ReadByte("Byte");

            packet.ReadInt32("Int32");
            packet.ReadInt32("Int32");

            packet.ReadCString("Account name");
            packet.ReadInt32("Int32");

            AddonHandler.ReadClientAddonsList(ref packet);
        }

        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_2_15211)]
        public static void HandleAuthSession430(Packet packet)
        {
            packet.ReadInt32("Int32");
            packet.ReadByte("Digest (1)");
            packet.ReadInt64("Int64");
            packet.ReadInt32("Int32");
            packet.ReadByte("Digest (2)");
            packet.ReadInt32("Int32");
            packet.ReadByte("Digest (3)");

            packet.ReadInt32("Int32");
            for (var i = 0; i < 7; i++)
                packet.ReadByte("Digest (4)", i);

            packet.ReadEnum<ClientVersionBuild>("Client Build", TypeCode.Int16);

            for (var i = 0; i < 8; i++)
                packet.ReadByte("Digest (5)", i);

            packet.ReadByte("Unk Byte");
            packet.ReadByte("Unk Byte");

            packet.ReadUInt32("Client Seed");

            for (var i = 0; i < 2; i++)
                packet.ReadByte("Digest (6)", i);

            using (var pkt = new Packet(packet.ReadBytes(packet.ReadInt32()), packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName))
            {
                var pkt2 = pkt;
                AddonHandler.ReadClientAddonsList(ref pkt2);
            }
            packet.ReadByte("Mask"); // TODO: Seems to affect how the size is read
            var size = (packet.ReadByte() >> 4);
            packet.WriteLine("Size: " + size);
            packet.WriteLine("Account name: {0}", Encoding.UTF8.GetString(packet.ReadBytes(size)));
        }

        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V4_3_2_15211, ClientVersionBuild.V4_3_3_15354)]
        public static void HandleAuthSession432(Packet packet)
        {
            var sha = new byte[20];
            packet.ReadInt32("Int32");
            sha[12] = packet.ReadByte();
            packet.ReadInt32("Int32");
            packet.ReadInt32("Int32");
            sha[0] = packet.ReadByte();
            sha[2] = packet.ReadByte();
            sha[18] = packet.ReadByte();
            sha[7] = packet.ReadByte();
            sha[9] = packet.ReadByte();
            sha[19] = packet.ReadByte();
            sha[17] = packet.ReadByte();
            sha[6] = packet.ReadByte();
            sha[11] = packet.ReadByte();

            packet.ReadEnum<ClientVersionBuild>("Client Build", TypeCode.Int16);

            sha[15] = packet.ReadByte();

            packet.ReadInt64("Int64");
            packet.ReadByte("Unk Byte");
            packet.ReadByte("Unk Byte");
            sha[3] = packet.ReadByte();
            sha[10] = packet.ReadByte();

            packet.ReadUInt32("Client Seed");

            sha[16] = packet.ReadByte();
            sha[4] = packet.ReadByte();
            packet.ReadInt32("Int32");
            sha[14] = packet.ReadByte();
            sha[8] = packet.ReadByte();
            sha[5] = packet.ReadByte();
            sha[1] = packet.ReadByte();
            sha[13] = packet.ReadByte();

            using (var pkt = new Packet(packet.ReadBytes(packet.ReadInt32()), packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName))
            {
                var pkt2 = pkt;
                AddonHandler.ReadClientAddonsList(ref pkt2);
            }

            var highBits = packet.ReadByte() << 5;
            var lowBits = packet.ReadByte() >> 3;
            var size = lowBits | highBits;
            packet.WriteLine("Size: " + size);
            packet.WriteLine("Account name: {0}", Encoding.UTF8.GetString(packet.ReadBytes(size)));
            packet.WriteLine("Proof SHA-1 Hash: " + Utilities.ByteArrayToHexString(sha));
        }

        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V5_0_5_16048)]
        public static void HandleAuthSession505(Packet packet)
        {
            var sha = new byte[20];
            packet.ReadUInt32("UInt32 2");//18
            sha[2] = packet.ReadByte();//24
            sha[15] = packet.ReadByte();//37
            sha[12] = packet.ReadByte();//34
            sha[11] = packet.ReadByte();//33
            sha[10] = packet.ReadByte();//32
            sha[9] = packet.ReadByte();//31
            packet.ReadByte("Unk Byte");//20
            packet.ReadUInt32("Client seed");//14
            sha[16] = packet.ReadByte();//38
            sha[5] = packet.ReadByte();//27
            packet.ReadEnum<ClientVersionBuild>("Client Build", TypeCode.Int16);//34
            packet.ReadUInt32("UInt32 4");//16
            sha[18] = packet.ReadByte();//40
            sha[0] = packet.ReadByte();//22
            sha[13] = packet.ReadByte();//35
            sha[3] = packet.ReadByte();//25
            sha[14] = packet.ReadByte();//36
            packet.ReadByte("Unk Byte");//21
            sha[8] = packet.ReadByte();//30
            sha[7] = packet.ReadByte();//29
            packet.ReadUInt32("UInt32 3");//15
            sha[4] = packet.ReadByte();//26
            packet.ReadInt64("Int64");//12,13
            sha[17] = packet.ReadByte();//39
            sha[19] = packet.ReadByte();//41
            packet.ReadUInt32("UInt32 1");//4
            sha[6] = packet.ReadByte();//28
            sha[1] = packet.ReadByte();//23

            using (var addons = new Packet(packet.ReadBytes(packet.ReadInt32()), packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName))
            {
                var pkt2 = addons;
                AddonHandler.ReadClientAddonsList(ref pkt2);
            }

            packet.ReadBit("Unk bit");
            var size = (int)packet.ReadBits(12);
            packet.WriteLine("Account name: {0}", Encoding.UTF8.GetString(packet.ReadBytes(size)));
            packet.WriteLine("Proof SHA-1 Hash: " + Utilities.ByteArrayToHexString(sha));
        }

        [Parser(Opcode.SMSG_AUTH_RESPONSE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleAuthResponse(Packet packet)
        {
            var code = packet.ReadEnum<ResponseCode>("Auth Code", TypeCode.Byte);

            switch (code)
            {
                case ResponseCode.AUTH_OK:
                {
                    ReadAuthResponseInfo(ref packet);
                    break;
                }
                case ResponseCode.AUTH_WAIT_QUEUE:
                {
                    if (packet.Length <= 6)
                    {
                        ReadQueuePositionInfo(ref packet);
                        break;
                    }

                    ReadAuthResponseInfo(ref packet);
                    ReadQueuePositionInfo(ref packet);
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
                packet.ReadEnum<ClientType>("Player Expansion", TypeCode.Byte);

                for (var i = 0; i < count; ++i)
                {
                    packet.ReadEnum<Class>("Class", TypeCode.Byte, i);
                    packet.ReadEnum<ClientType>("Class Expansion", TypeCode.Byte, i);
                }

                packet.ReadUInt32("Unk 8");
                packet.ReadUInt32("Unk 9");
                packet.ReadUInt32("Unk 10");

                for (var i = 0; i < count1; ++i)
                {
                    packet.ReadEnum<Race>("Race", TypeCode.Byte, i);
                    packet.ReadEnum<ClientType>("Race Expansion", TypeCode.Byte, i);
                }

                packet.ReadEnum<ClientType>("Account Expansion", TypeCode.Byte);
            }

            packet.ReadEnum<ResponseCode>("Auth Code", TypeCode.Byte);
        }

        public static void ReadAuthResponseInfo(ref Packet packet)
        {
            packet.ReadInt32("Billing Time Remaining");
            packet.ReadEnum<BillingFlag>("Billing Flags", TypeCode.Byte);
            packet.ReadInt32("Billing Time Rested");

            // Unknown, these two show the same as expansion payed for.
            // Eg. If account only has payed for Wotlk expansion it will show 2 for both.
            packet.ReadEnum<ClientType>("Account Expansion", TypeCode.Byte);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_3_13329))
                packet.ReadEnum<ClientType>("Account Expansion", TypeCode.Byte);
        }

        public static void ReadQueuePositionInfo(ref Packet packet)
        {
            packet.ReadInt32("Queue Position");
            packet.ReadBoolean("Realm Has Free Character Migration");
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
            LoginGuid = new Guid(BitConverter.ToUInt64(guid, 0));
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_3_15354)]
        public static void HandlePlayerLogin430(Packet packet)
        {
            var guid = packet.StartBitStream(0, 5, 3, 4, 7, 6, 2, 1);
            packet.ParseBitStream(guid, 4, 1, 7, 2, 6, 5, 3, 0);

            packet.WriteGuid("Guid", guid);
            LoginGuid = new Guid(BitConverter.ToUInt64(guid, 0));
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V4_3_3_15354, ClientVersionBuild.V4_3_4_15595)]
        public static void HandlePlayerLogin433(Packet packet)
        {
            var guid = packet.StartBitStream(6, 7, 4, 5, 0, 1, 3, 2);
            packet.ParseBitStream(guid, 1, 4, 7, 2, 3, 6, 0, 5);
            packet.WriteGuid("Guid", guid);
            LoginGuid = new Guid(BitConverter.ToUInt64(guid, 0));
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V5_1_0_16309)]
        public static void HandlePlayerLogin510(Packet packet)
        {
            var guid = packet.StartBitStream(1, 5, 0, 2, 7, 6, 3, 4);
            packet.ParseBitStream(guid, 6, 4, 3, 5, 0, 2, 7, 1);
            packet.WriteGuid("Guid", guid);
            packet.ReadSingle("Unk Float");
            LoginGuid = new Guid(BitConverter.ToUInt64(guid, 0));
        }

        [Parser(Opcode.SMSG_CHARACTER_LOGIN_FAILED)]
        public static void HandleLoginFailed(Packet packet)
        {
            packet.ReadEnum<ResponseCode>("Fail reason", TypeCode.Byte);
        }

        [Parser(Opcode.SMSG_LOGOUT_RESPONSE)]
        public static void HandlePlayerLogoutResponse(Packet packet)
        {
            packet.ReadInt32("Reason");
            packet.ReadBoolean("Instant");
            // From TC:
            // Reason 1: IsInCombat
            // Reason 2: InDuel or frozen by GM
            // Reason 3: Jumping or Falling
        }

        [Parser(Opcode.SMSG_LOGOUT_COMPLETE)]
        public static void HandleLogoutComplete(Packet packet)
        {
            LoginGuid = new Guid(0);
        }

        [Parser(Opcode.CMSG_CONNECT_TO_FAILED)]
        public static void HandleConnectToFailed(Packet packet)
        {
            packet.WriteLine("IP Address: {0}", packet.ReadIPAddress());
            packet.ReadByte("Reason?");
        }

        [Parser(Opcode.SMSG_REDIRECT_CLIENT, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleRedirectClient(Packet packet)
        {
            var ip = packet.ReadIPAddress();
            packet.WriteLine("IP Address: {0}", ip);
            packet.ReadUInt16("Port");
            packet.ReadInt32("Token");
            var hash = packet.ReadBytes(20);
            packet.WriteLine("Address SHA-1 Hash: {0}", Utilities.ByteArrayToHexString(hash));
        }

        [Parser(Opcode.SMSG_REDIRECT_CLIENT, ClientVersionBuild.V4_0_6a_13623, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleRedirectClient406(Packet packet)
        {
            packet.ReadInt64("Int 64");
            var hash = packet.ReadBytes(256);
            packet.WriteLine("RSA Hash: {0}", Utilities.ByteArrayToHexString(hash));
            packet.ReadByte("Byte");
            packet.ReadInt32("Int32");
        }

        [Parser(Opcode.SMSG_REDIRECT_CLIENT, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleRedirectClient422(Packet packet)
        {
            var hash = packet.ReadBytes(255);
            packet.WriteLine("RSA Hash: {0}", Utilities.ByteArrayToHexString(hash));
            packet.ReadInt16("Int 16");
            packet.ReadEnum<UnknownFlags>("Unknown int32 flag", TypeCode.Int32);
            packet.ReadInt64("Int 64");
        }

        [Parser(Opcode.CMSG_REDIRECTION_FAILED)]
        public static void HandleRedirectFailed(Packet packet)
        {
            packet.ReadInt32("Token");
        }

        [Parser(Opcode.CMSG_REDIRECTION_AUTH_PROOF, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleRedirectionAuthProof(Packet packet)
        {
            packet.ReadCString("Account");
            packet.ReadInt64("Unk Int64");

            var hash = packet.ReadBytes(20);
            packet.WriteLine("Proof SHA-1 Hash: " + Utilities.ByteArrayToHexString(hash));
        }

        [Parser(Opcode.CMSG_REDIRECTION_AUTH_PROOF, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleRedirectionAuthProof422(Packet packet)
        {
            var bytes = new byte[20];
            bytes[0] = packet.ReadByte();
            bytes[12] = packet.ReadByte();
            bytes[3] = packet.ReadByte();
            bytes[17] = packet.ReadByte();
            bytes[11] = packet.ReadByte();
            bytes[13] = packet.ReadByte();
            bytes[5] = packet.ReadByte();
            bytes[9] = packet.ReadByte();
            bytes[6] = packet.ReadByte();
            bytes[19] = packet.ReadByte();
            bytes[15] = packet.ReadByte();
            bytes[18] = packet.ReadByte();
            bytes[8] = packet.ReadByte();
            packet.ReadInt64("Unk long 1");
            bytes[2] = packet.ReadByte();
            bytes[1] = packet.ReadByte();
            packet.ReadInt64("Unk long 2");
            bytes[7] = packet.ReadByte();
            bytes[4] = packet.ReadByte();
            bytes[16] = packet.ReadByte();
            bytes[14] = packet.ReadByte();
            bytes[10] = packet.ReadByte();
            packet.WriteLine("Proof RSA Hash: " + Utilities.ByteArrayToHexString(bytes));
        }

        [Parser(Opcode.SMSG_KICK_REASON)]
        public static void HandleKickReason(Packet packet)
        {
            packet.ReadEnum<KickReason>("Reason", TypeCode.Byte);

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
            z_streams[packet.ConnectionIndex] = new ZlibCodec(CompressionMode.Decompress);
        }
    }
}
