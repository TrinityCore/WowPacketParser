using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V12_0_0_65390.Parsers
{
    public static class MiscellaneoutHandler
    {
        public static void ReadQuickJoinConfig(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();

            packet.ReadSingle("ToastDuration", indexes);
            packet.ReadSingle("DelayDuration", indexes);
            packet.ReadSingle("QueueMultiplier", indexes);
            packet.ReadSingle("PlayerMultiplier", indexes);
            packet.ReadSingle("PlayerFriendValue", indexes);
            packet.ReadSingle("PlayerGuildValue", indexes);
            packet.ReadSingle("ThrottleInitialThreshold", indexes);
            packet.ReadSingle("ThrottleDecayTime", indexes);
            packet.ReadSingle("ThrottlePrioritySpike", indexes);
            packet.ReadSingle("ThrottleMinThreshold", indexes);
            packet.ReadSingle("ThrottlePvPPriorityNormal", indexes);
            packet.ReadSingle("ThrottlePvPPriorityLow", indexes);
            packet.ReadSingle("ThrottlePvPHonorThreshold", indexes);
            packet.ReadSingle("ThrottleLfgListPriorityDefault", indexes);
            packet.ReadSingle("ThrottleLfgListPriorityAbove", indexes);
            packet.ReadSingle("ThrottleLfgListPriorityBelow", indexes);
            packet.ReadSingle("ThrottleLfgListIlvlScalingAbove", indexes);
            packet.ReadSingle("ThrottleLfgListIlvlScalingBelow", indexes);
            packet.ReadSingle("ThrottleRfPriorityAbove", indexes);
            packet.ReadSingle("ThrottleRfIlvlScalingAbove", indexes);
            packet.ReadSingle("ThrottleDfMaxItemLevel", indexes);
            packet.ReadSingle("ThrottleDfBestPriority", indexes);
            packet.ReadBit("ToastsDisabled", indexes);
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

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V12_1_0_69214))
                ReadQuickJoinConfig(packet, "QuickJoinConfig");

            packet.ReadInt64("RedeemForBalanceAmount");

            packet.ReadUInt32("ClubsPresenceDelay");
            packet.ReadUInt32("ClubPresenceUnsubscribeDelay");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V12_1_0_69214))
                V8_0_1_27101.Parsers.MiscellaneousHandler.ReadVoiceChatManagerSettings(packet, "Squelch");

            packet.ReadInt32("ContentSetID");
            var disabledGameModesCount = packet.ReadUInt32("DisabledGameModesCount");
            var gameRuleValuesCount = packet.ReadUInt32("GameRulesCount");
            packet.ReadInt32("ActiveTimerunningSeasonID");
            packet.ReadInt32("RemainingTimerunningSeasonSeconds");

            packet.ReadInt16("MaxPlayerGuidLookupsPerRequest");
            packet.ReadInt16("NameLookupTelemetryInterval");
            packet.ReadUInt32("NotFoundCacheTimeSeconds");

            packet.ReadUInt32("RealmPvpTypeOverride");

            packet.ReadInt32("AddonChatThrottle.MaxTries");
            packet.ReadInt32("AddonChatThrottle.TriesRestoredPerSecond");
            packet.ReadInt32("AddonChatThrottle.UsedTriesPerMessage");

            packet.ReadInt32("GuildChatThrottle.UsedTriesPerMessage");
            packet.ReadInt32("GuildChatThrottle.TriesRestoredPerSecond");
            packet.ReadInt32("GroupChatThrottle.UsedTriesPerMessage");
            packet.ReadInt32("GroupChatThrottle.TriesRestoredPerSecond");

            packet.ReadSingle("AddonPerformanceMsgWarning");
            packet.ReadSingle("AddonPerformanceMsgError");
            packet.ReadSingle("AddonPerformanceMsgOverall");

            for (var i = 0; i < disabledGameModesCount; ++i)
                V9_0_1_36216.Parsers.MiscellaneousHandler.ReadGameModeData(packet, "DisabledGameModes", i);

            for (var i = 0; i < gameRuleValuesCount; ++i)
                V9_0_1_36216.Parsers.MiscellaneousHandler.ReadGameRuleValuePair(packet, "GameRules");

            packet.ResetBitReader();
            packet.ReadBit("VoiceEnabled");
            var hasEuropaTicketSystemStatus = packet.ReadBit("HasEuropaTicketSystemStatus");
            packet.ReadBit("BpayStoreAvailable");
            packet.ReadBit("ItemRestorationButtonEnabled");
            var hasSessionAlert = packet.ReadBit("HasSessionAlert");
            packet.ReadBit("Enabled", "RAFSystem");
            packet.ReadBit("RecruitingEnabled", "RAFSystem");
            packet.ReadBit("CharUndeleteEnabled");

            packet.ReadBit("RestrictedAccount");
            packet.ReadBit("CommerceServerEnabled");
            packet.ReadBit("TutorialEnabled");
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
            packet.ReadBit("TimerunningEnabled");
            packet.ReadBit("PlayerIdentityOptionsEnabled");
            packet.ReadBit("IsPlayerContentTrackingEnabled");
            packet.ReadBit("IsLFDEnabled"); // classic only
            packet.ReadBit("IsLFREnabled"); // classic only
            packet.ReadBit("PetHappinessEnabled");
            packet.ReadBit("GuildEventsEditsEnabled");

            packet.ReadBit("GuildTradeSkillsEnabled");
            var unknown1027StrLen = packet.ReadBits(10);
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V12_0_7_68182))
            {
                packet.ReadBit("BNSendWhisperUseV2Services");
                packet.ReadBit("BNSendGameDataUseV2Services");
            }
            packet.ReadBit("IsAccountCurrencyTransferEnabled");
            packet.ReadBit("NetEaseChatTelemetryEnabled");
            packet.ReadBit("LobbyMatchmakerQueueFromMainlineEnabled");

            packet.ReadBit("CanSendLobbyMatchmakerPartyCustomizations");
            packet.ReadBit("AddonProfilingEnabled");
            packet.ReadBit("GlobalUserGeneratedContentMuteEnabled");
            packet.ReadBit("AccountUserGeneratedContentIsRisky");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V12_1_0_69214))
                packet.ReadBit("FriendsDisabled");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V12_1_0_69214))
                ReadQuickJoinConfig(packet, "QuickJoinConfig");

            if (hasEuropaTicketSystemStatus && ClientVersion.AddedInVersion(ClientVersionBuild.V12_1_0_69214))
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");

            if (hasSessionAlert)
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadClientSessionAlertConfig(packet, "SessionAlert");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_7_54577))
                packet.ReadWoWString("Unknown1027", unknown1027StrLen);

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V12_1_0_69214))
            {
                V8_0_1_27101.Parsers.MiscellaneousHandler.ReadVoiceChatManagerSettings(packet, "Squelch");

                if (hasEuropaTicketSystemStatus)
                    V6_0_2_19033.Parsers.MiscellaneousHandler.ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");
            }
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS_GLUE_SCREEN)]
        public static void HandleFeatureSystemStatusGlueScreen(Packet packet)
        {
            packet.ReadBit("BpayStoreAvailable");
            packet.ReadBit("CharUndeleteEnabled");
            packet.ReadBit("CommerceServerEnabled");
            packet.ReadBit("PaidCharacterTransfersBetweenBnetAccountsEnabled");
            packet.ReadBit("VeteranTokenRedeemWillKick");
            packet.ReadBit("WorldTokenRedeemWillKick");
            packet.ReadBit("ExpansionPreorderInStore");
            packet.ReadBit("KioskModeEnabled");

            packet.ReadBit("CompetitiveModeEnabled");
            packet.ReadBit("BoostEnabled");
            packet.ReadBit("TrialBoostEnabled");
            packet.ReadBit("RedeemForBalanceAvailable");
            packet.ReadBit("LiveRegionCharacterListEnabled");
            packet.ReadBit("LiveRegionCharacterCopyEnabled");
            packet.ReadBit("LiveRegionAccountCopyEnabled");
            packet.ReadBit("LiveRegionKeyBindingsCopyEnabled");

            packet.ReadBit("BrowserCrashReporterEnabled");
            packet.ReadBit("IsEmployeeAccount");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V12_0_7_68182))
                packet.ReadBit("UseBleep");

            var europaTicket = packet.ReadBit("IsEuropaTicketSystemStatusEnabled");
            packet.ReadBit("NameReservationOnly");
            var launchEta = packet.ReadBit();
            packet.ReadBit("TimerunningEnabled");
            packet.ReadBit("ScriptsDisallowedForBeta");
            packet.ReadBit("PlayerIdentityOptionsEnabled");

            packet.ReadBit("AccountExportEnabled");
            packet.ReadBit("AccountLockedPostExport");

            var realmHiddenAlertLen = packet.ReadBits(11);

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V12_0_7_68182))
            {
                packet.ReadBit("BNSendWhisperUseV2Services");
                packet.ReadBit("BNSendGameDataUseV2Services");
            }
            packet.ReadBit("CharacterSelectListModeRealmless");

            packet.ReadBit("WowTokenLimitedMode");
            packet.ReadBit("NavBarEnabled");
            packet.ReadBit("GlobalUserGeneratedContentMuteEnabled");
            packet.ReadBit("AccountUserGeneratedContentIsRisky");

            packet.ResetBitReader();

            if (europaTicket && ClientVersion.RemovedInVersion(ClientVersionBuild.V12_1_0_69214))
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");

            packet.ReadUInt32("CommercePricePollTimeSeconds");
            packet.ReadUInt32("KioskSessionDurationMinutes");
            packet.ReadInt64("RedeemForBalanceAmount");
            packet.ReadInt32("MaxCharactersOnThisRealm");
            var liveRegionCharacterCopySourceRegionsCount = packet.ReadUInt32("LiveRegionCharacterCopySourceRegionsCount");
            packet.ReadInt32("ActiveBoostType");
            packet.ReadInt32("TrialBoostType");
            packet.ReadInt32("MinimumExpansionLevel");
            packet.ReadInt32("MaximumExpansionLevel");
            packet.ReadInt32("ContentSetID");
            var disabledGameModesCount = packet.ReadUInt32("DisabledGameModesCount");
            var gameRuleValuesCount = packet.ReadUInt32("GameRuleValuesCount");
            var availableGameModesCount = packet.ReadUInt32("AvailableGameModeIDCount");
            packet.ReadInt32("ActiveTimerunningSeasonID");
            packet.ReadInt32("RemainingTimerunningSeasonSeconds");
            packet.ReadInt32("TimerunningConversionMinCharacterAge");
            packet.ReadInt32("TimerunningConversionMaxSeasonID");
            packet.ReadInt16("MaxPlayerGuidLookupsPerRequest");
            packet.ReadInt16("NameLookupTelemetryInterval");
            packet.ReadUInt32("NotFoundCacheTimeSeconds");
            var debugTimeEventCount = packet.ReadUInt32("DebugTimeEventCount");
            packet.ReadInt32("MostRecentTimeEventID");
            packet.ReadUInt32("EventRealmQueues");

            if (europaTicket && ClientVersion.AddedInVersion(ClientVersionBuild.V12_1_0_69214))
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");

            if (launchEta)
                packet.ReadInt32("LaunchETA");

            packet.ReadDynamicString("RealmHiddenAlert", realmHiddenAlertLen);

            for (var i = 0; i < liveRegionCharacterCopySourceRegionsCount; i++)
                packet.ReadUInt32("LiveRegionCharacterCopySourceRegion", i);

            for (var i = 0; i < disabledGameModesCount; ++i)
                V9_0_1_36216.Parsers.MiscellaneousHandler.ReadGameModeData(packet, "DisabledGameModes", i);

            for (var i = 0; i < gameRuleValuesCount; ++i)
                V9_0_1_36216.Parsers.MiscellaneousHandler.ReadGameRuleValuePair(packet, "GameRules", i);

            for (var i = 0; i < availableGameModesCount; ++i)
                packet.ReadInt32("AvailableGameModeID", i);

            for (var i = 0; i < debugTimeEventCount; ++i)
                V9_0_1_36216.Parsers.MiscellaneousHandler.ReadDebugTimeInfo(packet, "DebugTimeEvent", i);
        }
    }
}
