using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
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

        [Parser(Opcode.SMSG_CLEAR_RESURRECT)]
        [Parser(Opcode.SMSG_CLEAR_BOSS_EMOTES)]
        public static void HandleMiscZero(Packet packet)
        {
        }
    }
}
