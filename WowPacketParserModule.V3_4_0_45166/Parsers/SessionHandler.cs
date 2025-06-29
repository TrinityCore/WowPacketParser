using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class SessionHandler
    {
        [Parser(Opcode.SMSG_AUTH_CHALLENGE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleServerAuthChallenge(Packet packet)
        {
            for (uint i = 0; i < 8; ++i)
                packet.ReadUInt32("DosChallenge", i);

            packet.ReadBytes("Challenge", 32);
            packet.ReadByte("DosZeroBits");
        }

        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuthSession(Packet packet)
        {
            packet.ReadUInt64("DosResponse");
            packet.ReadUInt32("RegionID");
            packet.ReadUInt32("BattlegroupID");
            packet.ReadUInt32("RealmID");
            packet.ReadBytes("LocalChallenge", 32);
            packet.ReadBytes("Digest", 24);
            packet.ReadBit("UseIPv6");

            var realmJoinTicketSize = packet.ReadInt32();
            packet.ReadBytes("RealmJoinTicket", realmJoinTicketSize);
        }

        [Parser(Opcode.SMSG_CHANGE_REALM_TICKET_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleChangeRealmTicketResponse(Packet packet)
        {
            packet.ReadUInt32("Token");
            packet.ResetBitReader();
            packet.ReadBit("Allow");

            int protoSize = packet.ReadInt32();
            packet.ReadBytesTable("Ticket", protoSize);
        }

        [Parser(Opcode.SMSG_CHARACTER_LOGIN_FAILED, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleLoginFailed(Packet packet)
        {
            packet.ReadByteE<ResponseCode>("Code");
        }

        [Parser(Opcode.SMSG_SET_TIME_ZONE_INFORMATION)]
        public static void HandleSetTimeZoneInformation(Packet packet)
        {
            var len1 = packet.ReadBits(7);
            var len2 = packet.ReadBits(7);
            var len3 = packet.ReadBits(7);

            packet.ReadWoWString("ServerTimeTZ", len1);
            packet.ReadWoWString("GameTimeTZ", len2);
            packet.ReadWoWString("ServerRegionalTZ", len3);
        }

        [Parser(Opcode.SMSG_UPDATE_BNET_SESSION_KEY, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleUpdateBnetSessionKey(Packet packet)
        {
            var sessionKeyLength = (int)packet.ReadBits(7);

            packet.ReadBytes("Digest", 32);
            packet.ReadBytes("SessionKey", sessionKeyLength);
        }

        [Parser(Opcode.SMSG_BATTLE_NET_CONNECTION_STATUS, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBattleNetConnectionStatus(Packet packet)
        {
            packet.ReadBits("State", 2); // TODO: enum
            packet.ReadBit("SuppressNotification");
        }

        [Parser(Opcode.SMSG_CONNECT_TO, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleRedirectClient(Packet packet)
        {
            packet.ReadBytes("Where (RSA encrypted)", 256);

            AddressType type = packet.ReadByteE<AddressType>("Type");
            switch (type)
            {
                case AddressType.IPv4:
                    packet.ReadIPAddress("Address");
                    break;
                case AddressType.IPv6:
                    packet.ReadIPv6Address("Address");
                    break;
                case AddressType.NamedSocket:
                    packet.ReadWoWString("Address", 128);
                    break;
                default:
                    break;
            }

            packet.ReadUInt16("Port");
            packet.ReadUInt32E<ConnectToSerial>("Serial");
            packet.ReadByte("Con");
            packet.ReadUInt64("Key");
        }

        [Parser(Opcode.CMSG_QUEUED_MESSAGES_END, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleQueuedMessagesEnd(Packet packet)
        {
            packet.ReadInt32("Timestamp");
        }

        [Parser(Opcode.SMSG_SUSPEND_TOKEN, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_RESUME_TOKEN, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleResumeTokenPacket(Packet packet)
        {
            packet.ReadUInt32("Sequence");
            packet.ReadBits("Reason", 2);
        }

        [Parser(Opcode.SMSG_SUSPEND_COMMS, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSuspendCommsPackets(Packet packet)
        {
            packet.ReadInt32("Serial");
        }

        [Parser(Opcode.CMSG_SUSPEND_COMMS_ACK, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSuspendCommsAck(Packet packet)
        {
            packet.ReadInt32("Serial");
            packet.ReadInt32("Timestamp");
        }

        [Parser(Opcode.CMSG_LOG_DISCONNECT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleLogDisconnect(Packet packet)
        {
            packet.ReadUInt32("Reason");
            // 4 is inability for client to decrypt RSA
            // 3 is not receiving "WORLD OF WARCRAFT CONNECTION - SERVER TO CLIENT"
            // 11 is sent on receiving opcode 0x140 with some specific data
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V3_4_3_51505)]
        public static void HandlePlayerLogin(Packet packet)
        {
            var guid = packet.ReadPackedGuid128("Guid");
            packet.ReadSingle("FarClip");
            packet.Holder.PlayerLogin = new() { PlayerGuid = guid };
            WowPacketParser.Parsing.Parsers.SessionHandler.LoginGuid = guid;
        }

        [Parser(Opcode.SMSG_QUERY_TIME_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleQueryTimeResponse(Packet packet)
        {
            packet.ReadTime64("CurrentTime");
        }

        [Parser(Opcode.CMSG_SUSPEND_TOKEN_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSuspendToken(Packet packet)
        {
            packet.ReadUInt32("Sequence");
        }

        [Parser(Opcode.CMSG_QUERY_REALM_NAME, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleRealmQuery(Packet packet)
        {
            packet.ReadInt32("VirtualRealmAddress");
        }

        [Parser(Opcode.SMSG_REALM_QUERY_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleRealmQueryResponse(Packet packet)
        {
            packet.ReadUInt32("VirtualRealmAddress");

            var state = packet.ReadByte("LookupState");
            if (state == 0)
            {
                packet.ResetBitReader();

                packet.ReadBit("IsLocal");
                packet.ReadBit("Unk bit");

                var bits2 = packet.ReadBits(9);
                var bits258 = packet.ReadBits(9);
                packet.ReadBit();

                packet.ReadWoWString("RealmNameActual", bits2);
                packet.ReadWoWString("RealmNameNormalized", bits258);
            }
        }

        [Parser(Opcode.SMSG_LOGOUT_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePlayerLogoutResponse(Packet packet)
        {
            packet.ReadInt32("Reason");
            packet.ReadBit("Instant");
            // From TC:
            // Reason 1: IsInCombat
            // Reason 2: InDuel or frozen by GM
            // Reason 3: Jumping or Falling
        }

        [Parser(Opcode.SMSG_WAIT_QUEUE_UPDATE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleWaitQueueUpdate(Packet packet)
        {
            packet.ReadInt32("WaitCount");
            packet.ReadInt32("WaitTime");
            packet.ReadByte("AllowedFactionGroupForCharacterCreate");
            packet.ReadBit("HasFCM");
            packet.ReadBit("CanCreateOnlyIfExisting");
        }

        [Parser(Opcode.CMSG_CHANGE_REALM_TICKET, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleChangeRealmTicket(Packet packet)
        {
            packet.ReadUInt32("Token");
            packet.ReadBytes("Secret", 32);
        }

        [Parser(Opcode.CMSG_CONNECT_TO_FAILED, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleRedirectFailed(Packet packet)
        {
            packet.ReadSByte("Con");
            packet.ReadUInt32("Serial");
        }

        [Parser(Opcode.CMSG_LOGOUT_REQUEST, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleLogoutRequest(Packet packet)
        {
            packet.ReadBit("IdleLogout");
        }

        [Parser(Opcode.SMSG_MOTD)]
        public static void HandleMessageOfTheDay(Packet packet)
        {
            var lineCount = packet.ReadBits("Line Count", 4);

            packet.ResetBitReader();
            for (var i = 0; i < lineCount; i++)
            {
                var lineLength = (int)packet.ReadBits(7);
                packet.ResetBitReader();
                packet.ReadWoWString("Line", lineLength, i);
            }
        }

        [Parser(Opcode.CMSG_LOG_STREAMING_ERROR)]
        public static void HandleRouterClientLogStreamingError(Packet packet)
        {
            var bits16 = packet.ReadBits(9);
            packet.ReadWoWString("Error", bits16);
        }

        [Parser(Opcode.CMSG_ENTER_ENCRYPTED_MODE_ACK, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_LOGOUT_COMPLETE, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_WAIT_QUEUE_FINISH, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_KEEP_ALIVE, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_LOGOUT_CANCEL, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSessionZero(Packet packet)
        {
        }
    }
}
