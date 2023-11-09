using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class MiscellaneousHandler
    {
        public static void ReadVoiceChatManagerSettings(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();
            packet.ReadBit("IsSquelched", idx);
            packet.ReadPackedGuid128("BnetAccountID", idx);
            packet.ReadPackedGuid128("GuildGUID", idx);
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS)]
        public static void HandleFeatureSystemStatus(Packet packet)
        {
            packet.ReadByte("ComplaintStatus");

            packet.ReadUInt32("ScrollOfResurrectionRequestsRemaining");
            packet.ReadUInt32("ScrollOfResurrectionMaxRequestsPerDay");
            packet.ReadUInt32("CfgRealmID");
            packet.ReadInt32("CfgRealmRecID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_2_5_31921))
            {
                packet.ReadUInt32("MaxRecruits", "RAFSystem");
                packet.ReadUInt32("MaxRecruitMonths", "RAFSystem");
                packet.ReadUInt32("MaxRecruitmentUses", "RAFSystem");
                packet.ReadUInt32("DaysInCycle", "RAFSystem");
            }

            packet.ReadUInt32("TwitterPostThrottleLimit");
            packet.ReadUInt32("TwitterPostThrottleCooldown");
            packet.ReadUInt32("TokenPollTimeSeconds");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V8_2_5_31921))
                packet.ReadUInt32E<ConsumableTokenRedeem>("TokenRedeemIndex");

            packet.ReadInt64("TokenBalanceAmount");
            packet.ReadUInt32("BpayStoreProductDeliveryDelay");
            packet.ReadUInt32("ClubsPresenceUpdateTimer");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
                packet.ReadUInt32("HiddenUIClubsPresenceUpdateTimer");

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

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V8_2_5_31921))
                packet.ReadBit("RecruitAFriendSendingEnabled");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_2_5_31921))
            {
                packet.ReadBit("Enabled", "RAFSystem");
                packet.ReadBit("RecruitingEnabled", "RAFSystem");
            }

            packet.ReadBit("CharUndeleteEnabled");
            packet.ReadBit("RestrictedAccount");
            packet.ReadBit("CommerceSystemEnabled");
            packet.ReadBit("TutorialsEnabled");
            packet.ReadBit("NPETutorialsEnabled");
            packet.ReadBit("TwitterEnabled");
            packet.ReadBit("Unk67");
            packet.ReadBit("WillKickFromWorld");
            packet.ReadBit("KioskModeEnabled");
            packet.ReadBit("CompetitiveModeEnabled");

            var hasRaceClassExpansionLevels = false;
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V8_3_0_33062))
                hasRaceClassExpansionLevels = packet.ReadBit("RaceClassExpansionLevels");

            packet.ReadBit("TokenBalanceEnabled");
            packet.ReadBit("WarModeFeatureEnabled");
            packet.ReadBit("ClubsEnabled");
            packet.ReadBit("ClubsBattleNetClubTypeAllowed");
            packet.ReadBit("ClubsCharacterClubTypeAllowed");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_5_29683))
                packet.ReadBit("ClubsPresenceUpdateEnabled");

            packet.ReadBit("VoiceChatDisabledByParentalControl");
            packet.ReadBit("VoiceChatMutedByParentalControl");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_2_5_31921))
            {
                packet.ReadBit("QuestSessionEnabled");
                packet.ReadBit("Unused825");
                packet.ReadBit("ClubFinderEnabled");
            }

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
                var int88 = packet.ReadUInt32("RaceClassExpansionLevelsCount");
                for (int i = 0; i < int88; i++)
                    packet.ReadByte("RaceClassExpansionLevels", i);
            }

            ReadVoiceChatManagerSettings(packet, "VoiceChatManagerSettings");

            if (hasEuropaTicketSystemStatus)
            {
                packet.ResetBitReader();
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");
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

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V8_2_5_31921))
                packet.ReadBit("NoHandler"); // not accessed in handler

            packet.ReadBit("TrialBoostEnabled");
            packet.ReadBit("TokenBalanceEnabled");
            packet.ReadBit("LiveRegionCharacterListEnabled");
            packet.ReadBit("LiveRegionCharacterCopyEnabled");
            packet.ReadBit("LiveRegionAccountCopyEnabled");

            packet.ReadUInt32("TokenPollTimeSeconds");
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V8_2_5_31921))
                packet.ReadUInt32E<ConsumableTokenRedeem>("TokenRedeemIndex");
            packet.ReadInt64("TokenBalanceAmount");
            packet.ReadInt32("MaxCharactersPerRealm");
            packet.ReadUInt32("BpayStoreProductDeliveryDelay");
            packet.ReadInt32("ActiveCharacterUpgradeBoostType");
            packet.ReadInt32("ActiveClassTrialBoostType");
            packet.ReadInt32("MinimumExpansionLevel");
            packet.ReadInt32("MaximumExpansionLevel");
        }

        [Parser(Opcode.SMSG_START_LIGHTNING_STORM)]
        [Parser(Opcode.SMSG_END_LIGHTNING_STORM)]
        public static void HandleLightningStorm(Packet packet)
        {
            packet.ReadUInt32("LightningStormId");
        }

        [Parser(Opcode.CMSG_QUERY_PLAYER_NAME_BY_COMMUNITY_ID)]
        public static void HandleQueryPlayerNameByCommunityID(Packet packet)
        {
            packet.ReadPackedGuid128("BNetAccountGUID");
            packet.ReadUInt64("CommunityDbID");
        }

        [Parser(Opcode.SMSG_UPDATE_EXPANSION_LEVEL)]
        public static void HandleUpdateExpansionLevel(Packet packet)
        {
            var hasActiveExpansionLevel = packet.ReadBit();
            var hasAccountExpansionLevel = packet.ReadBit();
            var hasUpgradingFromExpansionTrial = packet.ReadBit();

            if (hasUpgradingFromExpansionTrial)
                packet.ReadBit("UpgradingFromExpansionTrial");

            var availableClasses = packet.ReadInt32("AvailableClasses"); // Maybe available races if some special bit is set???

            if (hasActiveExpansionLevel)
                packet.ReadByteE<ClientType>("ActiveExpansionLevel");

            if (hasAccountExpansionLevel)
                packet.ReadByteE<ClientType>("AccountExpansionLevel");

            for (var i = 0; i < availableClasses; i++)
            {
                packet.ReadByteE<Class>("ClassID", i); // Maybe also Race if some special bit is set???
                packet.ReadByteE<ClientType>("RequiredExpansion", i);
            }
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

        [Parser(Opcode.SMSG_WHO)]
        public static void HandleWho(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_3_7_35249) ||
                ClientVersion.IsBurningCrusadeClassicClientVersionBuild(ClientVersion.Build) ||
                ClientVersion.IsWotLKClientVersionBuild(ClientVersion.Build))
                packet.ReadUInt32("RequestID");

            var bits568 = packet.ReadBits("List count", 6);

            for (var i = 0; i < bits568; ++i)
                ReadWhoEntry(packet, i);
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

        [Parser(Opcode.SMSG_SET_CURRENCY)]
        public static void HandleSetCurrency(Packet packet)
        {
            packet.ReadInt32("Type");
            packet.ReadInt32("Quantity");
            packet.ReadUInt32("Flags");

            var hasWeeklyQuantity = packet.ReadBit("HasWeeklyQuantity");
            var hasTrackedQuantity = packet.ReadBit("HasTrackedQuantity");
            var hasMaxQuantity = packet.ReadBit("HasMaxQuantity");
            var hasTotalEarned = false;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_0_1_36216))
                hasTotalEarned = packet.ReadBit("HasTotalEarned");
            packet.ReadBit("SuppressChatLog");
            var hasQuantityChange = false;
            var hasQuantityGainSource = false;
            var hasQuantityLostSource = false;
            var hasFirstCraftOperationID = false;
            var hasNextRechargeTime = false;
            var hasRechargeCyclicStartTime = false;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_5_29683))
            {
                hasQuantityChange = packet.ReadBit("HasQuantityChange");
                hasQuantityLostSource = packet.ReadBit("HasQuantityLostSource");
                hasQuantityGainSource = packet.ReadBit("HasQuantityGainSource");
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_2_46479))
                {
                    hasFirstCraftOperationID = packet.ReadBit("HasFirstCraftOperationID");
                    hasNextRechargeTime = packet.ReadBit("HasNextRechargeTime");
                }
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_1_0_49407))
                    hasRechargeCyclicStartTime = packet.ReadBit("HasRechargeCyclicStartTime");
            }

            if (hasWeeklyQuantity)
                packet.ReadInt32("WeeklyQuantity");

            if (hasTrackedQuantity)
                packet.ReadInt32("TrackedQuantity");

            if (hasMaxQuantity)
                packet.ReadInt32("MaxQuantity");

            if (hasTotalEarned)
                packet.ReadInt32("TotalEarned");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_5_29683))
            {
                if (hasQuantityChange)
                    packet.ReadInt32("QuantityChange");

                if (hasQuantityLostSource)
                    packet.ReadInt32("QuantityLostSource");

                if (hasQuantityGainSource)
                    packet.ReadInt32("QuantityGainSource");
            }
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_2_46479))
            {
                if (hasFirstCraftOperationID)
                    packet.ReadUInt32("FirstCraftOperationID");

                if (hasNextRechargeTime)
                    packet.ReadTime64("NextRechargeTime");

                if (hasRechargeCyclicStartTime)
                    packet.ReadTime64("RechargeCyclicStartTime");
            }
        }

        [Parser(Opcode.SMSG_WORLD_SERVER_INFO, ClientVersionBuild.V8_3_0_33062)]
        public static void HandleWorldServerInfo(Packet packet)
        {
            CoreParsers.MovementHandler.CurrentDifficultyID = packet.ReadUInt32<DifficultyId>("DifficultyID");
            packet.ReadByte("IsTournamentRealm");

            packet.ReadBit("XRealmPvpAlert");
            packet.ReadBit("BlockExitingLoadingScreen");
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

        [Parser(Opcode.SMSG_CLEAR_RESURRECT)]
        public static void HandleClearResurrect(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_WHO)]
        public static void HandleWhoRequest(Packet packet)
        {
            var areaCount = packet.ReadBits(4);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_0_52038))
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
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_0_52038))
                packet.ReadByteE<WhoRequestOrigin>("Origin");

            for (var i = 0; i < areaCount; ++i)
                packet.ReadUInt32<AreaId>("Area", i);
        }
    }
}
