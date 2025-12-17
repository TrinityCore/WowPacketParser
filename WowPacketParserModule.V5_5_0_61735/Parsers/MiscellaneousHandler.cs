using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class MiscellaneousHandler
    {
        public static void ReadAreaPoiData(Packet packet, params object[] idx)
        {
            packet.ReadTime64("StartTime", idx);
            packet.ReadInt32("AreaPoiID", idx);
            packet.ReadInt32("DurationSec", idx);
            packet.ReadUInt32("WorldStateVariableID", idx);
            packet.ReadUInt32("WorldStateValue", idx);
        }

        public static void ReadWhoEntry(Packet packet, params object[] idx)
        {
            CharacterHandler.ReadPlayerGuidLookupData(packet, idx);

            packet.ReadPackedGuid128("GuildGUID", idx);

            packet.ReadUInt32("GuildVirtualRealmAddress", idx);
            packet.ReadInt32<AreaId>("AreaID", idx);

            packet.ResetBitReader();
            var guildNameLen = packet.ReadBits(7);
            packet.ReadBit("IsGM", idx);

            packet.ReadWoWString("GuildName", guildNameLen, idx);
        }

        public static void ReadGameRuleValuePair(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("Rule", indexes);
            packet.ReadInt32("Value", indexes);
            packet.ReadSingle("ValueF", indexes);
        }

        public static void ReadClientSessionAlertConfig(Packet packet, params object[] idx)
        {
            packet.ReadInt32("Delay", idx);
            packet.ReadInt32("Period", idx);
            packet.ReadInt32("DisplayTime", idx);
        }

        public static void ReadVoiceChatManagerSettings(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();
            packet.ReadBit("IsSquelched", idx);
            packet.ReadPackedGuid128("BnetAccountID", idx);
            packet.ReadPackedGuid128("GuildGUID", idx);
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

        public static void ReadDebugTimeInfo(Packet packet, params object[] indexes)
        {
            packet.ReadUInt32("TimeEvent", indexes);
            packet.ResetBitReader();
            var textLen = packet.ReadBits(7);
            packet.ReadWoWString("Text", textLen);
        }

        public static void ReadElaspedTimer(Packet packet, params object[] indexes)
        {
            packet.ReadUInt64("CurrentDuration", indexes);
            packet.ReadInt32("TimerID", indexes);
        }

        [Parser(Opcode.SMSG_PONG)]
        public static void HandleServerPong(Packet packet)
        {
            packet.ReadInt32("Serial");
        }

        [Parser(Opcode.SMSG_MULTIPLE_PACKETS)]
        public static void HandleMultiplePackets(Packet packet)
        {
            packet.WriteLine("{");
            int i = 0;
            while (packet.CanRead())
            {
                int opcode = 0;
                int len = 0;
                byte[] bytes = null;

                len = packet.ReadUInt16();
                opcode = packet.ReadUInt16();
                bytes = packet.ReadBytes(len);

                if (bytes == null || len == 0)
                    continue;

                if (i > 0)
                    packet.WriteLine();

                packet.Write("[{0}] ", i++);

                using (Packet newpacket = new Packet(bytes, opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName))
                    Handler.Parse(newpacket, true);

            }
            packet.WriteLine("}");
        }

        [Parser(Opcode.SMSG_INVALIDATE_PLAYER)]
        public static void HandleReadGuid(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_PLAYER_SKINNED)]
        public static void HandlePlayerSkinned(Packet packet)
        {
            packet.ReadBit("FreeRepop");
        }

        [Parser(Opcode.SMSG_AREA_POI_UPDATE_RESPONSE)]
        public static void HandleAreaPOIUpdateResponse(Packet packet)
        {
            var count = packet.ReadInt32("Count");

            for (var i = 0; i < count; i++)
                ReadAreaPoiData(packet, i);
        }

        [Parser(Opcode.SMSG_CACHE_VERSION)]
        public static void HandleClientCacheVersion(Packet packet)
        {
            packet.ReadInt32("Version");
        }

        [Parser(Opcode.SMSG_WHO)]
        public static void HandleWho(Packet packet)
        {
            packet.ReadUInt32("RequestID");
            var entriesCount = packet.ReadBits("EntriesCount", 6);

            for (var i = 0; i < entriesCount; ++i)
                ReadWhoEntry(packet, i);
        }

        [Parser(Opcode.SMSG_ZONE_UNDER_ATTACK)]
        public static void HandleZoneUpdate(Packet packet)
        {
            packet.ReadInt32<AreaId>("AreaID");
        }

        [Parser(Opcode.SMSG_INITIAL_SETUP)]
        public static void HandleInitialSetup(Packet packet)
        {
            packet.ReadByte("ServerExpansionLevel");
            packet.ReadByte("ServerExpansionTier");
        }

        [Parser(Opcode.SMSG_REQUEST_CEMETERY_LIST_RESPONSE)]
        public static void HandleRequestCemeteryListResponse(Packet packet)
        {
            packet.ReadBit("IsTriggered");

            var count = packet.ReadUInt32("Count");
            for (int i = 0; i < count; ++i)
                packet.ReadInt32("CemeteryID", i);
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

        [Parser(Opcode.SMSG_STREAMING_MOVIES)]
        public static void HandleStreamingMovie(Packet packet)
        {
            var count = packet.ReadInt32("MovieCount");
            for (var i = 0; i < count; i++)
                packet.ReadInt16("MovieIDs", i);
        }

        [Parser(Opcode.SMSG_START_TIMER)]
        public static void HandleStartTimer(Packet packet)
        {
            packet.ReadInt64("TotalTime");
            packet.ReadUInt32E<TimerType>("Type");
            packet.ReadInt64("TimeLeft");

            var hasPlayerGUID = packet.ReadBit("HasPlayerGUID");
            if (hasPlayerGUID)
                packet.ReadPackedGuid128("PlayerGUID");
        }

        [Parser(Opcode.SMSG_WORLD_SERVER_INFO)]
        public static void HandleWorldServerInfo(Packet packet)
        {
            CoreParsers.MovementHandler.CurrentDifficultyID = packet.ReadUInt32<DifficultyId>("DifficultyID");

            packet.ReadBit("IsTournamentRealm");
            packet.ReadBit("XRealmPvpAlert");
            var hasRestrictedAccountMaxLevel = packet.ReadBit("HasRestrictedAccountMaxLevel");
            var hasRestrictedAccountMaxMoney = packet.ReadBit("HasRestrictedAccountMaxMoney");
            var hasInstanceGroupSize = packet.ReadBit("HasInstanceGroupSize");

            if (hasRestrictedAccountMaxLevel)
                packet.ReadUInt32("RestrictedAccountMaxLevel");

            if (hasRestrictedAccountMaxMoney)
                packet.ReadUInt64("RestrictedAccountMaxMoney");

            if (hasInstanceGroupSize)
                packet.ReadUInt32("InstanceGroupSize");
        }

        [Parser(Opcode.SMSG_ACCOUNT_MOUNT_UPDATE)]
        public static void HandleAccountMountUpdate(Packet packet)
        {
            packet.ReadBit("IsFullUpdate");

            var mountSpellIDsCount = packet.ReadInt32("MountSpellIDsCount");

            for (int i = 0; i < mountSpellIDsCount; i++)
            {
                packet.ReadInt32("MountSpellIDs", i);

                packet.ResetBitReader();
                packet.ReadBits("Flags", 4, i);
            }
        }

        [Parser(Opcode.SMSG_ACCOUNT_HEIRLOOM_UPDATE)]
        public static void HandleAccountHeirloomUpdate(Packet packet)
        {
            packet.ReadBit("IsFullUpdate");

            packet.ReadInt32("ItemCollectionType");

            uint itemCount = packet.ReadUInt32("ItemCount");
            uint flagCount = packet.ReadUInt32("FlagsCount");

            for (uint i = 0u; i < itemCount; i++)
                packet.ReadInt32<ItemId>("ItemID", i);

            for (uint i = 0u; i < flagCount; i++)
                packet.ReadUInt32("Flags", i);
        }

        [Parser(Opcode.SMSG_SET_PLAY_HOVER_ANIM)]
        public static void HandlePlayHoverAnim(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            packet.ReadBit("PlayHoverAnim");
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS)]
        public static void HandleFeatureSystemStatus(Packet packet)
        {
            packet.ReadByte("ComplaintStatus");
            packet.ReadUInt32("CfgRealmID");
            packet.ReadInt32("CfgRealmRecID");

            packet.ReadUInt32("MaxRecruits", "RAFSystem");
            packet.ReadUInt32("MaxRecruitMonths", "RAFSystem");
            packet.ReadUInt32("MaxRecruitmentUses", "RAFSystem");
            packet.ReadUInt32("DaysInCycle", "RAFSystem");
            packet.ReadUInt32("RewardsVersion", "RAFSystem");
            packet.ReadUInt32("CommercePricePollTimeSeconds");
            packet.ReadUInt32("KioskSessionDurationMinutes");
            packet.ReadInt64("RedeemForBalanceAmount");
            packet.ReadUInt32("BpayStorePurchaseTimeout");
            packet.ReadUInt32("ClubsPresenceDelay");
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V5_5_1_63311))
                packet.ReadUInt32("ClubPresenceUnsubscribeDelay");
            packet.ReadInt32("ContentSetID");

            var gameRuleValuesCount = packet.ReadUInt32("GameRuleValuesCount");

            packet.ReadInt32("ActiveTimerunningSeasonID");
            packet.ReadInt32("RemainingTimerunningSeasonSeconds");

            packet.ReadInt16("MaxPlayerGuidLookupsPerRequest");
            packet.ReadInt16("NameLookupTelemetryInterval");
            packet.ReadUInt32("NotFoundCacheTimeSeconds");
            packet.ReadUInt32("RealmPvpTypeOverride");
            packet.ReadInt32("MaxTries", "AddonChatThrottle");
            packet.ReadInt32("TriesRestoredPerSecond", "AddonChatThrottle");
            packet.ReadInt32("UsedTriesPerMessage", "AddonChatThrottle");
            packet.ReadSingle("AddonPerformanceMsgWarning");
            packet.ReadSingle("AddonPerformanceMsgError");
            packet.ReadSingle("AddonPerformanceMsgOverall");

            for (var i = 0; i < gameRuleValuesCount; ++i)
                ReadGameRuleValuePair(packet, "GameRuleValues");

            packet.ResetBitReader();
            packet.ReadBit("VoiceEnabled");
            var hasEuropaTicketSystemStatus = packet.ReadBit("HasEuropaTicketSystemStatus");
            packet.ReadBit("BpayStoreEnabled");
            packet.ReadBit("BpayStoreAvailable");
            packet.ReadBit("BpayStoreDisabledByParentalControls");
            packet.ReadBit("ItemRestorationButtonEnabled");
            packet.ReadBit("BrowserEnabled");
            var hasSessionAlert = packet.ReadBit("HasSessionAlert");

            packet.ReadBit("Enabled", "RAFSystem");
            packet.ReadBit("RecruitingEnabled", "RAFSystem");
            packet.ReadBit("CharUndeleteEnabled");
            packet.ReadBit("RestrictedAccount");
            packet.ReadBit("CommerceServerEnabled");
            packet.ReadBit("TutorialsEnabled");
            packet.ReadBit("VeteranTokenRedeemWillKick");
            packet.ReadBit("WorldTokenRedeemWillKick");

            packet.ReadBit("KioskModeEnabled");
            packet.ReadBit("CompetitiveModeEnabled");
            packet.ReadBit("RedeemForBalanceAvailable");
            packet.ReadBit("WarModeEnabled");
            packet.ReadBit("CommunitiesEnabled");
            packet.ReadBit("BnetGroupsEnabled");
            packet.ReadBit("CharacterCommunitiesEnabled");
            packet.ReadBit("ClubPresenceAllowSubscribeAll");

            packet.ReadBit("VoiceChatParentalDisabled");
            packet.ReadBit("VoiceChatParentalMuted");
            packet.ReadBit("QuestSessionEnabled");
            packet.ReadBit("IsChatMuted");
            packet.ReadBit("ClubFinderEnabled");
            packet.ReadBit("CommunityFinderEnabled");
            packet.ReadBit("BrowserCrashReporterEnabled");
            packet.ReadBit("SpeakForMeAllowed");

            packet.ReadBit("DoesAccountNeedAADCPrompt");
            packet.ReadBit("IsAccountOptedInToAADC");
            packet.ReadBit("LfgRequireAuthenticatorEnabled");
            packet.ReadBit("ScriptsDisallowedForBeta");
            packet.ReadBit("WarGamesEnabled");
            var hasRaceClassExpansionLevels = packet.ReadBit("RaceClassExpansionLevels");
            packet.ReadBit("IsPlayerContentTrackingEnabled");
            packet.ReadBit("IsSellAllJunkEnabled");

            packet.ReadBit("GroupFinderEnabled");
            packet.ReadBit("PremadeGroupEnabled");
            packet.ReadBit("LFDEnabled");
            packet.ReadBit("LFREnabled");
            packet.ReadBit("PetHappinessEnabled");
            packet.ReadBit("GuildEventsEditsEnabled");
            packet.ReadBit("GuildTradeSkillsEnabled");
            var unknown1027StrLen = packet.ReadBits(7);
            packet.ReadBit("BNSendWhisperUseV2Services");
            packet.ReadBit("BNSendGameDataUseV2Services");

            packet.ReadBit("IsAccountCurrencyTransferEnabled");
            packet.ReadBit("NetEaseRelated");
            packet.ReadBit("LobbyMatchmakerQueueFromMainlineEnabled");
            packet.ReadBit("CanSendLobbyMatchmakerPartyCustomizations");
            packet.ReadBit("AddonProfilerEnabled");
            packet.ReadBit("Unused_11_1_7_1");
            packet.ReadBit("Unused_11_1_7_2");

            {
                packet.ResetBitReader();
                packet.ReadSingle("ToastDuration", "QuickJoinConfig");
                packet.ReadSingle("DelayDuration", "QuickJoinConfig");
                packet.ReadSingle("QueueMultiplier", "QuickJoinConfig");
                packet.ReadSingle("PlayerMultiplier", "QuickJoinConfig");
                packet.ReadSingle("PlayerFriendValue", "QuickJoinConfig");
                packet.ReadSingle("PlayerGuildValue", "QuickJoinConfig");
                packet.ReadSingle("ThrottleInitialThreshold", "QuickJoinConfig");
                packet.ReadSingle("ThrottleDecayTime", "QuickJoinConfig");
                packet.ReadSingle("ThrottlePrioritySpike", "QuickJoinConfig");
                packet.ReadSingle("ThrottleMinThreshold", "QuickJoinConfig");
                packet.ReadSingle("ThrottlePvPPriorityNormal", "QuickJoinConfig");
                packet.ReadSingle("ThrottlePvPPriorityLow", "QuickJoinConfig");
                packet.ReadSingle("ThrottlePvPHonorThreshold", "QuickJoinConfig");
                packet.ReadSingle("ThrottleLfgListPriorityDefault", "QuickJoinConfig");
                packet.ReadSingle("ThrottleLfgListPriorityAbove", "QuickJoinConfig");
                packet.ReadSingle("ThrottleLfgListPriorityBelow", "QuickJoinConfig");
                packet.ReadSingle("ThrottleLfgListIlvlScalingAbove", "QuickJoinConfig");
                packet.ReadSingle("ThrottleLfgListIlvlScalingBelow", "QuickJoinConfig");
                packet.ReadSingle("ThrottleRfPriorityAbove", "QuickJoinConfig");
                packet.ReadSingle("ThrottleRfIlvlScalingAbove", "QuickJoinConfig");
                packet.ReadSingle("ThrottleDfMaxItemLevel", "QuickJoinConfig");
                packet.ReadSingle("ThrottleDfBestPriority", "QuickJoinConfig");
                packet.ReadBit("ToastsDisabled", "QuickJoinConfig");
            }

            if (hasSessionAlert)
                ReadClientSessionAlertConfig(packet, "SessionAlert");

            if (hasRaceClassExpansionLevels)
            {
                var count = packet.ReadUInt32();

                for (var i = 0; i < count; ++i)
                    packet.ReadByte("RaceClassExpansionLevels", i);
            }

            packet.ReadWoWString("Unknown1027", unknown1027StrLen);

            ReadVoiceChatManagerSettings(packet, "VoiceChatManagerSettings");

            if (hasEuropaTicketSystemStatus)
            {
                packet.ResetBitReader();
                ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");
            }
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS_GLUE_SCREEN)]
        public static void HandleFeatureSystemStatusGlueScreen(Packet packet)
        {
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V5_5_1_63311))
                packet.ReadBit("BpayStoreEnabled");
            packet.ReadBit("BpayStoreAvailable");
            packet.ReadBit("BpayStoreDisabledByParentalControls");
            packet.ReadBit("CharUndeleteEnabled");
            packet.ReadBit("CommerceSystemEnabled");
            packet.ReadBit("Unk14");
            packet.ReadBit("WillKickFromWorld");
            packet.ReadBit("IsExpansionPreorderInStore");

            packet.ReadBit("KioskModeEnabled");
            packet.ReadBit("IsCompetitiveModeEnabled");
            packet.ReadBit("IsBoostEnabled");
            packet.ReadBit("TrialBoostEnabled");
            packet.ReadBit("TokenBalanceEnabled");
            packet.ReadBit("PaidCharacterTransfersBetweenBnetAccountsEnabled");
            packet.ReadBit("LiveRegionCharacterListEnabled");
            packet.ReadBit("LiveRegionCharacterCopyEnabled");

            packet.ReadBit("LiveRegionAccountCopyEnabled");
            packet.ReadBit("LiveRegionKeyBindingsCopyEnabled");
            packet.ReadBit("Unknown901CheckoutRelated");
            packet.ReadBit("SoftTargetEnabled");
            var europaTicket = packet.ReadBit("IsEuropaTicketSystemStatusEnabled");
            packet.ReadBit("IsNameReservationEnabled");
            bool launchETA = packet.ReadBit("IsLaunchETA");
            packet.ReadBit("TimerunningEnabled");

            packet.ReadBit("Unk");
            packet.ReadBit("Unk");
            packet.ReadBit("SoMNotificationEnabled");
            packet.ReadBit("Unk");
            packet.ReadBit("ScriptsDisallowedForBeta");
            packet.ReadBit("PlayerIdentityOptionsEnabled");
            packet.ReadBit("AccountSaveDataExportEnabled");
            packet.ReadBit("AccountLockedByExport");

            uint realmHiddenAlertLen = packet.ReadBits(11);
            packet.ReadBit("BNSendWhisperUseV2Services");
            packet.ReadBit("BNSendGameDataUseV2Services");
            packet.ReadBit("CharacterSelectListModeRealmless");
            packet.ReadBit("WowTokenLimitedMode");
            packet.ReadBit("NavBarEnabled");

            packet.ReadBit("Unused_11_1_7_1");
            packet.ReadBit("Unused_11_1_7_2");
            packet.ReadBit("PandarenLevelBoostAllowed");

            packet.ResetBitReader();

            if (europaTicket)
                ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");

            packet.ReadUInt32("TokenPollTimeSeconds");
            packet.ReadUInt32("KioskSessionMinutes");
            packet.ReadInt64("TokenBalanceAmount");
            packet.ReadInt32("MaxCharactersPerRealm");
            var liveRegionCharacterCopySourceRegionsCount = packet.ReadUInt32("LiveRegionCharacterCopySourceRegionsCount");
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V5_5_1_63311))
                packet.ReadUInt32("BpayStoreProductDeliveryDelay");
            packet.ReadInt32("ActiveCharacterUpgradeBoostType");
            packet.ReadInt32("ActiveClassTrialBoostType");
            packet.ReadInt32("MinimumExpansionLevel");
            packet.ReadInt32("MaximumExpansionLevel");
            packet.ReadInt32("ActiveSeason");
            var gameRuleValuesCount = packet.ReadUInt32("GameRuleValuesCount");
            packet.ReadInt32("ActiveTimerunningSeasonID");
            packet.ReadInt32("RemainingTimerunningSeasonSeconds");
            packet.ReadInt16("MaxPlayerNameQueriesPerPacket");
            packet.ReadInt16("PlayerNameQueryTelemetryInterval");
            packet.ReadInt32("PlayerNameQueryInterval");
            var debugTimeEventsCount = packet.ReadInt32("DebugTimeEventCount");
            packet.ReadInt32("Unused1007");
            packet.ReadInt32("EventRealmQueues");

            if (launchETA)
                packet.ReadPackedTime("LaunchETA");

            packet.ReadDynamicString("RealmHiddenAlert", realmHiddenAlertLen);

            for (int i = 0; i < liveRegionCharacterCopySourceRegionsCount; i++)
                packet.ReadUInt32("LiveRegionCharacterCopySourceRegion", i);

            for (var i = 0; i < gameRuleValuesCount; ++i)
                ReadGameRuleValuePair(packet, "GameRuleValues");

            for (var i = 0; i < debugTimeEventsCount; ++i)
                ReadDebugTimeInfo(packet, "DebugTimeEvent", i);
        }

        [Parser(Opcode.SMSG_CUSTOM_LOAD_SCREEN)]
        public static void HandleCustomLoadScreen(Packet packet)
        {
            packet.ReadUInt32("TeleportSpellID");
            packet.ReadUInt32("LoadingScreenID");
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

        [Parser(Opcode.SMSG_START_ELAPSED_TIMERS)]
        public static void HandleStartElapsedTimers(Packet packet)
        {
            var int3 = packet.ReadInt32("ElaspedTimerCounts");
            for (int i = 0; i < int3; i++)
                ReadElaspedTimer(packet, i);
        }

        [Parser(Opcode.SMSG_DISPLAY_TOAST)]
        public static void HandleDisplayToast(Packet packet)
        {
            packet.ReadUInt64("Quantity");
            packet.ReadUInt32("DisplayToastMethod");

            packet.ReadUInt32("QuestID");

            packet.ResetBitReader();

            packet.ReadBit("Mailed");
            var type = packet.ReadBits("Type", 2);
            packet.ReadBit("IsSecondaryResult");

            if (type == 0)
            {
                packet.ReadBit("BonusRoll");
                packet.ReadBit("ForceToast");
                Substructures.ItemHandler.ReadItemInstance(packet);
                packet.ReadInt32("LootSpec");
                packet.ReadSByte("Gender");
            }

            if (type == 1)
                packet.ReadUInt32("CurrencyID");
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

        [Parser(Opcode.SMSG_UPDATE_EXPANSION_LEVEL)]
        public static void HandleUpdateExpansionLevel(Packet packet)
        {
            var hasActiveExpansionLevel = packet.ReadBit();
            var hasAccountExpansionLevel = packet.ReadBit();
            var hasUpgradingFromExpansionTrial = packet.ReadBit();

            if (hasUpgradingFromExpansionTrial)
                packet.ReadBit("UpgradingFromExpansionTrial");

            var availableClasses = packet.ReadInt32("AvailableClasses");

            if (hasActiveExpansionLevel)
                packet.ReadByteE<ClientType>("ActiveExpansionLevel");

            if (hasAccountExpansionLevel)
                packet.ReadByteE<ClientType>("AccountExpansionLevel");

            for (var i = 0; i < availableClasses; i++)
            {
                packet.ReadByteE<Race>("RaceID", "AvailableClasses", i);
                var classesForRace = packet.ReadUInt32();
                for (var j = 0u; j < classesForRace; ++j)
                {
                    packet.ReadByteE<Class>("ClassID", "AvailableClasses", i, "Classes", j);
                    packet.ReadByteE<ClientType>("ActiveExpansionLevel", "AvailableClasses", i, "Classes", j);
                    packet.ReadByteE<ClientType>("AccountExpansionLevel", "AvailableClasses", i, "Classes", j);
                    packet.ReadByte("MinActiveExpansionLevel", "AvailableClasses", i, "Classes", j);
                }
            }
        }

        [Parser(Opcode.SMSG_DISPLAY_PROMOTION)]
        public static void HandleDisplayPromotion(Packet packet)
        {
            packet.ReadUInt32("PromotionID");
        }

        [Parser(Opcode.SMSG_SPECIAL_MOUNT_ANIM)]
        public static void HandleSpecialMountAnim(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            var spellVisualKitIdCount = packet.ReadUInt32("SpellVisualKitIdCount");
            packet.ReadInt32("SequenceVariation");

            for (var i = 0; i < spellVisualKitIdCount; i++)
                packet.ReadUInt32("SpellVisualKitID", i);
        }

        [Parser(Opcode.SMSG_WHO_IS)]
        public static void HandleWhoIsResponse(Packet packet)
        {
            var accNameLen = packet.ReadBits(11);
            packet.ReadWoWString("AccountName", accNameLen);
        }

        [Parser(Opcode.SMSG_WEATHER)]
        public static void HandleWeatherStatus(Packet packet)
        {
            WeatherState state = packet.ReadInt32E<WeatherState>("State");
            float grade = packet.ReadSingle("Intensity");
            Bit unk = packet.ReadBit("Abrupt"); // Type

            Storage.WeatherUpdates.Add(new WeatherUpdate
            {
                MapId = CoreParsers.MovementHandler.CurrentMapId,
                ZoneId = 0, // fixme
                State = state,
                Grade = grade,
                Unk = unk
            }, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_START_LIGHTNING_STORM)]
        [Parser(Opcode.SMSG_END_LIGHTNING_STORM)]
        public static void HandleLightningStorm(Packet packet)
        {
            packet.ReadUInt32("LightningStormId");
        }

        [Parser(Opcode.SMSG_OVERRIDE_LIGHT)]
        public static void HandleOverrideLight(Packet packet)
        {
            packet.ReadUInt32("AreaLightID");
            packet.ReadUInt32("OverrideLightID");
            packet.ReadUInt32("TransitionMilliseconds");
        }

        [Parser(Opcode.SMSG_ENABLE_BARBER_SHOP)]
        public static void HandleEnableBarberShop(Packet packet)
        {
            packet.ReadUInt32("CustomizationFeatureMask");
        }

        [Parser(Opcode.SMSG_CONFIRM_BARBERS_CHOICE)]
        public static void HandleConfirmBarbersChoice(Packet packet)
        {
            packet.ReadUInt32("Cost");
        }

        [Parser(Opcode.SMSG_BARBER_SHOP_RESULT)]
        public static void HandleBarberShopResult(Packet packet)
        {
            packet.ReadInt32E<BarberShopResult>("Result");
            packet.ReadBit("IgnoreChair");
        }

        [Parser(Opcode.SMSG_RECRUIT_A_FRIEND_FAILURE)]
        public static void HandleRaFFailure(Packet packet)
        {
            packet.ReadInt32("Reason");
            packet.ResetBitReader();
            var len = packet.ReadBits(6);
            packet.ReadWoWString("Str", len);
        }

        [Parser(Opcode.SMSG_TRIGGER_MOVIE)]
        [Parser(Opcode.SMSG_TRIGGER_CINEMATIC)]
        public static void HandleTriggerMovie(Packet packet)
        {
            packet.ReadInt32("CinematicID");
        }

        [Parser(Opcode.SMSG_DEATH_RELEASE_LOC)]
        public static void HandleDeathReleaseLoc(Packet packet)
        {
            packet.ReadInt32<MapId>("Map Id");
            packet.ReadVector3("Position");
        }

        [Parser(Opcode.SMSG_PROPOSE_LEVEL_GRANT)]
        public static void HandleGrantLevel(Packet packet)
        {
            packet.ReadPackedGuid128("GUID");
        }

        [Parser(Opcode.SMSG_SET_VEHICLE_REC_ID)]
        public static void HandleSetVehicleRecID(Packet packet)
        {
            packet.ReadPackedGuid128("VehicleGUID");
            packet.ReadInt32("VehicleRecID");
        }

        [Parser(Opcode.SMSG_START_MIRROR_TIMER)]
        public static void HandleStartMirrorTimer(Packet packet)
        {
            packet.ReadByteE<MirrorTimerType>("Timer");
            packet.ReadUInt32("Value");
            packet.ReadUInt32("MaxValue");
            packet.ReadInt32("Scale");
            packet.ReadUInt32<SpellId>("SpellID");
            packet.ReadBit("Paused");
        }

        [Parser(Opcode.SMSG_PAUSE_MIRROR_TIMER)]
        public static void HandlePauseMirrorTimer(Packet packet)
        {
            packet.ReadByteE<MirrorTimerType>("Timer");
            packet.ReadBit("Paused");
        }

        [Parser(Opcode.SMSG_STOP_MIRROR_TIMER)]
        public static void HandleStopMirrorTimer(Packet packet)
        {
            packet.ReadByteE<MirrorTimerType>("Timer");
        }

        [Parser(Opcode.SMSG_SUMMON_REQUEST)]
        public static void HandleSummonRequest(Packet packet)
        {
            packet.ReadPackedGuid128("SummonerGUID");
            packet.ReadUInt32("SummonerVirtualRealmAddress");
            packet.ReadInt32<AreaId>("AreaID");
            packet.ReadByte("Reason");
            packet.ResetBitReader();
            packet.ReadBit("SkipStartingArea");
        }

        [Parser(Opcode.SMSG_REFER_A_FRIEND_EXPIRED)]
        public static void HandleReferAFriendExpired(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_SET_AI_ANIM_KIT)]
        public static void SetAIAnimKitId(Packet packet)
        {
            var animKit = packet.Holder.SetAnimKit = new();
            var guid = packet.ReadPackedGuid128("Unit");
            var animKitID = packet.ReadUInt16("AnimKitID");

            if (guid.GetObjectType() == ObjectType.Unit)
                if (Storage.Objects.ContainsKey(guid))
                {
                    var timeSpan = Storage.Objects[guid].Item2 - packet.TimeSpan;
                    if (timeSpan != null && timeSpan.Value.Duration() <= TimeSpan.FromSeconds(1))
                        ((Unit)Storage.Objects[guid].Item1).AIAnimKit = animKitID;
                }

            animKit.Unit = guid;
            animKit.AnimKit = animKitID;
        }

        [Parser(Opcode.SMSG_PLAY_ONE_SHOT_ANIM_KIT)]
        public static void HandlePlayOneShotAnimKit(Packet packet)
        {
            var animKit = packet.Holder.OneShotAnimKit = new();
            animKit.Unit = packet.ReadPackedGuid128("Unit");
            animKit.AnimKit = packet.ReadUInt16("AnimKitID");
        }

        [Parser(Opcode.SMSG_SET_MOVEMENT_ANIM_KIT)]
        public static void HandleSetMovementAnimKit(Packet packet)
        {
            packet.ReadPackedGuid128("Unit");
            packet.ReadUInt16("AnimKitID");
        }

        [Parser(Opcode.SMSG_SET_MELEE_ANIM_KIT)]
        public static void SetMeleeAnimKitId(Packet packet)
        {
            var guid = packet.ReadPackedGuid128("Unit");
            var animKitID = packet.ReadUInt16("AnimKitID");

            if (guid.GetObjectType() == ObjectType.Unit)
                if (Storage.Objects.ContainsKey(guid))
                {
                    var timeSpan = Storage.Objects[guid].Item2 - packet.TimeSpan;
                    if (timeSpan != null && timeSpan.Value.Duration() <= TimeSpan.FromSeconds(1))
                        ((Unit)Storage.Objects[guid].Item1).MeleeAnimKit = animKitID;
                }
        }

        [Parser(Opcode.SMSG_SET_ANIM_TIER)]
        public static void HandleSetAnimTier(Packet packet)
        {
            packet.ReadPackedGuid128("Unit");
            packet.ReadBits("Tier", 3);
        }

        [Parser(Opcode.SMSG_DURABILITY_DAMAGE_DEATH)]
        public static void HandleDurabilityDamageDeath(Packet packet)
        {
            packet.ReadInt32("Percent");
        }

        [Parser(Opcode.SMSG_EXPLORATION_EXPERIENCE)]
        public static void HandleExplorationExperience(Packet packet)
        {
            packet.ReadUInt32<AreaId>("AreaID");
            packet.ReadUInt32("Experience");
        }

        [Parser(Opcode.SMSG_PRE_RESSURECT)]
        public static void HandlePreRessurect(Packet packet)
        {
            packet.ReadPackedGuid128("PlayerGUID");
        }

        [Parser(Opcode.SMSG_PLAY_SOUND)]
        public static void HandlePlaySound(Packet packet)
        {
            PacketPlaySound packetPlaySound = packet.Holder.PlaySound = new PacketPlaySound();
            var sound = packetPlaySound.Sound = (uint)packet.ReadInt32<SoundId>("SoundKitID");
            packetPlaySound.Source = packet.ReadPackedGuid128("SourceObjectGUID").ToUniversalGuid();
            packetPlaySound.BroadcastTextId = (uint)packet.ReadInt32("BroadcastTextID");

            Storage.Sounds.Add(sound, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_PLAY_MUSIC)]
        public static void HandlePlayMusic(Packet packet)
        {
            PacketPlayMusic packetMusic = packet.Holder.PlayMusic = new PacketPlayMusic();
            uint sound = packetMusic.Music = packet.ReadUInt32<SoundId>("SoundKitID");

            Storage.Sounds.Add(sound, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_PLAY_OBJECT_SOUND)]
        public static void HandlePlayObjectSound(Packet packet)
        {
            PacketPlayObjectSound packetSound = packet.Holder.PlayObjectSound = new PacketPlayObjectSound();
            uint sound = packetSound.Sound = packet.ReadUInt32<SoundId>("SoundId");
            packetSound.Source = packet.ReadPackedGuid128("SourceObjectGUID");
            packetSound.Target = packet.ReadPackedGuid128("TargetObjectGUID");
            packet.ReadVector3("Position");
            packet.ReadInt32("BroadcastTextID");

            Storage.Sounds.Add(sound, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_PLAY_SPEAKERBOT_SOUND)]
        public static void HandlePlaySpeakerbotSound(Packet packet)
        {
            packet.ReadPackedGuid128("SourceObjectGUID");
            packet.ReadUInt32("SoundId");
        }

        [Parser(Opcode.SMSG_STOP_SPEAKERBOT_SOUND)]
        public static void HandleStopSpeakerbotSound(Packet packet)
        {
            packet.ReadPackedGuid128("SourceObjectGUID");
        }

        [Parser(Opcode.SMSG_XP_GAIN_ENABLED)]
        public static void HandleXpGainEnabled(Packet packet)
        {
            packet.ReadBit("Enabled");
        }

        [Parser(Opcode.SMSG_TUTORIAL_FLAGS)]
        public static void HandleTutorialFlags(Packet packet)
        {
            for (var i = 0; i < 32; i++)
                packet.ReadByte("TutorialData", i);
        }

        [Parser(Opcode.SMSG_DISPLAY_WORLD_TEXT)]
        public static void HandleDisplayWorldText(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadInt32("Arg1");
            packet.ReadInt32("Arg2");
            var length = packet.ReadBits("TextLength", 12);
            packet.ReadWoWString("Text", length);
        }

        [Parser(Opcode.SMSG_ALLIED_RACE_DETAILS)]
        public static void HandleAlliedRaceDetails(Packet packet)
        {
            packet.ReadPackedGuid128("GUID");
            packet.ReadByteE<Race>("RaceID");
        }

        [Parser(Opcode.SMSG_SPLASH_SCREEN_SHOW_LATEST)]
        public static void HandleSplashScreenShowLastest(Packet packet)
        {
            packet.ReadInt32("SplashScreenID");
        }

        [Parser(Opcode.SMSG_SOCIAL_CONTRACT_REQUEST_RESPONSE)]
        public static void HandleSocialContractRequestResponse(Packet packet)
        {
            packet.ReadBit("ShowSocialContract");
        }

        [Parser(Opcode.CMSG_SET_PREFERRED_CEMETERY)]
        public static void HandleSetPreferedCemetery(Packet packet)
        {
            packet.ReadUInt32("CemeteryID");
        }

        [Parser(Opcode.CMSG_VIOLENCE_LEVEL)]
        public static void HandleSetViolenceLevel(Packet packet)
        {
            packet.ReadByte("Level");
        }

        [Parser(Opcode.CMSG_QUERY_COUNTDOWN_TIMER)]
        public static void HandleQueryCountdownTimer(Packet packet)
        {
            packet.ReadInt32("TimerType");
        }

        [Parser(Opcode.CMSG_REQUEST_VEHICLE_SWITCH_SEAT)]
        public static void HandleRequestVehicleSwitchSeat(Packet packet)
        {
            packet.ReadPackedGuid128("Vehicle");
            packet.ReadByte("SeatIndex");
        }

        [Parser(Opcode.CMSG_RIDE_VEHICLE_INTERACT)]
        public static void HandleRideVehicleInteract(Packet packet)
        {
            packet.ReadPackedGuid128("Vehicle");
        }

        [Parser(Opcode.CMSG_EJECT_PASSENGER)]
        public static void HandleEjectPassenger(Packet packet)
        {
            packet.ReadPackedGuid128("Passenger");
        }

        [Parser(Opcode.CMSG_MOUNT_SPECIAL_ANIM)]
        public static void HandleMountSpecialAnim(Packet packet)
        {
            var count = packet.ReadUInt32();
            packet.ReadInt32("SequenceVariation");

            for (var i = 0; i < count; ++i)
                packet.ReadInt32("SpellVisualKitID", i);
        }

        [Parser(Opcode.CMSG_FAR_SIGHT)]
        public static void HandleFarSight(Packet packet)
        {
            packet.ReadBit("Apply");
        }

        [Parser(Opcode.CMSG_LOW_LEVEL_RAID2)]
        public static void HandleLowLevelRaidPackets(Packet packet)
        {
            packet.ReadBool("Allow");
        }

        [Parser(Opcode.CMSG_OVERRIDE_SCREEN_FLASH)]
        public static void HandleOverrideScreenFlash(Packet packet)
        {
            packet.ReadBit("CVar overrideScreenFlash");
        }

        [Parser(Opcode.CMSG_REPOP_REQUEST)]
        public static void HandleRepopRequest(Packet packet)
        {
            packet.ReadBool("Accept");
        }

        [Parser(Opcode.CMSG_SET_SELECTION)]
        public static void HandleSetSelection(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_CLEAR_RESURRECT)]
        [Parser(Opcode.SMSG_CLEAR_BOSS_EMOTES)]
        [Parser(Opcode.SMSG_FISH_NOT_HOOKED)]
        [Parser(Opcode.SMSG_FISH_ESCAPED)]
        [Parser(Opcode.SMSG_INVALID_PROMOTION_CODE)]
        [Parser(Opcode.CMSG_REQUEST_CEMETERY_LIST)]
        [Parser(Opcode.CMSG_USED_FOLLOW)]
        [Parser(Opcode.CMSG_GAME_EVENT_DEBUG_ENABLE)]
        [Parser(Opcode.CMSG_GAME_EVENT_DEBUG_DISABLE)]
        [Parser(Opcode.CMSG_REQUEST_VEHICLE_EXIT)]
        [Parser(Opcode.CMSG_REQUEST_VEHICLE_PREV_SEAT)]
        [Parser(Opcode.CMSG_REQUEST_VEHICLE_NEXT_SEAT)]
        [Parser(Opcode.CMSG_REQUEST_AREA_POI_UPDATE)]
        [Parser(Opcode.CMSG_REPORT_SERVER_LAG)]
        [Parser(Opcode.CMSG_SEAMLESS_TRANSFER_COMPLETE)]
        [Parser(Opcode.CMSG_QUERY_TIME)]
        [Parser(Opcode.CMSG_COMPLETE_MOVIE)]
        [Parser(Opcode.CMSG_CLIENT_PORT_GRAVEYARD)]
        [Parser(Opcode.CMSG_OPENING_CINEMATIC)]
        [Parser(Opcode.CMSG_NEXT_CINEMATIC_CAMERA)]
        [Parser(Opcode.CMSG_COMPLETE_CINEMATIC)]
        public static void HandleMiscZero(Packet packet)
        {
        }
    }
}
