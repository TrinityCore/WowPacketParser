
using System;
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
            packet.Translator.ReadInt32("TimerID", indexes);
            packet.Translator.ReadInt32("CurrentDuration", indexes);
        }

        [Parser(Opcode.CMSG_REQUEST_ARTIFACT_COMPLETION_HISTORY)]
        [Parser(Opcode.CMSG_TWITTER_CHECK_STATUS)]
        public static void HandleMiscZero(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_INVALIDATE_PLAYER)]
        public static void HandleReadGuid(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Guid");
        }

        [HasSniffData]
        [Parser(Opcode.CMSG_LOADING_SCREEN_NOTIFY)]
        public static void HandleClientEnterWorld(Packet packet)
        {
            var mapId = packet.Translator.ReadInt32<MapId>("MapID");
            packet.Translator.ReadBit("Showing");

            packet.AddSniffData(StoreNameType.Map, mapId, "LOAD_SCREEN");
        }

        [Parser(Opcode.SMSG_WEATHER)]
        public static void HandleWeatherStatus(Packet packet)
        {
            WeatherState state = packet.Translator.ReadInt32E<WeatherState>("State");
            float grade = packet.Translator.ReadSingle("Intensity");
            Bit unk = packet.Translator.ReadBit("Abrupt"); // Type

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
            packet.Translator.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS_GLUE_SCREEN)]
        public static void HandleFeatureSystemStatusGlueScreen(Packet packet)
        {
            // educated guess order
            packet.Translator.ReadBit("BpayStoreEnabled");
            packet.Translator.ReadBit("BpayStoreAvailable");
            packet.Translator.ReadBit("BpayStoreDisabledByParentalControls");
            packet.Translator.ReadBit("CharUndeleteEnabled");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_2_19802))
            {
                packet.Translator.ReadBit("CommerceSystemEnabled");
                packet.Translator.ReadBit("Unk14");
                packet.Translator.ReadBit("WillKickFromWorld");
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_2_20444))
                    packet.Translator.ReadBit("IsExpansionPreorderInStore");

                packet.Translator.ReadInt32("TokenPollTimeSeconds");
                packet.Translator.ReadInt32E<ConsumableTokenRedeem>("TokenRedeemIndex");
            }
        }

        public static void ReadCliSavedThrottleObjectState(Packet packet, params object[] idx)
        {
            packet.Translator.ReadUInt32("MaxTries", idx);
            packet.Translator.ReadUInt32("PerMilliseconds", idx);
            packet.Translator.ReadUInt32("TryCount", idx);
            packet.Translator.ReadUInt32("LastResetTimeBeforeNow", idx);
        }

        public static void ReadCliEuropaTicketConfig(Packet packet, params object[] idx)
        {
            packet.Translator.ReadBit("TicketsEnabled", idx);
            packet.Translator.ReadBit("BugsEnabled", idx);
            packet.Translator.ReadBit("ComplaintsEnabled", idx);
            packet.Translator.ReadBit("SuggestionsEnabled", idx);

            ReadCliSavedThrottleObjectState(packet, idx, "ThrottleState");
        }

        public static void ReadClientSessionAlertConfig(Packet packet, params object[] idx)
        {
            packet.Translator.ReadInt32("Delay", idx);
            packet.Translator.ReadInt32("Period", idx);
            packet.Translator.ReadInt32("DisplayTime", idx);
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS, ClientVersionBuild.V6_0_2_19033, ClientVersionBuild.V6_1_0_19678)]
        public static void HandleFeatureSystemStatus60x(Packet packet)
        {
            packet.Translator.ReadByte("ComplaintStatus");

            packet.Translator.ReadInt32("ScrollOfResurrectionRequestsRemaining");
            packet.Translator.ReadInt32("ScrollOfResurrectionMaxRequestsPerDay");
            packet.Translator.ReadInt32("CfgRealmID");
            packet.Translator.ReadInt32("CfgRealmRecID");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("VoiceEnabled");
            var hasEuropaTicketSystemStatus = packet.Translator.ReadBit("HasEuropaTicketSystemStatus");
            packet.Translator.ReadBit("ScrollOfResurrectionEnabled");
            packet.Translator.ReadBit("BpayStoreEnabled");
            packet.Translator.ReadBit("BpayStoreAvailable");
            packet.Translator.ReadBit("BpayStoreDisabledByParentalControls");
            packet.Translator.ReadBit("ItemRestorationButtonEnabled");
            packet.Translator.ReadBit("BrowserEnabled");
            var hasSessionAlert = packet.Translator.ReadBit("HasSessionAlert");
            packet.Translator.ReadBit("RecruitAFriendSendingEnabled");
            packet.Translator.ReadBit("CharUndeleteEnabled");
            packet.Translator.ReadBit("RestrictedAccount");
            packet.Translator.ReadBit("TutorialsEnabled");
            packet.Translator.ReadBit("NPETutorialsEnabled");

            if (hasEuropaTicketSystemStatus)
                ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");

            if (hasSessionAlert)
                ReadClientSessionAlertConfig(packet, "SessionAlert");
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS, ClientVersionBuild.V6_1_0_19678, ClientVersionBuild.V6_1_2_19802)]
        public static void HandleFeatureSystemStatus610(Packet packet)
        {
            packet.Translator.ReadByte("ComplaintStatus");

            packet.Translator.ReadInt32("ScrollOfResurrectionRequestsRemaining");
            packet.Translator.ReadInt32("ScrollOfResurrectionMaxRequestsPerDay");
            packet.Translator.ReadInt32("CfgRealmID");
            packet.Translator.ReadInt32("CfgRealmRecID");
            packet.Translator.ReadInt32("Int27");
            packet.Translator.ReadInt32("Int29");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("VoiceEnabled");
            var hasEuropaTicketSystemStatus = packet.Translator.ReadBit("HasEuropaTicketSystemStatus");
            packet.Translator.ReadBit("ScrollOfResurrectionEnabled");
            packet.Translator.ReadBit("BpayStoreEnabled");
            packet.Translator.ReadBit("BpayStoreAvailable");
            packet.Translator.ReadBit("BpayStoreDisabledByParentalControls");
            packet.Translator.ReadBit("ItemRestorationButtonEnabled");
            packet.Translator.ReadBit("BrowserEnabled");
            var hasSessionAlert = packet.Translator.ReadBit("HasSessionAlert");
            packet.Translator.ReadBit("RecruitAFriendSendingEnabled");
            packet.Translator.ReadBit("CharUndeleteEnabled");
            packet.Translator.ReadBit("RestrictedAccount");
            packet.Translator.ReadBit("TutorialsEnabled");
            packet.Translator.ReadBit("NPETutorialsEnabled");
            packet.Translator.ReadBit("TwitterEnabled");

            var bit61 = ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_0_19702) && packet.Translator.ReadBit("Unk bit61");

            if (hasEuropaTicketSystemStatus)
                ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");

            if (hasSessionAlert)
                ReadClientSessionAlertConfig(packet, "SessionAlert");

            // Note: Only since ClientVersionBuild.V6_1_0_19702
            if (bit61)
            {
                var int88 = packet.Translator.ReadInt32("int88");
                for (int i = 0; i < int88; i++)
                    packet.Translator.ReadByte("byte23", i);
            }
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS, ClientVersionBuild.V6_1_2_19802)]
        public static void HandleFeatureSystemStatus612(Packet packet)
        {
            packet.Translator.ReadByte("ComplaintStatus");

            packet.Translator.ReadInt32("ScrollOfResurrectionRequestsRemaining");
            packet.Translator.ReadInt32("ScrollOfResurrectionMaxRequestsPerDay");
            packet.Translator.ReadInt32("CfgRealmID");
            packet.Translator.ReadInt32("CfgRealmRecID");
            packet.Translator.ReadInt32("TwitterPostThrottleLimit");
            packet.Translator.ReadInt32("TwitterPostThrottleCooldown");
            packet.Translator.ReadInt32("TokenPollTimeSeconds");
            packet.Translator.ReadInt32E<ConsumableTokenRedeem>("TokenRedeemIndex");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("VoiceEnabled");
            var hasEuropaTicketSystemStatus = packet.Translator.ReadBit("HasEuropaTicketSystemStatus");
            packet.Translator.ReadBit("ScrollOfResurrectionEnabled");
            packet.Translator.ReadBit("BpayStoreEnabled");
            packet.Translator.ReadBit("BpayStoreAvailable");
            packet.Translator.ReadBit("BpayStoreDisabledByParentalControls");
            packet.Translator.ReadBit("ItemRestorationButtonEnabled");
            packet.Translator.ReadBit("BrowserEnabled");
            var hasSessionAlert = packet.Translator.ReadBit("HasSessionAlert");
            packet.Translator.ReadBit("RecruitAFriendSendingEnabled");
            packet.Translator.ReadBit("CharUndeleteEnabled");
            packet.Translator.ReadBit("RestrictedAccount");
            packet.Translator.ReadBit("TutorialsEnabled");
            packet.Translator.ReadBit("NPETutorialsEnabled");
            packet.Translator.ReadBit("TwitterEnabled");
            packet.Translator.ReadBit("CommerceSystemEnabled");
            packet.Translator.ReadBit("Unk67");
            packet.Translator.ReadBit("WillKickFromWorld");
            var bit4A = packet.Translator.ReadBit("Unk4A");

            packet.Translator.ResetBitReader();

            if (hasEuropaTicketSystemStatus)
                ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");

            if (hasSessionAlert)
                ReadClientSessionAlertConfig(packet, "SessionAlert");

            if (bit4A)
            {
                var int88 = packet.Translator.ReadInt32("int88");
                for (int i = 0; i < int88; i++)
                    packet.Translator.ReadByte("byte23", i);
            }
        }

        [Parser(Opcode.SMSG_WORLD_SERVER_INFO)]
        public static void HandleWorldServerInfo(Packet packet)
        {
            packet.Translator.ReadInt32("DifficultyID");
            packet.Translator.ReadByte("IsTournamentRealm");
            packet.Translator.ReadTime("WeeklyReset");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173))
                packet.Translator.ReadBit("XRealmPvpAlert");

            var hasRestrictedAccountMaxLevel = packet.Translator.ReadBit("HasRestrictedAccountMaxLevel");
            var hasRestrictedAccountMaxMoney = packet.Translator.ReadBit("HasRestrictedAccountMaxMoney");
            var hasIneligibleForLootMask = packet.Translator.ReadBit("HasIneligibleForLootMask");
            var hasInstanceGroupSize = packet.Translator.ReadBit("HasInstanceGroupSize");

            if (hasRestrictedAccountMaxLevel)
                packet.Translator.ReadInt32("RestrictedAccountMaxLevel");

            if (hasRestrictedAccountMaxMoney)
                packet.Translator.ReadInt32("RestrictedAccountMaxMoney");

            if (hasIneligibleForLootMask)
                packet.Translator.ReadInt32("IneligibleForLootMask");

            if (hasInstanceGroupSize)
                packet.Translator.ReadInt32("InstanceGroupSize");
        }

        [Parser(Opcode.CMSG_WHO)]
        public static void HandleWhoRequest(Packet packet)
        {
            var bits728 = packet.Translator.ReadBits("WhoWordCount", 4);

            packet.Translator.ReadInt32("MinLevel");
            packet.Translator.ReadInt32("MaxLevel");
            packet.Translator.ReadInt32("RaceFilter");
            packet.Translator.ReadInt32("ClassFilter");

            packet.Translator.ResetBitReader();

            var bits2 = packet.Translator.ReadBits(6);
            var bits57 = packet.Translator.ReadBits(9);
            var bits314 = packet.Translator.ReadBits(7);
            var bits411 = packet.Translator.ReadBits(9);
            var bit169 = packet.Translator.ReadBits(3);

            packet.Translator.ReadBit("ShowEnemies");
            packet.Translator.ReadBit("ShowArenaPlayers");
            packet.Translator.ReadBit("ExactName");
            var bit708 = packet.Translator.ReadBit("HasServerInfo");

            packet.Translator.ReadWoWString("Name", bits2);
            packet.Translator.ReadWoWString("VirtualRealmName", bits57);
            packet.Translator.ReadWoWString("Guild", bits314);
            packet.Translator.ReadWoWString("GuildVirtualRealmName", bits411);

            for (var i = 0; i < bit169; ++i)
            {
                packet.Translator.ResetBitReader();
                var bits0 = packet.Translator.ReadBits(7);
                packet.Translator.ReadWoWString("Word", bits0, i);
            }

            // WhoRequestServerInfo
            if (bit708)
            {
                packet.Translator.ReadInt32("FactionGroup");
                packet.Translator.ReadInt32("Locale");
                packet.Translator.ReadInt32("RequesterVirtualRealmAddress");
            }

            for (var i = 0; i < bits728; ++i)
                packet.Translator.ReadUInt32<AreaId>("Area", i);
        }

        [Parser(Opcode.SMSG_WHO)]
        public static void HandleWho(Packet packet)
        {
            var bits568 = packet.Translator.ReadBits("List count", 6);

            for (var i = 0; i < bits568; ++i)
            {
                packet.Translator.ResetBitReader();
                packet.Translator.ReadBit("IsDeleted", i);
                var bits15 = packet.Translator.ReadBits(6);

                var declinedNamesLen = new int[5];
                for (var j = 0; j < 5; ++j)
                {
                    packet.Translator.ResetBitReader();
                    declinedNamesLen[j] = (int)packet.Translator.ReadBits(7);
                }

                for (var j = 0; j < 5; ++j)
                    packet.Translator.ReadWoWString("DeclinedNames", declinedNamesLen[j], i, j);

                packet.Translator.ReadPackedGuid128("AccountID", i);
                packet.Translator.ReadPackedGuid128("BnetAccountID", i);
                packet.Translator.ReadPackedGuid128("GuidActual", i);

                packet.Translator.ReadInt32("VirtualRealmAddress", i);

                packet.Translator.ReadByteE<Race>("Race", i);
                packet.Translator.ReadByteE<Gender>("Sex", i);
                packet.Translator.ReadByteE<Class>("ClassId", i);
                packet.Translator.ReadByte("Level", i);

                packet.Translator.ReadWoWString("Name", bits15, i);

                packet.Translator.ReadPackedGuid128("GuildGUID", i);

                packet.Translator.ReadInt32("GuildVirtualRealmAddress", i);
                packet.Translator.ReadInt32("AreaID", i);

                packet.Translator.ResetBitReader();
                var bits460 = packet.Translator.ReadBits(7);
                packet.Translator.ReadBit("IsGM", i);

                packet.Translator.ReadWoWString("GuildName", bits460, i);
            }
        }

        [Parser(Opcode.CMSG_PING)]
        public static void HandleClientPing(Packet packet)
        {
            packet.Translator.ReadInt32("Serial");
            packet.Translator.ReadInt32("Latency");
        }

        [Parser(Opcode.SMSG_PONG)]
        public static void HandleServerPong(Packet packet)
        {
            packet.Translator.ReadInt32("Serial");
        }

        [Parser(Opcode.SMSG_INITIAL_SETUP, ClientVersionBuild.V6_0_2_19033, ClientVersionBuild.V6_0_3_19342)]
        public static void HandleInitialSetup60x(Packet packet)
        {
            var int6 = packet.Translator.ReadInt32("QuestsCompletedCount");

            packet.Translator.ReadByte("ServerExpansionLevel");
            packet.Translator.ReadByte("ServerExpansionTier");

            packet.Translator.ReadInt32("ServerRegionID");
            packet.Translator.ReadTime("RaidOrigin");

            for (var i = 0; i < int6; ++i)
                packet.Translator.ReadByte("QuestsCompleted", i);
        }

        [Parser(Opcode.SMSG_INITIAL_SETUP, ClientVersionBuild.V6_1_0_19678)]
        public static void HandleInitialSetup61x(Packet packet)
        {
            packet.Translator.ReadByte("ServerExpansionLevel");
            packet.Translator.ReadByte("ServerExpansionTier");

            packet.Translator.ReadInt32("ServerRegionID");
            packet.Translator.ReadTime("RaidOrigin");
        }

        [Parser(Opcode.CMSG_AREA_TRIGGER)]
        public static void HandleClientAreaTrigger(Packet packet)
        {
            var entry = packet.Translator.ReadEntry("Area Trigger Id");
            packet.Translator.ReadBit("Entered");
            packet.Translator.ReadBit("FromClient");

            packet.AddSniffData(StoreNameType.AreaTrigger, entry.Key, "AREATRIGGER");
        }

        [Parser(Opcode.SMSG_ACCOUNT_MOUNT_UPDATE)]
        public static void HandleAccountMountUpdate(Packet packet)
        {
            packet.Translator.ReadBit("IsFullUpdate");

            var int32 = packet.Translator.ReadInt32("MountSpellIDsCount");
            var int16 = packet.Translator.ReadInt32("MountIsFavoriteCount");

            for (int i = 0; i < int32; i++)
                packet.Translator.ReadInt32("MountSpellIDs", i);

            packet.Translator.ResetBitReader();

            for (int i = 0; i < int16; i++)
                packet.Translator.ReadBit("MountIsFavorite", i);
        }

        [Parser(Opcode.SMSG_ACCOUNT_TOYS_UPDATE)]
        public static void HandleAccountToysUpdate(Packet packet)
        {
            packet.Translator.ReadBit("IsFullUpdate");

            var int32 = packet.Translator.ReadInt32("ToyItemIDsCount");
            var int16 = packet.Translator.ReadInt32("ToyIsFavoriteCount");

            for (int i = 0; i < int32; i++)
                packet.Translator.ReadInt32("ToyItemID", i);

            packet.Translator.ResetBitReader();

            for (int i = 0; i < int16; i++)
                packet.Translator.ReadBit("ToyIsFavorite", i);
        }

        [Parser(Opcode.SMSG_ACCOUNT_HEIRLOOM_UPDATE)]
        public static void HandleAccountHeirloomUpdate(Packet packet)
        {
            packet.Translator.ReadBit("IsFullUpdate");

            packet.Translator.ReadInt32("Unk");

            int int32 = packet.Translator.ReadInt32("ItemCount");
            int int16 = packet.Translator.ReadInt32("FlagsCount");

            for (int i = 0; i < int32; i++)
                packet.Translator.ReadInt32("ItemID", i);

            for (int i = 0; i < int16; i++)
                packet.Translator.ReadInt32("Flags", i);
        }

        [Parser(Opcode.SMSG_PLAY_SOUND)]
        public static void HandlePlaySound(Packet packet)
        {
            uint sound = packet.Translator.ReadUInt32("SoundKitID");
            packet.Translator.ReadPackedGuid128("SourceObjectGUID");

            Storage.Sounds.Add(sound, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_PLAY_MUSIC)]
        public static void HandlePlayMusic(Packet packet)
        {
            uint sound = packet.Translator.ReadUInt32("SoundKitID");

            Storage.Sounds.Add(sound, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_ZONE_UNDER_ATTACK)]
        public static void HandleZoneUpdate(Packet packet)
        {
            packet.Translator.ReadInt32<ZoneId>("AreaID");
        }

        [Parser(Opcode.CMSG_QUERY_PAGE_TEXT)]
        public static void HandlePageTextQuery(Packet packet)
        {
            packet.Translator.ReadInt32("Entry");
            packet.Translator.ReadPackedGuid128("Guid");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_PAGE_TEXT_RESPONSE)]
        public static void HandlePageTextResponse(Packet packet)
        {
            packet.Translator.ReadUInt32("PageTextID");
            packet.Translator.ResetBitReader();

            Bit hasData = packet.Translator.ReadBit("Allow");
            if (!hasData)
                return; // nothing to do

            PageText pageText = new PageText();

            uint entry = packet.Translator.ReadUInt32("ID");
            pageText.ID = entry;
            pageText.NextPageID = packet.Translator.ReadUInt32("NextPageID");

            packet.Translator.ResetBitReader();
            uint textLen = packet.Translator.ReadBits(12);
            pageText.Text = packet.Translator.ReadWoWString("Text", textLen);

            packet.AddSniffData(StoreNameType.PageText, (int)entry, "QUERY_RESPONSE");
            Storage.PageTexts.Add(pageText, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_PLAY_ONE_SHOT_ANIM_KIT)]
        public static void HandlePlayOneShotAnimKit(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Unit");
            packet.Translator.ReadUInt16("AnimKitID");
        }

        [Parser(Opcode.SMSG_SET_AI_ANIM_KIT)]
        public static void SetAIAnimKitId(Packet packet)
        {
            var guid = packet.Translator.ReadPackedGuid128("Unit");
            var animKitID = packet.Translator.ReadUInt16("AnimKitID");

            if (guid.GetObjectType() == ObjectType.Unit)
                if (Storage.Objects.ContainsKey(guid))
                {
                    var timeSpan = Storage.Objects[guid].Item2 - packet.TimeSpan;
                    if (timeSpan != null && timeSpan.Value.Duration() <= TimeSpan.FromSeconds(1))
                        ((Unit)Storage.Objects[guid].Item1).AIAnimKit = animKitID;
                }
        }

        [Parser(Opcode.SMSG_SET_MELEE_ANIM_KIT)]
        public static void SetMeleeAnimKitId(Packet packet)
        {
            var guid = packet.Translator.ReadPackedGuid128("Unit");
            var animKitID = packet.Translator.ReadUInt16("AnimKitID");

            if (guid.GetObjectType() == ObjectType.Unit)
                if (Storage.Objects.ContainsKey(guid))
                {
                    var timeSpan = Storage.Objects[guid].Item2 - packet.TimeSpan;
                    if (timeSpan != null && timeSpan.Value.Duration() <= TimeSpan.FromSeconds(1))
                        ((Unit)Storage.Objects[guid].Item1).MeleeAnimKit = animKitID;
                }
        }

        [Parser(Opcode.SMSG_DISPLAY_PROMOTION)]
        public static void HandleDisplayPromotion(Packet packet)
        {
            packet.Translator.ReadUInt32("PromotionID");
        }

        [Parser(Opcode.SMSG_SET_ALL_TASK_PROGRESS)]
        [Parser(Opcode.SMSG_UPDATE_TASK_PROGRESS)]
        public static void HandleSetAllTaskProgress(Packet packet)
        {
            var int4 = packet.Translator.ReadInt32("TaskProgressCount");
            for (int i = 0; i < int4; i++)
            {
                packet.Translator.ReadUInt32("TaskID", i);
                packet.Translator.ReadUInt32("FailureTime", i);
                packet.Translator.ReadUInt32("Flags", i);

                var int3 = packet.Translator.ReadInt32("ProgressCounts", i);
                for (int j = 0; j < int3; j++)
                    packet.Translator.ReadInt16("Counts", i, j);
            }
        }

        [Parser(Opcode.CMSG_TUTORIAL_FLAG, ClientVersionBuild.V6_0_2_19033, ClientVersionBuild.V6_0_3_19342)]
        public static void HandleTutorialFlag60x(Packet packet)
        {
            var action = packet.Translator.ReadBitsE<TutorialAction>("TutorialAction", 2);

            if (action == TutorialAction.Update)
                packet.Translator.ReadInt32E<Tutorial>("TutorialBit");
        }

        [Parser(Opcode.CMSG_TUTORIAL_FLAG, ClientVersionBuild.V6_1_0_19678, ClientVersionBuild.V6_1_2_19802)]
        public static void HandleTutorialFlag610(Packet packet)
        {
            var action = packet.Translator.ReadBitsE<TutorialAction610>("TutorialAction", 2);

            if (action == TutorialAction610.Update)
                packet.Translator.ReadInt32E<Tutorial>("TutorialBit");
        }

        [Parser(Opcode.CMSG_TUTORIAL_FLAG, ClientVersionBuild.V6_1_2_19802, ClientVersionBuild.V6_2_0_20173)]
        public static void HandleTutorialFlag612(Packet packet)
        {
            var action = packet.Translator.ReadBitsE<TutorialAction612>("TutorialAction", 2);

            if (action == TutorialAction612.Update)
                packet.Translator.ReadInt32E<Tutorial>("TutorialBit");
        }

        [Parser(Opcode.CMSG_TUTORIAL_FLAG, ClientVersionBuild.V6_2_0_20173)]
        public static void HandleTutorialFlag620(Packet packet)
        {
            var action = packet.Translator.ReadBitsE<TutorialAction620>("TutorialAction", 2);

            if (action == TutorialAction620.Update)
                packet.Translator.ReadInt32E<Tutorial>("TutorialBit");
        }

        [Parser(Opcode.SMSG_START_ELAPSED_TIMERS)]
        public static void HandleStartElapsedTimers(Packet packet)
        {
            var int3 = packet.Translator.ReadInt32("ElaspedTimerCounts");
            for (int i = 0; i < int3; i++)
            {
                packet.Translator.ReadUInt32("TimerID", i);
                packet.Translator.ReadTime("CurrentDuration", i);
            }
        }

        [Parser(Opcode.SMSG_REQUEST_CEMETERY_LIST_RESPONSE)]
        public static void HandleRequestCemeteryListResponse(Packet packet)
        {
            packet.Translator.ReadBit("IsTriggered");

            var count = packet.Translator.ReadUInt32("Count");
            for (int i = 0; i < count; ++i)
                packet.Translator.ReadInt32("CemeteryID", i);
        }

        [Parser(Opcode.SMSG_START_MIRROR_TIMER)]
        public static void HandleStartMirrorTimer(Packet packet)
        {
            packet.Translator.ReadUInt32("TimerType"); // Timer in magic
            packet.Translator.ReadUInt32("InitialValue");
            packet.Translator.ReadUInt32("MaxValue");
            packet.Translator.ReadInt32("Scale");
            packet.Translator.ReadUInt32<SpellId>("SpellID");
            packet.Translator.ReadBit("Paused");
        }

        [Parser(Opcode.CMSG_BUG_REPORT)]
        public static void HandleBugReport(Packet packet)
        {
            packet.Translator.ReadBit("Type");

            var len1 = packet.Translator.ReadBits(12);
            var len2 = packet.Translator.ReadBits(10);

            packet.Translator.ReadWoWString("DiagInfo", len1);
            packet.Translator.ReadWoWString("Text", len2);
        }

        [Parser(Opcode.SMSG_RESURRECT_REQUEST)]
        public static void HandleResurrectRequest(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("ResurrectOffererGUID");

            packet.Translator.ReadUInt32("ResurrectOffererVirtualRealmAddress");
            packet.Translator.ReadUInt32("PetNumber");
            packet.Translator.ReadInt32<SpellId>("SpellID");

            var len = packet.Translator.ReadBits(6);

            packet.Translator.ReadBit("UseTimer");
            packet.Translator.ReadBit("Sickness");

            packet.Translator.ReadWoWString("Name", len);
        }

        [Parser(Opcode.CMSG_RESURRECT_RESPONSE)]
        public static void HandleResurrectResponse(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Resurrecter");
            packet.Translator.ReadInt32("Response");
        }

        [Parser(Opcode.CMSG_ACCEPT_LEVEL_GRANT)]
        public static void HandleAcceptLevelGrant(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Granter");
        }

        [Parser(Opcode.SMSG_PRE_RESSURECT)]
        public static void HandlePreRessurect(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("PlayerGUID");
        }

        [Parser(Opcode.SMSG_STREAMING_MOVIES)]
        public static void HandleStreamingMovie(Packet packet)
        {
            var count = packet.Translator.ReadInt32("MovieCount");
            for (var i = 0; i < count; i++)
                packet.Translator.ReadInt16("MovieIDs", i);
        }

        [Parser(Opcode.SMSG_CUSTOM_LOAD_SCREEN)]
        public static void HandleCustomLoadScreen(Packet packet)
        {
            packet.Translator.ReadInt32("TeleportSpellID");
        }

        [Parser(Opcode.CMSG_CONVERSATION_UNK1)]
        public static void HandleConversationUnk1(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Conversation");
            packet.Translator.ReadInt32("ConversationID?");
        }

        [Parser(Opcode.SMSG_PLAY_OBJECT_SOUND)]
        public static void HandlePlayObjectSound(Packet packet)
        {
            uint sound = packet.Translator.ReadUInt32("SoundId");
            packet.Translator.ReadPackedGuid128("SourceObjectGUID");
            packet.Translator.ReadPackedGuid128("TargetObjectGUID");
            packet.Translator.ReadVector3("Position");

            Storage.Sounds.Add(sound, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_PLAY_SPEAKERBOT_SOUND)]
        public static void HandlePlaySpeakerbotSound(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("SourceObjectGUID");
            packet.Translator.ReadUInt32("SoundId");
        }

        [Parser(Opcode.SMSG_START_ELAPSED_TIMER)]
        public static void HandleStartElapsedTimer(Packet packet)
        {
            ReadElaspedTimer(packet);
        }

        [Parser(Opcode.SMSG_STOP_ELAPSED_TIMER)]
        public static void HandleStopElapsedTimer(Packet packet)
        {
            packet.Translator.ReadInt32("TimerID");
            packet.Translator.ReadBit("KeepTimer");
        }

        [Parser(Opcode.CMSG_RANDOM_ROLL)]
        public static void HandleRandomRoll(Packet packet)
        {
            packet.Translator.ReadInt32("Min");
            packet.Translator.ReadInt32("Max");
            packet.Translator.ReadByte("PartyIndex");
        }

        [Parser(Opcode.SMSG_RANDOM_ROLL)]
        public static void HandleRandomRollResult(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Roller");
            packet.Translator.ReadPackedGuid128("RollerWowAccount");
            packet.Translator.ReadInt32("Min");
            packet.Translator.ReadInt32("Max");
            packet.Translator.ReadInt32("Result");
        }

        [Parser(Opcode.SMSG_SET_TASK_COMPLETE)]
        public static void HandleSetTaskComplete(Packet packet)
        {
            packet.Translator.ReadInt32("TaskID");
        }

        [Parser(Opcode.SMSG_DISPLAY_GAME_ERROR)]
        public static void HandleDisplayGameError(Packet packet)
        {
            packet.Translator.ReadUInt32("Error");
            var hasArg = packet.Translator.ReadBit("HasArg");
            var hasArg2 = packet.Translator.ReadBit("HasArg2");

            if (hasArg)
                packet.Translator.ReadUInt32("Arg");

            if (hasArg2)
                packet.Translator.ReadUInt32("Arg2");
        }

        [Parser(Opcode.SMSG_RESTRICTED_ACCOUNT_WARNING)]
        public static void HandleRestrictedAccountWarning(Packet packet)
        {
            packet.Translator.ReadUInt32("Arg");
            packet.Translator.ReadByte("Type");
        }

        [Parser(Opcode.SMSG_WEEKLY_LAST_RESET)]
        public static void HandleLastWeeklyReset(Packet packet)
        {
            packet.Translator.ReadTime("Reset");
        }

        [Parser(Opcode.SMSG_SUMMON_REQUEST)]
        public static void HandleSummonRequest(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("SummonerGUID");
            packet.Translator.ReadUInt32("SummonerVirtualRealmAddress");
            packet.Translator.ReadInt32<AreaId>("AreaID");
            packet.Translator.ReadBit("ConfirmSummon_NC");
        }

        // new opcode on 6.x, related to combat log and mostly used in garrisons
        [Parser(Opcode.SMSG_WORLD_TEXT)]
        public static void HandleWorldText(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Guid");
            packet.Translator.ReadInt32("Arg1");
            packet.Translator.ReadInt32("Arg2");
            var length = packet.Translator.ReadBits("TextLength", 12);
            packet.Translator.ReadWoWString("Text", length);
        }

        [Parser(Opcode.CMSG_ENGINE_SURVEY)]
        public static void HandleEngineSurvey(Packet packet)
        {
            packet.Translator.ReadUInt32("GPUVendorID");
            packet.Translator.ReadUInt32("GPUModelID");
            packet.Translator.ReadUInt32("Unk1C");
            packet.Translator.ReadUInt32("Unk10");
            packet.Translator.ReadUInt32("Unk38");
            packet.Translator.ReadUInt32("DisplayResWidth");
            packet.Translator.ReadUInt32("DisplayResHeight");
            packet.Translator.ReadUInt32("Unk2C");
            packet.Translator.ReadUInt32("MemoryCapacity");
            packet.Translator.ReadUInt32("Unk30");
            packet.Translator.ReadUInt32("Unk18");
            packet.Translator.ReadByte("HasHDPlayerModels");
            packet.Translator.ReadByte("Is64BitSystem");
            packet.Translator.ReadByte("Unk3C");
            packet.Translator.ReadByte("Unk3F");
            packet.Translator.ReadByte("Unk3E");
        }

        [Parser(Opcode.SMSG_TWITTER_STATUS)]
        public static void HandleTwitterStatus(Packet packet)
        {
            packet.Translator.ReadUInt32("StatusInt");
        }

        [Parser(Opcode.SMSG_DURABILITY_DAMAGE_DEATH)]
        public static void HandleDurabilityDamageDeath(Packet packet)
        {
            packet.Translator.ReadInt32("Percent");
        }

        [Parser(Opcode.CMSG_TOY_SET_FAVORITE)]
        public static void HandleToySetFavorite(Packet packet)
        {
            packet.Translator.ReadUInt32("ItemID");
            packet.Translator.ReadBit("Favorite");
        }
    }
}
