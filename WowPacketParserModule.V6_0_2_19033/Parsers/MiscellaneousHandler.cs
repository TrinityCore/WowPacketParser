using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

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
        public static void HandleMiscZero(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_INVALIDATE_PLAYER)]
        public static void HandleReadGuid(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [HasSniffData]
        [Parser(Opcode.CMSG_LOAD_SCREEN)]
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

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS)]
        public static void HandleFeatureSystemStatus(Packet packet)
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
            packet.ReadBit("Unk bit90"); // Also tutorials related

            if (hasEuropaTicketSystemStatus)
                ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");

            if (hasSessionAlert)
                ReadClientSessionAlertConfig(packet, "SessionAlert");
        }

        [Parser(Opcode.SMSG_WORLD_SERVER_INFO)]
        public static void HandleWorldServerInfo(Packet packet)
        {
            packet.ReadInt32("DifficultyID");
            packet.ReadByte("IsTournamentRealm");
            packet.ReadTime("WeeklyReset");

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

        [Parser(Opcode.SMSG_INITIAL_SETUP)]
        public static void HandleInitialSetup(Packet packet)
        {
            var int6 = packet.ReadInt32("QuestsCompletedCount");

            packet.ReadByte("ServerExpansionLevel");
            packet.ReadByte("ServerExpansionTier");

            packet.ReadInt32("ServerRegionID");
            packet.ReadTime("RaidOrigin");

            for (var i = 0; i < int6; ++i)
                packet.ReadByte("QuestsCompleted", i);
        }

        [Parser(Opcode.CMSG_AREATRIGGER)]
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

            var int32 = packet.ReadInt32("ToySpellIDsCount");
            var int16 = packet.ReadInt32("ToyIsFavoriteCount");

            for (int i = 0; i < int32; i++)
                packet.ReadInt32("ToySpellIDs", i);

            packet.ResetBitReader();

            for (int i = 0; i < int16; i++)
                packet.ReadBit("ToyIsFavorite", i);
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

        [Parser(Opcode.CMSG_PAGE_TEXT_QUERY)]
        public static void HandlePageTextQuery(Packet packet)
        {
            packet.ReadInt32("Entry");
            packet.ReadPackedGuid128("Guid");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_PAGE_TEXT_QUERY_RESPONSE)]
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

        [Parser(Opcode.SMSG_SET_AI_ANIM_KIT)]
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

        [Parser(Opcode.CMSG_TUTORIAL_FLAG)]
        public static void HandleTutorialFlag(Packet packet)
        {
            var action = packet.ReadEnum<TutorialAction>("TutorialAction", 2);

            if (action == TutorialAction.Update)
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

        [Parser(Opcode.SMSG_PLAY_ONE_SHOT_ANIM_KIT)]
        public static void HandlePlayOneShotAnimKit(Packet packet)
        {
            packet.ReadPackedGuid128("Unit");
            packet.ReadUInt16("AnimKitID");
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

            packet.ReadInt32("ResurrectOffererVirtualRealmAddress");
            packet.ReadInt32("PetNumber");
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

        [Parser(Opcode.SMSG_STREAMING_MOVIE)]
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
        [Parser(Opcode.SMSG_COMBAT_LOG_UNK)]
        public static void HandleCombatLogUnk(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadInt32("Arg1");
            packet.ReadInt32("Arg2");
            var length = packet.ReadBits("TextLength", 12);
            packet.ReadWoWString("Text", length);
        }
    }
}
