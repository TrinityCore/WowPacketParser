using System;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store.Objects;
using Guid=WowPacketParser.Misc.Guid;

namespace WowPacketParser.Parsing.Parsers
{
    public static class SessionHandler
    {
        public static Guid LoginGuid;

        public static Player LoggedInCharacter;

        [Parser(Opcode.SMSG_AUTH_CHALLENGE)]
        public static void HandleServerAuthChallenge(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
                packet.ReadInt32("Shuffle Count");

            packet.ReadInt32("Server Seed");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
                for (var i = 0; i < 8; i++)
                    packet.ReadInt32("Server State", i);
        }

        [Parser(Opcode.SMSG_AUTH_CHALLENGE, ClientVersionBuild.V4_2_2_14545)]
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

        [Parser(Opcode.CMSG_AUTH_SESSION)]
        public static void HandleAuthSession(Packet packet)
        {
            // Do not overwrite version after Handler was initialized
            packet.ReadEnum<ClientVersionBuild>("Client Build", TypeCode.Int32);
 
            packet.ReadInt32("Unk Int32 1");
            packet.ReadCString("Account");
 
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                packet.ReadInt32("Unk Int32 2");
 
            packet.ReadInt32("Client Seed");
 
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
                packet.ReadInt64("Unk Int64");
 
            packet.Writer.WriteLine("Proof SHA-1 Hash: " + Utilities.ByteArrayToHexString(packet.ReadBytes(20)));
 
            AddonHandler.ReadClientAddonsList(ref packet);
        }

