using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class MiscellaneousHandler
    {
        public static void ReadGameRuleValuePair(Packet packet, params object[] indexes)
        {
            packet.ReadInt32E<GameRule>("Rule", indexes);
            packet.ReadInt32("Value", indexes);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_7_58123))
                packet.ReadSingle("ValueF", indexes);
        }

        public static void ReadDebugTimeInfo(Packet packet, params object[] indexes)
        {
            packet.ReadUInt32("TimeEvent", indexes);
            packet.ResetBitReader();
            var textLen = packet.ReadBits(7);
            packet.ReadWoWString("Text", textLen);
        }

        [Parser(Opcode.CMSG_REQUEST_WEEKLY_REWARDS)]
        [Parser(Opcode.CMSG_REQUEST_LATEST_SPLASH_SCREEN)]
        public static void HandleEmptyPacket(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_SET_EXCLUDED_CHAT_CENSOR_SOURCES)]
        public static void HandleSetExcludedChatCensorSources(Packet packet)
        {
            packet.ReadBit("Exclude");
        }

        [Parser(Opcode.CMSG_OVERRIDE_SCREEN_FLASH)]
        public static void HandleOverrideScreenFlash(Packet packet)
        {
            packet.ReadBit("Override");
        }

        [Parser(Opcode.SMSG_SPLASH_SCREEN_SHOW_LATEST)]
        public static void HandleSplashScreenShowLastest(Packet packet)
        {
            packet.ReadInt32("SplashScreenID");
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS)]
        public static void HandleFeatureSystemStatus(Packet packet)
        {
            packet.ReadByte("ComplaintStatus");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V10_0_5_47777))
            {
                packet.ReadUInt32("ScrollOfResurrectionRequestsRemaining");
                packet.ReadUInt32("ScrollOfResurrectionMaxRequestsPerDay");
            }
            packet.ReadUInt32("CfgRealmID");
            packet.ReadInt32("CfgRealmRecID");

            packet.ReadUInt32("MaxRecruits", "RAFSystem");
            packet.ReadUInt32("MaxRecruitMonths", "RAFSystem");
            packet.ReadUInt32("MaxRecruitmentUses", "RAFSystem");
            packet.ReadUInt32("DaysInCycle", "RAFSystem");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_7_48676))
            {
                packet.ReadUInt32("RewardsVersion", "RAFSystem");
            }
            else
            {
                packet.ReadUInt32("TwitterPostThrottleLimit");
                packet.ReadUInt32("TwitterPostThrottleCooldown");
            }

            packet.ReadUInt32("CommercePricePollTimeSeconds");
            packet.ReadUInt32("KioskSessionDurationMinutes");
            packet.ReadInt64("RedeemForBalanceAmount");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V11_2_0_62213))
                packet.ReadUInt32("BpayStorePurchaseTimeout");

            packet.ReadUInt32("ClubsPresenceDelay");
            packet.ReadUInt32("ClubPresenceUnsubscribeDelay");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_0_42423))
            {
                packet.ReadInt32("ContentSetID");
                var gameRuleValuesCount = packet.ReadUInt32("GameRulesCount");
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_0_55666))
                {
                    packet.ReadInt32("ActiveTimerunningSeasonID");
                    packet.ReadInt32("RemainingTimerunningSeasonSeconds");
                }

                packet.ReadInt16("MaxPlayerGuidLookupsPerRequest");
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_7_45114))
                    packet.ReadInt16("NameLookupTelemetryInterval");
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_2_46479))
                    packet.ReadUInt32("NotFoundCacheTimeSeconds");

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_1_0_59347))
                    packet.ReadUInt32("RealmPvpTypeOverride");

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_7_54577))
                {
                    packet.ReadInt32("AddonChatThrottle.MaxTries");
                    packet.ReadInt32("AddonChatThrottle.TriesRestoredPerSecond");
                    packet.ReadInt32("AddonChatThrottle.UsedTriesPerMessage");
                }

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_2_5_63506))
                {
                    packet.ReadInt32("GuildChatThrottle.UsedTriesPerMessage");
                    packet.ReadInt32("GuildChatThrottle.TriesRestoredPerSecond");
                    packet.ReadInt32("GroupChatThrottle.UsedTriesPerMessage");
                    packet.ReadInt32("GroupChatThrottle.TriesRestoredPerSecond");
                }

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_1_0_59347))
                {
                    packet.ReadSingle("AddonPerformanceMsgWarning");
                    packet.ReadSingle("AddonPerformanceMsgError");
                    packet.ReadSingle("AddonPerformanceMsgOverall");
                }

                for (var i = 0; i < gameRuleValuesCount; ++i)
                    ReadGameRuleValuePair(packet, "GameRules");
            }

            packet.ResetBitReader();
            packet.ReadBit("VoiceEnabled");
            var hasEuropaTicketSystemStatus = packet.ReadBit("HasEuropaTicketSystemStatus");
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V10_0_5_47777))
                packet.ReadBit("ScrollOfResurrectionEnabled");
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V11_2_0_62213))
                packet.ReadBit("BpayStoreEnabled");
            packet.ReadBit("BpayStoreAvailable");
            packet.ReadBit("BpayStoreDisabledByParentalControls");
            packet.ReadBit("ItemRestorationButtonEnabled");
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V11_2_0_62213))
                packet.ReadBit("BrowserEnabled");
            var hasSessionAlert = packet.ReadBit("HasSessionAlert");

            packet.ReadBit("Enabled", "RAFSystem");
            packet.ReadBit("RecruitingEnabled", "RAFSystem");
            packet.ReadBit("CharUndeleteEnabled");
            packet.ReadBit("RestrictedAccount");
            packet.ReadBit("CommerceServerEnabled");
            packet.ReadBit("TutorialEnabled");
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V10_0_7_48676))
                packet.ReadBit("TwitterEnabled");
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
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_7_54577))
                packet.ReadBit("CommunityFinderEnabled");
            packet.ReadBit("BrowserCrashReporterEnabled");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_5_40772))
            {
                packet.ReadBit("SpeakForMeAllowed");
                packet.ReadBit("DoesAccountNeedAADCPrompt");
                packet.ReadBit("IsAccountOptedInToAADC");
                packet.ReadBit("LfgRequireAuthenticatorEnabled");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_2_46479))
            {
                packet.ReadBit("ScriptsDisallowedForBeta");
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_0_55666))
                    packet.ReadBit("TimerunningEnabled");

                packet.ReadBit("WarGamesEnabled"); // classic only
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_1_5_50232))
            {
                packet.ReadBit("IsPlayerContentTrackingEnabled");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_1_7_51187))
                packet.ReadBit("IsSellAllJunkEnabled");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_0_52038))
            {
                packet.ReadBit("IsGroupFinderEnabled"); // classic only

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V11_0_7_58123))
                {
                    packet.ReadBit("IsLFDEnabled"); // classic only
                    packet.ReadBit("IsLFREnabled"); // classic only
                    packet.ReadBit("IsPremadeGroupEnabled"); // classic only
                }
            }

            var unknown1027StrLen = 0u;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_7_54577))
            {
                packet.ReadBit("PremadeGroupsEnabled");
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_7_58123) && ClientVersion.RemovedInVersion(ClientVersionBuild.V11_1_5_60392))
                    packet.ReadBit("UseActivePlayerDataQuestCompleted");

                packet.ReadBit("Unused1027_1");
                packet.ReadBit("GuildEventsEditsEnabled");
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_0_55666))
                    packet.ReadBit("GuildTradeSkillsEnabled");

                unknown1027StrLen = packet.ReadBits(ClientVersion.AddedInVersion(ClientVersionBuild.V11_2_0_62213) ? 10 : 7);
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_0_55666))
            {
                packet.ReadBit("BNSendWhisperUseV2Services");
                packet.ReadBit("BNSendGameDataUseV2Services");
                packet.ReadBit("IsAccountCurrencyTransferEnabled");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_7_58123))
            {
                packet.ReadBit("Unused1107_1");
                packet.ReadBit("LobbyMatchmakerQueueFromMainlineEnabled");
                packet.ReadBit("CanSendLobbyMatchmakerPartyCustomizations");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_1_0_59347))
                packet.ReadBit("AddonProfilerEnabled");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_1_7_61491))
            {
                packet.ReadBit("Unused_11_1_7_1");
                packet.ReadBit("Unused_11_1_7_2");
            }

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
            {
                packet.ResetBitReader();
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");
            }
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS2)]
        public static void HandleFeatureSystemStatus2(Packet packet)
        {
            packet.ReadBit("TextToSpeechFeatureEnabled");
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS_GLUE_SCREEN)]
        public static void HandleFeatureSystemStatusGlueScreen(Packet packet)
        {
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V11_2_0_62213))
                packet.ReadBit("BpayStoreEnabled");
            packet.ReadBit("BpayStoreAvailable");
            packet.ReadBit("BpayStoreDisabledByParentalControls");
            packet.ReadBit("CharUndeleteEnabled");
            packet.ReadBit("CommerceServerEnabled");
            packet.ReadBit("VeteranTokenRedeemWillKick");
            packet.ReadBit("WorldTokenRedeemWillKick");
            packet.ReadBit("ExpansionPreorderInStore");
            packet.ReadBit("KioskModeEnabled");
            packet.ReadBit("CompetitiveModeEnabled");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_2_46479))
                packet.ReadBit("BoostEnabled");
            packet.ReadBit("TrialBoostEnabled");
            packet.ReadBit("RedeemForBalanceAvailable");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_0_55666))
                packet.ReadBit("PaidCharacterTransfersBetweenBnetAccountsEnabled");
            packet.ReadBit("LiveRegionCharacterListEnabled");
            packet.ReadBit("LiveRegionCharacterCopyEnabled");
            packet.ReadBit("LiveRegionAccountCopyEnabled");

            packet.ReadBit("LiveRegionKeyBindingsCopyEnabled");
            packet.ReadBit("BrowserCrashReporterEnabled");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_2_46479))
                packet.ReadBit("IsEmployeeAccount");
            var europaTicket = packet.ReadBit("IsEuropaTicketSystemStatusEnabled");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_2_46479))
                packet.ReadBit("NameReservationOnly");
            var launchEta = packet.ReadBit();
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_7_54577))
                packet.ReadBit("TimerunningEnabled");
            packet.ReadBit("ScriptsDisallowedForBeta");
            packet.ReadBit("PlayerIdentityOptionsEnabled");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_7_48676))
            {
                packet.ReadBit("AccountExportEnabled");
                packet.ReadBit("AccountLockedPostExport");
            }

            var realmHiddenAlertLen = 0u;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_7_54577))
                realmHiddenAlertLen = packet.ReadBits(11);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_0_55666))
            {
                packet.ReadBit("BNSendWhisperUseV2Services");
                packet.ReadBit("BNSendGameDataUseV2Services");
                packet.ReadBit("CharacterSelectListModeRealmless");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_1_7_61491))
            {
                packet.ReadBit("WowTokenLimitedMode");
                packet.ReadBit("Unused_11_1_7_1");
                packet.ReadBit("Unused_11_1_7_2");
                packet.ReadBit("PandarenLevelBoostAllowed");
            }

            packet.ResetBitReader();

            if (europaTicket)
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");

            packet.ReadUInt32("CommercePricePollTimeSeconds");
            packet.ReadUInt32("KioskSessionDurationMinutes");
            packet.ReadInt64("RedeemForBalanceAmount");
            packet.ReadInt32("MaxCharactersOnThisRealm");
            var liveRegionCharacterCopySourceRegionsCount = packet.ReadUInt32("LiveRegionCharacterCopySourceRegionsCount");
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V11_2_0_62213))
                packet.ReadUInt32("BpayStorePurchaseTimeout");
            packet.ReadInt32("ActiveBoostType");
            packet.ReadInt32("TrialBoostType");
            packet.ReadInt32("MinimumExpansionLevel");
            packet.ReadInt32("MaximumExpansionLevel");

            var gameRuleValuesCount = 0u;

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_0_42423))
            {
                packet.ReadInt32("ContentSetID");
                gameRuleValuesCount = packet.ReadUInt32("GameRuleValuesCount");

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_7_54577))
                {
                    packet.ReadInt32("ActiveTimerunningSeasonID");
                    packet.ReadInt32("RemainingTimerunningSeasonSeconds");
                }

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_2_5_63506))
                {
                    packet.ReadInt32("TimerunningConversionMinCharacterAge");
                    packet.ReadInt32("TimerunningConversionMaxSeasonID");
                }

                packet.ReadInt16("MaxPlayerGuidLookupsPerRequest");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_7_45114))
                packet.ReadInt16("NameLookupTelemetryInterval");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_2_46479))
                packet.ReadUInt32("NotFoundCacheTimeSeconds");

            uint debugTimeEventCount = 0;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_7_48676))
            {
                debugTimeEventCount = packet.ReadUInt32("DebugTimeEventCount");
                packet.ReadInt32("MostRecentTimeEventID");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_6_53840))
                packet.ReadUInt32("EventRealmQueues");

            if (launchEta)
                packet.ReadInt32("LaunchETA");

            packet.ReadDynamicString("RealmHiddenAlert", realmHiddenAlertLen);

            for (int i = 0; i < liveRegionCharacterCopySourceRegionsCount; i++)
                packet.ReadUInt32("LiveRegionCharacterCopySourceRegion", i);

            for (var i = 0; i < gameRuleValuesCount; ++i)
                ReadGameRuleValuePair(packet, "GameRules", i);

            for (var i = 0; i < debugTimeEventCount; ++i)
                ReadDebugTimeInfo(packet, "DebugTimeEvent", i);
        }

        [Parser(Opcode.SMSG_SET_ALL_TASK_PROGRESS)]
        [Parser(Opcode.SMSG_UPDATE_TASK_PROGRESS)]
        public static void HandleSetAllTaskProgress(Packet packet)
        {
            var int4 = packet.ReadUInt32("TaskProgressCount");
            for (int i = 0; i < int4; i++)
            {
                packet.ReadUInt32("TaskID", i);

                // these fields might have been shuffled
                packet.ReadUInt32("FailureTime", i);
                packet.ReadUInt32("Flags", i);
                packet.ReadUInt32("Unk", i);

                var int3 = packet.ReadUInt32("ProgressCounts", i);
                for (int j = 0; j < int3; j++)
                    packet.ReadUInt16("Counts", i, j);
            }
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

        [Parser(Opcode.SMSG_WORLD_SERVER_INFO)]
        public static void HandleWorldServerInfo(Packet packet)
        {
            CoreParsers.MovementHandler.CurrentDifficultyID = packet.ReadUInt32<DifficultyId>("DifficultyID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_7_45114))
                packet.ReadBit("IsTournamentRealm");
            else
                packet.ReadByte("IsTournamentRealm");

            packet.ReadBit("XRealmPvpAlert");
            packet.ReadBit("BlockExitingLoadingScreen");
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

        [Parser(Opcode.SMSG_PLAY_SOUND)]
        public static void HandlePlaySound(Packet packet)
        {
            PacketPlaySound packetPlaySound = packet.Holder.PlaySound = new PacketPlaySound();
            var sound = packetPlaySound.Sound = (uint)packet.ReadInt32<SoundId>("SoundKitID");
            packetPlaySound.Source = packet.ReadPackedGuid128("SourceObjectGUID").ToUniversalGuid();
            packetPlaySound.BroadcastTextId = (uint)packet.ReadInt32("BroadcastTextID");

            Storage.Sounds.Add(sound, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_CONVERSATION_CINEMATIC_READY)]
        public static void HandleConversationCinematicReady(Packet packet)
        {
            packet.ReadPackedGuid128("ConversationGUID");
        }
    }
}
