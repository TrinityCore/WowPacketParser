using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class SessionHandler
    {
        public static void ReadClientSettings(Packet packet, params object[] idx)
        {
            packet.Translator.ReadSingle("FarClip", idx);
        }

        [Parser(Opcode.SMSG_AUTH_RESPONSE)]
        public static void HandleAuthResponse(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_4_21315))
                packet.Translator.ReadUInt32E<BattlenetRpcErrorCode>("Result");
            else
                packet.Translator.ReadByteE<ResponseCode>("Auth Code");

            var ok = packet.Translator.ReadBit("Success");
            var queued = packet.Translator.ReadBit("Queued");
            if (ok)
            {
                packet.Translator.ReadUInt32("VirtualRealmAddress");
                var realms = packet.Translator.ReadUInt32();
                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V6_2_4_21315))
                {
                    packet.Translator.ReadUInt32("TimeRemaining");
                    packet.Translator.ReadUInt32("TimeOptions");
                }
                packet.Translator.ReadUInt32("TimeRested");
                packet.Translator.ReadByte("ActiveExpansionLevel");
                packet.Translator.ReadByte("AccountExpansionLevel");
                packet.Translator.ReadUInt32("TimeSecondsUntilPCKick");
                var races = packet.Translator.ReadUInt32("AvailableRaces");
                var classes = packet.Translator.ReadUInt32("AvailableClasses");
                var templates = packet.Translator.ReadUInt32("Templates");
                packet.Translator.ReadUInt32("AccountCurrency");

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_4_21315))
                {
                    packet.Translator.ResetBitReader();
                    packet.Translator.ReadUInt32("BillingPlan");
                    packet.Translator.ReadUInt32("TimeRemain");
                    packet.Translator.ReadBit("InGameRoom");
                    packet.Translator.ReadBit("InGameRoom");
                    packet.Translator.ReadBit("InGameRoom");
                }

                for (var i = 0; i < realms; ++i)
                {
                    packet.Translator.ReadUInt32("VirtualRealmAddress", i);
                    packet.Translator.ResetBitReader();
                    packet.Translator.ReadBit("IsLocal", i);
                    packet.Translator.ReadBit("unk", i);
                    var nameLen1 = packet.Translator.ReadBits(8);
                    var nameLen2 = packet.Translator.ReadBits(8);
                    packet.Translator.ReadWoWString("RealmNameActual", nameLen1, i);
                    packet.Translator.ReadWoWString("RealmNameNormalized", nameLen2, i);
                }

                for (var i = 0; i < races; ++i)
                {
                    packet.Translator.ReadByteE<Race>("Race", i);
                    packet.Translator.ReadByteE<ClientType>("RequiredExpansion", i);
                }

                for (var i = 0; i < classes; ++i)
                {
                    packet.Translator.ReadByteE<Class>("Class", i);
                    packet.Translator.ReadByteE<ClientType>("RequiredExpansion", i);
                }

                for (var i = 0; i < templates; ++i)
                {
                    packet.Translator.ReadUInt32("TemplateSetId", i);
                    var templateClasses = packet.Translator.ReadUInt32();
                    for (var j = 0; j < templateClasses; ++j)
                    {
                        packet.Translator.ReadByteE<Class>("Class", i, j);
                        packet.Translator.ReadByte("FactionGroup", i, j);
                    }

                    packet.Translator.ResetBitReader();
                    var nameLen = packet.Translator.ReadBits(7);
                    var descLen = packet.Translator.ReadBits(10);
                    packet.Translator.ReadWoWString("Name", nameLen, i);
                    packet.Translator.ReadWoWString("Description", descLen, i);
                }

                packet.Translator.ResetBitReader();
                packet.Translator.ReadBit("Trial");
                packet.Translator.ReadBit("ForceCharacterTemplate");
                var horde = packet.Translator.ReadBit(); // NumPlayersHorde
                var alliance = packet.Translator.ReadBit(); // NumPlayersAlliance
                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V6_2_4_21315))
                    packet.Translator.ReadBit("IsVeteranTrial");

                if (horde)
                    packet.Translator.ReadUInt16("NumPlayersHorde");

                if (alliance)
                    packet.Translator.ReadUInt16("NumPlayersAlliance");
            }

            if (queued)
            {
                packet.Translator.ReadUInt32("QueuePos");
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_4_21315))
                    packet.Translator.ReadUInt32("WaitTime");
                packet.Translator.ResetBitReader();
                packet.Translator.ReadBit("HasFCM");
            }
        }

        [Parser(Opcode.SMSG_MOTD)]
        public static void HandleMessageOfTheDay(Packet packet)
        {
            var lineCount = packet.Translator.ReadBits("Line Count", 4);

            packet.Translator.ResetBitReader();
            for (var i = 0; i < lineCount; i++)
            {
                var lineLength = (int)packet.Translator.ReadBits(7);
                packet.Translator.ResetBitReader();
                packet.Translator.ReadWoWString("Line", lineLength, i);
            }
        }

        [Parser(Opcode.SMSG_SET_TIME_ZONE_INFORMATION)]
        public static void HandleSetTimeZoneInformation(Packet packet)
        {
            var len1 = packet.Translator.ReadBits(7);
            var len2 = packet.Translator.ReadBits(7);

            packet.Translator.ReadWoWString("ServerTimeTZ", len1);
            packet.Translator.ReadWoWString("GameTimeTZ", len2);
        }

        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.Zero, ClientVersionBuild.V6_2_4_21315)]
        public static void HandleAuthSession(Packet packet)
        {
            var sha = new byte[20];
            packet.Translator.ReadUInt32("Grunt ServerId");
            packet.Translator.ReadInt16E<ClientVersionBuild>("Client Build");
            packet.Translator.ReadUInt32("Region");
            packet.Translator.ReadUInt32("Battlegroup");
            packet.Translator.ReadUInt32("RealmIndex");
            packet.Translator.ReadByte("Login Server Type");
            packet.Translator.ReadByte("Unk");
            packet.Translator.ReadUInt32("Client Seed");
            packet.Translator.ReadUInt64("DosResponse");

            for (uint i = 0; i < 20; ++i)
                sha[i] = packet.Translator.ReadByte();

            var accountNameLength = packet.Translator.ReadBits("Account Name Length", 11);
            packet.Translator.ResetBitReader();
            packet.Translator.ReadWoWString("Account Name", accountNameLength);
            packet.Translator.ReadBit("UseIPv6");

            var addonSize = packet.Translator.ReadInt32("Addons Size");

            if (addonSize > 0)
            {
                var addons = new Packet(packet.Translator.ReadBytes(addonSize), packet.Opcode, packet.Time, packet.Direction,
                packet.Number, packet.Formatter, packet.FileName);
                CoreParsers.AddonHandler.ReadClientAddonsList(addons);
                addons.ClosePacket(false);
            }

            packet.AddValue("Proof SHA-1 Hash", Utilities.ByteArrayToHexString(sha));
        }

        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V6_2_4_21315)]
        public static void HandleAuthSession624(Packet packet)
        {
            packet.Translator.ReadInt16E<ClientVersionBuild>("Build");
            packet.Translator.ReadByte("BuildType");
            packet.Translator.ReadUInt32("RegionID");
            packet.Translator.ReadUInt32("BattlegroupID");
            packet.Translator.ReadUInt32("RealmID");
            packet.Translator.ReadBytes("LocalChallenge", 16);
            packet.Translator.ReadBytes("Digest", 24);
            packet.Translator.ReadUInt64("DosResponse");

            var addonSize = packet.Translator.ReadInt32();
            if (addonSize > 0)
            {
                var addons = new Packet(packet.Translator.ReadBytes(addonSize), packet.Opcode, packet.Time, packet.Direction,
                packet.Number, packet.Formatter, packet.FileName);
                CoreParsers.AddonHandler.ReadClientAddonsList(addons);
                addons.ClosePacket(false);
            }

            var realmJoinTicketSize = packet.Translator.ReadInt32();
            packet.Translator.ReadBytes("RealmJoinTicket", realmJoinTicketSize);
            packet.Translator.ReadBit("UseIPv6");

        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN)]
        public static void HandlePlayerLogin(Packet packet)
        {
            var guid = packet.Translator.ReadPackedGuid128("Guid");
            ReadClientSettings(packet, "ClientSettings");
            CoreParsers.SessionHandler.LoginGuid = guid;
        }

        [Parser(Opcode.CMSG_AUTH_CONTINUED_SESSION)]
        public static void HandleRedirectAuthProof(Packet packet)
        {
            packet.Translator.ReadInt64("DosResponse");
            packet.Translator.ReadInt64("Key");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_4_21315))
            {
                packet.Translator.ReadBytes("LocalChallenge", 16);
                packet.Translator.ReadBytes("Digest", 24);
            }
            else
                packet.Translator.ReadBytes("Digest", 20);
        }

        [Parser(Opcode.SMSG_CONNECT_TO)]
        public static void HandleRedirectClient422(Packet packet)
        {
            packet.Translator.ReadUInt64("Key");
            packet.Translator.ReadUInt32("Serial");
            packet.Translator.ReadBytes("Where (RSA encrypted)", 256);
            packet.Translator.ReadByte("Con");
        }

        [Parser(Opcode.SMSG_AUTH_CHALLENGE)]
        public static void HandleServerAuthChallenge(Packet packet)
        {
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V6_2_4_21315))
                packet.Translator.ReadUInt32("Challenge");
            for (uint i = 0; i < 8; ++i)
                packet.Translator.ReadUInt32("DosChallenge", i);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_4_21315))
                packet.Translator.ReadBytes("Challenge", 16);
            packet.Translator.ReadByte("DosZeroBits");
        }

        [Parser(Opcode.SMSG_LOGOUT_COMPLETE)]
        public static void HandleLogoutComplete(Packet packet)
        {
            CoreParsers.SessionHandler.LoginGuid = packet.Translator.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_DANCE_STUDIO_CREATE_RESULT)]
        public static void HandleDanceStudioCreateResult(Packet packet)
        {
            packet.Translator.ReadBit("Enable");

            for (int i = 0; i < 4; i++)
                packet.Translator.ReadInt32("Secrets", i);
        }

        [Parser(Opcode.CMSG_SUSPEND_COMMS_ACK)]
        public static void HandleSuspendCommsAck(Packet packet)
        {
            packet.Translator.ReadInt32("Serial");
            packet.Translator.ReadInt32("Timestamp");
        }

        [Parser(Opcode.CMSG_QUEUED_MESSAGES_END)]
        public static void HandleQueuedMessagesEnd(Packet packet)
        {
            packet.Translator.ReadInt32("Timestamp");
        }

        [Parser(Opcode.SMSG_WAIT_QUEUE_UPDATE)]
        public static void HandleWaitQueueUpdate(Packet packet)
        {
            packet.Translator.ReadInt32("WaitCount");
            packet.Translator.ReadBit("HasFCM");
        }

        [Parser(Opcode.SMSG_WAIT_QUEUE_FINISH)]
        public static void HandleZeroLengthPackets(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_QUERY_REALM_NAME)]
        public static void HandleRealmQuery(Packet packet)
        {
            packet.Translator.ReadInt32("VirtualRealmAddress");
        }

        [Parser(Opcode.SMSG_REALM_QUERY_RESPONSE)]
        public static void HandleRealmQueryResponse(Packet packet)
        {
            packet.Translator.ReadUInt32("VirtualRealmAddress");

            var state = packet.Translator.ReadByte("LookupState");
            if (state == 0)
            {
                packet.Translator.ResetBitReader();

                packet.Translator.ReadBit("IsLocal");
                packet.Translator.ReadBit("Unk bit");

                var bits2 = packet.Translator.ReadBits(8);
                var bits258 = packet.Translator.ReadBits(8);
                packet.Translator.ReadBit();

                packet.Translator.ReadWoWString("RealmNameActual", bits2);
                packet.Translator.ReadWoWString("RealmNameNormalized", bits258);
            }
        }

        [Parser(Opcode.SMSG_QUERY_TIME_RESPONSE)]
        public static void HandleTimeQueryResponse(Packet packet)
        {
            packet.Translator.ReadTime("CurrentTime");
            packet.Translator.ReadInt32("TimeOutRequest");
        }

        [Parser(Opcode.CMSG_CONNECT_TO_FAILED)]
        public static void HandleRedirectFailed(Packet packet)
        {
            packet.Translator.ReadUInt32("Serial");
            packet.Translator.ReadSByte("Con");
        }

        [Parser(Opcode.CMSG_SUSPEND_TOKEN_RESPONSE)]
        public static void HandleSuspendToken(Packet packet)
        {
            packet.Translator.ReadUInt32("Sequence");
        }

        [Parser(Opcode.SMSG_SUSPEND_TOKEN)]
        [Parser(Opcode.SMSG_RESUME_TOKEN)]
        public static void HandleResumeTokenPacket(Packet packet)
        {
            packet.Translator.ReadUInt32("Sequence");
            packet.Translator.ReadBits("Reason", 2);
        }

        [Parser(Opcode.CMSG_LOG_STREAMING_ERROR)]
        public static void HandleRouterClientLogStreamingError(Packet packet)
        {
            var bits16 = packet.Translator.ReadBits(9);
            packet.Translator.ReadWoWString("Error", bits16);
        }

        [Parser(Opcode.SMSG_BATTLENET_CHALLENGE_START)]
        public static void HandleBattlenetChallengeStart(Packet packet)
        {
            packet.Translator.ReadUInt32("Token");
            var bits16 = packet.Translator.ReadBits(9);
            packet.Translator.ReadWoWString("ChallengeURL", bits16);
        }

        [Parser(Opcode.CMSG_BATTLENET_CHALLENGE_RESPONSE)]
        public static void HandleBattlenetChallengeResponse(Packet packet)
        {
            packet.Translator.ReadUInt32("Token");
            var result = packet.Translator.ReadBits(3);
            if (result == 3)
            {
                var bits24 = packet.Translator.ReadBits(6);
                packet.Translator.ReadWoWString("BattlenetError", bits24);
            }
        }

        [Parser(Opcode.SMSG_BATTLENET_CHALLENGE_ABORT)]
        public static void HandleBattlenetChallengeAbort(Packet packet)
        {
            packet.Translator.ReadUInt32("Token");
            packet.Translator.ReadBit("Timeout");
        }

        public static void ReadMethodCall(Packet packet, params object[] idx)
        {
            packet.Translator.ReadUInt64("Type", idx);
            packet.Translator.ReadUInt64("ObjectId", idx);
            packet.Translator.ReadUInt32("Token", idx);
        }

        [Parser(Opcode.CMSG_BATTLENET_REQUEST)]
        public static void HandleBattlenetRequest(Packet packet)
        {
            ReadMethodCall(packet, "Method");

            int protoSize = packet.Translator.ReadInt32();
            packet.Translator.ReadBytesTable("Data", protoSize);
        }

        [Parser(Opcode.SMSG_BATTLENET_NOTIFICATION)]
        public static void HandleBattlenetNotification(Packet packet)
        {
            ReadMethodCall(packet, "Method");

            int protoSize = packet.Translator.ReadInt32();
            packet.Translator.ReadBytesTable("Data", protoSize);
        }

        [Parser(Opcode.SMSG_BATTLENET_RESPONSE)]
        public static void HandleBattlenetResponse(Packet packet)
        {
            packet.Translator.ReadInt32E<BattlenetRpcErrorCode>("BnetStatus");
            ReadMethodCall(packet, "Method");

            int protoSize = packet.Translator.ReadInt32();
            packet.Translator.ReadBytesTable("Data", protoSize);
        }

        [Parser(Opcode.SMSG_BATTLENET_SET_SESSION_STATE)]
        public static void HandleBattlenetSetSessionState(Packet packet)
        {
            packet.Translator.ReadBits("State", 2); // TODO: enum
        }

        [Parser(Opcode.CMSG_UPDATE_CLIENT_SETTINGS)]
        public static void HandleUpdateClientSettings(Packet packet)
        {
            ReadClientSettings(packet, "ClientSettings");
        }
    }
}