        //[Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V4_2_0_14333)]
        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V4_2_2_14545)]
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

        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V4_3_0_15005)]
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
                packet.ReadByte("Digest (5)",i);

            packet.ReadByte("Unk Byte");
            packet.ReadByte("Unk Byte");

            packet.ReadInt32("Client Seed");

            for (var i = 0; i < 2; i++)
                packet.ReadByte("Digest (6)", i);

            var pkt = new Packet(packet.ReadBytes(packet.ReadInt32()), packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer);
            AddonHandler.ReadClientAddonsList(ref pkt);
            packet.ReadByte("Mask"); // TODO: Seems to affect how the size is read
            var size = (packet.ReadByte() >> 4);
            packet.Writer.WriteLine("Size: " + size);
            packet.Writer.WriteLine("Account name: {0}", Encoding.UTF8.GetString(packet.ReadBytes(size)));
        }

        [Parser(Opcode.SMSG_AUTH_RESPONSE)]
        public static void HandleAuthResponse(Packet packet)
        {
            var code = (ResponseCode)packet.ReadByte();
            packet.Writer.WriteLine("Auth Code: " + code);

            switch (code)
            {
                case ResponseCode.AUTH_OK:
                {
                    ReadAuthResponseInfo(ref packet);
                    break;
                }
                case ResponseCode.AUTH_WAIT_QUEUE:
                {
                    if (packet.GetLength() <= 6)
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
            var position = packet.ReadInt32();
            packet.Writer.WriteLine("Queue Position: " + position);

            var unkByte = packet.ReadByte();
            packet.Writer.WriteLine("Unk Byte: " + unkByte);
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN)]
        public static void HandlePlayerLogin(Packet packet)
        {
            var guid = packet.ReadGuid();
            packet.Writer.WriteLine("GUID: " + guid);
            LoginGuid = guid;
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V4_2_2_14545)]
        public static void HandlePlayerLogin422(Packet packet)
        {
            var bits = new bool[8];
            for (int c = 7; c >= 0; c--)
                bits[c] = packet.ReadBit();

            var bytes = new byte[8];

            // Data - Real
            // BC 04 03 03 A4 BD = 02 00 00 00 02 A5 BC 05
            // BC F6 05 04 1E 2F = 05 00 00 00 04 1F 2E F7
            // (BC = 10111100)

            // 3C 05 04 20 9E = 05 00 00 00 04 21 9F 00
            // (3C = 00111100)

            if (bits[7]) bytes[0] = (byte)(packet.ReadByte() ^ 1); // 1
            if (bits[6]) bytes[4] = (byte)(packet.ReadByte() ^ 1); // NOTCONF
            if (bits[5]) bytes[3] = (byte)(packet.ReadByte() ^ 1); // 2
            if (bits[4]) bytes[7] = (byte)(packet.ReadByte() ^ 1); // 3
            if (bits[3]) bytes[2] = (byte)(packet.ReadByte() ^ 1); // 4
            if (bits[2]) bytes[1] = (byte)(packet.ReadByte() ^ 1); // 5
            if (bits[1]) bytes[5] = (byte)(packet.ReadByte() ^ 1); // NOTCONF
            if (bits[0]) bytes[6] = (byte)(packet.ReadByte() ^ 1); // NOTCONF
            
            packet.Writer.WriteLine("GUID: {0}", new Guid(BitConverter.ToUInt64(bytes, 0)));
        }

        /*
        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V4_3_0_15005)]
        public static void HandlePlayerLogin430(Packet packet)
        {
        }
        */

        [Parser(Opcode.SMSG_CHARACTER_LOGIN_FAILED)]
        public static void HandleLoginFailed(Packet packet)
        {
            var unk = packet.ReadByte();
            packet.Writer.WriteLine("Unk Byte: " + unk);
        }

        [Parser(Opcode.SMSG_LOGOUT_RESPONSE)]
        public static void HandlePlayerLogoutResponse(Packet packet)
        {
            packet.ReadByte("Reason");
            // From TC:
            // Reason 1: IsInCombat
            // Reason 2: InDuel or frozen by GM
            // Reason 3: Jumping or Falling

            packet.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.SMSG_LOGOUT_COMPLETE)]
        public static void HandleLogoutComplete(Packet packet)
        {
            LoggedInCharacter = null;
        }

        [Parser(Opcode.SMSG_REDIRECT_CLIENT)]
        public static void HandleRedirectClient(Packet packet)
        {
            var ip = packet.ReadIPAddress();
            packet.Writer.WriteLine("IP Address: " + ip);

            var port = packet.ReadUInt16();
            packet.Writer.WriteLine("Port: " + port);

            var unk = packet.ReadInt32();
            packet.Writer.WriteLine("Token: " + unk);

            var hash = packet.ReadBytes(20);
            packet.Writer.WriteLine("Address SHA-1 Hash: " + Utilities.ByteArrayToHexString(hash));
        }

        [Parser(Opcode.CMSG_REDIRECTION_FAILED)]
        public static void HandleRedirectFailed(Packet packet)
        {
            var token = packet.ReadInt32();
            packet.Writer.WriteLine("Token: " + token);
        }

        [Parser(Opcode.CMSG_REDIRECTION_AUTH_PROOF)]
        public static void HandleRedirectionAuthProof(Packet packet)
        {
            var name = packet.ReadCString();
            packet.Writer.WriteLine("Account: " + name);

            var unk = packet.ReadInt64();
            packet.Writer.WriteLine("Unk Int64: " + unk);

            var hash = packet.ReadBytes(20);
            packet.Writer.WriteLine("Proof SHA-1 Hash: " + Utilities.ByteArrayToHexString(hash));
        }

        [Parser(Opcode.CMSG_REDIRECTION_AUTH_PROOF, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleRedirectionAuthProof422(Packet packet)
        {
            packet.ReadByte("Unk byte 1");
            packet.ReadByte("Unk byte 2");
            packet.ReadByte("Unk byte 3");
            packet.ReadByte("Unk byte 4");
            packet.ReadByte("Unk byte 5");
            packet.ReadByte("Unk byte 6");
            packet.ReadByte("Unk byte 7");
            packet.ReadByte("Unk byte 8");
            packet.ReadByte("Unk byte 9");
            packet.ReadByte("Unk byte 10");
            packet.ReadByte("Unk byte 11");
            packet.ReadByte("Unk byte 12");
            packet.ReadByte("Unk byte 13");
            packet.ReadInt64("Unk long 1");
            packet.ReadByte("Unk byte 14");
            packet.ReadByte("Unk byte 15");
            packet.ReadInt64("Unk long 2");
            packet.ReadByte("Unk byte 16");
            packet.ReadByte("Unk byte 17");
            packet.ReadByte("Unk byte 18");
            packet.ReadByte("Unk byte 19");
            packet.ReadByte("Unk byte 20");
        }

        [Parser(Opcode.SMSG_KICK_REASON)]
        public static void HandleKickReason(Packet packet)
        {
            var reason = (KickReason)packet.ReadByte();
            packet.Writer.WriteLine("Reason: " + reason);

            if (!packet.CanRead())
                return;

            var str = packet.ReadCString();
            packet.Writer.WriteLine("Unk String: " + str);
        }

        [Parser(Opcode.SMSG_MOTD)]
        public static void HandleMessageOfTheDay(Packet packet)
        {
            var lineCount = packet.ReadInt32();
            packet.Writer.WriteLine("Line Count: " + lineCount);

            for (var i = 0; i < lineCount; i++)
            {
                var lineStr = packet.ReadCString();
                packet.Writer.WriteLine("Line " + i + ": " + lineStr);
            }
        }
    }
}
