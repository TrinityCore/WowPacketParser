using System;
using System.Diagnostics.CodeAnalysis;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.Parsing.Parsers
{
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    public static class MiscellaneousParsers
    {
        [Parser(Opcode.CMSG_LOG_DISCONNECT)]
        public static void HandleLogDisconnect(Packet packet)
        {
            packet.Translator.ReadUInt32("Reason");
            // 4 is inability for client to decrypt RSA
            // 3 is not receiving "WORLD OF WARCRAFT CONNECTION - SERVER TO CLIENT"
            // 11 is sent on receiving opcode 0x140 with some specific data
        }

        [Parser(Opcode.CMSG_VIOLENCE_LEVEL)]
        public static void HandleSetViolenceLevel(Packet packet)
        {
            packet.Translator.ReadByte("Level");
        }

        [Parser(Opcode.SMSG_HOTFIX_NOTIFY)]
        public static void HandleHotfixNotify(Packet packet)
        {
            packet.Translator.ReadInt32("Unk int32");
            packet.Translator.ReadUInt32("Unk int32");
            packet.Translator.ReadUInt32("Unk int32");
        }

        [Parser(Opcode.SMSG_HOTFIX_NOTIFY_BLOB)]
        public static void HandleHotfixInfo(Packet packet)
        {
            uint count = ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_4_15595) ? packet.Translator.ReadBits("Count", 22) : packet.Translator.ReadUInt32("Count");

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadUInt32E<DB2Hash>("Hotfix DB2 File", i);
                packet.Translator.ReadTime("Hotfix date", i);
                packet.Translator.ReadInt32("Hotfixed entry", i);
            }
        }

        [Parser(Opcode.TEST_430_SYNC_PLAYER_MOVE)]
        public static void HandleUnk5(Packet packet)
        {
            packet.Translator.ReadVector4("Position");
        }

        [Parser(Opcode.CMSG_ENABLE_NAGLE)]
        public static void HandleEnableNagle(Packet packet)
        {
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_3_4_15595))
                packet.Translator.ReadUInt32("Enable");
        }

        [Parser(Opcode.CMSG_SUSPEND_TOKEN_RESPONSE)]
        public static void HandleSuspendToken(Packet packet)
        {
            packet.Translator.ReadUInt32("Count");
        }

        [Parser(Opcode.SMSG_SUSPEND_TOKEN)]
        public static void HandleSuspendTokenResponse(Packet packet)
        {
            packet.Translator.ReadBit("Unk");
            packet.Translator.ReadUInt32("Count");
        }

        [Parser(Opcode.SMSG_COMPRESSED_MULTIPLE_PACKETS)]
        public static void HandleCompressedMultiplePackets(Packet packet)
        {
            using (Packet packet2 = packet.Inflate(packet.Translator.ReadInt32()))
                HandleMultiplePackets(packet2);
        }

        [Parser(Opcode.SMSG_MULTIPLE_PACKETS)]
        public static void HandleMultiplePackets(Packet packet)
        {
            packet.Formatter.OpenCollection("");
            int i = 0;
            while (packet.CanRead())
            {
                int opcode = 0;
                int len = 0;
                byte[] bytes = null;
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005))
                {
                    opcode = packet.Translator.ReadUInt16();
                    // Why are there so many 0s in some packets? Should we have some check if opcode == 0 here?
                    len = packet.Translator.ReadUInt16();
                    bytes = packet.Translator.ReadBytes(len);
                }
                else if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                {
                    len = packet.Translator.ReadUInt16();
                    opcode = packet.Translator.ReadUInt16();
                    bytes = packet.Translator.ReadBytes(len - 2);
                }
                else
                {
                    packet.ReadToEnd();
                }

                if (bytes == null || len == 0)
                    continue;

                //if (i > 0)
                //    packet.WriteLi();

                //packet.Write("[{0}] ", i++);
                if (i > 0)
                    packet.Formatter.AppendItem("[{0}] ", i++);

                using (Packet newpacket = new Packet(bytes, opcode, packet.Time, packet.Direction, packet.Number, packet.Formatter, packet.FileName))
                    Handler.Parse(newpacket, true);

            }
            packet.Formatter.CloseCollection("");
        }

        [Parser(Opcode.SMSG_MULTIPLE_PACKETS_2)]
        public static void HandleMultiplePackets2(Packet packet)
        {

            if (ClientVersion.AddedInVersion(ClientType.Cataclysm))
            {
                packet.ReadToEnd();
                throw new NotImplementedException("This opcode heavily relies on ALL" +
                                                  "of its contained packets to be parsed successfully");
                // Some sort of infinite loop happens here...
            }

            packet.Formatter.OpenCollection("");
            //packet.WriteLine();
            while (packet.CanRead())
            {
                packet.Opcode = packet.Translator.ReadUInt16();

                Handler.Parse(packet, true);
                //packet.WriteLine();
            }
            packet.Formatter.CloseCollection("");
        }

        [Parser(Opcode.SMSG_STOP_DANCE)]
        [Parser(Opcode.SMSG_INVALIDATE_PLAYER)]
        [Parser(Opcode.CMSG_SET_SELECTION, ClientVersionBuild.Zero, ClientVersionBuild.V5_1_0_16309)]
        [Parser(Opcode.CMSG_INSPECT)]
        [Parser(Opcode.CMSG_BUY_BANK_SLOT)]
        [Parser(Opcode.CMSG_DEL_FRIEND)]
        [Parser(Opcode.CMSG_DEL_IGNORE)]
        [Parser(Opcode.CMSG_DUEL_ACCEPTED)]
        [Parser(Opcode.CMSG_DUEL_CANCELLED)]
        [Parser(Opcode.SMSG_REFER_A_FRIEND_EXPIRED)]
        [Parser(Opcode.CMSG_PLAYER_VEHICLE_ENTER)]
        [Parser(Opcode.CMSG_EJECT_PASSENGER)]
        public static void HandleReadGuid(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_SET_SELECTION, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleSetSelection510(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(0, 1, 2, 4, 7, 3, 6, 5);
            packet.Translator.ParseBitStream(guid, 4, 1, 5, 2, 6, 7, 0, 3);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_GRANT_LEVEL)]
        [Parser(Opcode.CMSG_ACCEPT_LEVEL_GRANT)]
        [Parser(Opcode.SMSG_PROPOSE_LEVEL_GRANT)]
        public static void HandleGrantLevel(Packet packet)
        {
            packet.Translator.ReadPackedGuid("GUID");
        }

        [Parser(Opcode.CMSG_REQUEST_VEHICLE_SWITCH_SEAT)]
        public static void HandleRequestVehicleSwitchSeat(Packet packet)
        {
            packet.Translator.ReadPackedGuid("Vehicle");
            packet.Translator.ReadByte("SeatIndex");
        }

        [Parser(Opcode.CMSG_CHANGE_SEATS_ON_CONTROLLED_VEHICLE)]
        public static void HandleChangeSeatsOnControlledVehicle(Packet packet)
        {
            var guid = packet.Translator.ReadPackedGuid("Vehicle GUID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3a_11723))
                MovementHandler.ReadMovementInfo(packet, guid);

            packet.Translator.ReadPackedGuid("Accessory GUID");
            packet.Translator.ReadByte("Seat");
        }

        [Parser(Opcode.SMSG_CROSSED_INEBRIATION_THRESHOLD)]
        public static void HandleCrossedInerbriationThreshold(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadUInt32E<DrunkenState>("Drunken State");
            packet.Translator.ReadUInt32<ItemId>("Entry");
        }

        [Parser(Opcode.SMSG_BUY_BANK_SLOT_RESULT)]
        public static void HandleBuyBankSlotResult(Packet packet)
        {
            packet.Translator.ReadUInt32E<BankSlotResult>("Result");
        }

        [Parser(Opcode.CMSG_ADD_FRIEND)]
        public static void HandleAddFriend(Packet packet)
        {
            packet.Translator.ReadCString("Name");
            packet.Translator.ReadCString("Note");
        }

        [Parser(Opcode.CMSG_ADD_IGNORE)]
        public static void HandleAddIgnore(Packet packet)
        {
            packet.Translator.ReadCString("Name");
        }

        [Parser(Opcode.CMSG_SET_CONTACT_NOTES)]
        public static void HandleSetContactNotes(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadCString("Name");
        }

        [Parser(Opcode.CMSG_BUG)]
        public static void HandleBug(Packet packet)
        {
            packet.Translator.ReadUInt32("Suggestion");
            packet.Translator.ReadUInt32("Content Lenght");
            packet.Translator.ReadCString("Content");
            packet.Translator.ReadUInt32("Text Lenght");
            packet.Translator.ReadCString("Text");
        }

        [Parser(Opcode.CMSG_SET_ACTION_BUTTON, ClientVersionBuild.Zero, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleActionButton(Packet packet)
        {
            packet.Translator.ReadByte("Button");
            var data = packet.Translator.ReadInt32();
            packet.AddValue("Type", (ActionButtonType)((data & 0xFF000000) >> 24));
            packet.AddValue("ID", data & 0x00FFFFFF);
        }

        [Parser(Opcode.CMSG_SET_ACTION_BUTTON, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleSetActionButton(Packet packet)
        {
            packet.Translator.ReadByte("Slot Id");
            var actionId = packet.Translator.StartBitStream(0, 7, 6, 1, 3, 5, 2, 4);
            packet.Translator.ParseBitStream(actionId, 3, 0, 1, 4, 7, 2, 6, 5);
            packet.AddValue("Action Id", BitConverter.ToUInt32(actionId, 0));
        }

        [Parser(Opcode.SMSG_RESURRECT_REQUEST)]
        public static void HandleResurrectRequest(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadUInt32("Name length");
            packet.Translator.ReadCString("Resurrector Name");
            packet.Translator.ReadBool("Resurrection Sickness");
            packet.Translator.ReadBool("Use Timer");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                packet.Translator.ReadInt32<SpellId>("Spell ID");   // Used only for: <if (Spell ID == 83968 && Unit_HasAura(95223) return 1;>
        }

        [Parser(Opcode.CMSG_RESURRECT_RESPONSE)]
        public static void HandleResurrectResponse(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadBool("Accept");
        }

        [Parser(Opcode.CMSG_REPOP_REQUEST)]
        public static void HandleRepopRequest(Packet packet)
        {
            packet.Translator.ReadBool("Accept");
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleFeatureSystemStatus(Packet packet)
        {
            packet.Translator.ReadBool("Enable Complaint Chat");
            packet.Translator.ReadBool("Enable Voice Chat");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                packet.Translator.ReadInt32("Complain System Status");
            else if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_0_14333))
            {
                packet.Translator.ReadByte("Complain System Status");
                packet.Translator.ReadInt32("Unknown Mail Url Related Value");
            }
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleFeatureSystemStatus430(Packet packet)
        {
            packet.Translator.ReadInt32("Unk int32");
            packet.Translator.ReadByte("Complain System Status");
            packet.Translator.ReadInt32("Unknown Mail Url Related Value");
            packet.Translator.ReadBit("IsVoiceChatAllowedByServer");
            packet.Translator.ReadBit("CanSendSoRByText");
            packet.Translator.ReadBit("HasTravelPass");
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleFeatureSystemStatus434(Packet packet)
        {
            packet.Translator.ReadByte("Complain System Status");
            packet.Translator.ReadInt32("Scroll of Resurrections Remaining");
            packet.Translator.ReadInt32("Scroll of Resurrections Per Day");
            packet.Translator.ReadInt32("Unused Int32");
            packet.Translator.ReadInt32("Unused Int32");
            packet.Translator.ReadBit("HasTravelPass");
            packet.Translator.ReadBit("GMItemRestorationButtonEnabled");
            packet.Translator.ReadBit("Scroll of Resurrection Enabled");
            var quickTicket = packet.Translator.ReadBit("EuropaTicketSystemEnabled");
            var sessionTimeAlert = packet.Translator.ReadBit("Session Time Alert");
            packet.Translator.ReadBit("IsVoiceChatAllowedByServer");

            if (quickTicket)
            {
                packet.Translator.ReadInt32("Unk5");
                packet.Translator.ReadInt32("Unk6");
                packet.Translator.ReadInt32("Unk7");
                packet.Translator.ReadInt32("Unk8");
            }

            if (sessionTimeAlert)
            {
                packet.Translator.ReadInt32("Session Alert Delay");
                packet.Translator.ReadInt32("Session Alert Period");
                packet.Translator.ReadInt32("Session Alert DisplayTime");
            }
        }

        [Parser(Opcode.CMSG_REALM_SPLIT)]
        public static void HandleClientRealmSplit(Packet packet)
        {
            packet.Translator.ReadInt32E<ClientSplitState>("Client State");
        }

        [Parser(Opcode.SMSG_REALM_SPLIT)]
        public static void HandleServerRealmSplit(Packet packet)
        {
            packet.Translator.ReadInt32E<ClientSplitState>("Client State");
            packet.Translator.ReadInt32E<PendingSplitState>("Split State");
            packet.Translator.ReadCString("Split Date");
        }

        [Parser(Opcode.CMSG_PING)]
        public static void HandleClientPing(Packet packet)
        {
            packet.Translator.ReadInt32("Ping");
            packet.Translator.ReadInt32("Ping Count");
        }

        [Parser(Opcode.SMSG_PONG)]
        public static void HandleServerPong(Packet packet)
        {
            packet.Translator.ReadInt32("Ping");
        }

        [Parser(Opcode.SMSG_CACHE_VERSION)]
        public static void HandleClientCacheVersion(Packet packet)
        {
            packet.Translator.ReadInt32("Version");
        }

        [Parser(Opcode.SMSG_TIME_SYNC_REQUEST)]
        public static void HandleTimeSyncReq(Packet packet)
        {
            packet.Translator.ReadInt32("Count");
        }

        [Parser(Opcode.SMSG_LEARNED_DANCE_MOVES)]
        public static void HandleLearnedDanceMoves(Packet packet)
        {
            packet.Translator.ReadInt32("Dance Move Id"); // Dance move is Int64?
            packet.Translator.ReadInt32("Unk int");
        }

        [Parser(Opcode.SMSG_TRIGGER_CINEMATIC)]
        [Parser(Opcode.SMSG_TRIGGER_MOVIE)]
        public static void HandleTriggerSequence(Packet packet)
        {
            packet.Translator.ReadInt32("Sequence Id");
        }

        [Parser(Opcode.SMSG_PLAY_SOUND)]
        [Parser(Opcode.SMSG_PLAY_MUSIC)]
        [Parser(Opcode.SMSG_PLAY_OBJECT_SOUND)]
        public static void HandleSoundMessages(Packet packet)
        {
            uint sound = packet.Translator.ReadUInt32("Sound Id");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005))
                packet.Translator.ReadGuid("GUID");

            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_PLAY_OBJECT_SOUND, Direction.ServerToClient))
                packet.Translator.ReadGuid("GUID 2");

            Storage.Sounds.Add(sound, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_WEATHER)]
        public static void HandleWeatherStatus(Packet packet)
        {
            WeatherState state = packet.Translator.ReadInt32E<WeatherState>("State");
            float grade = packet.Translator.ReadSingle("Grade");
            byte unk = packet.Translator.ReadByte("Unk Byte"); // Type

            Storage.WeatherUpdates.Add(new WeatherUpdate
            {
                MapId = MovementHandler.CurrentMapId,
                ZoneId = 0, // fixme
                State = state,
                Grade = grade,
                Unk = unk
            }, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_TUTORIAL_FLAG)]
        public static void HandleTutorialFlag(Packet packet)
        {
            packet.Translator.ReadInt32("Flag");
        }

        [Parser(Opcode.SMSG_TUTORIAL_FLAGS)]
        public static void HandleTutorialFlags(Packet packet)
        {
            for (var i = 0; i < 32; i++)
                packet.Translator.ReadByte("TutorialData", i);
        }

        [Parser(Opcode.CMSG_AREA_TRIGGER)]
        public static void HandleClientAreaTrigger(Packet packet)
        {
            var entry = packet.Translator.ReadEntry("Area Trigger Id");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_1_0_16309))
                packet.Translator.ReadByte("Unk Byte");

            packet.AddSniffData(StoreNameType.AreaTrigger, entry.Key, "AREATRIGGER");
        }

        [Parser(Opcode.SMSG_PRE_RESSURECT)]
        public static void HandlePreResurrect(Packet packet)
        {
            packet.Translator.ReadPackedGuid("GUID");
        }

        [Parser(Opcode.SMSG_FORCE_ANIM)]
        public static void HandleForceAnimation(Packet packet) // It's still unknown until confirmed.
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadCString("Unk String");
        }

        [Parser(Opcode.SMSG_SUSPEND_COMMS)]
        [Parser(Opcode.CMSG_SUSPEND_COMMS_ACK)]
        public static void HandleSuspendCommsPackets(Packet packet)
        {
            packet.Translator.ReadInt32("Serial");
        }

        [Parser(Opcode.CMSG_LOW_LEVEL_RAID1)]
        [Parser(Opcode.CMSG_LOW_LEVEL_RAID2)]
        public static void HandleLowLevelRaidPackets(Packet packet)
        {
            packet.Translator.ReadBool("Allow");
        }

        [Parser(Opcode.SMSG_EXPLORATION_EXPERIENCE)]
        public static void HandleExplorationExperience(Packet packet)
        {
            packet.Translator.ReadUInt32<AreaId>("Area ID");
            packet.Translator.ReadUInt32("Experience");
        }

        [Parser(Opcode.SMSG_START_MIRROR_TIMER)]
        public static void HandleStartMirrorTimer(Packet packet)
        {
            packet.Translator.ReadUInt32E<MirrorTimerType>("Timer Type");
            packet.Translator.ReadUInt32("Current Value");
            packet.Translator.ReadUInt32("Max Value");
            packet.Translator.ReadInt32("Regen");
            packet.Translator.ReadBool("Paused");
            packet.Translator.ReadUInt32("Spell Id");
        }

        [Parser(Opcode.SMSG_PAUSE_MIRROR_TIMER)]
        public static void HandlePauseMirrorTimer(Packet packet)
        {
            packet.Translator.ReadUInt32E<MirrorTimerType>("Timer Type");
            packet.Translator.ReadBool("Paused");
        }

        [Parser(Opcode.SMSG_STOP_MIRROR_TIMER)]
        public static void HandleStopMirrorTimer(Packet packet)
        {
            packet.Translator.ReadUInt32E<MirrorTimerType>("Timer Type");
        }

        [Parser(Opcode.SMSG_DEATH_RELEASE_LOC)]
        public static void HandleDeathReleaseLoc(Packet packet)
        {
            packet.Translator.ReadInt32<MapId>("Map Id");
            packet.Translator.ReadVector3("Position");
        }

        [Parser(Opcode.CMSG_ZONEUPDATE)]
        [Parser(Opcode.SMSG_ZONE_UNDER_ATTACK)]
        public static void HandleZoneUpdate(Packet packet)
        {
            packet.Translator.ReadUInt32<ZoneId>("Zone Id");
        }

        [Parser(Opcode.CMSG_PLAY_DANCE)]
        public static void HandleClientPlayDance(Packet packet)
        {
            packet.Translator.ReadInt32("Unk int32 1");
            packet.Translator.ReadInt32("Unk int32 2");
        }

        /*
        [Parser(Opcode.SMSG_NOTIFY_DANCE)]
        public static void HandleNotifyDance(Packet packet)
        {
            var flag = packet.Translator.ReadInt32E<?>("Flag");

            if (flag & 0x8)
            {
                var unk4 = packet.Translator.ReadInt32();
                if (unk4 == 1)
                    packet.WriteLine("Error msg = ERR_DANCE_SAVE_FAILED");
                else if (unk4 == 2)
                    packet.WriteLine("Error msg = ERR_DANCE_DELETE_FAILED");
                else if (unk4 == 0)
                    packet.WriteLine("Error msg = ERR_DANCE_CREATE_DUPLICATE");
            }
            else
            {
                packet.Translator.ReadInt32("Unk int 1");
                packet.Translator.ReadCString("Unk string");
                packet.Translator.ReadInt32("Unk int 2");
            }
        }
        */

        [Parser(Opcode.SMSG_PLAY_DANCE)]
        public static void HandleServerPlayDance(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadInt32("Unk int32 1");
            packet.Translator.ReadInt32("Unk int32 2");
            packet.Translator.ReadUInt32("Unk int32 3");
            packet.Translator.ReadInt32("Unk int32 4");
        }

        [Parser(Opcode.CMSG_WHO)]
        public static void HandleWhoRequest(Packet packet)
        {
            packet.Translator.ReadInt32("Min Level");
            packet.Translator.ReadInt32("Max Level");
            packet.Translator.ReadCString("Player Name");
            packet.Translator.ReadCString("Guild Name");
            packet.Translator.ReadInt32("RaceMask");
            packet.Translator.ReadInt32("ClassMask");

            var zones = packet.Translator.ReadUInt32("Zones count");
            for (var i = 0; i < zones; ++i)
                packet.Translator.ReadUInt32<ZoneId>("Zone Id");

            var patterns = packet.Translator.ReadUInt32("Pattern count");
            for (var i = 0; i < patterns; ++i)
                packet.Translator.ReadCString("Pattern", i);
        }

        [Parser(Opcode.SMSG_WHO)]
        public static void HandleWho(Packet packet)
        {
            var counter = packet.Translator.ReadUInt32("List count");
            packet.Translator.ReadUInt32("Online count");

            for (var i = 0; i < counter; ++i)
            {
                packet.Translator.ReadCString("Name", i);
                packet.Translator.ReadCString("Guild", i);
                packet.Translator.ReadUInt32("Level", i);
                packet.Translator.ReadUInt32E<Class>("Class", i);
                packet.Translator.ReadUInt32E<Race>("Race", i);
                packet.Translator.ReadByteE<Gender>("Gender", i);
                packet.Translator.ReadUInt32<ZoneId>("Zone Id", i);
            }
        }

        [Parser(Opcode.CMSG_TIME_SYNC_RESPONSE)]
        public static void HandleTimeSyncResp(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545) && ClientVersion.RemovedInVersion(ClientVersionBuild.V4_3_4_15595)) // no idea when this was added exactly
            {
                packet.Translator.ReadUInt32("Ticks");
                packet.Translator.ReadUInt32("Counter");
            }
            else
            {
                packet.Translator.ReadUInt32("Counter");
                packet.Translator.ReadUInt32("Ticks");
            }
        }

        [Parser(Opcode.SMSG_GAME_TIME_SET)]
        public static void HandleGametimeSet(Packet packet)
        {
            packet.Translator.ReadUInt32("Unk time");
            packet.Translator.ReadUInt32("Unk int32");
        }

        [Parser(Opcode.SMSG_GAME_TIME_UPDATE)]
        public static void HandleGametimeUpdate(Packet packet)
        {
            packet.Translator.ReadUInt32("Unk time"); // Time online?

            if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing)) // no idea when this was added exactly, doesn't exist in 2.4.0
                packet.Translator.ReadUInt32("Unk int32");
        }

        [Parser(Opcode.CMSG_FAR_SIGHT)]
        [Parser(Opcode.SMSG_PLAYER_SKINNED)]
        public static void HandleFarSight(Packet packet)
        {
            packet.Translator.ReadBool("Apply");
        }

        [Parser(Opcode.SMSG_CHAT_SERVER_MESSAGE)]
        public static void HandleServerMessage(Packet packet)
        {
            packet.Translator.ReadUInt32("Server Message DBC Id");
            packet.Translator.ReadCString("Message");
        }

        [Parser(Opcode.SMSG_WORLD_SERVER_INFO, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleWorldServerInfo(Packet packet)
        {
            packet.Translator.ReadByte("Unk Byte");
            packet.Translator.ReadInt32("Unk Int32");
            packet.Translator.ReadInt32("Unk Int32");

            var b0 = packet.Translator.ReadBit("Unk Bit 1");
            var b1 = packet.Translator.ReadBit("Unk Bit 2");
            var b2 = packet.Translator.ReadBit("Unk Bit 3");

            if (b2)
                packet.Translator.ReadInt32("Unk Int32 (EVENT_INELIGIBLE_FOR_LOOT)");

            if (b0)
                packet.Translator.ReadInt32("Unk Int32");

            if (b1)
                packet.Translator.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.SMSG_WORLD_SERVER_INFO, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleWorldServerInfo434(Packet packet)
        {
            var hasRestrictedMoney = packet.Translator.ReadBit();
            var hasRestrictedLevel = packet.Translator.ReadBit();
            var isNotEligibleForLoot = packet.Translator.ReadBit();

            // Sends: "You are not eligible for these items because you recently defeated this encounter."
            if (isNotEligibleForLoot)
                packet.Translator.ReadUInt32("Unk UInt32");

            packet.Translator.ReadBool("Is On Tournament Realm");

            if (hasRestrictedLevel)
                packet.Translator.ReadInt32("Restricted Account Max Level");

            if (hasRestrictedMoney)
                packet.Translator.ReadInt32("Restricted Account Max Money");

            packet.Translator.ReadTime("Last Weekly Reset");
            packet.Translator.ReadInt32("Instance Difficulty ID");
        }

        [Parser(Opcode.CMSG_REQUEST_HONOR_STATS)]
        [Parser(Opcode.MSG_INSPECT_HONOR_STATS)]
        public static void HandleInspectHonorStats(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            if (packet.Direction == Direction.ClientToServer)
                return;

            packet.Translator.ReadByte("Honor Points");
            packet.Translator.ReadUInt32("Kills");
            packet.Translator.ReadUInt32("Today");
            packet.Translator.ReadUInt32("Yesterday");
            packet.Translator.ReadUInt32("Life Time Kills");
        }

        [Parser(Opcode.SMSG_INSPECT_HONOR_STATS)]
        public static void HandleInspectHonorStats434(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(4, 3, 6, 2, 5, 0, 7, 1);

            packet.Translator.ReadByte("Lifetime Max Rank");
            // Might be swapped, unsure
            packet.Translator.ReadInt16("Yesterday Honorable Kills");
            packet.Translator.ReadInt16("Today Honorable Kills");

            packet.Translator.ParseBitStream(guid, 2, 0, 6, 3, 4, 1, 5);

            packet.Translator.ReadInt32("Life Time Kills");

            packet.Translator.ParseBitStream(guid, 7);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [HasSniffData]
        [Parser(Opcode.CMSG_LOADING_SCREEN_NOTIFY, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)] // Also named CMSG_LOADING_SCREEN_NOTIFY
        public static void HandleClientEnterWorld(Packet packet)
        {
            packet.Translator.ReadBit("Showing");
            var mapId = packet.Translator.ReadUInt32<MapId>("MapID");
            MovementHandler.CurrentMapId = mapId;

            if (mapId < 1000) // Getting some weird results in a couple of packets
                packet.AddSniffData(StoreNameType.Map, (int) mapId, "LOAD_SCREEN");
        }

        [HasSniffData]
        [Parser(Opcode.CMSG_LOADING_SCREEN_NOTIFY, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleClientEnterWorld434(Packet packet)
        {
            var mapId = packet.Translator.ReadUInt32<MapId>("MapID");
            packet.Translator.ReadBit("Showing");
            MovementHandler.CurrentMapId = mapId;

            if (mapId < 1000) // Getting some weird results in a couple of packets
                packet.AddSniffData(StoreNameType.Map, (int) mapId, "LOAD_SCREEN");
        }

        [Parser(Opcode.MSG_VERIFY_CONNECTIVITY)]
        public static void HandleServerInfo(Packet packet)
        {
            packet.Translator.ReadCString("String");
        }

        [Parser(Opcode.SMSG_OVERRIDE_LIGHT)]
        public static void HandleOverrideLight(Packet packet)
        {
            packet.Translator.ReadUInt32("Current Light.dbc entry");
            packet.Translator.ReadUInt32("Target Light.dbc entry");
            packet.Translator.ReadUInt32("Fade in time");
        }

        [Parser(Opcode.SMSG_DURABILITY_DAMAGE_DEATH)]
        public static void HandleDurabilityDamageDeath(Packet packet)
        {
            // Confirm
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                packet.Translator.ReadInt32("Durability");
        }

        [Parser(Opcode.SMSG_CAMERA_SHAKE)]
        public static void HandleCameraShake(Packet packet)
        {
            packet.Translator.ReadInt32("SpellEffectCameraShakes ID");
            packet.Translator.ReadInt32("Sound ID");
        }

        [Parser(Opcode.SMSG_COMPLAINT_RESULT)]
        public static void HandleComplainResult(Packet packet)
        {
            packet.Translator.ReadByte("Result"); // value 1 resets CGChat::m_complaintsSystemStatus in client. (unused?)
            packet.Translator.ReadByte("ComplaintType"); // value 0xC generates a "CalendarError" in client. (found in 3.3.3a and 4.2.2a at least)
        }

        [Parser(Opcode.CMSG_MINIGAME_MOVE)]
        public static void HandleMinigameMove(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadByte("MoveType");
            packet.Translator.ReadUInt32("Param");
        }

        [Parser(Opcode.SMSG_MINIGAME_SETUP)]
        public static void HandleMiniGameSetup(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadByte("unk byte");
            byte unk1 = packet.Translator.ReadByte("unk byte");
            packet.Translator.ReadGuid("unk guid");
            packet.Translator.ReadGuid("unk guid");
            packet.Translator.ReadCString("string");
            if (unk1 == 2)
                packet.Translator.ReadByte("unk byte");
        }

        [Parser(Opcode.SMSG_SUMMON_REQUEST)]
        public static void HandleSummonRequest(Packet packet)
        {
            packet.Translator.ReadGuid("Summoner GUID");
            packet.Translator.ReadInt32<AreaId>("Area ID");
            packet.Translator.ReadTime("Summon Confirm Time");
        }

        [Parser(Opcode.CMSG_SUMMON_RESPONSE)]
        public static void HandleSummonResponse(Packet packet)
        {
            packet.Translator.ReadGuid("Summoner GUID");
            packet.Translator.ReadBool("Accept");
        }

        [Parser(Opcode.CMSG_SPELL_CLICK)]
        public static void HandleSpellClick(Packet packet)
        {
            WowGuid guid = packet.Translator.ReadGuid("GUID");

            if (guid.GetObjectType() == ObjectType.Unit)
                Storage.NpcSpellClicks.Add(guid, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_START_TIMER)]
        public static void HandleStartTimer(Packet packet)
        {
            packet.Translator.ReadUInt32E<TimerType>("Timer type");
            packet.Translator.ReadInt32("Time left (secs)");
            packet.Translator.ReadInt32("Total time (secs)");
        }

        [Parser(Opcode.CMSG_SET_PREFERRED_CEMETERY)] // 4.3.4
        public static void HandleSetPreferedCemetery(Packet packet)
        {
            packet.Translator.ReadUInt32("Cemetery Id");
        }

        [Parser(Opcode.SMSG_REQUEST_CEMETERY_LIST_RESPONSE)] // 4.3.4
        public static void HandleRequestCemeteryListResponse(Packet packet)
        {
            packet.Translator.ReadBit("Is MicroDungeon"); // Named in WorldMapFrame.lua
            var count = packet.Translator.ReadBits("Count", 24);
            for (int i = 0; i < count; ++i)
                packet.Translator.ReadInt32("Cemetery Id", i);
        }

        [Parser(Opcode.SMSG_FORCE_SET_VEHICLE_REC_ID)] // 4.3.4
        public static void HandleForceSetVehicleRecId(Packet packet)
        {
            packet.Translator.ReadInt32("Unk Int32 1"); // ##
            packet.Translator.ReadInt32("Vehicle Id");

            var guid = packet.Translator.StartBitStream(3, 0, 1, 7, 2, 6, 5, 4);
            packet.Translator.ParseBitStream(guid, 5, 7, 4, 3, 2, 6, 1, 0);
            packet.Translator.WriteGuid("Player GUID", guid);
        }

        [Parser(Opcode.CMSG_SET_VEHICLE_REC_ID_ACK)] //  4.3.4
        public static void HandleSetVehicleRecIdAck(Packet packet)
        {
            var guid = new byte[8];
            var transportGuid = new byte[8];
            var hasTransTime2 = false;
            var hasTransTime3 = false;
            var hasFallDirection = false;
            var pos = new Vector4();

            pos.Z = packet.Translator.ReadSingle();
            packet.Translator.ReadUInt32("Unk Int32 1"); // ##
            pos.X = packet.Translator.ReadSingle();
            pos.Y = packet.Translator.ReadSingle();
            packet.Translator.ReadUInt32("Vehicle ID");
            guid[7] = packet.Translator.ReadBit();
            var hasMovementFlags2 = !packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            var hasFallData = packet.Translator.ReadBit("Has fall data");
            var hasO = !packet.Translator.ReadBit();
            packet.Translator.ReadBit("Has Spline");
            var hasMovementFlags = !packet.Translator.ReadBit();
            var hasTrans = packet.Translator.ReadBit("Has transport");
            packet.Translator.ReadBit();
            var hasPitch = !packet.Translator.ReadBit("Has pitch");
            var hasSplineElev = !packet.Translator.ReadBit("Has Spline Elevation");
            var hasTime = !packet.Translator.ReadBit("Has timestamp");
            guid[6] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();

            if (hasTrans)
            {
                transportGuid[7] = packet.Translator.ReadBit();
                transportGuid[6] = packet.Translator.ReadBit();
                transportGuid[0] = packet.Translator.ReadBit();
                transportGuid[4] = packet.Translator.ReadBit();
                transportGuid[3] = packet.Translator.ReadBit();
                transportGuid[2] = packet.Translator.ReadBit();
                transportGuid[5] = packet.Translator.ReadBit();
                transportGuid[1] = packet.Translator.ReadBit();
                hasTransTime3 = packet.Translator.ReadBit();
                hasTransTime2 = packet.Translator.ReadBit();
            }

            if (hasMovementFlags)
                packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30);

            if (hasFallData)
                hasFallDirection = packet.Translator.ReadBit();

            if (hasMovementFlags2)
                packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 12);

            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 2);

            if (hasFallData)
            {
                if (hasFallDirection)
                {
                    packet.Translator.ReadSingle("Fall Sin");
                    packet.Translator.ReadSingle("Horizontal Speed");
                    packet.Translator.ReadSingle("Fall Cos");
                }

                packet.Translator.ReadUInt32("Fall time");
                packet.Translator.ReadSingle("Vertical Speed");
            }

            if (hasTrans)
            {
                var tpos = new Vector4();

                packet.Translator.ReadXORByte(transportGuid, 4);
                packet.Translator.ReadXORByte(transportGuid, 5);
                tpos.O = packet.Translator.ReadSingle();
                tpos.X = packet.Translator.ReadSingle();
                tpos.Z = packet.Translator.ReadSingle();

                if (hasTransTime3)
                    packet.Translator.ReadUInt32("Transport Time 3");

                packet.Translator.ReadXORByte(transportGuid, 0);
                packet.Translator.ReadXORByte(transportGuid, 7);
                packet.Translator.ReadXORByte(transportGuid, 3);
                tpos.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadUInt32("Transport Time");
                packet.Translator.ReadSByte("Transport Seat");

                packet.Translator.ReadXORByte(transportGuid, 2);
                packet.Translator.ReadXORByte(transportGuid, 1);

                if (hasTransTime2)
                    packet.Translator.ReadUInt32("Transport Time 2");

                packet.Translator.ReadXORByte(transportGuid, 6);

                packet.Translator.WriteGuid("Transport Guid", transportGuid);
                packet.AddValue("Transport Position: {0}", tpos);
            }

            if (hasTime)
                packet.Translator.ReadUInt32("Timestamp");
            if (hasO)
                pos.O = packet.Translator.ReadSingle();
            if (hasSplineElev)
                packet.Translator.ReadSingle("Spline elevation");
            if (hasPitch)
                packet.Translator.ReadSingle("Pitch");

            packet.Translator.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_MEETINGSTONE_IN_PROGRESS)]
        public static void HandleMeetingstoneInProgress(Packet packet)
        {
            packet.Translator.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.SMSG_MEETINGSTONE_COMPLETE)]
        public static void HandleMeetingstoneComplete(Packet packet)
        {
            packet.Translator.ReadGuid("Guid");
        }

        [Parser(Opcode.SMSG_UNIT_HEALTH_FREQUENT)]
        public static void HandleUnitHealthFrequent(Packet packet)
        {
            packet.Translator.ReadPackedGuid("Guid");
            packet.Translator.ReadInt32("New Health Value");
        }

        [Parser(Opcode.SMSG_STREAMING_MOVIES)]
        public static void HandleStreamingMovie(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 25);
            for (int i = 0; i < count; ++i)
                packet.Translator.ReadInt16("File Data ID");
        }

        [Parser(Opcode.SMSG_WEEKLY_LAST_RESET)]
        public static void HandleWeeklyLastReset(Packet packet)
        {
            packet.Translator.ReadTime("Date");
        }

        [Parser(Opcode.SMSG_MAP_OBJ_EVENTS)]
        public static void HandleMapObjEvents(Packet packet)
        {
            packet.Translator.ReadInt32("unk int32");
            var count = packet.Translator.ReadInt32("Data Count");
            for (var i = 0; i < count; i++)
                packet.Translator.ReadByte("Unk Byte", i);
        }


        [Parser(Opcode.SMSG_DISPLAY_GAME_ERROR)] // 4.3.4
        public static void HandleDisplayGameError(Packet packet)
        {
            var hasAchieveOrSpellFailedIdOrCurrencyCount = packet.Translator.ReadBit();
            var achieveOrSpellFailedIdOrCurrencyCount = 0u;
            var hasCurrencyId = packet.Translator.ReadBit();

            if (hasAchieveOrSpellFailedIdOrCurrencyCount)
                achieveOrSpellFailedIdOrCurrencyCount = packet.Translator.ReadUInt32();
            var err = packet.Translator.ReadUInt32("Error Code");
            if (hasAchieveOrSpellFailedIdOrCurrencyCount)
                switch (err)
                {
                    case 48: // ERR_SPELL_FAILED_S
                        packet.AddValue("Spell Failed Id", achieveOrSpellFailedIdOrCurrencyCount);
                        break;
                    case 784: // ERR_REQUIRES_ACHIEVEMENT_I
                        packet.AddValue("Achievement Id", achieveOrSpellFailedIdOrCurrencyCount);
                        break;
                    case 790: // ERR_INSUFF_TRACKED_CURRENCY_IS
                        packet.AddValue("Currency Count", achieveOrSpellFailedIdOrCurrencyCount);
                        break;
                    default:
                        packet.AddValue("Unk UInt32", achieveOrSpellFailedIdOrCurrencyCount);
                        break;
                }

            if (hasCurrencyId)
                packet.Translator.ReadUInt32("CurrencyId");
        }

        [Parser(Opcode.SMSG_NOTIFICATION)]
        [Parser(Opcode.SMSG_NOTIFICATION_2)]
        public static void HandleNotification(Packet packet)
        {
            packet.Translator.ReadCString("Notification");
        }

        [Parser(Opcode.CMSG_TIME_SYNC_RESPONSE_FAILED)]
        public static void HandleTimeSyncRespFailed(Packet packet)
        {
            packet.Translator.ReadUInt32("SequenceIndex");
        }

        [Parser(Opcode.SMSG_AREA_TRIGGER_MESSAGE)]
        public static void HandleAreaTriggerMessage(Packet packet)
        {
            var length = packet.Translator.ReadUInt32("Length");
            packet.Translator.ReadWoWString("Text", length);
        }

        [Parser(Opcode.SMSG_PLAY_ONE_SHOT_ANIM_KIT)]
        [Parser(Opcode.SMSG_SET_AI_ANIM_KIT)]
        [Parser(Opcode.SMSG_SET_MELEE_ANIM_KIT)]
        [Parser(Opcode.SMSG_SET_MOVEMENT_ANIM_KIT)]
        public static void HandlePlayOneShotAnimKit(Packet packet)
        {
            packet.Translator.ReadPackedGuid("Guid");
            packet.Translator.ReadUInt16("AnimKit.dbc Id");
        }

        [Parser(Opcode.CMSG_QUERY_COUNTDOWN_TIMER)]
        public static void HandleQueryCountdownTimer(Packet packet)
        {
            packet.Translator.ReadInt32("TimerType");
        }

        [Parser(Opcode.SMSG_MINIGAME_STATE)]
        [Parser(Opcode.CMSG_KEEP_ALIVE)]
        [Parser(Opcode.CMSG_TUTORIAL_RESET)]
        [Parser(Opcode.CMSG_TUTORIAL_CLEAR)]
        [Parser(Opcode.MSG_MOVE_WORLDPORT_ACK)]
        [Parser(Opcode.CMSG_QUERY_TIME)]
        [Parser(Opcode.CMSG_UI_TIME_REQUEST)]
        [Parser(Opcode.SMSG_COMSAT_CONNECT_FAIL)]
        [Parser(Opcode.SMSG_COMSAT_RECONNECT_TRY)]
        [Parser(Opcode.SMSG_COMSAT_DISCONNECT)]
        [Parser(Opcode.SMSG_VOICESESSION_FULL)] // 61 bytes in 2.4.1
        [Parser(Opcode.SMSG_DEBUG_SERVER_GEO)] // Was unknown
        [Parser(Opcode.SMSG_RESUME_COMMS)]
        [Parser(Opcode.SMSG_GOSSIP_COMPLETE)]
        [Parser(Opcode.SMSG_INVALID_PROMOTION_CODE)]
        [Parser(Opcode.CMSG_COMPLETE_CINEMATIC)]
        [Parser(Opcode.CMSG_NEXT_CINEMATIC_CAMERA)]
        [Parser(Opcode.CMSG_REQUEST_VEHICLE_EXIT)]
        [Parser(Opcode.SMSG_ENABLE_BARBER_SHOP)]
        [Parser(Opcode.SMSG_FISH_NOT_HOOKED)]
        [Parser(Opcode.SMSG_FISH_ESCAPED)]
        [Parser(Opcode.SMSG_SUMMON_CANCEL)]
        [Parser(Opcode.CMSG_MEETINGSTONE_INFO)]
        [Parser(Opcode.CMSG_CLIENT_PORT_GRAVEYARD)]
        [Parser(Opcode.CMSG_REQUEST_CEMETERY_LIST)]
        [Parser(Opcode.CMSG_REQUEST_RESEARCH_HISTORY)]
        [Parser(Opcode.CMSG_COMPLETE_MOVIE)]
        [Parser(Opcode.SMSG_WEEKLY_RESET_CURRENCY)]
        [Parser(Opcode.CMSG_USED_FOLLOW)]
        [Parser(Opcode.SMSG_CLEAR_BOSS_EMOTES)]
        [Parser(Opcode.SMSG_NEW_WORLD_ABORT)]
        [Parser(Opcode.CMSG_ROLE_POLL_BEGIN)]
        [Parser(Opcode.CMSG_UPDATE_VAS_PURCHASE_STATES)]
        public static void HandleZeroLengthPackets(Packet packet)
        {
        }
    }
}
