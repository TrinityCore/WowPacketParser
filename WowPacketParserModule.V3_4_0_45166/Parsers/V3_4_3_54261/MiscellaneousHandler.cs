using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V3_4_0_45166.Parsers.V3_4_3_54261
{
    public static class MiscellaneousHandler
    {
        public static void ReadGameRuleValuePair(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("Rule", indexes);
            packet.ReadInt32("Value", indexes);
        }               

        [BuildMatch(ClientVersionBuild.V3_4_3_54261)]
        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS_GLUE_SCREEN, true)]
        public static void HandleFeatureSystemStatusGlueScreen54261(Packet packet)
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
            packet.ReadBit("Unk");
            var europaTicket = packet.ReadBit("IsEuropaTicketSystemStatusEnabled");
            packet.ReadBit("Unk");
            bool launchETA = packet.ReadBit("IsLaunchETA");
            packet.ReadBit("AddonsDisabled");
            packet.ReadBit("Unused_10_0");
            packet.ReadBit("AccountSaveDataExportEnabled");
            packet.ReadBit("AccountLockedByExport");
            var realmHiddenAlert = packet.ReadBit("IsRealmHiddenAlert");

            uint realmHiddenAlertLen = 0;
            if (realmHiddenAlert)
                realmHiddenAlertLen = packet.ReadBits("RealmHiddenAlert", 11);

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

            if (realmHiddenAlert)
                packet.ReadWoWString("RealmHiddenAlert", realmHiddenAlertLen);

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

        [BuildMatch(ClientVersionBuild.V3_4_3_54261)]
        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS, true)]
        public static void HandleFeatureSystemStatus(Packet packet)
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

            V8_0_1_27101.Parsers.MiscellaneousHandler.ReadVoiceChatManagerSettings(packet, "VoiceChatManagerSettings");
            
            if (hasEuropaTicketSystemStatus)
            {
                packet.ResetBitReader();
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");
            }
        }
    }
}
