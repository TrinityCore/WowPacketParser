using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V12_0_0_65390.Parsers
{
    public static class MiscellaneoutHandler
    {
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

            packet.ReadUInt32("ClubsPresenceDelay");
            packet.ReadUInt32("ClubPresenceUnsubscribeDelay");

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
            packet.ReadBit("BNSendWhisperUseV2Services");
            packet.ReadBit("BNSendGameDataUseV2Services");
            packet.ReadBit("IsAccountCurrencyTransferEnabled");
            packet.ReadBit("NetEaseChatTelemetryEnabled");
            packet.ReadBit("LobbyMatchmakerQueueFromMainlineEnabled");

            packet.ReadBit("CanSendLobbyMatchmakerPartyCustomizations");
            packet.ReadBit("AddonProfilingEnabled");
            packet.ReadBit("GlobalUserGeneratedContentMuteEnabled");
            packet.ReadBit("AccountUserGeneratedContentIsRisky");

            {
                packet.ResetBitReader();
                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V11_1_7_61491))
                    packet.ReadBit("ToastsDisabled", "QuickJoinConfig");

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
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_1_7_61491))
                    packet.ReadBit("ToastsDisabled", "QuickJoinConfig");
            }

            if (hasSessionAlert)
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadClientSessionAlertConfig(packet, "SessionAlert");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_7_54577))
                packet.ReadWoWString("Unknown1027", unknown1027StrLen);

            V8_0_1_27101.Parsers.MiscellaneousHandler.ReadVoiceChatManagerSettings(packet, "VoiceChatManagerSettings");

            if (hasEuropaTicketSystemStatus)
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");
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
            var europaTicket = packet.ReadBit("IsEuropaTicketSystemStatusEnabled");
            packet.ReadBit("NameReservationOnly");
            var launchEta = packet.ReadBit();
            packet.ReadBit("TimerunningEnabled");
            packet.ReadBit("ScriptsDisallowedForBeta");
            packet.ReadBit("PlayerIdentityOptionsEnabled");

            packet.ReadBit("AccountExportEnabled");
            packet.ReadBit("AccountLockedPostExport");

            var realmHiddenAlertLen = packet.ReadBits(11);

            packet.ReadBit("BNSendWhisperUseV2Services");
            packet.ReadBit("BNSendGameDataUseV2Services");
            packet.ReadBit("CharacterSelectListModeRealmless");

            packet.ReadBit("WowTokenLimitedMode");
            packet.ReadBit("NavBarEnabled");
            packet.ReadBit("GlobalUserGeneratedContentMuteEnabled");
            packet.ReadBit("AccountUserGeneratedContentIsRisky");

            packet.ResetBitReader();

            if (europaTicket)
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
