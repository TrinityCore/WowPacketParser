using System;
using System.Text;
using PacketParser.Enums;
using PacketParser.Misc;
using PacketParser.Processing;
using PacketParser.DataStructures;
using Guid = PacketParser.DataStructures.Guid;

namespace PacketParser.Parsing.Parsers
{
    public static class SessionHandler
    {
        [Parser(Opcode.SMSG_AUTH_CHALLENGE, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_1a_13205)]
        public static void HandleServerAuthChallenge(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
                packet.ReadInt32("Shuffle Count");

            packet.ReadInt32("Server Seed");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
            {
                var SStateCount = ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_5a_12340) ? 8 : 4;
                packet.StoreBeginList("Server States");
                for (var i = 0; i < SStateCount; i++)
                    packet.ReadInt32("Server State", i);
                packet.StoreEndList();
            }
        }

        [Parser(Opcode.SMSG_AUTH_CHALLENGE, ClientVersionBuild.V4_0_1a_13205, ClientVersionBuild.V4_0_3_13329)]
        public static void HandleServerAuthChallenge401(Packet packet)
        {
            var keys = new UInt32[2, 4];
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
            var keys = new UInt32[2, 4];

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
            packet.ReadInt32("Unk1");
            packet.ReadInt32("Unk2");
            packet.ReadInt32("Unk3");
            packet.ReadInt32("Unk4");
            packet.ReadInt32("Server Seed");
            packet.ReadByte("Unk Byte");
            packet.ReadInt32("Unk5");
            packet.ReadInt32("Unk6");
            packet.ReadInt32("Unk7");
            packet.ReadInt32("Unk8");
        }

