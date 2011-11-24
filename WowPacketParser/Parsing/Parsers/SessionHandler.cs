using System;
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

            // for (var i = 0; i < 3; i++) ??
            packet.ReadInt32("Int32");
            packet.ReadByte("Byte");

            packet.ReadInt32("Int32");
            packet.ReadByte("Byte");

            packet.ReadInt32("Int32");
            packet.ReadByte("Byte");

            packet.ReadByte("Byte");
            packet.ReadByte("Byte");
            packet.ReadByte("Byte");
            packet.ReadByte("Byte");

            packet.ReadInt64("Int64");
            packet.ReadByte("Byte");
            packet.ReadByte("Byte");
            packet.ReadByte("Byte");
            packet.ReadByte("Byte");

            packet.ReadCString("String");
            packet.ReadInt32("Int32");

            AddonHandler.ReadClientAddonsList(ref packet);
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
            // 4.2.2:
            // Length = 6
            // BC 04 03 03 A4 BD = 0x200000002A5BC05
            var guid = packet.ReadGuid();
            packet.Writer.WriteLine("GUID: " + guid);
            LoginGuid = guid;
        }

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
