using Google.Protobuf.WellKnownTypes;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class SessionHandler
    {
        public static void ReadVirtualRealmNameInfo(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();
            packet.ReadBit("IsLocal", indexes);
            packet.ReadBit("IsHiddenFromPlayers", indexes);

            var actualNameLength = packet.ReadBits(8);
            var normalizedNameLength = packet.ReadBits(8);

            packet.ReadWoWString("RealmNameActual", actualNameLength, indexes);
            packet.ReadWoWString("RealmNameNormalized", normalizedNameLength, indexes);
        }

        public static void ReadVirtualRealmInfo(Packet packet, params object[] indexes)
        {
            packet.ReadUInt32("RealmAddress", indexes);
            ReadVirtualRealmNameInfo(packet, indexes, "RealmNameInfo");
        }

        public static void ReadGameModeData(Packet packet, params object[] indexes)
        {
            packet.ReadUInt32("Unknown_1107_0", indexes);
            packet.ReadPackedGuid128("Guid", indexes);
            packet.ReadByte("GameMode", indexes);
            packet.ReadInt32("MapID", indexes);
            packet.ReadByte("Unknown_1107_1", indexes);
            packet.ReadByte("Unknown_1107_2", indexes);
            packet.ReadByte("Unknown_1107_3", indexes);
            var customizationsCount = packet.ReadUInt32("CustomizationsCount", indexes);
            var unkCount = packet.ReadUInt32("Unknown_1107_4_Count", indexes);

            for (var i = 0; i < customizationsCount; i++)
                CharacterHandler.ReadChrCustomizationChoice(packet, indexes, "Customizations", i);

            for (var i = 0; i < unkCount; i++)
                CharacterHandler.ReadChrCustomizationChoice(packet, indexes, "Unknown_1107_4", i);
        }

        [Parser(Opcode.SMSG_REALM_QUERY_RESPONSE)]
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

        [Parser(Opcode.SMSG_WAIT_QUEUE_UPDATE)]
        public static void HandleWaitQueueUpdate(Packet packet)
        {
            packet.ReadInt32("WaitCount");
            packet.ReadInt32("WaitTime");
            packet.ReadByte("AllowedFactionGroupForCharacterCreate");
            packet.ReadBit("HasFCM");
            packet.ReadBit("CanCreateOnlyIfExisting");
        }

        [Parser(Opcode.SMSG_SUSPEND_TOKEN)]
        [Parser(Opcode.SMSG_RESUME_TOKEN)]
        public static void HandleResumeTokenPacket(Packet packet)
        {
            packet.ReadUInt32("Sequence");
            packet.ReadBits("Reason", 2);
        }

        [Parser(Opcode.SMSG_SET_TIME_ZONE_INFORMATION)]
        public static void HandleSetTimeZoneInformation(Packet packet)
        {
            var gameTimeTZLength = packet.ReadBits(7);
            var serverTimeTZLength = packet.ReadBits(7);
            var serverRegionalTimeTZLength = packet.ReadBits(7);

            packet.ReadWoWString("GameTimeTZ", gameTimeTZLength);
            packet.ReadWoWString("ServerTimeTZ", serverTimeTZLength);
            packet.ReadWoWString("ServerRegionalTimeTZ", serverRegionalTimeTZLength);
        }

        [Parser(Opcode.SMSG_LOGOUT_RESPONSE)]
        public static void HandlePlayerLogoutResponse(Packet packet)
        {
            packet.ReadInt32("Reason");
            packet.ReadBit("Instant");
            // From TC:
            // Reason 1: IsInCombat
            // Reason 2: InDuel or frozen by GM
            // Reason 3: Jumping or Falling
        }

        [Parser(Opcode.SMSG_LOGOUT_COMPLETE)]
        public static void HandleLogoutComplete(Packet packet)
        {
            var switchGameMode = packet.ReadBit("SwitchGameMode");
            if (switchGameMode)
            {
                packet.ResetBitReader();
                packet.ReadBit("IsFastLogin");
                ReadGameModeData(packet, "CurrentGameMode");
                ReadGameModeData(packet, "NewGameMode");
            }
        }

        [Parser(Opcode.SMSG_QUERY_TIME_RESPONSE)]
        public static void HandleQueryTimeResponse(Packet packet)
        {
            packet.ReadTime64("CurrentTime");
        }

        [Parser(Opcode.SMSG_CHARACTER_LOGIN_FAILED)]
        public static void HandleLoginFailed(Packet packet)
        {
            packet.ReadByteE<ResponseCode>("Code");
        }

        [Parser(Opcode.SMSG_BATTLENET_CHALLENGE_START)]
        public static void HandleBattlenetChallengeStart(Packet packet)
        {
            packet.ReadUInt32("Token");
            var bits16 = packet.ReadBits(9);
            packet.ReadWoWString("ChallengeURL", bits16);
        }

        [Parser(Opcode.SMSG_BATTLENET_CHALLENGE_ABORT)]
        public static void HandleBattlenetChallengeAbort(Packet packet)
        {
            packet.ReadUInt32("Token");
            packet.ReadBit("Timeout");
        }

        [Parser(Opcode.SMSG_BATTLE_NET_CONNECTION_STATUS)]
        public static void HandleBattleNetConnectionStatus(Packet packet)
        {
            packet.ReadBits("State", 2); // TODO: enum
            packet.ReadBit("SuppressNotification");
        }

        [Parser(Opcode.SMSG_CHANGE_REALM_TICKET_RESPONSE)]
        public static void HandleChangeRealmTicketResponse(Packet packet)
        {
            packet.ReadUInt32("Token");
            packet.ResetBitReader();
            packet.ReadBit("Allow");

            int protoSize = packet.ReadInt32();
            packet.ReadBytesTable("Ticket", protoSize);
        }

        [Parser(Opcode.SMSG_UPDATE_BNET_SESSION_KEY)]
        public static void HandleUpdateBnetSessionKey(Packet packet)
        {
            var sessionKeyLength = (int)packet.ReadBits(7);

            packet.ReadBytes("Digest", 32);
            packet.ReadBytes("SessionKey", sessionKeyLength);
        }

        [Parser(Opcode.CMSG_LOGOUT_REQUEST)]
        public static void HandleLogoutRequest(Packet packet)
        {
            packet.ReadBit("IdleLogout");
        }

        [Parser(Opcode.SMSG_WAIT_QUEUE_FINISH)]
        [Parser(Opcode.CMSG_LOGOUT_CANCEL)]
        [Parser(Opcode.CMSG_LOGOUT_INSTANT)]
        public static void HandleSessionZero(Packet packet)
        {
        }
    }
}
