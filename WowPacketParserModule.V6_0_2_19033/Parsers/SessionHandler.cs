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
            packet.ReadSingle("FarClip", idx);
        }

        [Parser(Opcode.SMSG_AUTH_RESPONSE)]
        public static void HandleAuthResponse(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_4_21315))
                packet.ReadUInt32E<BattlenetRpcErrorCode>("Result");
            else
                packet.ReadByteE<ResponseCode>("Auth Code");

            var ok = packet.ReadBit("Success");
            var queued = packet.ReadBit("Queued");
            if (ok)
            {
                packet.ReadUInt32("VirtualRealmAddress");
                var realms = packet.ReadUInt32();
                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V6_2_4_21315))
                {
                    packet.ReadUInt32("TimeRemaining");
                    packet.ReadUInt32("TimeOptions");
                }
                packet.ReadUInt32("TimeRested");
                packet.ReadByte("ActiveExpansionLevel");
                packet.ReadByte("AccountExpansionLevel");
                packet.ReadUInt32("TimeSecondsUntilPCKick");
                var races = packet.ReadUInt32("AvailableRaces");
                var classes = packet.ReadUInt32("AvailableClasses");
                var templates = packet.ReadUInt32("Templates");
                packet.ReadUInt32("AccountCurrency");

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_4_21315))
                {
                    packet.ResetBitReader();
                    packet.ReadUInt32("BillingPlan");
                    packet.ReadUInt32("TimeRemain");
                    packet.ReadBit("InGameRoom");
                    packet.ReadBit("InGameRoom");
                    packet.ReadBit("InGameRoom");
                }

                for (var i = 0; i < realms; ++i)
                {
                    packet.ReadUInt32("VirtualRealmAddress", i);
                    packet.ResetBitReader();
                    packet.ReadBit("IsLocal", i);
                    packet.ReadBit("unk", i);
                    var nameLen1 = packet.ReadBits(8);
                    var nameLen2 = packet.ReadBits(8);
                    packet.ReadWoWString("RealmNameActual", nameLen1, i);
                    packet.ReadWoWString("RealmNameNormalized", nameLen2, i);
                }

                for (var i = 0; i < races; ++i)
                {
                    packet.ReadByteE<Race>("Race", i);
                    packet.ReadByteE<ClientType>("RequiredExpansion", i);
                }

                for (var i = 0; i < classes; ++i)
                {
                    packet.ReadByteE<Class>("Class", i);
                    packet.ReadByteE<ClientType>("RequiredExpansion", i);
                }

                for (var i = 0; i < templates; ++i)
                {
                    packet.ReadUInt32("TemplateSetId", i);
                    var templateClasses = packet.ReadUInt32();
                    for (var j = 0; j < templateClasses; ++j)
                    {
                        packet.ReadByteE<Class>("Class", i, j);
                        packet.ReadByte("FactionGroup", i, j);
                    }

                    packet.ResetBitReader();
                    var nameLen = packet.ReadBits(7);
                    var descLen = packet.ReadBits(10);
                    packet.ReadWoWString("Name", nameLen, i);
                    packet.ReadWoWString("Description", descLen, i);
                }

                packet.ResetBitReader();
                packet.ReadBit("Trial");
                packet.ReadBit("ForceCharacterTemplate");
                var horde = packet.ReadBit(); // NumPlayersHorde
                var alliance = packet.ReadBit(); // NumPlayersAlliance
                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V6_2_4_21315))
                    packet.ReadBit("IsVeteranTrial");

                if (horde)
                    packet.ReadUInt16("NumPlayersHorde");

                if (alliance)
                    packet.ReadUInt16("NumPlayersAlliance");
            }

            if (queued)
            {
                packet.ReadUInt32("QueuePos");
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_4_21315))
                    packet.ReadUInt32("WaitTime");
                packet.ResetBitReader();
                packet.ReadBit("HasFCM");
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

        [Parser(Opcode.SMSG_SET_TIME_ZONE_INFORMATION)]
        public static void HandleSetTimeZoneInformation(Packet packet)
        {
            var len1 = packet.ReadBits(7);
            var len2 = packet.ReadBits(7);

            packet.ReadWoWString("ServerTimeTZ", len1);
            packet.ReadWoWString("GameTimeTZ", len2);
        }

        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.Zero, ClientVersionBuild.V6_2_4_21315)]
        public static void HandleAuthSession(Packet packet)
        {
            var sha = new byte[20];
            packet.ReadUInt32("Grunt ServerId");
            packet.ReadInt16E<ClientVersionBuild>("Client Build");
            packet.ReadUInt32("Region");
            packet.ReadUInt32("Battlegroup");
            packet.ReadUInt32("RealmIndex");
            packet.ReadByte("Login Server Type");
            packet.ReadByte("Unk");
            packet.ReadUInt32("Client Seed");
            packet.ReadUInt64("DosResponse");

            for (uint i = 0; i < 20; ++i)
                sha[i] = packet.ReadByte();

            var accountNameLength = packet.ReadBits("Account Name Length", 11);
            packet.ResetBitReader();
            packet.ReadWoWString("Account Name", accountNameLength);
            packet.ReadBit("UseIPv6");

            var addonSize = packet.ReadInt32("Addons Size");

            if (addonSize > 0)
            {
                var addons = new Packet(packet.ReadBytes(addonSize), packet.Opcode, packet.Time, packet.Direction,
                packet.Number, packet.Writer, packet.FileName);
                CoreParsers.AddonHandler.ReadClientAddonsList(addons);
                addons.ClosePacket(false);
            }

            packet.AddValue("Proof SHA-1 Hash", Convert.ToHexString(sha));
        }

        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V6_2_4_21315)]
        public static void HandleAuthSession624(Packet packet)
        {
            packet.ReadInt16E<ClientVersionBuild>("Build");
            packet.ReadByte("BuildType");
            packet.ReadUInt32("RegionID");
            packet.ReadUInt32("BattlegroupID");
            packet.ReadUInt32("RealmID");
            packet.ReadBytes("LocalChallenge", 16);
            packet.ReadBytes("Digest", 24);
            packet.ReadUInt64("DosResponse");

            var addonSize = packet.ReadInt32();
            if (addonSize > 0)
            {
                var addons = new Packet(packet.ReadBytes(addonSize), packet.Opcode, packet.Time, packet.Direction,
                packet.Number, packet.Writer, packet.FileName);
                CoreParsers.AddonHandler.ReadClientAddonsList(addons);
                addons.ClosePacket(false);
            }

            var realmJoinTicketSize = packet.ReadInt32();
            packet.ReadBytes("RealmJoinTicket", realmJoinTicketSize);
            packet.ReadBit("UseIPv6");

        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN)]
        public static void HandlePlayerLogin(Packet packet)
        {
            var guid = packet.ReadPackedGuid128("Guid");
            ReadClientSettings(packet, "ClientSettings");
            CoreParsers.SessionHandler.LoginGuid = guid;
            packet.Holder.PlayerLogin = new() { PlayerGuid = guid };
        }

        [Parser(Opcode.CMSG_AUTH_CONTINUED_SESSION)]
        public static void HandleRedirectAuthProof(Packet packet)
        {
            packet.ReadInt64("DosResponse");
            packet.ReadInt64("Key");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_4_21315))
            {
                packet.ReadBytes("LocalChallenge", 16);
                packet.ReadBytes("Digest", 24);
            }
            else
                packet.ReadBytes("Digest", 20);
        }

        [Parser(Opcode.SMSG_CONNECT_TO)]
        public static void HandleRedirectClient422(Packet packet)
        {
            packet.ReadUInt64("Key");
            packet.ReadUInt32("Serial");
            packet.ReadBytes("Where (RSA encrypted)", 256);
            packet.ReadByte("Con");
        }

        [Parser(Opcode.SMSG_AUTH_CHALLENGE)]
        public static void HandleServerAuthChallenge(Packet packet)
        {
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V6_2_4_21315))
                packet.ReadUInt32("Challenge");
            for (uint i = 0; i < 8; ++i)
                packet.ReadUInt32("DosChallenge", i);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_4_21315))
                packet.ReadBytes("Challenge", 16);
            packet.ReadByte("DosZeroBits");
        }

        [Parser(Opcode.SMSG_LOGOUT_COMPLETE)]
        public static void HandleLogoutComplete(Packet packet)
        {
            CoreParsers.SessionHandler.LoginGuid = packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_DANCE_STUDIO_CREATE_RESULT)]
        public static void HandleDanceStudioCreateResult(Packet packet)
        {
            packet.ReadBit("Enable");

            for (int i = 0; i < 4; i++)
                packet.ReadInt32("Secrets", i);
        }

        [Parser(Opcode.CMSG_SUSPEND_COMMS_ACK)]
        public static void HandleSuspendCommsAck(Packet packet)
        {
            packet.ReadInt32("Serial");
            packet.ReadInt32("Timestamp");
        }

        [Parser(Opcode.CMSG_QUEUED_MESSAGES_END)]
        public static void HandleQueuedMessagesEnd(Packet packet)
        {
            packet.ReadInt32("Timestamp");
        }

        [Parser(Opcode.SMSG_WAIT_QUEUE_UPDATE)]
        public static void HandleWaitQueueUpdate(Packet packet)
        {
            packet.ReadInt32("WaitCount");
            packet.ReadBit("HasFCM");
        }

        [Parser(Opcode.SMSG_WAIT_QUEUE_FINISH)]
        public static void HandleZeroLengthPackets(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_QUERY_REALM_NAME)]
        public static void HandleRealmQuery(Packet packet)
        {
            packet.ReadInt32("VirtualRealmAddress");
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

                var bits2 = packet.ReadBits(8);
                var bits258 = packet.ReadBits(8);
                packet.ReadBit();

                packet.ReadWoWString("RealmNameActual", bits2);
                packet.ReadWoWString("RealmNameNormalized", bits258);
            }
        }

        [Parser(Opcode.SMSG_QUERY_TIME_RESPONSE)]
        public static void HandleTimeQueryResponse(Packet packet)
        {
            packet.ReadTime("CurrentTime");
            packet.ReadInt32("TimeOutRequest");
        }

        [Parser(Opcode.CMSG_CONNECT_TO_FAILED)]
        public static void HandleRedirectFailed(Packet packet)
        {
            if (ClientVersion.RemovedInVersion(ClientType.TheWarWithin))
                packet.ReadUInt32("Serial");

            packet.ReadSByte("Con");

            if (ClientVersion.AddedInVersion(ClientType.TheWarWithin))
                packet.ReadUInt32("Serial");
        }

        [Parser(Opcode.CMSG_SUSPEND_TOKEN_RESPONSE)]
        public static void HandleSuspendToken(Packet packet)
        {
            packet.ReadUInt32("Sequence");
        }

        [Parser(Opcode.SMSG_SUSPEND_TOKEN)]
        [Parser(Opcode.SMSG_RESUME_TOKEN)]
        public static void HandleResumeTokenPacket(Packet packet)
        {
            packet.ReadUInt32("Sequence");
            packet.ReadBits("Reason", 2);
        }

        [Parser(Opcode.CMSG_LOG_STREAMING_ERROR)]
        public static void HandleRouterClientLogStreamingError(Packet packet)
        {
            var bits16 = packet.ReadBits(9);
            packet.ReadWoWString("Error", bits16);
        }

        [Parser(Opcode.SMSG_BATTLENET_CHALLENGE_START)]
        public static void HandleBattlenetChallengeStart(Packet packet)
        {
            packet.ReadUInt32("Token");
            var bits16 = packet.ReadBits(9);
            packet.ReadWoWString("ChallengeURL", bits16);
        }

        [Parser(Opcode.CMSG_BATTLENET_CHALLENGE_RESPONSE)]
        public static void HandleBattlenetChallengeResponse(Packet packet)
        {
            packet.ReadUInt32("Token");
            var result = packet.ReadBits(3);
            if (result == 3)
            {
                var bits24 = packet.ReadBits(6);
                packet.ReadWoWString("BattlenetError", bits24);
            }
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
        }

        [Parser(Opcode.CMSG_UPDATE_CLIENT_SETTINGS)]
        public static void HandleUpdateClientSettings(Packet packet)
        {
            ReadClientSettings(packet, "ClientSettings");
        }

        [Parser(Opcode.SMSG_CHANGE_REALM_TICKET_RESPONSE)]
        public static void HandleChangeRealmTicketResponse(Packet packet)
        {
            packet.ReadUInt32("Token");
            packet.ResetBitReader();
            packet.ReadBit("Allow");

            int protoSize = packet.ReadInt32();
            packet.ReadBytesTable("Data", protoSize);
        }

        [Parser(Opcode.CMSG_CHANGE_REALM_TICKET)]
        public static void HandleChangeRealmTicket(Packet packet)
        {
            packet.ReadUInt32("Token");
            packet.ReadBytes("Secret", 32);
        }
    }
}
