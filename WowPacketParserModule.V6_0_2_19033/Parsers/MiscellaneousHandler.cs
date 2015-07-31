using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using TutorialAction610 = WowPacketParser.Enums.Version.V6_1_0_19678.TutorialAction;
using TutorialAction612 = WowPacketParser.Enums.Version.V6_1_2_19802.TutorialAction;
using TutorialAction620 = WowPacketParser.Enums.Version.V6_2_0_20173.TutorialAction;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class MiscellaneousHandler
    {
        public static void ReadElaspedTimer(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("TimerID", indexes);
            packet.ReadInt32("CurrentDuration", indexes);
        }

        [Parser(Opcode.CMSG_REQUEST_ARTIFACT_COMPLETION_HISTORY)]
        [Parser(Opcode.CMSG_TWITTER_CHECK_STATUS)]
        public static void HandleMiscZero(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_INVALIDATE_PLAYER)]
        public static void HandleReadGuid(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [HasSniffData]
        [Parser(Opcode.CMSG_LOADING_SCREEN_NOTIFY)]
        public static void HandleClientEnterWorld(Packet packet)
        {
            var mapId = packet.ReadInt32<MapId>("MapID");
            packet.ReadBit("Showing");

            packet.AddSniffData(StoreNameType.Map, mapId, "LOAD_SCREEN");
        }

        [Parser(Opcode.SMSG_WEATHER)]
        public static void HandleWeatherStatus(Packet packet)
        {
            var state = packet.ReadInt32E<WeatherState>("State");
            var grade = packet.ReadSingle("Intensity");
            var unk = packet.ReadBit("Abrupt"); // Type

            Storage.WeatherUpdates.Add(new WeatherUpdate
            {
                MapId = CoreParsers.MovementHandler.CurrentMapId,
                ZoneId = 0, // fixme
                State = state,
                Grade = grade,
                Unk = unk
            }, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_SET_SELECTION)]
        public static void HandleSetSelection(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS_GLUE_SCREEN)]
        public static void HandleFeatureSystemStatusGlueScreen(Packet packet)
        {
            // educated guess order
            packet.ReadBit("BpayStoreEnabled");
            packet.ReadBit("BpayStoreAvailable");
            packet.ReadBit("BpayStoreDisabledByParentalControls");
            packet.ReadBit("CharUndeleteEnabled");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_2_19802))
            {
                packet.ReadBit("CommerceSystemEnabled");
                packet.ReadBit("Unk14");
                packet.ReadBit("WillKickFromWorld");
                packet.ReadInt32("TokenPollTimeSeconds");
                packet.ReadInt32E<ConsumableTokenRedeem>("TokenRedeemIndex");
            }
        }

        public static void ReadCliSavedThrottleObjectState(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("MaxTries", idx);
            packet.ReadUInt32("PerMilliseconds", idx);
            packet.ReadUInt32("TryCount", idx);
            packet.ReadUInt32("LastResetTimeBeforeNow", idx);
        }

        public static void ReadCliEuropaTicketConfig(Packet packet, params object[] idx)
        {
            packet.ReadBit("TicketsEnabled", idx);
            packet.ReadBit("BugsEnabled", idx);
            packet.ReadBit("ComplaintsEnabled", idx);
            packet.ReadBit("SuggestionsEnabled", idx);

            ReadCliSavedThrottleObjectState(packet, idx, "ThrottleState");
        }

        public static void ReadClientSessionAlertConfig(Packet packet, params object[] idx)
        {
            packet.ReadInt32("Delay", idx);
            packet.ReadInt32("Period", idx);
            packet.ReadInt32("DisplayTime", idx);
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS, ClientVersionBuild.V6_0_2_19033, ClientVersionBuild.V6_1_0_19678)]
        public static void HandleFeatureSystemStatus60x(Packet packet)
        {
            packet.ReadByte("ComplaintStatus");

            packet.ReadInt32("ScrollOfResurrectionRequestsRemaining");
            packet.ReadInt32("ScrollOfResurrectionMaxRequestsPerDay");
            packet.ReadInt32("CfgRealmID");
            packet.ReadInt32("CfgRealmRecID");

            packet.ResetBitReader();

            packet.ReadBit("VoiceEnabled");
            var hasEuropaTicketSystemStatus = packet.ReadBit("HasEuropaTicketSystemStatus");
            packet.ReadBit("ScrollOfResurrectionEnabled");
            packet.ReadBit("BpayStoreEnabled");
            packet.ReadBit("BpayStoreAvailable");
            packet.ReadBit("BpayStoreDisabledByParentalControls");
            packet.ReadBit("ItemRestorationButtonEnabled");
            packet.ReadBit("BrowserEnabled");
            var hasSessionAlert = packet.ReadBit("HasSessionAlert");
            packet.ReadBit("RecruitAFriendSendingEnabled");
            packet.ReadBit("CharUndeleteEnabled");
            packet.ReadBit("RestrictedAccount");
            packet.ReadBit("TutorialsEnabled");
            packet.ReadBit("NPETutorialsEnabled");

            if (hasEuropaTicketSystemStatus)
                ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");

            if (hasSessionAlert)
                ReadClientSessionAlertConfig(packet, "SessionAlert");
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS, ClientVersionBuild.V6_1_0_19678, ClientVersionBuild.V6_1_2_19802)]
        public static void HandleFeatureSystemStatus610(Packet packet)
        {
            packet.ReadByte("ComplaintStatus");

            packet.ReadInt32("ScrollOfResurrectionRequestsRemaining");
            packet.ReadInt32("ScrollOfResurrectionMaxRequestsPerDay");
            packet.ReadInt32("CfgRealmID");
            packet.ReadInt32("CfgRealmRecID");
            packet.ReadInt32("Int27");
            packet.ReadInt32("Int29");

            packet.ResetBitReader();

            packet.ReadBit("VoiceEnabled");
            var hasEuropaTicketSystemStatus = packet.ReadBit("HasEuropaTicketSystemStatus");
            packet.ReadBit("ScrollOfResurrectionEnabled");
            packet.ReadBit("BpayStoreEnabled");
            packet.ReadBit("BpayStoreAvailable");
            packet.ReadBit("BpayStoreDisabledByParentalControls");
            packet.ReadBit("ItemRestorationButtonEnabled");
            packet.ReadBit("BrowserEnabled");
            var hasSessionAlert = packet.ReadBit("HasSessionAlert");
            packet.ReadBit("RecruitAFriendSendingEnabled");
            packet.ReadBit("CharUndeleteEnabled");
            packet.ReadBit("RestrictedAccount");
            packet.ReadBit("TutorialsEnabled");
            packet.ReadBit("NPETutorialsEnabled");
            packet.ReadBit("TwitterEnabled");

            var bit61 = ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_0_19702) && packet.ReadBit("Unk bit61");

            if (hasEuropaTicketSystemStatus)
                ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");

            if (hasSessionAlert)
                ReadClientSessionAlertConfig(packet, "SessionAlert");

            // Note: Only since ClientVersionBuild.V6_1_0_19702
            if (bit61)
            {
                var int88 = packet.ReadInt32("int88");
                for (int i = 0; i < int88; i++)
                    packet.ReadByte("byte23", i);
            }
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS, ClientVersionBuild.V6_1_2_19802)]
        public static void HandleFeatureSystemStatus612(Packet packet)
        {
            packet.ReadByte("ComplaintStatus");

            packet.ReadInt32("ScrollOfResurrectionRequestsRemaining");
            packet.ReadInt32("ScrollOfResurrectionMaxRequestsPerDay");
            packet.ReadInt32("CfgRealmID");
            packet.ReadInt32("CfgRealmRecID");
            packet.ReadInt32("Int27");
            packet.ReadInt32("TwitterMsTillCanPost");
            packet.ReadInt32("TokenPollTimeSeconds");
            packet.ReadInt32E<ConsumableTokenRedeem>("TokenRedeemIndex");

            packet.ResetBitReader();

            packet.ReadBit("VoiceEnabled");
            var hasEuropaTicketSystemStatus = packet.ReadBit("HasEuropaTicketSystemStatus");
            packet.ReadBit("ScrollOfResurrectionEnabled");
            packet.ReadBit("BpayStoreEnabled");
            packet.ReadBit("BpayStoreAvailable");
            packet.ReadBit("BpayStoreDisabledByParentalControls");
            packet.ReadBit("ItemRestorationButtonEnabled");
            packet.ReadBit("BrowserEnabled");
            var hasSessionAlert = packet.ReadBit("HasSessionAlert");
            packet.ReadBit("RecruitAFriendSendingEnabled");
            packet.ReadBit("CharUndeleteEnabled");
            packet.ReadBit("RestrictedAccount");
            packet.ReadBit("TutorialsEnabled");
            packet.ReadBit("NPETutorialsEnabled");
            packet.ReadBit("TwitterEnabled");
            packet.ReadBit("CommerceSystemEnabled");
            packet.ReadBit("Unk67");
            packet.ReadBit("WillKickFromWorld");
            var bit4A = packet.ReadBit("Unk4A");

            packet.ResetBitReader();

            if (hasEuropaTicketSystemStatus)
                ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");

            if (hasSessionAlert)
                ReadClientSessionAlertConfig(packet, "SessionAlert");

            if (bit4A)
            {
                var int88 = packet.ReadInt32("int88");
                for (int i = 0; i < int88; i++)
                    packet.ReadByte("byte23", i);
            }
        }

        [Parser(Opcode.SMSG_WORLD_SERVER_INFO)]
        public static void HandleWorldServerInfo(Packet packet)
        {
            packet.ReadInt32("DifficultyID");
            packet.ReadByte("IsTournamentRealm");
            packet.ReadTime("WeeklyReset");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173))
                packet.ReadBit("XRealmPvpAlert");

            var hasRestrictedAccountMaxLevel = packet.ReadBit("HasRestrictedAccountMaxLevel");
            var hasRestrictedAccountMaxMoney = packet.ReadBit("HasRestrictedAccountMaxMoney");
            var hasIneligibleForLootMask = packet.ReadBit("HasIneligibleForLootMask");
            var hasInstanceGroupSize = packet.ReadBit("HasInstanceGroupSize");

            if (hasRestrictedAccountMaxLevel)
                packet.ReadInt32("RestrictedAccountMaxLevel");

            if (hasRestrictedAccountMaxMoney)
                packet.ReadInt32("RestrictedAccountMaxMoney");

            if (hasIneligibleForLootMask)
                packet.ReadInt32("IneligibleForLootMask");

            if (hasInstanceGroupSize)
                packet.ReadInt32("InstanceGroupSize");
        }

        [Parser(Opcode.CMSG_WHO)]
        public static void HandleWhoRequest(Packet packet)
        {
            var bits728 = packet.ReadBits("WhoWordCount", 4);

            packet.ReadInt32("MinLevel");
            packet.ReadInt32("MaxLevel");
            packet.ReadInt32("RaceFilter");
            packet.ReadInt32("ClassFilter");

            packet.ResetBitReader();

            var bits2 = packet.ReadBits(6);
            var bits57 = packet.ReadBits(9);
            var bits314 = packet.ReadBits(7);
            var bits411 = packet.ReadBits(9);
            var bit169 = packet.ReadBits(3);

            packet.ReadBit("ShowEnemies");
            packet.ReadBit("ShowArenaPlayers");
            packet.ReadBit("ExactName");
            var bit708 = packet.ReadBit("HasServerInfo");

            packet.ReadWoWString("Name", bits2);
            packet.ReadWoWString("VirtualRealmName", bits57);
            packet.ReadWoWString("Guild", bits314);
            packet.ReadWoWString("GuildVirtualRealmName", bits411);

            for (var i = 0; i < bit169; ++i)
            {
                packet.ResetBitReader();
                var bits0 = packet.ReadBits(7);
                packet.ReadWoWString("Word", bits0, i);
            }

            // WhoRequestServerInfo
            if (bit708)
            {
                packet.ReadInt32("FactionGroup");
                packet.ReadInt32("Locale");
                packet.ReadInt32("RequesterVirtualRealmAddress");
            }

            for (var i = 0; i < bits728; ++i)
                packet.ReadUInt32<AreaId>("Area", i);
        }

        [Parser(Opcode.SMSG_WHO)]
        public static void HandleWho(Packet packet)
        {
            var bits568 = packet.ReadBits("List count", 6);

            for (var i = 0; i < bits568; ++i)
            {
                packet.ResetBitReader();
                packet.ReadBit("IsDeleted", i);
                var bits15 = packet.ReadBits(6);

                var declinedNamesLen = new int[5];
                for (var j = 0; j < 5; ++j)
                {
                    packet.ResetBitReader();
                    declinedNamesLen[j] = (int)packet.ReadBits(7);
                }

                for (var j = 0; j < 5; ++j)
                    packet.ReadWoWString("DeclinedNames", declinedNamesLen[j], i, j);

                packet.ReadPackedGuid128("AccountID", i);
                packet.ReadPackedGuid128("BnetAccountID", i);
                packet.ReadPackedGuid128("GuidActual", i);

                packet.ReadInt32("VirtualRealmAddress", i);

                packet.ReadByteE<Race>("Race", i);
                packet.ReadByteE<Gender>("Sex", i);
                packet.ReadByteE<Class>("ClassId", i);
                packet.ReadByte("Level", i);

                packet.ReadWoWString("Name", bits15, i);

                packet.ReadPackedGuid128("GuildGUID", i);

                packet.ReadInt32("GuildVirtualRealmAddress", i);
                packet.ReadInt32("AreaID", i);

                packet.ResetBitReader();
                var bits460 = packet.ReadBits(7);
                packet.ReadBit("IsGM", i);

                packet.ReadWoWString("GuildName", bits460, i);
            }
        }

        [Parser(Opcode.CMSG_PING)]
        public static void HandleClientPing(Packet packet)
        {
            packet.ReadInt32("Serial");
            packet.ReadInt32("Latency");
        }

        [Parser(Opcode.SMSG_PONG)]
        public static void HandleServerPong(Packet packet)
        {
            packet.ReadInt32("Serial");
        }

        [Parser(Opcode.SMSG_INITIAL_SETUP, ClientVersionBuild.V6_0_2_19033, ClientVersionBuild.V6_0_3_19342)]
        public static void HandleInitialSetup60x(Packet packet)
        {
            var int6 = packet.ReadInt32("QuestsCompletedCount");

            packet.ReadByte("ServerExpansionLevel");
            packet.ReadByte("ServerExpansionTier");

            packet.ReadInt32("ServerRegionID");
            packet.ReadTime("RaidOrigin");

            for (var i = 0; i < int6; ++i)
                packet.ReadByte("QuestsCompleted", i);
        }

        [Parser(Opcode.SMSG_INITIAL_SETUP, ClientVersionBuild.V6_1_0_19678)]
        public static void HandleInitialSetup61x(Packet packet)
        {
            packet.ReadByte("ServerExpansionLevel");
            packet.ReadByte("ServerExpansionTier");

            packet.ReadInt32("ServerRegionID");
            packet.ReadTime("RaidOrigin");
        }

        [Parser(Opcode.CMSG_AREA_TRIGGER)]
        public static void HandleClientAreaTrigger(Packet packet)
        {
            var entry = packet.ReadEntry("Area Trigger Id");
            packet.ReadBit("Entered");
            packet.ReadBit("FromClient");

            packet.AddSniffData(StoreNameType.AreaTrigger, entry.Key, "AREATRIGGER");
        }

        [Parser(Opcode.SMSG_ACCOUNT_MOUNT_UPDATE)]
        public static void HandleAccountMountUpdate(Packet packet)
        {
            packet.ReadBit("IsFullUpdate");

            var int32 = packet.ReadInt32("MountSpellIDsCount");
            var int16 = packet.ReadInt32("MountIsFavoriteCount");

            for (int i = 0; i < int32; i++)
                packet.ReadInt32("MountSpellIDs", i);

            packet.ResetBitReader();

            for (int i = 0; i < int16; i++)
                packet.ReadBit("MountIsFavorite", i);
        }

        [Parser(Opcode.SMSG_ACCOUNT_TOYS_UPDATE)]
        public static void HandleAccountToysUpdate(Packet packet)
        {
            packet.ReadBit("IsFullUpdate");

            var int32 = packet.ReadInt32("ToyItemIDsCount");
            var int16 = packet.ReadInt32("ToyIsFavoriteCount");

            for (int i = 0; i < int32; i++)
                packet.ReadInt32("ToyItemID", i);

            packet.ResetBitReader();

            for (int i = 0; i < int16; i++)
                packet.ReadBit("ToyIsFavorite", i);
        }

        [Parser(Opcode.SMSG_ACCOUNT_HEIRLOOM_UPDATE)]
        public static void HandleAccountHeirloomUpdate(Packet packet)
        {
            packet.ReadBit("IsFullUpdate");

            packet.ReadInt32("Unk");

            var int32 = packet.ReadInt32("ItemCount");
            var int16 = packet.ReadInt32("FlagsCount");

            for (int i = 0; i < int32; i++)
                packet.ReadInt32("ItemID", i);

            for (int i = 0; i < int16; i++)
                packet.ReadInt32("Flags", i);
        }

        [Parser(Opcode.SMSG_PLAY_SOUND)]
        public static void HandlePlaySound(Packet packet)
        {
            var sound = packet.ReadUInt32("SoundKitID");
            packet.ReadPackedGuid128("SourceObjectGUID");

            Storage.Sounds.Add(sound, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_PLAY_MUSIC)]
        public static void HandlePlayMusic(Packet packet)
        {
            var sound = packet.ReadUInt32("SoundKitID");

            Storage.Sounds.Add(sound, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_ZONE_UNDER_ATTACK)]
        public static void HandleZoneUpdate(Packet packet)
        {
            packet.ReadInt32<ZoneId>("AreaID");
        }

        [Parser(Opcode.CMSG_QUERY_PAGE_TEXT)]
        public static void HandlePageTextQuery(Packet packet)
        {
            packet.ReadInt32("Entry");
            packet.ReadPackedGuid128("Guid");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_PAGE_TEXT_RESPONSE)]
        public static void HandlePageTextResponse(Packet packet)
        {
            var pageText = new PageText();

            packet.ReadUInt32("PageTextID");

            packet.ResetBitReader();

            var hasData = packet.ReadBit("Allow");
            if (!hasData)
                return; // nothing to do

            var entry = packet.ReadUInt32("ID");
            pageText.NextPageID = packet.ReadUInt32("NextPageID");

            packet.ResetBitReader();
            var textLen = packet.ReadBits(12);
            pageText.Text = packet.ReadWoWString("Text", textLen);

            packet.AddSniffData(StoreNameType.PageText, (int)entry, "QUERY_RESPONSE");
            Storage.PageTexts.Add(entry, pageText, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_PLAY_ONE_SHOT_ANIM_KIT)]
        [Parser(Opcode.SMSG_SET_AI_ANIM_KIT)]
        [Parser(Opcode.SMSG_SET_MELEE_ANIM_KIT)]
        public static void HandleSetAIAnimKit(Packet packet)
        {
            packet.ReadPackedGuid128("Unit");
            packet.ReadUInt16("AnimKitID");
        }

        [Parser(Opcode.SMSG_DISPLAY_PROMOTION)]
        public static void HandleDisplayPromotion(Packet packet)
        {
            packet.ReadUInt32("PromotionID");
        }

        [Parser(Opcode.SMSG_SET_ALL_TASK_PROGRESS)]
        [Parser(Opcode.SMSG_UPDATE_TASK_PROGRESS)]
        public static void HandleSetAllTaskProgress(Packet packet)
        {
            var int4 = packet.ReadInt32("TaskProgressCount");
            for (int i = 0; i < int4; i++)
            {
                packet.ReadUInt32("TaskID", i);
                packet.ReadUInt32("FailureTime", i);
                packet.ReadUInt32("Flags", i);

                var int3 = packet.ReadInt32("ProgressCounts", i);
                for (int j = 0; j < int3; j++)
                    packet.ReadInt16("Counts", i, j);
            }
        }

        [Parser(Opcode.CMSG_TUTORIAL_FLAG, ClientVersionBuild.V6_0_2_19033, ClientVersionBuild.V6_0_3_19342)]
        public static void HandleTutorialFlag60x(Packet packet)
        {
            var action = packet.ReadBitsE<TutorialAction>("TutorialAction", 2);

            if (action == TutorialAction.Update)
                packet.ReadInt32E<Tutorial>("TutorialBit");
        }

        [Parser(Opcode.CMSG_TUTORIAL_FLAG, ClientVersionBuild.V6_1_0_19678, ClientVersionBuild.V6_1_2_19802)]
        public static void HandleTutorialFlag610(Packet packet)
        {
            var action = packet.ReadBitsE<TutorialAction610>("TutorialAction", 2);

            if (action == TutorialAction610.Update)
                packet.ReadInt32E<Tutorial>("TutorialBit");
        }

        [Parser(Opcode.CMSG_TUTORIAL_FLAG, ClientVersionBuild.V6_1_2_19802, ClientVersionBuild.V6_2_0_20173)]
        public static void HandleTutorialFlag612(Packet packet)
        {
            var action = packet.ReadBitsE<TutorialAction612>("TutorialAction", 2);

            if (action == TutorialAction612.Update)
                packet.ReadInt32E<Tutorial>("TutorialBit");
        }

        [Parser(Opcode.CMSG_TUTORIAL_FLAG, ClientVersionBuild.V6_2_0_20173)]
        public static void HandleTutorialFlag620(Packet packet)
        {
            var action = packet.ReadBitsE<TutorialAction620>("TutorialAction", 2);

            if (action == TutorialAction620.Update)
                packet.ReadInt32E<Tutorial>("TutorialBit");
        }

        [Parser(Opcode.SMSG_START_ELAPSED_TIMERS)]
        public static void HandleStartElapsedTimers(Packet packet)
        {
            var int3 = packet.ReadInt32("ElaspedTimerCounts");
            for (int i = 0; i < int3; i++)
            {
                packet.ReadUInt32("TimerID", i);
                packet.ReadTime("CurrentDuration", i);
            }
        }

        [Parser(Opcode.SMSG_REQUEST_CEMETERY_LIST_RESPONSE)]
        public static void HandleRequestCemeteryListResponse(Packet packet)
        {
            packet.ReadBit("IsTriggered");

            var count = packet.ReadUInt32("Count");
            for (int i = 0; i < count; ++i)
                packet.ReadInt32("CemeteryID", i);
        }

        [Parser(Opcode.SMSG_START_MIRROR_TIMER)]
        public static void HandleStartMirrorTimer(Packet packet)
        {
            packet.ReadUInt32("TimerType"); // Timer in magic
            packet.ReadUInt32("InitialValue");
            packet.ReadUInt32("MaxValue");
            packet.ReadInt32("Scale");
            packet.ReadUInt32("SpellId");
            packet.ReadBit("Paused");
        }

        [Parser(Opcode.CMSG_BUG_REPORT)]
        public static void HandleBugReport(Packet packet)
        {
            packet.ReadBit("Type");

            var len1 = packet.ReadBits(12);
            var len2 = packet.ReadBits(10);

            packet.ReadWoWString("DiagInfo", len1);
            packet.ReadWoWString("Text", len2);
        }

        [Parser(Opcode.SMSG_RESURRECT_REQUEST)]
        public static void HandleResurrectRequest(Packet packet)
        {
            packet.ReadPackedGuid128("ResurrectOffererGUID");

            packet.ReadUInt32("ResurrectOffererVirtualRealmAddress");
            packet.ReadUInt32("PetNumber");
            packet.ReadInt32("SpellID");

            var len = packet.ReadBits(6);

            packet.ReadBit("UseTimer");
            packet.ReadBit("Sickness");

            packet.ReadWoWString("Name", len);
        }

        [Parser(Opcode.CMSG_RESURRECT_RESPONSE)]
        public static void HandleResurrectResponse(Packet packet)
        {
            packet.ReadPackedGuid128("Resurrecter");
            packet.ReadInt32("Response");
        }

        [Parser(Opcode.CMSG_ACCEPT_LEVEL_GRANT)]
        public static void HandleAcceptLevelGrant(Packet packet)
        {
            packet.ReadPackedGuid128("Granter");
        }

        [Parser(Opcode.SMSG_PRE_RESSURECT)]
        public static void HandlePreRessurect(Packet packet)
        {
            packet.ReadPackedGuid128("PlayerGUID");
        }

        [Parser(Opcode.CMSG_QUERY_COUNTDOWN_TIMER)]
        public static void HandleQueryCountdownTimer(Packet packet)
        {
            packet.ReadInt32("TimerType");
        }

        [Parser(Opcode.SMSG_STREAMING_MOVIES)]
        public static void HandleStreamingMovie(Packet packet)
        {
            var count = packet.ReadInt32("MovieCount");
            for (var i = 0; i < count; i++)
                packet.ReadInt16("MovieIDs", i);
        }

        [Parser(Opcode.SMSG_CUSTOM_LOAD_SCREEN)]
        public static void HandleCustomLoadScreen(Packet packet)
        {
            packet.ReadInt32("TeleportSpellID");
        }

        [Parser(Opcode.CMSG_CONVERSATION_UNK1)]
        public static void HandleConversationUnk1(Packet packet)
        {
            packet.ReadPackedGuid128("Conversation");
            packet.ReadInt32("ConversationID?");
        }

        [Parser(Opcode.SMSG_PLAY_OBJECT_SOUND)]
        public static void HandlePlayObjectSound(Packet packet)
        {
            packet.ReadUInt32("SoundId");
            packet.ReadPackedGuid128("SourceObjectGUID");
            packet.ReadPackedGuid128("TargetObjectGUID");
            packet.ReadVector3("Position");
        }

        [Parser(Opcode.SMSG_PLAY_SPEAKERBOT_SOUND)]
        public static void HandlePlaySpeakerbotSound(Packet packet)
        {
            packet.ReadPackedGuid128("SourceObjectGUID");
            packet.ReadUInt32("SoundId");
        }

        [Parser(Opcode.SMSG_START_ELAPSED_TIMER)]
        public static void HandleStartElapsedTimer(Packet packet)
        {
            ReadElaspedTimer(packet);
        }

        [Parser(Opcode.SMSG_STOP_ELAPSED_TIMER)]
        public static void HandleStopElapsedTimer(Packet packet)
        {
            packet.ReadInt32("TimerID");
            packet.ReadBit("KeepTimer");
        }

        [Parser(Opcode.CMSG_RANDOM_ROLL)]
        public static void HandleRandomRoll(Packet packet)
        {
            packet.ReadInt32("Min");
            packet.ReadInt32("Max");
            packet.ReadByte("PartyIndex");
        }

        [Parser(Opcode.SMSG_RANDOM_ROLL)]
        public static void HandleRandomRollResult(Packet packet)
        {
            packet.ReadPackedGuid128("Roller");
            packet.ReadPackedGuid128("RollerWowAccount");
            packet.ReadInt32("Min");
            packet.ReadInt32("Max");
            packet.ReadInt32("Result");
        }

        [Parser(Opcode.SMSG_SET_TASK_COMPLETE)]
        public static void HandleSetTaskComplete(Packet packet)
        {
            packet.ReadInt32("TaskID");
        }

        [Parser(Opcode.SMSG_DISPLAY_GAME_ERROR)]
        public static void HandleDisplayGameError(Packet packet)
        {
            packet.ReadUInt32("Error");
            var hasArg = packet.ReadBit("HasArg");
            var hasArg2 = packet.ReadBit("HasArg2");

            if (hasArg)
                packet.ReadUInt32("Arg");

            if (hasArg2)
                packet.ReadUInt32("Arg2");
        }

        [Parser(Opcode.SMSG_RESTRICTED_ACCOUNT_WARNING)]
        public static void HandleRestrictedAccountWarning(Packet packet)
        {
            packet.ReadUInt32("Arg");
            packet.ReadByte("Type");
        }

        [Parser(Opcode.SMSG_WEEKLY_LAST_RESET)]
        public static void HandleLastWeeklyReset(Packet packet)
        {
            packet.ReadTime("Reset");
        }

        [Parser(Opcode.SMSG_SUMMON_REQUEST)]
        public static void HandleSummonRequest(Packet packet)
        {
            packet.ReadPackedGuid128("SummonerGUID");
            packet.ReadUInt32("SummonerVirtualRealmAddress");
            packet.ReadInt32<AreaId>("AreaID");
            packet.ReadBit("ConfirmSummon_NC");
        }

        // new opcode on 6.x, related to combat log and mostly used in garrisons
        [Parser(Opcode.SMSG_WORLD_TEXT)]
        public static void HandleWorldText(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadInt32("Arg1");
            packet.ReadInt32("Arg2");
            var length = packet.ReadBits("TextLength", 12);
            packet.ReadWoWString("Text", length);
        }

        [Parser(Opcode.CMSG_ENGINE_SURVEY)]
        public static void HandleEngineSurvey(Packet packet)
        {
            packet.ReadUInt32("GPUVendorID");
            packet.ReadUInt32("GPUModelID");
            packet.ReadUInt32("Unk1C");
            packet.ReadUInt32("Unk10");
            packet.ReadUInt32("Unk38");
            packet.ReadUInt32("DisplayResWidth");
            packet.ReadUInt32("DisplayResHeight");
            packet.ReadUInt32("Unk2C");
            packet.ReadUInt32("MemoryCapacity");
            packet.ReadUInt32("Unk30");
            packet.ReadUInt32("Unk18");
            packet.ReadByte("HasHDPlayerModels");
            packet.ReadByte("Is64BitSystem");
            packet.ReadByte("Unk3C");
            packet.ReadByte("Unk3F");
            packet.ReadByte("Unk3E");
        }

        [Parser(Opcode.SMSG_TWITTER_STATUS)]
        public static void HandleTwitterStatus(Packet packet)
        {
            packet.ReadUInt32("StatusInt");
        }

        [Parser(Opcode.SMSG_DURABILITY_DAMAGE_DEATH)]
        public static void HandleDurabilityDamageDeath(Packet packet)
        {
            packet.ReadInt32("Percent");
        }

        [Parser(Opcode.CMSG_TOY_SET_FAVORITE)]
        public static void HandleToySetFavorite(Packet packet)
        {
            packet.ReadUInt32("ItemID");
            packet.ReadBit("Favorite");
        }
    }
}
