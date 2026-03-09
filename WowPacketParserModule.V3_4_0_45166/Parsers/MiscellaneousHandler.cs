using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class MiscellaneousHandler
    {
        public static void ReadGameRuleValuePair(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("Rule", indexes);
            packet.ReadInt32("Value", indexes);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_4_59817))
                packet.ReadSingle("ValueF", indexes);
        }

        public static void ReadDebugTimeInfo(Packet packet, params object[] indexes)
        {
            packet.ReadUInt32("TimeEvent", indexes);
            packet.ResetBitReader();
            var textLen = packet.ReadBits(7);
            packet.ReadWoWString("Text", textLen);
        }

        public static void ReadAreaPoiData(Packet packet, params object[] idx)
        {
            packet.ReadTime64("StartTime", idx);
            packet.ReadInt32("AreaPoiID", idx);
            packet.ReadInt32("DurationSec", idx);
            packet.ReadUInt32("WorldStateVariableID", idx);
            packet.ReadUInt32("WorldStateValue", idx);
        }

        public static void ReadUIEventToast(Packet packet, params object[] args)
        {
            packet.ReadInt32("UiEventToastID", args);
            packet.ReadInt32("Asset", args);
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

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS_GLUE_SCREEN, ClientVersionBuild.V3_4_0_44832, ClientVersionBuild.V3_4_3_51505)]
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
            packet.ReadBit("TrialBoostEnabled");
            packet.ReadBit("TokenBalanceEnabled");
            packet.ReadBit("LiveRegionCharacterListEnabled");
            packet.ReadBit("LiveRegionCharacterCopyEnabled");
            packet.ReadBit("LiveRegionAccountCopyEnabled");
            packet.ReadBit("LiveRegionKeyBindingsCopyEnabled");

            packet.ReadBit("UnusedBit1_340");
            packet.ReadBit("UnusedBit2_340");
            var europaTicket = packet.ReadBit("IsEuropaTicketSystemStatusEnabled");
            packet.ReadBit("Unknown901CheckoutRelated");
            bool launchETA = packet.ReadBit("IsLaunchETA");
            packet.ReadBit("TBCInfoPaneEnabled");
            packet.ReadBit("TBCInfoPanePriceEnabled");
            packet.ReadBit("TBCTransitionUIEnabled");

            packet.ReadBit("SoMNotificationEnabled");
            packet.ResetBitReader();

            if (europaTicket)
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");

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
            packet.ReadInt32("GameRuleUnknown1");
            var gameRuleValuesCount = packet.ReadUInt32("GameRuleValuesCount");
            packet.ReadInt16("MaxPlayerNameQueriesPerPacket");
            packet.ReadInt16("PlayerNameQueryTelemetryInterval");

            if (launchETA)
                packet.ReadPackedTime("LaunchETA");

            for (int i = 0; i < liveRegionCharacterCopySourceRegionsCount; i++)
                packet.ReadUInt32("LiveRegionCharacterCopySourceRegion", i);

            for (var i = 0; i < gameRuleValuesCount; ++i)
                ReadGameRuleValuePair(packet, "GameRuleValues");
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS_GLUE_SCREEN, ClientVersionBuild.V3_4_3_51505, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleFeatureSystemStatusGlueScreen343(Packet packet)
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
            packet.ReadBit("LiveRegionCharacterListEnabled");
            packet.ReadBit("LiveRegionCharacterCopyEnabled");
            packet.ReadBit("LiveRegionAccountCopyEnabled");

            packet.ReadBit("LiveRegionKeyBindingsCopyEnabled");
            packet.ReadBit("Unknown901CheckoutRelated");
            packet.ReadBit("SoftTargetEnabled");
            var europaTicket = packet.ReadBit("IsEuropaTicketSystemStatusEnabled");
            packet.ReadBit("IsNameReservationEnabled");
            bool launchETA = packet.ReadBit("IsLaunchETA");
            packet.ReadBit("AddonsDisabled");
            packet.ReadBit("Unk");

            packet.ReadBit("Unk");
            packet.ReadBit("SoMNotificationEnabled");
            packet.ReadBit("AccountSaveDataExportEnabled");
            packet.ReadBit("AccountLockedByExport");
            packet.ReadBit("Unk");
            var realmHiddenAlert = packet.ReadBit("IsRealmHiddenAlert");
            if (realmHiddenAlert)
                packet.ReadBits("RealmHiddenAlert", 11);

            packet.ResetBitReader();

            if (europaTicket)
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");

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
            packet.ReadInt16("MaxPlayerNameQueriesPerPacket");
            packet.ReadInt16("PlayerNameQueryTelemetryInterval");

            packet.ReadInt32("PlayerNameQueryInterval");
            var debugTimeEventsCount = packet.ReadInt32("DebugTimeEventsSize");
            packet.ReadInt32("Unused1007");

            if (launchETA)
                packet.ReadPackedTime("LaunchETA");

            for (int i = 0; i < liveRegionCharacterCopySourceRegionsCount; i++)
                packet.ReadUInt32("LiveRegionCharacterCopySourceRegion", i);

            for (var i = 0; i < gameRuleValuesCount; ++i)
                ReadGameRuleValuePair(packet, "GameRuleValues");

            for (int i = 0; i < debugTimeEventsCount; i++)
            {
                packet.ReadUInt32("TimeEvent", i);
                var textlength = packet.ReadBits("TextLength", 7, i);
                packet.ResetBitReader();

                packet.ReadWoWString("Text", textlength, i);
            }
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS_GLUE_SCREEN, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleFeatureSystemStatusGlueScreen344(Packet packet)
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
            packet.ReadBit("AccountSaveDataExportEnabled");
            packet.ReadBit("AccountLockedByExport");

            uint realmHiddenAlertLen = packet.ReadBits(11);

            packet.ReadBit("BNSendWhisperUseV2Services");
            packet.ReadBit("BNSendGameDataUseV2Services");
            packet.ReadBit("CharacterSelectListModeRealmless");

            packet.ResetBitReader();

            if (europaTicket)
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");

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

        [Parser(Opcode.SMSG_TRIGGER_CINEMATIC)]
        [Parser(Opcode.SMSG_TRIGGER_MOVIE)]
        public static void HandleTriggerSequence(Packet packet)
        {
            packet.ReadInt32("CinematicID");
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS, ClientVersionBuild.V3_4_0_44832, ClientVersionBuild.V3_4_3_51505)]
        public static void HandleFeatureSystemStatus(Packet packet)
        {
            packet.ReadByte("ComplaintStatus");

            packet.ReadUInt32("ScrollOfResurrectionRequestsRemaining");
            packet.ReadUInt32("ScrollOfResurrectionMaxRequestsPerDay");
            packet.ReadUInt32("CfgRealmID");
            packet.ReadInt32("CfgRealmRecID");
            packet.ReadUInt32("MaxRecruits", "RAFSystem");
            packet.ReadUInt32("MaxRecruitMonths", "RAFSystem");
            packet.ReadUInt32("MaxRecruitmentUses", "RAFSystem");
            packet.ReadUInt32("DaysInCycle", "RAFSystem");
            packet.ReadUInt32("TwitterPostThrottleLimit");
            packet.ReadUInt32("TwitterPostThrottleCooldown");
            packet.ReadUInt32("TokenPollTimeSeconds");
            packet.ReadUInt32("KioskSessionMinutes");
            packet.ReadInt64("TokenBalanceAmount");
            packet.ReadUInt32("BpayStoreProductDeliveryDelay");
            packet.ReadUInt32("ClubsPresenceUpdateTimer");
            packet.ReadUInt32("HiddenUIClubsPresenceUpdateTimer");

            packet.ReadInt32("ActiveSeason");
            var gameRuleValuesCount = packet.ReadUInt32("GameRuleValuesCount");
            packet.ReadInt16("MaxPlayerNameQueriesPerPacket");
            packet.ReadInt16("PlayerNameQueryTelemetryInterval");

            for (var i = 0; i < gameRuleValuesCount; ++i)
                ReadGameRuleValuePair(packet, "GameRuleValues");

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
            packet.ReadBit("Enabled", "RAFSystem");
            packet.ReadBit("RecruitingEnabled", "RAFSystem");
            packet.ReadBit("CharUndeleteEnabled");
            packet.ReadBit("RestrictedAccount");
            packet.ReadBit("CommerceSystemEnabled");
            packet.ReadBit("TutorialsEnabled");
            packet.ReadBit("TwitterEnabled");

            packet.ReadBit("Unk67");
            packet.ReadBit("WillKickFromWorld");
            packet.ReadBit("KioskModeEnabled");
            packet.ReadBit("CompetitiveModeEnabled");
            packet.ReadBit("TokenBalanceEnabled");
            packet.ReadBit("WarModeFeatureEnabled");
            packet.ReadBit("ClubsEnabled");
            packet.ReadBit("ClubsBattleNetClubTypeAllowed");

            packet.ReadBit("ClubsCharacterClubTypeAllowed");
            packet.ReadBit("ClubsPresenceUpdateEnabled");
            packet.ReadBit("VoiceChatDisabledByParentalControl");
            packet.ReadBit("VoiceChatMutedByParentalControl");
            packet.ReadBit("QuestSessionEnabled");
            packet.ReadBit("IsMuted");
            packet.ReadBit("ClubFinderEnabled");
            packet.ReadBit("Unknown901CheckoutRelated");

            packet.ReadBit("TextToSpeechFeatureEnabled");
            packet.ReadBit("ChatDisabledByDefault");
            packet.ReadBit("ChatDisabledByPlayer");
            packet.ReadBit("LFGListCustomRequiresAuthenticator");
            packet.ReadBit("BattlegroundsEnabled");
            var unknown = packet.ReadBit("Unk340");

            {
                packet.ResetBitReader();
                packet.ReadBit("ToastsDisabled");
                packet.ReadSingle("ToastDuration");
                packet.ReadSingle("DelayDuration");
                packet.ReadSingle("QueueMultiplier");
                packet.ReadSingle("PlayerMultiplier");
                packet.ReadSingle("PlayerFriendValue");
                packet.ReadSingle("PlayerGuildValue");
                packet.ReadSingle("ThrottleInitialThreshold");
                packet.ReadSingle("ThrottleDecayTime");
                packet.ReadSingle("ThrottlePrioritySpike");
                packet.ReadSingle("ThrottleMinThreshold");
                packet.ReadSingle("ThrottlePvPPriorityNormal");
                packet.ReadSingle("ThrottlePvPPriorityLow");
                packet.ReadSingle("ThrottlePvPHonorThreshold");
                packet.ReadSingle("ThrottleLfgListPriorityDefault");
                packet.ReadSingle("ThrottleLfgListPriorityAbove");
                packet.ReadSingle("ThrottleLfgListPriorityBelow");
                packet.ReadSingle("ThrottleLfgListIlvlScalingAbove");
                packet.ReadSingle("ThrottleLfgListIlvlScalingBelow");
                packet.ReadSingle("ThrottleRfPriorityAbove");
                packet.ReadSingle("ThrottleRfIlvlScalingAbove");
                packet.ReadSingle("ThrottleDfMaxItemLevel");
                packet.ReadSingle("ThrottleDfBestPriority");
            }

            if (hasSessionAlert)
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadClientSessionAlertConfig(packet, "SessionAlert");

            if (unknown)
            {
                var count = packet.ReadUInt32("Count", "Unk340");
                for (int i = 0; i < count; i++)
                    packet.ReadByte("UnknownByte", "Unk340", i);
            }

            V8_0_1_27101.Parsers.MiscellaneousHandler.ReadVoiceChatManagerSettings(packet, "VoiceChatManagerSettings");

            if (hasEuropaTicketSystemStatus)
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS, ClientVersionBuild.V3_4_3_51505, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleFeatureSystemStatus343(Packet packet)
        {
            packet.ReadByte("ComplaintStatus");
            packet.ReadUInt32("CfgRealmID");
            packet.ReadInt32("CfgRealmRecID");

            packet.ReadUInt32("MaxRecruits", "RAFSystem");
            packet.ReadUInt32("MaxRecruitMonths", "RAFSystem");
            packet.ReadUInt32("MaxRecruitmentUses", "RAFSystem");
            packet.ReadUInt32("DaysInCycle", "RAFSystem");
            packet.ReadUInt32("Unknown1007", "RAFSystem");
            packet.ReadUInt32("TokenPollTimeSeconds");
            packet.ReadUInt32("KioskSessionMinutes");
            packet.ReadInt64("TokenBalanceAmount");
            packet.ReadUInt32("BpayStoreProductDeliveryDelay");
            packet.ReadUInt32("ClubsPresenceUpdateTimer");
            packet.ReadUInt32("HiddenUIClubsPresenceUpdateTimer");
            packet.ReadInt32("ActiveSeason");

            var gameRuleValuesCount = packet.ReadUInt32("GameRuleValuesCount");

            packet.ReadInt16("MaxPlayerNameQueriesPerPacket");
            packet.ReadInt16("PlayerNameQueryTelemetryInterval");
            packet.ReadUInt32("PlayerNameQueryInterval");

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
            packet.ReadBit("CommerceSystemEnabled");
            packet.ReadBit("TutorialsEnabled");
            packet.ReadBit("Unk67");
            packet.ReadBit("WillKickFromWorld");

            packet.ReadBit("KioskModeEnabled");
            packet.ReadBit("CompetitiveModeEnabled");
            packet.ReadBit("TokenBalanceEnabled");
            packet.ReadBit("WarModeFeatureEnabled");
            packet.ReadBit("ClubsEnabled");
            packet.ReadBit("ClubsBattleNetClubTypeAllowed");
            packet.ReadBit("ClubsCharacterClubTypeAllowed");
            packet.ReadBit("ClubsPresenceUpdateEnabled");

            packet.ReadBit("VoiceChatDisabledByParentalControl");
            packet.ReadBit("VoiceChatMutedByParentalControl");
            packet.ReadBit("QuestSessionEnabled");
            packet.ReadBit("IsMuted");
            packet.ReadBit("ClubFinderEnabled");
            packet.ReadBit("Unknown901CheckoutRelated");
            packet.ReadBit("TextToSpeechFeatureEnabled");
            packet.ReadBit("ChatDisabledByDefault");

            packet.ReadBit("ChatDisabledByPlayer");
            packet.ReadBit("LFGListCustomRequiresAuthenticator");
            packet.ReadBit("AddonsDisabled");
            packet.ReadBit("WarGamesEnabled");
            var unk = packet.ReadBit("Unk343_1");
            packet.ReadBit("Unk343_2");
            packet.ReadBit("ContentTrackingEnabled");
            packet.ReadBit("IsSellAllJunkEnabled");

            packet.ReadBit("IsGroupFinderEnabled");
            packet.ReadBit("IsLFDEnabled");
            packet.ReadBit("IsLFREnabled");
            packet.ReadBit("IsPremadeGroupEnabled");
            {
                packet.ResetBitReader();
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
            }

            if (hasSessionAlert)
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadClientSessionAlertConfig(packet, "SessionAlert");

            if (unk)
            {
                var count = packet.ReadUInt32("UnkCount");

                for (var i = 0; i < count; ++i)
                    packet.ReadByte("UnkByte", i);
            }

            V8_0_1_27101.Parsers.MiscellaneousHandler.ReadVoiceChatManagerSettings(packet, "VoiceChatManagerSettings");

            if (hasEuropaTicketSystemStatus)
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleFeatureSystemStatus344(Packet packet)
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
            packet.ReadBit("UseActivePlayerDataQuestCompleted");
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

            {
                packet.ResetBitReader();
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
            }

            if (hasSessionAlert)
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadClientSessionAlertConfig(packet, "SessionAlert");

            if (hasRaceClassExpansionLevels)
            {
                var count = packet.ReadUInt32();

                for (var i = 0; i < count; ++i)
                    packet.ReadByte("RaceClassExpansionLevels", i);
            }

            packet.ReadWoWString("Unknown1027", unknown1027StrLen);

            V8_0_1_27101.Parsers.MiscellaneousHandler.ReadVoiceChatManagerSettings(packet, "VoiceChatManagerSettings");

            if (hasEuropaTicketSystemStatus)
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");
        }

        [Parser(Opcode.SMSG_WORLD_SERVER_INFO, ClientVersionBuild.V3_4_3_51505)]
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

        [Parser(Opcode.SMSG_ACCOUNT_HEIRLOOM_UPDATE)]
        public static void HandleAccountHeirloomUpdate(Packet packet)
        {
            packet.ReadBit("IsFullUpdate");

            packet.ReadInt32("Unk");

            uint itemCount = packet.ReadUInt32("ItemCount");
            uint flagCount = packet.ReadUInt32("FlagsCount");

            for (uint i = 0u; i < itemCount; i++)
                packet.ReadInt32<ItemId>("ItemID", i);

            for (uint i = 0u; i < flagCount; i++)
                packet.ReadUInt32("Flags", i);
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

        [Parser(Opcode.SMSG_ALLIED_RACE_DETAILS, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAlliedRaceDetails(Packet packet)
        {
            packet.ReadPackedGuid128("GUID"); // Creature or GameObject
            packet.ReadByteE<Race>("RaceID");
        }

        [Parser(Opcode.SMSG_AREA_POI_UPDATE_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAreaPOIUpdateResponse(Packet packet)
        {
            var count = packet.ReadInt32("Count");

            for (var i = 0; i < count; i++)
                ReadAreaPoiData(packet, i);
        }

        [Parser(Opcode.SMSG_CACHE_VERSION, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleClientCacheVersion(Packet packet)
        {
            packet.ReadInt32("Version");
        }

        [Parser(Opcode.SMSG_INVALIDATE_PLAYER, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleReadGuid(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_TUTORIAL_FLAGS, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleTutorialFlags(Packet packet)
        {
            for (var i = 0; i < 32; i++)
                packet.ReadByte("TutorialData", i);
        }

        [Parser(Opcode.SMSG_DISPLAY_PROMOTION, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleDisplayPromotion(Packet packet)
        {
            packet.ReadUInt32("PromotionID");
        }

        [Parser(Opcode.SMSG_SOCIAL_CONTRACT_REQUEST_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSocialContractRequestResponse(Packet packet)
        {
            packet.ReadBit("ShowSocialContract");
        }

        [Parser(Opcode.CMSG_PING, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleClientPing(Packet packet)
        {
            packet.ReadInt32("Serial");
            packet.ReadInt32("Latency");
        }

        [Parser(Opcode.SMSG_PONG, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleServerPong(Packet packet)
        {
            packet.ReadInt32("Serial");
        }

        [Parser(Opcode.CMSG_OVERRIDE_SCREEN_FLASH, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleOverrideScreenFlash(Packet packet)
        {
            packet.ReadBit("CVar overrideScreenFlash");
        }

        [Parser(Opcode.CMSG_VIOLENCE_LEVEL, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSetViolenceLevel(Packet packet)
        {
            packet.ReadByte("Level");
        }

        [Parser(Opcode.SMSG_EXPLORATION_EXPERIENCE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleExplorationExperience(Packet packet)
        {
            packet.ReadUInt32<AreaId>("AreaID");
            packet.ReadUInt32("Experience");
        }

        [Parser(Opcode.SMSG_TIME_SYNC_REQUEST, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleTimeSyncReq(Packet packet)
        {
            packet.ReadInt32("Count");
        }

        [Parser(Opcode.SMSG_WEATHER, ClientVersionBuild.V3_4_4_59817)]
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

        [Parser(Opcode.SMSG_START_LIGHTNING_STORM, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_END_LIGHTNING_STORM, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleLightningStorm(Packet packet)
        {
            packet.ReadUInt32("LightningStormId");
        }

        [Parser(Opcode.SMSG_INITIAL_SETUP, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleInitialSetup(Packet packet)
        {
            packet.ReadByte("ServerExpansionLevel");
            packet.ReadByte("ServerExpansionTier");
        }

        [HasSniffData]
        [Parser(Opcode.CMSG_LOADING_SCREEN_NOTIFY, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleClientEnterWorld(Packet packet)
        {
            var mapId = packet.ReadInt32<MapId>("MapID");
            packet.ReadBit("Showing");

            packet.AddSniffData(StoreNameType.Map, mapId, "LOAD_SCREEN");
        }

        [Parser(Opcode.CMSG_TIME_SYNC_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleTimeSyncResp(Packet packet)
        {
            packet.ReadUInt32("Counter");
            packet.ReadUInt32("Ticks");
        }

        [Parser(Opcode.CMSG_QUERY_COUNTDOWN_TIMER, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleQueryCountdownTimer(Packet packet)
        {
            packet.ReadInt32("TimerType");
        }

        [Parser(Opcode.SMSG_REQUEST_CEMETERY_LIST_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleRequestCemeteryListResponse(Packet packet)
        {
            packet.ReadBit("IsTriggered");

            var count = packet.ReadUInt32("Count");
            for (int i = 0; i < count; ++i)
                packet.ReadInt32("CemeteryID", i);
        }

        [Parser(Opcode.SMSG_PLAY_OBJECT_SOUND, ClientVersionBuild.V3_4_4_59817)]
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

        [Parser(Opcode.SMSG_ZONE_UNDER_ATTACK, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleZoneUpdate(Packet packet)
        {
            packet.ReadInt32<AreaId>("AreaID");
        }

        [Parser(Opcode.CMSG_SET_SELECTION, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSetSelection(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_CUSTOM_LOAD_SCREEN, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleCustomLoadScreen(Packet packet)
        {
            packet.ReadUInt32("TeleportSpellID");
            packet.ReadUInt32("LoadingScreenID");
        }

        [Parser(Opcode.SMSG_DEATH_RELEASE_LOC, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleDeathReleaseLoc(Packet packet)
        {
            packet.ReadInt32<MapId>("Map Id");
            packet.ReadVector3("Position");
        }

        [Parser(Opcode.SMSG_DISPLAY_GAME_ERROR, ClientVersionBuild.V3_4_4_59817)]
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

        [Parser(Opcode.SMSG_DISPLAY_TOAST, ClientVersionBuild.V3_4_4_59817)]
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

        [Parser(Opcode.SMSG_DURABILITY_DAMAGE_DEATH, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleDurabilityDamageDeath(Packet packet)
        {
            packet.ReadInt32("Percent");
        }

        [Parser(Opcode.SMSG_ENABLE_BARBER_SHOP, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleEnableBarberShop(Packet packet)
        {
            packet.ReadByte("CustomizationScope");
        }

        [Parser(Opcode.SMSG_OVERRIDE_LIGHT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleOverrideLight(Packet packet)
        {
            packet.ReadUInt32("AreaLightID");
            packet.ReadUInt32("OverrideLightID");
            packet.ReadUInt32("TransitionMilliseconds");
        }

        [Parser(Opcode.SMSG_PAUSE_MIRROR_TIMER, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePauseMirrorTimer(Packet packet)
        {
            packet.ReadByteE<MirrorTimerType>("Timer");
            packet.ReadBit("Paused");
        }

        [Parser(Opcode.SMSG_PLAY_MUSIC, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePlayMusic(Packet packet)
        {
            PacketPlayMusic packetMusic = packet.Holder.PlayMusic = new PacketPlayMusic();
            uint sound = packetMusic.Music = packet.ReadUInt32<SoundId>("SoundKitID");

            Storage.Sounds.Add(sound, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_PLAY_ONE_SHOT_ANIM_KIT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePlayOneShotAnimKit(Packet packet)
        {
            var animKit = packet.Holder.OneShotAnimKit = new();
            animKit.Unit = packet.ReadPackedGuid128("Unit");
            animKit.AnimKit = packet.ReadUInt16("AnimKitID");
        }

        [Parser(Opcode.SMSG_PLAY_SOUND, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePlaySound(Packet packet)
        {
            PacketPlaySound packetPlaySound = packet.Holder.PlaySound = new PacketPlaySound();
            var sound = packetPlaySound.Sound = (uint)packet.ReadInt32<SoundId>("SoundKitID");
            packetPlaySound.Source = packet.ReadPackedGuid128("SourceObjectGUID").ToUniversalGuid();
            packetPlaySound.BroadcastTextId = (uint)packet.ReadInt32("BroadcastTextID");

            Storage.Sounds.Add(sound, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_PLAY_SPEAKERBOT_SOUND, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePlaySpeakerbotSound(Packet packet)
        {
            packet.ReadPackedGuid128("SourceObjectGUID");
            packet.ReadUInt32("SoundId");
        }

        [Parser(Opcode.SMSG_PRE_RESSURECT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePreRessurect(Packet packet)
        {
            packet.ReadPackedGuid128("PlayerGUID");
        }

        [Parser(Opcode.SMSG_RANDOM_ROLL, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleRandomRollResult(Packet packet)
        {
            packet.ReadPackedGuid128("Roller");
            packet.ReadPackedGuid128("RollerWowAccount");
            packet.ReadInt32("Min");
            packet.ReadInt32("Max");
            packet.ReadInt32("Result");
        }

        [Parser(Opcode.SMSG_RECRUIT_A_FRIEND_FAILURE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleRaFFailure(Packet packet)
        {
            packet.ReadInt32("Reason");
            packet.ResetBitReader();
            var len = packet.ReadBits(6);
            packet.ReadWoWString("Str", len);
        }

        [Parser(Opcode.SMSG_REFER_A_FRIEND_EXPIRED, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleReferAFriendExpired(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_SET_AI_ANIM_KIT, ClientVersionBuild.V3_4_4_59817)]
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

        [Parser(Opcode.SMSG_SET_ANIM_TIER, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSetAnimTier(Packet packet)
        {
            packet.ReadPackedGuid128("Unit");
            packet.ReadBits("Tier", 3);
        }

        [Parser(Opcode.SMSG_SET_CURRENCY, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSetCurrency(Packet packet)
        {
            packet.ReadInt32("Type");
            packet.ReadInt32("Quantity");
            packet.ReadUInt32("Flags");
            uint toastCount = packet.ReadUInt32("UiEventToastCount");
            for (var i = 0; i < toastCount; i++)
                ReadUIEventToast(packet, "UiEventToast", i);

            var hasWeeklyQuantity = packet.ReadBit("HasWeeklyQuantity");
            var hasTrackedQuantity = packet.ReadBit("HasTrackedQuantity");
            var hasMaxQuantity = packet.ReadBit("HasMaxQuantity");
            var hasTotalEarned = packet.ReadBit("HasTotalEarned");
            packet.ReadBit("SuppressChatLog");
            var hasQuantityChange = packet.ReadBit("HasQuantityChange");
            var hasQuantityGainSource = packet.ReadBit("HasQuantityGainSource");
            var hasQuantityLostSource = packet.ReadBit("HasQuantityLostSource");
            var hasFirstCraftOperationID = packet.ReadBit("HasFirstCraftOperationID");
            var hasHasNextRechargeTime = packet.ReadBit("HasNextRechargeTime");
            var hasRechargeCycleStartTime = packet.ReadBit("HasRechargeCycleStartTime");
            var hasOverflownCurrencyID = packet.ReadBit("HasOverflownCurrencyID");

            if (hasWeeklyQuantity)
                packet.ReadInt32("WeeklyQuantity");

            if (hasTrackedQuantity)
                packet.ReadInt32("TrackedQuantity");

            if (hasMaxQuantity)
                packet.ReadInt32("MaxQuantity");

            if (hasTotalEarned)
                packet.ReadInt32("TotalEarned");

            if (hasQuantityChange)
                packet.ReadInt32("QuantityChange");

            if (hasQuantityGainSource)
                packet.ReadInt32("QuantityGainSource");

            if (hasQuantityLostSource)
                packet.ReadInt32("QuantityLostSource");

            if (hasFirstCraftOperationID)
                packet.ReadUInt32("FirstCraftOperationID");

            if (hasHasNextRechargeTime)
                packet.ReadTime64("NextRechargeTime");

            if (hasRechargeCycleStartTime)
                packet.ReadTime64("RechargeCycleStartTime");

            if (hasOverflownCurrencyID)
                packet.ReadUInt32("OverflownCurrencyID");
        }

        [Parser(Opcode.SMSG_SET_MELEE_ANIM_KIT, ClientVersionBuild.V3_4_4_59817)]
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

        [Parser(Opcode.SMSG_SET_MOVEMENT_ANIM_KIT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSetMovementAnimKit(Packet packet)
        {
            packet.ReadPackedGuid128("Unit");
            packet.ReadUInt16("AnimKitID");
        }

        [Parser(Opcode.SMSG_SET_PLAY_HOVER_ANIM, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePlayHoverAnim(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            packet.ReadBit("PlayHoverAnim");
        }

        [Parser(Opcode.SMSG_SET_VEHICLE_REC_ID, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSetVehicleRecID(Packet packet)
        {
            packet.ReadPackedGuid128("VehicleGUID");
            packet.ReadInt32("VehicleRecID");
        }

        [Parser(Opcode.SMSG_SPECIAL_MOUNT_ANIM, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSpecialMountAnim(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            var spellVisualKitIdCount = packet.ReadUInt32("SpellVisualKitIdCount");
            packet.ReadInt32("SequenceVariation");

            for (var i = 0; i < spellVisualKitIdCount; i++)
                packet.ReadUInt32("SpellVisualKitID", i);
        }

        [Parser(Opcode.SMSG_START_MIRROR_TIMER, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleStartMirrorTimer(Packet packet)
        {
            packet.ReadByteE<MirrorTimerType>("Timer");
            packet.ReadInt32("Value");
            packet.ReadInt32("MaxValue");
            packet.ReadInt32("Scale");
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadBit("Paused");
        }

        [Parser(Opcode.SMSG_START_TIMER, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleStartTimer(Packet packet)
        {
            packet.ReadInt64("TotalTime");
            packet.ReadUInt32E<TimerType>("Type");
            packet.ReadInt64("TimeLeft");

            var hasPlayerGUID = packet.ReadBit("HasPlayerGUID");
            if (hasPlayerGUID)
                packet.ReadPackedGuid128("PlayerGUID");
        }

        [Parser(Opcode.SMSG_STOP_MIRROR_TIMER, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleStopMirrorTimer(Packet packet)
        {
            packet.ReadByteE<MirrorTimerType>("Timer");
        }

        [Parser(Opcode.SMSG_STOP_SPEAKERBOT_SOUND, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleStopSpeakerbotSound(Packet packet)
        {
            packet.ReadPackedGuid128("SourceObjectGUID");
        }

        [Parser(Opcode.SMSG_SUMMON_REQUEST, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSummonRequest(Packet packet)
        {
            packet.ReadPackedGuid128("SummonerGUID");
            packet.ReadUInt32("SummonerVirtualRealmAddress");
            packet.ReadInt32<AreaId>("AreaID");
            packet.ReadByte("Reason");
            packet.ResetBitReader();
            packet.ReadBit("SkipStartingArea");
        }

        [Parser(Opcode.SMSG_WHO, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleWho(Packet packet)
        {
            packet.ReadUInt32("RequestID");
            var entriesCount = packet.ReadBits("EntriesCount", 6);

            for (var i = 0; i < entriesCount; ++i)
                ReadWhoEntry(packet, i);
        }

        [Parser(Opcode.SMSG_WHO_IS, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleWhoIsResponse(Packet packet)
        {
            var accNameLen = packet.ReadBits(11);
            packet.ReadWoWString("AccountName", accNameLen);
        }

        [Parser(Opcode.CMSG_BUG_REPORT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBugReport(Packet packet)
        {
            packet.ReadBit("Type");

            var len1 = packet.ReadBits(12);
            var len2 = packet.ReadBits(10);

            packet.ReadWoWString("DiagInfo", len1);
            packet.ReadWoWString("Text", len2);
        }

        [Parser(Opcode.CMSG_COLLECTION_ITEM_SET_FAVORITE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleCollectionItemSetFavorite(Packet packet)
        {
            packet.ReadInt32E<CollectionType>("CollectionType");
            packet.ReadUInt32("ID");
            packet.ResetBitReader();
            packet.ReadBit("IsFavorite");
        }

        [Parser(Opcode.CMSG_EJECT_PASSENGER, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleEjectPassenger(Packet packet)
        {
            packet.ReadPackedGuid128("Passenger");
        }

        [Parser(Opcode.CMSG_FAR_SIGHT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleFarSight(Packet packet)
        {
            packet.ReadBit("Apply");
        }

        [Parser(Opcode.CMSG_MOUNT_SPECIAL_ANIM, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleMountSpecialAnim(Packet packet)
        {
            var count = packet.ReadUInt32();
            packet.ReadInt32("SequenceVariation");

            for (var i = 0; i < count; ++i)
                packet.ReadInt32("SpellVisualKitID", i);
        }

        [Parser(Opcode.CMSG_QUERY_PAGE_TEXT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePageTextQuery(Packet packet)
        {
            packet.ReadUInt32("Entry");
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.CMSG_RANDOM_ROLL, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleRandomRoll(Packet packet)
        {
            var hasPartyIndex = packet.ReadBit("HasPartyIndex");
            packet.ReadInt32("Min");
            packet.ReadInt32("Max");

            if (hasPartyIndex)
                packet.ReadByte("PartyIndex");
        }

        [Parser(Opcode.CMSG_REPOP_REQUEST, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleRepopRequest(Packet packet)
        {
            packet.ReadBool("CheckInstance");
        }

        [Parser(Opcode.CMSG_REQUEST_VEHICLE_SWITCH_SEAT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleRequestVehicleSwitchSeat(Packet packet)
        {
            packet.ReadPackedGuid128("Vehicle");
            packet.ReadByte("SeatIndex");
        }

        [Parser(Opcode.CMSG_RESURRECT_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleResurrectResponse(Packet packet)
        {
            packet.ReadPackedGuid128("Resurrecter");
            packet.ReadUInt32("Response");
        }

        [Parser(Opcode.CMSG_RIDE_VEHICLE_INTERACT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleRideVehicleInteract(Packet packet)
        {
            packet.ReadPackedGuid128("Vehicle");
        }

        [Parser(Opcode.CMSG_TUTORIAL, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleTutorial(Packet packet)
        {
            var action = packet.ReadBitsE<TutorialAction>("TutorialAction", 2);

            if (action == TutorialAction.Update)
                packet.ReadInt32E<Tutorial>("TutorialBit");
        }

        [Parser(Opcode.CMSG_WHO, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleWhoRequest(Packet packet)
        {
            var areaCount = packet.ReadBits(4);
            packet.ReadBit("IsFromAddon");

            packet.ReadInt32("MinLevel");
            packet.ReadInt32("MaxLevel");
            packet.ReadInt64("RaceFilter");
            packet.ReadInt32("ClassFilter");

            packet.ResetBitReader();

            var nameLen = packet.ReadBits(6);
            var virtualRealmNameLen = packet.ReadBits(9);
            var guildLen = packet.ReadBits(7);
            var guildVirtualRealmNameLen = packet.ReadBits(9);
            var wordCount = packet.ReadBits(3);

            packet.ReadBit("ShowEnemies");
            packet.ReadBit("ShowArenaPlayers");
            packet.ReadBit("ExactName");
            var hasServerInfo = packet.ReadBit("HasServerInfo");
            packet.ResetBitReader();

            for (var i = 0; i < wordCount; ++i)
            {
                var bits0 = packet.ReadBits(7);
                packet.ReadWoWString("Word", bits0, i);
                packet.ResetBitReader();
            }

            packet.ReadWoWString("Name", nameLen);
            packet.ReadWoWString("VirtualRealmName", virtualRealmNameLen);
            packet.ReadWoWString("Guild", guildLen);
            packet.ReadWoWString("GuildVirtualRealmName", guildVirtualRealmNameLen);

            // WhoRequestServerInfo
            if (hasServerInfo)
            {
                packet.ReadInt32("FactionGroup");
                packet.ReadInt32("Locale");
                packet.ReadInt32("RequesterVirtualRealmAddress");
            }

            packet.ReadUInt32("RequestID");
            packet.ReadByteE<WhoRequestOrigin>("Origin");

            for (var i = 0; i < areaCount; ++i)
                packet.ReadUInt32<AreaId>("Area", i);
        }

        [Parser(Opcode.CMSG_WHO_IS, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleWhoIsRequest(Packet packet)
        {
            var len = packet.ReadBits(6);
            packet.ReadWoWString("CharName", len);
        }

        [Parser(Opcode.SMSG_PLAYER_SKINNED)]
        public static void HandlePlayerSkinned(Packet packet)
        {
            packet.ReadBit("FreeRepop");
        }

        [Parser(Opcode.SMSG_PROPOSE_LEVEL_GRANT)]
        public static void HandleGrantLevel(Packet packet)
        {
            packet.ReadPackedGuid128("GUID");
        }

        [Parser(Opcode.SMSG_UPDATE_EXPANSION_LEVEL, ClientVersionBuild.V3_4_4_59817)]
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

        [Parser(Opcode.SMSG_RESUME_COMMS, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_SOCIAL_CONTRACT_REQUEST, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_SERVER_TIME_OFFSET_REQUEST, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_QUERY_TIME, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_REQUEST_CEMETERY_LIST, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_CLEAR_BOSS_EMOTES, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_FISH_ESCAPED, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_FISH_NOT_HOOKED, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_CLIENT_PORT_GRAVEYARD, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_COMPLETE_CINEMATIC, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_COMPLETE_MOVIE, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_ENABLE_NAGLE, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_NEXT_CINEMATIC_CAMERA, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_OPENING_CINEMATIC, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_REQUEST_VEHICLE_EXIT, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_REQUEST_VEHICLE_NEXT_SEAT, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_REQUEST_VEHICLE_PREV_SEAT, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_ACCEPT_SOCIAL_CONTRACT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleZeroLengthPackets(Packet packet)
        {
        }
    }
}
