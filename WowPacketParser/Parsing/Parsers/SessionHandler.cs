using System;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using Guid=WowPacketParser.Misc.Guid;

namespace WowPacketParser.Parsing.Parsers
{
    public static class SessionHandler
    {
        public static Guid LoginGuid;

        public static CharacterInfo LoggedInCharacter;

        [Parser(Opcode.SMSG_AUTH_CHALLENGE)]
        public static void HandleServerAuthChallenge(Packet packet)
        {
            var unk = packet.ReadInt32();
            Console.WriteLine("Shuffle Count: " + unk);

            var seed = packet.ReadInt32();
            Console.WriteLine("Server Seed: " + seed);

            for (var i = 0; i < 8; i++)
            {
                var rand = packet.ReadInt32();
                Console.WriteLine("Server State " + i + ": " + rand);
            }
        }

        [Parser(Opcode.CMSG_AUTH_SESSION)]
        public static void HandleAuthSession(Packet packet)
        {
            var build = packet.ReadInt32();
            Console.WriteLine("Client Build: " + build);

            var unk1 = packet.ReadInt32();
            Console.WriteLine("Unk Int32 1: " + unk1);

            var account = packet.ReadCString();
            Console.WriteLine("Account: " + account);

            var unk2 = packet.ReadInt32();
            Console.WriteLine("Unk Int32 2: " + unk2);

            var clientSeed = packet.ReadInt32();
            Console.WriteLine("Client Seed: " + clientSeed);

            var unk3 = packet.ReadInt64();
            Console.WriteLine("Unk Int64: " + unk3);

            var digest = packet.ReadBytes(20);
            Console.WriteLine("Proof SHA-1 Hash: " + Utilities.ByteArrayToHexString(digest));

            AddonHandler.ReadClientAddonsList(packet);
        }

        [Parser(Opcode.SMSG_AUTH_RESPONSE)]
        public static void HandleAuthResponse(Packet packet)
        {
            var code = (ResponseCode)packet.ReadByte();
            Console.WriteLine("Auth Code: " + code);

            switch (code)
            {
                case ResponseCode.AUTH_OK:
                {
                    ReadAuthResponseInfo(packet);
                    break;
                }
                case ResponseCode.AUTH_WAIT_QUEUE:
                {
                    if (packet.GetLength() <= 6)
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

        public static void ReadAuthResponseInfo(Packet packet)
        {
            var billingRemaining = packet.ReadInt32();
            Console.WriteLine("Billing Time Remaining: " + billingRemaining);

            var billingFlags = (BillingFlag)packet.ReadByte();
            Console.WriteLine("Billing Flags: " + billingFlags);

            var billingRested = packet.ReadInt32();
            Console.WriteLine("Billing Time Rested: " + billingRested);

            var expansion = (ClientType)packet.ReadByte();
            Console.WriteLine("Expansion: " + expansion);
        }

        public static void ReadQueuePositionInfo(Packet packet)
        {
            var position = packet.ReadInt32();
            Console.WriteLine("Queue Position: " + position);

            var unkByte = packet.ReadByte();
            Console.WriteLine("Unk Byte: " + unkByte);
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN)]
        public static void HandlePlayerLogin(Packet packet)
        {
            var guid = packet.ReadGuid();
            Console.WriteLine("GUID: " + guid);
            LoginGuid = guid;
        }

        [Parser(Opcode.SMSG_CHARACTER_LOGIN_FAILED)]
        public static void HandleLoginFailed(Packet packet)
        {
            var unk = packet.ReadByte();
            Console.WriteLine("Unk Byte: " + unk);
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
            Console.WriteLine("IP Address: " + ip);

            var port = packet.ReadUInt16();
            Console.WriteLine("Port: " + port);

            var unk = packet.ReadInt32();
            Console.WriteLine("Token: " + unk);

            var hash = packet.ReadBytes(20);
            Console.WriteLine("Address SHA-1 Hash: " + Utilities.ByteArrayToHexString(hash));
        }

        [Parser(Opcode.CMSG_REDIRECTION_FAILED)]
        public static void HandleRedirectFailed(Packet packet)
        {
            var token = packet.ReadInt32();
            Console.WriteLine("Token: " + token);
        }

        [Parser(Opcode.CMSG_REDIRECTION_AUTH_PROOF)]
        public static void HandleRedirectionAuthProof(Packet packet)
        {
            var name = packet.ReadCString();
            Console.WriteLine("Account: " + name);

            var unk = packet.ReadInt64();
            Console.WriteLine("Unk Int64: " + unk);

            var hash = packet.ReadBytes(20);
            Console.WriteLine("Proof SHA-1 Hash: " + Utilities.ByteArrayToHexString(hash));
        }

        [Parser(Opcode.SMSG_KICK_REASON)]
        public static void HandleKickReason(Packet packet)
        {
            var reason = (KickReason)packet.ReadByte();
            Console.WriteLine("Reason: " + reason);

            if (packet.IsRead())
                return;

            var str = packet.ReadCString();
            Console.WriteLine("Unk String: " + str);
        }

        [Parser(Opcode.SMSG_MOTD)]
        public static void HandleMessageOfTheDay(Packet packet)
        {
            var lineCount = packet.ReadInt32();
            Console.WriteLine("Line Count: " + lineCount);

            for (var i = 0; i < lineCount; i++)
            {
                var lineStr = packet.ReadCString();
                Console.WriteLine("Line " + i + ": " + lineStr);
            }
        }
    }
}
