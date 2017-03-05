using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class MiscellaneousHandler
    {
        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS_GLUE_SCREEN, ClientVersionBuild.V7_0_3_22248, ClientVersionBuild.V7_1_0_22900)]
        public static void HandleFeatureSystemStatusGlueScreen(Packet packet)
        {
            packet.Translator.ReadBit("BpayStoreEnabled");
            packet.Translator.ReadBit("BpayStoreAvailable");
            packet.Translator.ReadBit("BpayStoreDisabledByParentalControls");
            packet.Translator.ReadBit("CharUndeleteEnabled");
            packet.Translator.ReadBit("CommerceSystemEnabled");
            packet.Translator.ReadBit("Unk14");
            packet.Translator.ReadBit("WillKickFromWorld");
            packet.Translator.ReadBit("IsExpansionPreorderInStore");
            packet.Translator.ReadBit("KioskModeEnabled");
            packet.Translator.ReadBit("NoHandler"); // not accessed in handler
            packet.Translator.ReadBit("TrialBoostEnabled");

            packet.Translator.ReadInt32("TokenPollTimeSeconds");
            packet.Translator.ReadInt32E<ConsumableTokenRedeem>("TokenRedeemIndex");
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS_GLUE_SCREEN, ClientVersionBuild.V7_1_0_22900, ClientVersionBuild.V7_1_5_23360)]
        public static void HandleFeatureSystemStatusGlueScreen710(Packet packet)
        {
            packet.Translator.ReadBit("BpayStoreEnabled");
            packet.Translator.ReadBit("BpayStoreAvailable");
            packet.Translator.ReadBit("BpayStoreDisabledByParentalControls");
            packet.Translator.ReadBit("CharUndeleteEnabled");
            packet.Translator.ReadBit("CommerceSystemEnabled");
            packet.Translator.ReadBit("Unk14");
            packet.Translator.ReadBit("WillKickFromWorld");
            packet.Translator.ReadBit("IsExpansionPreorderInStore");
            packet.Translator.ReadBit("KioskModeEnabled");
            packet.Translator.ReadBit("CompetitiveModeEnabled");
            packet.Translator.ReadBit("NoHandler"); // not accessed in handler
            packet.Translator.ReadBit("TrialBoostEnabled");

            packet.Translator.ReadInt32("TokenPollTimeSeconds");
            packet.Translator.ReadInt32E<ConsumableTokenRedeem>("TokenRedeemIndex");
        }

        [Parser(Opcode.SMSG_INITIAL_SETUP)]
        public static void HandleInitialSetup(Packet packet)
        {
            packet.Translator.ReadByte("ServerExpansionLevel");
            packet.Translator.ReadByte("ServerExpansionTier");
        }

        [Parser(Opcode.SMSG_WORLD_SERVER_INFO)]
        public static void HandleWorldServerInfo(Packet packet)
        {
            packet.Translator.ReadInt32("DifficultyID");
            packet.Translator.ReadByte("IsTournamentRealm");

            packet.Translator.ReadBit("XRealmPvpAlert");
            var hasRestrictedAccountMaxLevel = packet.Translator.ReadBit("HasRestrictedAccountMaxLevel");
            var hasRestrictedAccountMaxMoney = packet.Translator.ReadBit("HasRestrictedAccountMaxMoney");
            var hasInstanceGroupSize = packet.Translator.ReadBit("HasInstanceGroupSize");

            if (hasRestrictedAccountMaxLevel)
                packet.Translator.ReadInt32("RestrictedAccountMaxLevel");

            if (hasRestrictedAccountMaxMoney)
                packet.Translator.ReadInt32("RestrictedAccountMaxMoney");

            if (hasInstanceGroupSize)
                packet.Translator.ReadInt32("InstanceGroupSize");
        }

        [Parser(Opcode.SMSG_ACCOUNT_MOUNT_UPDATE)]
        public static void HandleAccountMountUpdate(Packet packet)
        {
            packet.Translator.ReadBit("IsFullUpdate");

            var mountSpellIDsCount = packet.Translator.ReadInt32("MountSpellIDsCount");

            for (int i = 0; i < mountSpellIDsCount; i++)
            {
                packet.Translator.ReadInt32("MountSpellIDs", i);

                packet.Translator.ResetBitReader();
                packet.Translator.ReadBits("MountIsFavorite", 2, i);
            }
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

            var pagesCount = packet.Translator.ReadInt32("PagesCount");

            for (int i = 0; i < pagesCount; i++)
            {
                PageText pageText = new PageText();

                uint entry = packet.Translator.ReadUInt32("ID", i);
                pageText.ID = entry;
                pageText.NextPageID = packet.Translator.ReadUInt32("NextPageID", i);

                pageText.PlayerConditionID = packet.Translator.ReadInt32("PlayerConditionID", i);
                pageText.Flags = packet.Translator.ReadByte("Flags", i);

                packet.Translator.ResetBitReader();
                uint textLen = packet.Translator.ReadBits(12);
                pageText.Text = packet.Translator.ReadWoWString("Text", textLen, i);

                packet.AddSniffData(StoreNameType.PageText, (int)entry, "QUERY_RESPONSE");
                Storage.PageTexts.Add(pageText, packet.TimeSpan);
            }
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS, ClientVersionBuild.V7_1_0_22900, ClientVersionBuild.V7_1_5_23360)]
        public static void HandleFeatureSystemStatus710(Packet packet)
        {
            packet.Translator.ReadByte("ComplaintStatus");

            packet.Translator.ReadUInt32("ScrollOfResurrectionRequestsRemaining");
            packet.Translator.ReadUInt32("ScrollOfResurrectionMaxRequestsPerDay");
            packet.Translator.ReadUInt32("CfgRealmID");
            packet.Translator.ReadInt32("CfgRealmRecID");
            packet.Translator.ReadUInt32("TwitterPostThrottleLimit");
            packet.Translator.ReadUInt32("TwitterPostThrottleCooldown");
            packet.Translator.ReadUInt32("TokenPollTimeSeconds");
            packet.Translator.ReadUInt32E<ConsumableTokenRedeem>("TokenRedeemIndex");

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
            packet.Translator.ReadBit("KioskModeEnabled");
            packet.Translator.ReadBit("CompetitiveModeEnabled");
            var hasRaceClassExpansionLevels = packet.Translator.ReadBit("RaceClassExpansionLevels");

            {
                packet.Translator.ResetBitReader();
                packet.Translator.ReadBit("ToastsDisabled");
                packet.Translator.ReadSingle("ToastDuration");
                packet.Translator.ReadSingle("DelayDuration");
                packet.Translator.ReadSingle("QueueMultiplier");
                packet.Translator.ReadSingle("PlayerMultiplier");
                packet.Translator.ReadSingle("PlayerFriendValue");
                packet.Translator.ReadSingle("PlayerGuildValue");
                packet.Translator.ReadSingle("ThrottleInitialThreshold");
                packet.Translator.ReadSingle("ThrottleDecayTime");
                packet.Translator.ReadSingle("ThrottlePrioritySpike");
                packet.Translator.ReadSingle("ThrottleMinThreshold");
                packet.Translator.ReadSingle("ThrottlePvPPriorityNormal");
                packet.Translator.ReadSingle("ThrottlePvPPriorityLow");
                packet.Translator.ReadSingle("ThrottlePvPHonorThreshold");
                packet.Translator.ReadSingle("ThrottleLfgListPriorityDefault");
                packet.Translator.ReadSingle("ThrottleLfgListPriorityAbove");
                packet.Translator.ReadSingle("ThrottleLfgListPriorityBelow");
                packet.Translator.ReadSingle("ThrottleLfgListIlvlScalingAbove");
                packet.Translator.ReadSingle("ThrottleLfgListIlvlScalingBelow");
                packet.Translator.ReadSingle("ThrottleRfPriorityAbove");
                packet.Translator.ReadSingle("ThrottleRfIlvlScalingAbove");
                packet.Translator.ReadSingle("ThrottleDfMaxItemLevel");
                packet.Translator.ReadSingle("ThrottleDfBestPriority");
            }

            if (hasSessionAlert)
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadClientSessionAlertConfig(packet, "SessionAlert");

            if (hasRaceClassExpansionLevels)
            {
                var int88 = packet.Translator.ReadInt32("RaceClassExpansionLevelsCount");
                for (int i = 0; i < int88; i++)
                    packet.Translator.ReadByte("RaceClassExpansionLevels", i);
            }

            packet.Translator.ResetBitReader();

            if (hasEuropaTicketSystemStatus)
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS, ClientVersionBuild.V7_1_5_23360)]
        public static void HandleFeatureSystemStatus715(Packet packet)
        {
            packet.Translator.ReadByte("ComplaintStatus");

            packet.Translator.ReadUInt32("ScrollOfResurrectionRequestsRemaining");
            packet.Translator.ReadUInt32("ScrollOfResurrectionMaxRequestsPerDay");
            packet.Translator.ReadUInt32("CfgRealmID");
            packet.Translator.ReadInt32("CfgRealmRecID");
            packet.Translator.ReadUInt32("TwitterPostThrottleLimit");
            packet.Translator.ReadUInt32("TwitterPostThrottleCooldown");
            packet.Translator.ReadUInt32("TokenPollTimeSeconds");
            packet.Translator.ReadUInt32E<ConsumableTokenRedeem>("TokenRedeemIndex");
            packet.Translator.ReadUInt64("TokenBalanceAmount");

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
            packet.Translator.ReadBit("KioskModeEnabled");
            packet.Translator.ReadBit("CompetitiveModeEnabled");
            var hasRaceClassExpansionLevels = packet.Translator.ReadBit("RaceClassExpansionLevels");
            packet.Translator.ReadBit("TokenBalanceEnabled");

            {
                packet.Translator.ResetBitReader();
                packet.Translator.ReadBit("ToastsDisabled");
                packet.Translator.ReadSingle("ToastDuration");
                packet.Translator.ReadSingle("DelayDuration");
                packet.Translator.ReadSingle("QueueMultiplier");
                packet.Translator.ReadSingle("PlayerMultiplier");
                packet.Translator.ReadSingle("PlayerFriendValue");
                packet.Translator.ReadSingle("PlayerGuildValue");
                packet.Translator.ReadSingle("ThrottleInitialThreshold");
                packet.Translator.ReadSingle("ThrottleDecayTime");
                packet.Translator.ReadSingle("ThrottlePrioritySpike");
                packet.Translator.ReadSingle("ThrottleMinThreshold");
                packet.Translator.ReadSingle("ThrottlePvPPriorityNormal");
                packet.Translator.ReadSingle("ThrottlePvPPriorityLow");
                packet.Translator.ReadSingle("ThrottlePvPHonorThreshold");
                packet.Translator.ReadSingle("ThrottleLfgListPriorityDefault");
                packet.Translator.ReadSingle("ThrottleLfgListPriorityAbove");
                packet.Translator.ReadSingle("ThrottleLfgListPriorityBelow");
                packet.Translator.ReadSingle("ThrottleLfgListIlvlScalingAbove");
                packet.Translator.ReadSingle("ThrottleLfgListIlvlScalingBelow");
                packet.Translator.ReadSingle("ThrottleRfPriorityAbove");
                packet.Translator.ReadSingle("ThrottleRfIlvlScalingAbove");
                packet.Translator.ReadSingle("ThrottleDfMaxItemLevel");
                packet.Translator.ReadSingle("ThrottleDfBestPriority");
            }

            if (hasSessionAlert)
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadClientSessionAlertConfig(packet, "SessionAlert");

            if (hasRaceClassExpansionLevels)
            {
                var int88 = packet.Translator.ReadInt32("RaceClassExpansionLevelsCount");
                for (int i = 0; i < int88; i++)
                    packet.Translator.ReadByte("RaceClassExpansionLevels", i);
            }

            packet.Translator.ResetBitReader();

            if (hasEuropaTicketSystemStatus)
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS_GLUE_SCREEN, ClientVersionBuild.V7_1_5_23360)]
        public static void HandleFeatureSystemStatusGlueScreen715(Packet packet)
        {
            packet.Translator.ReadBit("BpayStoreEnabled");
            packet.Translator.ReadBit("BpayStoreAvailable");
            packet.Translator.ReadBit("BpayStoreDisabledByParentalControls");
            packet.Translator.ReadBit("CharUndeleteEnabled");
            packet.Translator.ReadBit("CommerceSystemEnabled");
            packet.Translator.ReadBit("Unk14");
            packet.Translator.ReadBit("WillKickFromWorld");
            packet.Translator.ReadBit("IsExpansionPreorderInStore");
            packet.Translator.ReadBit("KioskModeEnabled");
            packet.Translator.ReadBit("NoHandler"); // not accessed in handler
            packet.Translator.ReadBit("TrialBoostEnabled");
            packet.Translator.ReadBit("TokenBalanceEnabled");

            packet.Translator.ReadInt32("TokenPollTimeSeconds");
            packet.Translator.ReadInt32E<ConsumableTokenRedeem>("TokenRedeemIndex");
            packet.Translator.ReadInt64("TokenBalanceAmount");
        }
    }
}