        [Parser(Opcode.SMSG_AUTH_CHALLENGE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleServerAuthChallenge434(Packet packet)
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

            packet.Store("Proof SHA-1 Hash: ", Utilities.ByteArrayToHexString(packet.ReadBytes(20)));

            AddonHandler.ReadClientAddonsList(packet);
        }

        //[Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V4_2_0_14333)]
        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleAuthSession422(Packet packet)
        {
            packet.ReadByte("Byte 1");
            packet.ReadByte("Byte 2");
            packet.ReadInt32("Int32 3");
            packet.ReadInt32("Int32 4");
            packet.ReadByte("Byte 5");
            packet.ReadByte("Byte 6");
            packet.ReadByte("Byte 7");

            packet.ReadByte("Byte 8");
            packet.ReadByte("Byte 9");
            packet.ReadByte("Byte 10");
            packet.ReadEnum<ClientVersionBuild>("Client Build", TypeCode.Int16);

            packet.ReadByte("Byte 11");
            packet.ReadByte("Byte 12");
            packet.ReadByte("Byte 13");
            packet.ReadByte("Byte 14");

            packet.ReadInt32("Int32 15");
            packet.ReadByte("Byte 16");
            packet.ReadByte("Byte 17");
            packet.ReadByte("Byte 18");
            packet.ReadByte("Byte 19");

            packet.ReadInt32("Int32 20");
            packet.ReadByte("Byte 21");

            packet.ReadInt32("Int32 22");
            packet.ReadByte("Byte 23");

            packet.ReadInt32("Int32 24");
            packet.ReadByte("Byte 25");

            packet.ReadInt32("Int32 26");
            packet.ReadInt32("Int32 27");

            packet.ReadCString("Account name");
            packet.ReadInt32("Int32 28");

            AddonHandler.ReadClientAddonsList(packet);
        }

        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_2_15211)]
        public static void HandleAuthSession430(Packet packet)
        {
            packet.ReadInt32("Int32 1");
            packet.ReadByte("Digest (1)");
            packet.ReadInt64("Int64 2");
            packet.ReadInt32("Int32 3");
            packet.ReadByte("Digest (2)");
            packet.ReadInt32("Int32 4");
            packet.ReadByte("Digest (3)");

            packet.ReadInt32("Int32 5");
            packet.StoreBeginList("DigestArray (4)");
            for (var i = 0; i < 7; i++)
                packet.ReadByte("Digest (4)", i);
            packet.StoreEndList();

            packet.ReadEnum<ClientVersionBuild>("Client Build", TypeCode.Int16);

            packet.StoreBeginList("DigestArray (5)");
            for (var i = 0; i < 8; i++)
                packet.ReadByte("Digest (5)", i);
            packet.StoreEndList();

            packet.ReadByte("Unk Byte 6");
            packet.ReadByte("Unk Byte 7");

            packet.ReadInt32("Client Seed");

            packet.StoreBeginList("DigestArray (6)");
            for (var i = 0; i < 2; i++)
                packet.ReadByte("Digest (6)", i);
            packet.StoreEndList();

            AddonHandler.ReadClientAddonsList(packet, packet.ReadInt32());
            packet.ReadByte("Mask"); // TODO: Seems to affect how the size is read
            var size = (packet.ReadByte() >> 4);
            packet.Store("Size", size);
            packet.Store("Account name", Encoding.UTF8.GetString(packet.ReadBytes(size)));
        }

        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V4_3_2_15211, ClientVersionBuild.V4_3_3_15354)]
        public static void HandleAuthSession432(Packet packet)
        {
            var sha = new byte[20];
            packet.ReadInt32("Int32 1");
            sha[12] = packet.ReadByte();
            packet.ReadInt32("Int32 2 ");
            packet.ReadInt32("Int32 3");
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

            packet.ReadInt64("Int64 4");
            packet.ReadByte("Unk Byte 5");
            packet.ReadByte("Unk Byte 6");
            sha[3] = packet.ReadByte();
            sha[10] = packet.ReadByte();

            packet.ReadInt32("Client Seed");

            sha[16] = packet.ReadByte();
            sha[4] = packet.ReadByte();
            packet.ReadInt32("Int32 7");
            sha[14] = packet.ReadByte();
            sha[8] = packet.ReadByte();
            sha[5] = packet.ReadByte();
            sha[1] = packet.ReadByte();
            sha[13] = packet.ReadByte();

            AddonHandler.ReadClientAddonsList(packet, packet.ReadInt32());
            var highBits = packet.ReadByte() << 5;
            var lowBits = packet.ReadByte() >> 3;
            var size = lowBits | highBits;
            packet.Store("Size", size);
            packet.Store("Account name", Encoding.UTF8.GetString(packet.ReadBytes(size)));
            packet.Store("Proof SHA-1 Hash", Utilities.ByteArrayToHexString(sha));
        }

        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleAuthSession434(Packet packet)
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
            packet.ReadEnum<ClientVersionBuild>("Client Build", TypeCode.Int16);
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

            AddonHandler.ReadClientAddonsList(packet, packet.ReadInt32());

            packet.ReadBit("Unk bit");
            var size = (int)packet.ReadBits(12);
            packet.Store("Account name", Encoding.UTF8.GetString(packet.ReadBytes(size)));
            packet.Store("Proof SHA-1 Hash", Utilities.ByteArrayToHexString(sha));
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

        [Parser(Opcode.SMSG_AUTH_RESPONSE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleAuthResponse434(Packet packet)
        {
            var isQueued = packet.ReadBit("Queued");
            var hasAccountInfo = packet.ReadBit("Has account info");

            if (isQueued)
                packet.ReadBit("UnkBit");

            if (hasAccountInfo)
            {
                packet.ReadInt32("Billing Time Remaining");
                packet.ReadEnum<ClientType>("Player Expansion", TypeCode.Byte);
                packet.ReadInt32("Unknown UInt32");
                packet.ReadEnum<ClientType>("Account Expansion", TypeCode.Byte);
                packet.ReadInt32("Billing Time Rested");
                packet.ReadEnum<BillingFlag>("Billing Flags", TypeCode.Byte);
            }

            packet.ReadEnum<ResponseCode>("Auth Code", TypeCode.Byte);

            if (isQueued)
                packet.ReadInt32("Queue Position");
        }

        public static void ReadAuthResponseInfo(ref Packet packet)
        {
            packet.ReadInt32("Billing Time Remaining");
            packet.ReadEnum<BillingFlag>("Billing Flags", TypeCode.Byte);
            packet.ReadInt32("Billing Time Rested");

            // Unknown, these two show the same as expansion payed for.
            // Eg. If account only has payed for Wotlk expansion it will show 2 for both.
            packet.ReadEnum<ClientType>("Account Expansion1", TypeCode.Byte);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_3_13329))
                packet.ReadEnum<ClientType>("Account Expansion2", TypeCode.Byte);
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
            PacketFileProcessor.Current.GetProcessor<SessionStore>().LoginGuid = guid;
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandlePlayerLogin422(Packet packet)
        {
            var guid = packet.StartBitStream(0, 4, 7, 1, 3, 2, 5, 6);
            packet.ParseBitStream(guid, 5, 0, 3, 4, 7, 2, 6, 1);
            PacketFileProcessor.Current.GetProcessor<SessionStore>().LoginGuid = packet.StoreBitstreamGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_3_15354)]
        public static void HandlePlayerLogin430(Packet packet)
        {
            var guid = packet.StartBitStream(0, 5, 3, 4, 7, 6, 2, 1);
            packet.ParseBitStream(guid, 4, 1, 7, 2, 6, 5, 3, 0);
            PacketFileProcessor.Current.GetProcessor<SessionStore>().LoginGuid = packet.StoreBitstreamGuid("GUID", guid);
        }


        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V4_3_3_15354, ClientVersionBuild.V4_3_4_15595)]
        public static void HandlePlayerLogin433(Packet packet)
        {
            var guid = packet.StartBitStream(6, 7, 4, 5, 0, 1, 3, 2);
            packet.ParseBitStream(guid, 1, 4, 7, 2, 3, 6, 0, 5);
            PacketFileProcessor.Current.GetProcessor<SessionStore>().LoginGuid = packet.StoreBitstreamGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V4_3_4_15595)]
        public static void HandlePlayerLogin434(Packet packet)
        {
            var guid = packet.StartBitStream(2, 3, 0, 6, 4, 5, 1, 7);
            packet.ParseBitStream(guid, 2, 7, 0, 3, 5, 6, 1, 4);
            PacketFileProcessor.Current.GetProcessor<SessionStore>().LoginGuid = packet.StoreBitstreamGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_CHARACTER_LOGIN_FAILED)]
        public static void HandleLoginFailed(Packet packet)
        {
            packet.ReadEnum<ResponseCode>("Fail reason", TypeCode.Byte);
            PacketFileProcessor.Current.GetProcessor<SessionStore>().LoginGuid = null;
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
            PacketFileProcessor.Current.GetProcessor<SessionStore>().LoginGuid = null;
        }

        [Parser(Opcode.CMSG_CONNECT_TO_FAILED)]
        public static void HandleConnectToFailed(Packet packet)
        {
            packet.Store("IP Address", packet.ReadIPAddress());
            packet.ReadByte("Reason?");
        }

        [Parser(Opcode.SMSG_REDIRECT_CLIENT, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleRedirectClient(Packet packet)
        {
            var ip = packet.ReadIPAddress();
            packet.Store("IP Address", ip);
            packet.ReadUInt16("Port");
            packet.ReadInt32("Token");
            var hash = packet.ReadBytes(20);
            packet.Store("Address SHA-1 Hash", Utilities.ByteArrayToHexString(hash));
        }

        [Parser(Opcode.SMSG_REDIRECT_CLIENT, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleRedirectClient422(Packet packet)
        {
            var hash = packet.ReadBytes(255);
            packet.Store("RSA Hash", Utilities.ByteArrayToHexString(hash));
            packet.ReadInt16("Int 16");
            packet.ReadEnum<UnknownFlags>("Unknown int32 flag", TypeCode.Int32);
            packet.ReadInt64("Int 64");
        }

        [Parser(Opcode.SMSG_REDIRECT_CLIENT, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleRedirectClient434(Packet packet)
        {
            packet.ReadUInt64("Unk Long");
            packet.ReadUInt32("Token");
            var hash = packet.ReadBytes(0x100);
            packet.Store("RSA Hash", Utilities.ByteArrayToHexString(hash));
            packet.ReadByte("Unk Byte"); // 1 == Connecting to world server
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
            packet.Store("Proof SHA-1 Hash", Utilities.ByteArrayToHexString(hash));
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
            packet.Store("Proof RSA Hash", Utilities.ByteArrayToHexString(bytes));
        }

        [Parser(Opcode.CMSG_REDIRECTION_AUTH_PROOF, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleRedirectionAuthProof434(Packet packet)
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
            packet.StoreBeginList("Lines");
            for (var i = 0; i < lineCount; i++)
            {
                packet.ReadCString("Line", i);
            }
            packet.StoreEndList();
        }
    }
}
