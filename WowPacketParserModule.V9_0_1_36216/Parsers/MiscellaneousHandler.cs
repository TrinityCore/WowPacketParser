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
            packet.ReadInt32("Rule", indexes);
            packet.ReadInt32("Value", indexes);
        }

        public static void ReadDebugTimeInfo(Packet packet, params object[] indexes)
        {
            packet.ReadUInt32("TimeEvent", indexes);
            packet.ResetBitReader();
            var textLen = packet.ReadBits(7);
            packet.ReadWoWString("Text", textLen);
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
                packet.ReadUInt32("Unknown1007", "RAFSystem");
            }
            else
            {
                packet.ReadUInt32("TwitterPostThrottleLimit");
                packet.ReadUInt32("TwitterPostThrottleCooldown");
            }

            packet.ReadUInt32("TokenPollTimeSeconds");
            packet.ReadUInt32("KioskSessionMinutes");
            packet.ReadInt64("TokenBalanceAmount");

            packet.ReadUInt32("BpayStoreProductDeliveryDelay");

            packet.ReadUInt32("ClubsPresenceUpdateTimer");
            packet.ReadUInt32("HiddenUIClubsPresenceUpdateTimer");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_0_42423))
            {
                packet.ReadInt32("ActiveSeason");
                var gameRuleValuesCount = packet.ReadUInt32("GameRuleValuesCount");

                packet.ReadInt16("MaxPlayerNameQueriesPerPacket");
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_7_45114))
                    packet.ReadInt16("PlayerNameQueryTelemetryInterval");
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_2_46479))
                    packet.ReadUInt32("PlayerNameQueryInterval");

                for (var i = 0; i < gameRuleValuesCount; ++i)
                    ReadGameRuleValuePair(packet, "GameRuleValues");
            }

            packet.ResetBitReader();
            packet.ReadBit("VoiceEnabled");
            var hasEuropaTicketSystemStatus = packet.ReadBit("HasEuropaTicketSystemStatus");
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V10_0_5_47777))
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
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V10_0_7_48676))
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

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_5_40772))
            {
                packet.ReadBit("TextToSpeechFeatureEnabled");
                packet.ReadBit("ChatDisabledByDefault");
                packet.ReadBit("ChatDisabledByPlayer");
                packet.ReadBit("LFGListCustomRequiresAuthenticator");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_2_46479))
            {
                packet.ReadBit("AddonsDisabled");
                packet.ReadBit("WarGamesEnabled"); // classic only
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_1_5_50232))
            {
                packet.ReadBit("ContentTrackingEnabled");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_1_7_51187))
                packet.ReadBit("IsSellAllJunkEnabled");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_0_52038))
            {
                packet.ReadBit("IsGroupFinderEnabled"); // classic only
                packet.ReadBit("IsLFDEnabled"); // classic only

                packet.ReadBit("IsLFREnabled"); // classic only
                packet.ReadBit("IsPremadeGroupEnabled"); // classic only
            }


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

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS2)]
        public static void HandleFeatureSystemStatus2(Packet packet)
        {
            packet.ReadBit("TextToSpeechFeatureEnabled");
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
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_2_46479))
                packet.ReadBit("Unused1002_1");
            packet.ReadBit("TrialBoostEnabled");
            packet.ReadBit("TokenBalanceEnabled");
            packet.ReadBit("LiveRegionCharacterListEnabled");
            packet.ReadBit("LiveRegionCharacterCopyEnabled");
            packet.ReadBit("LiveRegionAccountCopyEnabled");

            packet.ReadBit("LiveRegionKeyBindingsCopyEnabled");
            packet.ReadBit("Unknown901CheckoutRelated");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_2_46479))
                packet.ReadBit("Unused1002_2");
            var europaTicket = packet.ReadBit("IsEuropaTicketSystemStatusEnabled");
            var launchEta = packet.ReadBit();
            packet.ReadBit("AddonsDisabled");
            packet.ReadBit("Unused1000");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_7_48676))
            {
                packet.ReadBit("AccountSaveDataExportEnabled");
                packet.ReadBit("AccountLockedByExport");
            }

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

            var gameRuleValuesCount = 0u;

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_0_42423))
            {
                packet.ReadInt32("GameRuleUnknown1");
                gameRuleValuesCount = packet.ReadUInt32("GameRuleValuesCount");
                packet.ReadInt16("MaxPlayerNameQueriesPerPacket");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_7_45114))
                packet.ReadInt16("PlayerNameQueryTelemetryInterval");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_2_46479))
                packet.ReadUInt32("PlayerNameQueryInterval");

            uint debugTimeEventCount = 0;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_7_48676))
            {
                debugTimeEventCount = packet.ReadUInt32("DebugTimeEventCount");
                packet.ReadInt32("Unused1007");
            }

            for (int i = 0; i < liveRegionCharacterCopySourceRegionsCount; i++)
                packet.ReadUInt32("LiveRegionCharacterCopySourceRegion", i);

            for (var i = 0; i < gameRuleValuesCount; ++i)
                ReadGameRuleValuePair(packet, "GameRuleValues", i);

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
