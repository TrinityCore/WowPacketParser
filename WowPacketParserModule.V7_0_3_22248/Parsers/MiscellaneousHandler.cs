using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using TutorialAction703 = WowPacketParser.Enums.Version.V7_0_3_22248.TutorialAction;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class MiscellaneousHandler
    {
        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS_GLUE_SCREEN, ClientVersionBuild.V7_0_3_22248, ClientVersionBuild.V7_1_0_22900)]
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
            packet.ReadBit("NoHandler"); // not accessed in handler
            packet.ReadBit("TrialBoostEnabled");

            packet.ReadInt32("TokenPollTimeSeconds");
            packet.ReadInt32E<ConsumableTokenRedeem>("TokenRedeemIndex");
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS_GLUE_SCREEN, ClientVersionBuild.V7_1_0_22900, ClientVersionBuild.V7_1_5_23360)]
        public static void HandleFeatureSystemStatusGlueScreen710(Packet packet)
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
            packet.ReadBit("CompetitiveModeEnabled");
            packet.ReadBit("NoHandler"); // not accessed in handler
            packet.ReadBit("TrialBoostEnabled");

            packet.ReadInt32("TokenPollTimeSeconds");
            packet.ReadInt32E<ConsumableTokenRedeem>("TokenRedeemIndex");
        }

        [Parser(Opcode.SMSG_INITIAL_SETUP)]
        public static void HandleInitialSetup(Packet packet)
        {
            packet.ReadByte("ServerExpansionLevel");
            packet.ReadByte("ServerExpansionTier");
        }

        [Parser(Opcode.SMSG_WORLD_SERVER_INFO)]
        public static void HandleWorldServerInfo(Packet packet)
        {
            CoreParsers.MovementHandler.CurrentDifficultyID = packet.ReadUInt32<DifficultyId>("DifficultyID");
            packet.ReadByte("IsTournamentRealm");

            packet.ReadBit("XRealmPvpAlert");
            var hasRestrictedAccountMaxLevel = packet.ReadBit("HasRestrictedAccountMaxLevel");
            var hasRestrictedAccountMaxMoney = packet.ReadBit("HasRestrictedAccountMaxMoney");
            var hasInstanceGroupSize = packet.ReadBit("HasInstanceGroupSize");

            if (hasRestrictedAccountMaxLevel)
                packet.ReadInt32("RestrictedAccountMaxLevel");

            if (hasRestrictedAccountMaxMoney)
                packet.ReadInt32("RestrictedAccountMaxMoney");

            if (hasInstanceGroupSize)
                packet.ReadInt32("InstanceGroupSize");
        }

        [Parser(Opcode.SMSG_ACCOUNT_MOUNT_UPDATE)]
        public static void HandleAccountMountUpdate(Packet packet)
        {
            packet.ReadBit("IsFullUpdate");

            var mountSpellIDsCount = packet.ReadInt32("MountSpellIDsCount");

            for (int i = 0; i < mountSpellIDsCount; i++)
            {
                packet.ReadInt32("MountSpellIDs", i);

                var flagsLen = 2;
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_1_0_49318))
                    flagsLen = 4;

                packet.ResetBitReader();
                packet.ReadBits("Flags", flagsLen, i);
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_PAGE_TEXT_RESPONSE)]
        public static void HandlePageTextResponse(Packet packet)
        {
            packet.ReadUInt32("PageTextID");
            packet.ResetBitReader();

            Bit hasData = packet.ReadBit("Allow");
            if (!hasData)
                return; // nothing to do

            var pagesCount = packet.ReadInt32("PagesCount");

            for (int i = 0; i < pagesCount; i++)
            {
                PageText pageText = new PageText();

                uint entry = packet.ReadUInt32("ID", i);
                pageText.ID = entry;
                pageText.NextPageID = packet.ReadUInt32("NextPageID", i);

                pageText.PlayerConditionID = packet.ReadInt32("PlayerConditionID", i);
                pageText.Flags = packet.ReadByte("Flags", i);

                packet.ResetBitReader();
                uint textLen = packet.ReadBits(12);
                pageText.Text = packet.ReadWoWString("Text", textLen, i);

                packet.AddSniffData(StoreNameType.PageText, (int)entry, "QUERY_RESPONSE");
                Storage.PageTexts.Add(pageText, packet.TimeSpan);

                if (ClientLocale.PacketLocale != LocaleConstant.enUS && pageText.Text != string.Empty)
                {
                    PageTextLocale localesPageText = new PageTextLocale
                    {
                        ID = pageText.ID,
                        Text = pageText.Text
                    };
                    Storage.LocalesPageText.Add(localesPageText, packet.TimeSpan);
                }
            }
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS, ClientVersionBuild.V7_1_0_22900, ClientVersionBuild.V7_1_5_23360)]
        public static void HandleFeatureSystemStatus710(Packet packet)
        {
            packet.ReadByte("ComplaintStatus");

            packet.ReadUInt32("ScrollOfResurrectionRequestsRemaining");
            packet.ReadUInt32("ScrollOfResurrectionMaxRequestsPerDay");
            packet.ReadUInt32("CfgRealmID");
            packet.ReadInt32("CfgRealmRecID");
            packet.ReadUInt32("TwitterPostThrottleLimit");
            packet.ReadUInt32("TwitterPostThrottleCooldown");
            packet.ReadUInt32("TokenPollTimeSeconds");
            packet.ReadUInt32E<ConsumableTokenRedeem>("TokenRedeemIndex");

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
            packet.ReadBit("KioskModeEnabled");
            packet.ReadBit("CompetitiveModeEnabled");
            var hasRaceClassExpansionLevels = packet.ReadBit("RaceClassExpansionLevels");

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

            if (hasRaceClassExpansionLevels)
            {
                var int88 = packet.ReadInt32("RaceClassExpansionLevelsCount");
                for (int i = 0; i < int88; i++)
                    packet.ReadByte("RaceClassExpansionLevels", i);
            }

            packet.ResetBitReader();

            if (hasEuropaTicketSystemStatus)
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS, ClientVersionBuild.V7_1_5_23360)]
        public static void HandleFeatureSystemStatus715(Packet packet)
        {
            packet.ReadByte("ComplaintStatus");

            packet.ReadUInt32("ScrollOfResurrectionRequestsRemaining");
            packet.ReadUInt32("ScrollOfResurrectionMaxRequestsPerDay");
            packet.ReadUInt32("CfgRealmID");
            packet.ReadInt32("CfgRealmRecID");
            packet.ReadUInt32("TwitterPostThrottleLimit");
            packet.ReadUInt32("TwitterPostThrottleCooldown");
            packet.ReadUInt32("TokenPollTimeSeconds");
            packet.ReadUInt32E<ConsumableTokenRedeem>("TokenRedeemIndex");
            packet.ReadUInt64("TokenBalanceAmount");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_3_0_24920))
                packet.ReadUInt32("BpayStoreProductDeliveryDelay");

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
            packet.ReadBit("KioskModeEnabled");
            packet.ReadBit("CompetitiveModeEnabled");
            var hasRaceClassExpansionLevels = packet.ReadBit("RaceClassExpansionLevels");
            packet.ReadBit("TokenBalanceEnabled");

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

            if (hasRaceClassExpansionLevels)
            {
                var int88 = packet.ReadInt32("RaceClassExpansionLevelsCount");
                for (int i = 0; i < int88; i++)
                    packet.ReadByte("RaceClassExpansionLevels", i);
            }

            packet.ResetBitReader();

            if (hasEuropaTicketSystemStatus)
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS_GLUE_SCREEN, ClientVersionBuild.V7_1_5_23360)]
        public static void HandleFeatureSystemStatusGlueScreen715(Packet packet)
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
            packet.ReadBit("CompetetiveModeEnabled");
            packet.ReadBit("NoHandler"); // not accessed in handler
            packet.ReadBit("TrialBoostEnabled");
            packet.ReadBit("TokenBalanceEnabled");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_3_0_24920))
            {
                packet.ReadBit("LiveRegionCharacterListEnabled");
                packet.ReadBit("LiveRegionCharacterCopyEnabled");
                packet.ReadBit("LiveRegionAccountCopyEnabled");
            }

            packet.ReadInt32("TokenPollTimeSeconds");
            packet.ReadInt32E<ConsumableTokenRedeem>("TokenRedeemIndex");
            packet.ReadInt64("TokenBalanceAmount");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_3_0_24920))
                packet.ReadUInt32("BpayStoreProductDeliveryDelay");
        }

        [Parser(Opcode.SMSG_CUSTOM_LOAD_SCREEN)]
        public static void HandleCustomLoadScreen(Packet packet)
        {
            packet.ReadUInt32("TeleportSpellID");
            packet.ReadUInt32("LoadingScreenID");
        }

        [Parser(Opcode.SMSG_ALLIED_RACE_DETAILS)]
        public static void HandleAlliedRaceDetails(Packet packet)
        {
            packet.ReadPackedGuid128("GUID"); // Creature or GameObject
            packet.ReadInt32("RaceID");
        }

        public static void ReadAreaPoiData(Packet packet, params object[] idx)
        {
            packet.ReadTime("StartTime", idx);
            packet.ReadInt32("AreaPoiID", idx);
            packet.ReadInt32("DurationSec", idx);
            packet.ReadUInt32("WorldStateVariableID", idx);
            packet.ReadUInt32("WorldStateValue", idx);
        }

        [Parser(Opcode.SMSG_AREA_POI_UPDATE_RESPONSE)]
        public static void HandleAreaPOIUpdateResponse(Packet packet)
        {
            var count = packet.ReadInt32("Count");

            for (var i = 0; i < count; i++)
                ReadAreaPoiData(packet, i);
        }

        [Parser(Opcode.CMSG_REQUEST_AREA_POI_UPDATE)]
        public static void HandleAreaPoiZero(Packet packet) { }

        [Parser(Opcode.SMSG_SET_MOVEMENT_ANIM_KIT)]
        public static void HandlePlayOneShotAnimKit(Packet packet)
        {
            packet.ReadPackedGuid128("Unit");
            packet.ReadUInt16("AnimKitID");
        }

        [Parser(Opcode.CMSG_TUTORIAL_FLAG)]
        public static void HandleTutorialFlag620(Packet packet)
        {
            var action = packet.ReadBitsE<TutorialAction703>("TutorialAction", 2);

            if (action == TutorialAction703.Update)
                packet.ReadInt32E<Tutorial>("TutorialBit");
        }
    }
}
