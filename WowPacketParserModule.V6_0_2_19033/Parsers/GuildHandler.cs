using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class GuildHandler
    {
        [Parser(Opcode.CMSG_GUILD_GET_ROSTER)]
        [Parser(Opcode.CMSG_GUILD_BANK_REMAINING_WITHDRAW_MONEY_QUERY)]
        [Parser(Opcode.CMSG_GUILD_CHALLENGE_UPDATE_REQUEST)]
        [Parser(Opcode.CMSG_GUILD_DELETE)]
        [Parser(Opcode.CMSG_GUILD_PERMISSIONS_QUERY)]
        [Parser(Opcode.CMSG_GUILD_REPLACE_GUILD_MASTER)]
        [Parser(Opcode.CMSG_ACCEPT_GUILD_INVITE)]
        [Parser(Opcode.CMSG_GUILD_LEAVE)]
        [Parser(Opcode.CMSG_GUILD_AUTO_DECLINE_INVITATION)]
        [Parser(Opcode.CMSG_GUILD_DECLINE_INVITATION)]
        [Parser(Opcode.CMSG_GUILD_EVENT_LOG_QUERY)]
        [Parser(Opcode.SMSG_GUILD_MEMBER_DAILY_RESET)]
        [Parser(Opcode.SMSG_GUILD_EVENT_BANK_CONTENTS_CHANGED)]
        [Parser(Opcode.SMSG_GUILD_EVENT_RANKS_UPDATED)]
        public static void HandleGuildZero(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_QUERY_GUILD_INFO)]
        public static void HandleGuildQuery(Packet packet)
        {
            packet.ReadPackedGuid128("Guild Guid");
            packet.ReadPackedGuid128("Player Guid");
        }

        [Parser(Opcode.SMSG_QUERY_GUILD_INFO_RESPONSE)]
        public static void HandleGuildQueryResponse(Packet packet)
        {
            packet.ReadPackedGuid128("Guild Guid");

            var hasData = packet.ReadBit();
            if (hasData)
            {
                packet.ReadPackedGuid128("Guild Guid");
                packet.ReadInt32("VirtualRealmAddress");
                var rankCount = packet.ReadInt32("RankCount");
                packet.ReadInt32("EmblemColor");
                packet.ReadInt32("EmblemStyle");
                packet.ReadInt32("BorderColor");
                packet.ReadInt32("BorderStyle");
                packet.ReadInt32("BackgroundColor");

                for (var i = 0; i < rankCount; i++)
                {
                    packet.ReadInt32("RankID", i);
                    packet.ReadInt32("RankOrder", i);

                    packet.ResetBitReader();
                    var rankNameLen = packet.ReadBits(7);
                    packet.ReadWoWString("Rank Name", rankNameLen, i);
                }

                packet.ResetBitReader();
                var nameLen = packet.ReadBits(7);
                packet.ReadWoWString("Guild Name", nameLen);
            }
        }

        [Parser(Opcode.SMSG_GUILD_EVENT_MOTD)]
        public static void HandleEventMotd(Packet packet)
        {
            packet.ReadWoWString("MotdText", (int)packet.ReadBits(10));
        }

        [Parser(Opcode.CMSG_GUILD_BANK_BUY_TAB)]
        public static void HandleGuildBankBuyTab(Packet packet)
        {
            packet.ReadPackedGuid128("Banker");
            packet.ReadByte("BankTab");
        }

        [Parser(Opcode.CMSG_GUILD_GET_RANKS)]
        [Parser(Opcode.CMSG_GUILD_QUERY_RECIPES)]
        public static void HandleGuildGuildGUID(Packet packet)
        {
            packet.ReadPackedGuid128("GuildGUID");
        }

        [Parser(Opcode.SMSG_GUILD_RANKS)]
        public static void HandleGuildRankServer434(Packet packet)
        {
            var count = packet.ReadUInt32("Count");

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("RankID", i);
                packet.ReadInt32("RankOrder", i);
                packet.ReadInt32("Flags", i);
                packet.ReadInt32("WithdrawGoldLimit", i);

                for (var j = 0; j < 8; ++j)
                {
                    packet.ReadInt32E<GuildBankRightsFlag>("TabFlags", i, j);
                    packet.ReadInt32("TabWithdrawItemLimit", i, j);
                }

                packet.ResetBitReader();
                var bits8 = (int)packet.ReadBits(7);
                packet.ReadWoWString("RankName", bits8, i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_ROSTER)]
        public static void HandleGuildRoster(Packet packet)
        {
            packet.ReadUInt32("NumAccounts");
            packet.ReadPackedTime("CreateDate");
            packet.ReadUInt32("GuildFlags");
            var int20 = packet.ReadUInt32("MemberDataCount");

            for (var i = 0; i < int20; ++i)
            {
                packet.ReadPackedGuid128("Guid", i);

                packet.ReadUInt32("RankID", i);
                packet.ReadUInt32("AreaID", i);
                packet.ReadUInt32("PersonalAchievementPoints", i);
                packet.ReadUInt32("GuildReputation", i);

                packet.ReadSingle("LastSave", i);

                for (var j = 0; j < 2; ++j)
                {
                    packet.ReadUInt32("DbID", i, j);
                    packet.ReadUInt32("Rank", i, j);
                    packet.ReadUInt32("Step", i, j);
                }

                packet.ReadUInt32("VirtualRealmAddress", i);

                packet.ReadByteE<GuildMemberFlag>("Status", i);
                packet.ReadByte("Level", i);
                packet.ReadByteE<Class>("ClassID", i);
                packet.ReadByteE<Gender>("Gender", i);

                packet.ResetBitReader();

                var bits36 = packet.ReadBits(6);
                var bits92 = packet.ReadBits(8);
                var bits221 = packet.ReadBits(8);

                packet.ReadBit("Authenticated", i);
                packet.ReadBit("SorEligible", i);

                packet.ReadWoWString("Name", bits36, i);
                packet.ReadWoWString("Note", bits92, i);
                packet.ReadWoWString("OfficerNote", bits221, i);
            }

            packet.ResetBitReader();
            var bits2037= packet.ReadBits(10);
            var bits9 = packet.ReadBits(11);

            packet.ReadWoWString("WelcomeText", bits2037);
            packet.ReadWoWString("InfoText", bits9);
        }

        [Parser(Opcode.SMSG_GUILD_NEWS)]
        public static void HandleGuildUpdateRoster(Packet packet)
        {
            var count = packet.ReadInt32("NewsCount");

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("Id", i);
                packet.ReadPackedTime("CompletedDate", i);
                packet.ReadInt32("Type", i);
                packet.ReadInt32("Flags", i);

                for (var j = 0; j < 2; ++j)
                    packet.ReadInt32("Data", i, j);

                packet.ReadPackedGuid128("MemberGuid", i);

                var int64 = packet.ReadInt32("MemberListCount", i);

                for (var j = 0; j < int64; ++j)
                    packet.ReadPackedGuid128("MemberList", i, j);

                packet.ResetBitReader();

                var bit80 = packet.ReadBit("HasItemInstance", i);
                if (bit80)
                    ItemHandler.ReadItemInstance(packet, i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_EVENT_PRESENCE_CHANGE)]
        public static void HandleGuildEventPresenceChange(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");

            packet.ReadInt32("VirtualRealmAddress");

            var bits38 = packet.ReadBits(6);
            packet.ReadBit("LoggedOn");
            packet.ReadBit("Mobile");

            packet.ReadWoWString("Name", bits38);
        }

        [Parser(Opcode.SMSG_GUILD_KNOWN_RECIPES)]
        public static void HandleGuildRecipes(Packet packet)
        {
            var count = packet.ReadInt32("Criteria count");

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("Skill Id", i);
                packet.ReadBytes("Skill Bits", 300, i);
            }
        }

        [Parser(Opcode.CMSG_REQUEST_GUILD_PARTY_STATE)]
        public static void HandleGuildUpdatePartyState(Packet packet)
        {
            packet.ReadPackedGuid128("GuildGUID");
        }

        [Parser(Opcode.SMSG_GUILD_PARTY_STATE)]
        public static void HandleGuildPartyStateResponse(Packet packet)
        {
            packet.ReadBit("Is guild group");
            packet.ReadUInt32("Current guild members");
            packet.ReadUInt32("Needed guild members");
            packet.ReadSingle("Guild XP multiplier");
        }

        [Parser(Opcode.SMSG_ALL_GUILD_ACHIEVEMENTS)]
        public static void HandleGuildAchievementData(Packet packet)
        {
            var int10 = packet.ReadUInt32("EarnedAchievementCount");
            for (var i = 0; i < int10; ++i)
            {
                packet.ReadInt32("Id", i);
                packet.ReadPackedTime("Date", i);
                packet.ReadPackedGuid128("Owner", i);
                packet.ReadInt32("VirtualRealmAddress", i);
                packet.ReadInt32("NativeRealmAddress", i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_PERMISSIONS_QUERY_RESULTS)]
        public static void HandleGuildPermissionsQueryResult(Packet packet)
        {
            packet.ReadInt32("RankID");
            packet.ReadInt32("WithdrawGoldLimit");
            packet.ReadUInt32E<GuildRankRightsFlag>("Flags");
            packet.ReadUInt32("NumTabs");

            var int16 = packet.ReadInt32("TabCount");

            for (var i = 0; i < int16; i++)
            {
                packet.ReadInt32E<GuildBankRightsFlag>("Flags", i);
                packet.ReadInt32("WithdrawItemLimit", i);
            }
        }

        [Parser(Opcode.CMSG_GUILD_INVITE_BY_NAME)]
        public static void HandleGuildInviteByName(Packet packet)
        {
            var bits16 = packet.ReadBits(9);
            packet.ReadWoWString("Name", bits16);
        }

        [Parser(Opcode.SMSG_GUILD_CRITERIA_UPDATE)]
        public static void HandleGuildCriteriaUpdate(Packet packet)
        {
            var int16 = packet.ReadUInt32("ProgressCount");
            for (int i = 0; i < int16; i++)
            {
                packet.ReadInt32("CriteriaID", i);
                packet.ReadTime("DateCreated", i);
                packet.ReadTime("DateStarted", i);
                packet.ReadTime("DateUpdated", i);
                packet.ReadInt64("Quantity", i);
                packet.ReadPackedGuid128("PlayerGUID", i);

                packet.ReadInt32("Flags", i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_REWARD_LIST)]
        public static void HandleGuildRewardsList(Packet packet)
        {
            packet.ReadTime("Version");

            var size = packet.ReadUInt32("RewardItemsCount");
            for (int i = 0; i < size; i++)
            {
                packet.ReadUInt32("ItemID", i);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_0_19678))
                    packet.ReadUInt32("Unk4", i);

                var int1 = packet.ReadInt32("AchievementsRequiredCount", i);
                packet.ReadUInt32("RaceMask", i);
                packet.ReadInt32("MinGuildLevel", i);
                packet.ReadInt32("MinGuildRep", i);
                packet.ReadInt64("Cost", i);

                for (int j = 0; j < int1; j++)
                    packet.ReadInt32("AchievementsRequired", i, j);
            }
        }

        [Parser(Opcode.SMSG_GUILD_EVENT_BANK_MONEY_CHANGED)]
        public static void HandleGuildEventBankMoneyChanged(Packet packet)
        {
            packet.ReadUInt64("Money");
        }

        [Parser(Opcode.SMSG_GUILD_INVITE)]
        public static void HandleGuildInvite(Packet packet)
        {
            var bits149 = packet.ReadBits(6);
            var bits216 = packet.ReadBits(7);
            var bits52 = packet.ReadBits(7);

            packet.ReadInt32("InviterVirtualRealmAddress");
            packet.ReadUInt32("GuildVirtualRealmAddress");
            packet.ReadPackedGuid128("GuildGUID");
            packet.ReadUInt32("OldGuildVirtualRealmAddress");
            packet.ReadPackedGuid128("OldGuildGUID");
            packet.ReadUInt32("EmblemColor");
            packet.ReadUInt32("EmblemStyle");
            packet.ReadUInt32("BorderColor");
            packet.ReadUInt32("BorderStyle");
            packet.ReadUInt32("BackgroundColor");
            packet.ReadInt32("Level");

            packet.ReadWoWString("InviterName", bits149);
            packet.ReadWoWString("OldGuildName", bits216);
            packet.ReadWoWString("GuildName", bits52);
        }

        [Parser(Opcode.SMSG_GUILD_BANK_QUERY_RESULTS)]
        public static void HandleGuildBankList(Packet packet)
        {
            packet.ReadUInt64("Money");
            packet.ReadInt32("Tab");
            packet.ReadInt32("WithdrawalsRemaining");

            var int36 = packet.ReadInt32("TabInfoCount");
            var int16 = packet.ReadInt32("ItemInfoCount");

            for (int i = 0; i < int36; i++)
            {
                packet.ReadInt32("TabIndex", i);

                packet.ResetBitReader();

                var bits1 = packet.ReadBits(7);
                var bits69 = packet.ReadBits(9);

                packet.ReadWoWString("Name", bits1, i);
                packet.ReadWoWString("Icon", bits69, i);
            }

            for (int i = 0; i < int16; i++)
            {
                packet.ReadInt32("Slot", i);
                ItemHandler.ReadItemInstance(packet, i);

                packet.ReadInt32("Count", i);
                packet.ReadInt32("EnchantmentID", i);
                packet.ReadInt32("Charges", i);
                packet.ReadInt32("OnUseEnchantmentID", i);
                var int76 = packet.ReadInt32("SocketEnchant", i);
                packet.ReadInt32("Flags", i);

                for (int j = 0; j < int76; j++)
                {
                    packet.ReadInt32("SocketIndex", i, j);
                    packet.ReadInt32("SocketEnchantID", i, j);
                }

                packet.ResetBitReader();
                packet.ReadBit("Locked");
            }

            packet.ResetBitReader();
            packet.ReadBit("FullUpdate");
        }

        [Parser(Opcode.SMSG_GUILD_BANK_LOG_QUERY_RESULTS)]
        public static void HandleGuildBankLogQueryResult(Packet packet)
        {
            packet.ReadInt32("Tab");
            var int32 = packet.ReadInt32("GuildBankLogEntryCount");
            for (int i = 0; i < int32; i++)
            {
                packet.ReadPackedGuid128("PlayerGUID", i);
                packet.ReadInt32("TimeOffset", i);
                packet.ReadSByte("EntryType", i);

                packet.ResetBitReader();

                var bit33 = packet.ReadBit("HasMoney", i);
                var bit44 = packet.ReadBit("HasItemID", i);
                var bit52 = packet.ReadBit("HasCount", i);
                var bit57 = packet.ReadBit("HasOtherTab", i);

                if (bit33)
                    packet.ReadInt64("Money", i);

                if (bit44)
                    packet.ReadInt32("ItemID", i);

                if (bit52)
                    packet.ReadInt32("Count", i);

                if (bit57)
                    packet.ReadSByte("OtherTab", i);

            }

            packet.ResetBitReader();
            var bit24 = packet.ReadBit("HasWeeklyBonusMoney");
            if (bit24)
                packet.ReadInt64("WeeklyBonusMoney");
        }

        [Parser(Opcode.SMSG_GUILD_CHALLENGE_UPDATE)]
        public static void HandleGuildChallengeUpdated(Packet packet)
        {
            for (int i = 0; i < 6; ++i)
                packet.ReadInt32("CurrentCount", i);

            for (int i = 0; i < 6; ++i)
                packet.ReadInt32("MaxCount", i);

            for (int i = 0; i < 6; ++i)
                packet.ReadInt32("Gold", i);

            for (int i = 0; i < 6; ++i)
                packet.ReadInt32("MaxLevelGold", i);
        }

        [Parser(Opcode.SMSG_GUILD_SEND_RANK_CHANGE)]
        public static void HandleGuildRanksUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("Officer");
            packet.ReadPackedGuid128("Other");
            packet.ReadInt32("RankID");
            packet.ReadBit("Promote");
        }

        [Parser(Opcode.SMSG_GUILD_EVENT_RANK_CHANGED)]
        public static void HandleGuildEventRankChanged(Packet packet)
        {
            packet.ReadInt32("RankID");
        }

        [Parser(Opcode.CMSG_GUILD_SET_ACHIEVEMENT_TRACKING)]
        public static void HandleGuildSetAchievementTracking(Packet packet)
        {
            var count = packet.ReadUInt32("Count");
            for (var i = 0; i < count; ++i)
                packet.ReadInt32<AchievementId>("AchievementIDs", i);
        }

        [Parser(Opcode.SMSG_GUILD_COMMAND_RESULT)]
        public static void HandleGuildCommandResult(Packet packet)
        {
            packet.ReadUInt32E<GuildCommandError>("Result");
            packet.ReadUInt32E<GuildCommandType>("Command");
            var len = packet.ReadBits(8);
            packet.ReadWoWString("Name", len);
        }

        [Parser(Opcode.SMSG_GUILD_NAME_CHANGED)]
        public static void HandleGuildNameChanged(Packet packet)
        {
            packet.ReadPackedGuid128("GuildGUID");

            var len = packet.ReadBits(7);
            packet.ReadWoWString("GuildName", len);
        }

        [Parser(Opcode.CMSG_GUILD_BANK_QUERY_TAB)]
        public static void HandleGuildBankQueryTab(Packet packet)
        {
            packet.ReadPackedGuid128("Banker");
            packet.ReadByte("Tab");

            packet.ResetBitReader();
            packet.ReadBit("FullUpdate");
        }

        [Parser(Opcode.CMSG_GUILD_BANK_UPDATE_TAB)]
        public static void HandleGuildBankUpdateTab(Packet packet)
        {
            packet.ReadPackedGuid128("Banker");
            packet.ReadByte("BankTab");

            packet.ResetBitReader();
            var nameLen = packet.ReadBits(7);
            var iconLen = packet.ReadBits(9);

            packet.ReadWoWString("Name", nameLen);
            packet.ReadWoWString("Icon", iconLen);
        }

        [Parser(Opcode.SMSG_GUILD_EVENT_LOG_QUERY_RESULTS)]
        public static void HandleGuildEventLogQueryResults(Packet packet)
        {
            var eventCount = packet.ReadInt32("EventEntryCount");
            for (int i = 0; i < eventCount; i++)
            {
                packet.ReadPackedGuid128("PlayerGUID", i);
                packet.ReadPackedGuid128("OtherGUID", i);
                packet.ReadByte("TransactionType", i);
                packet.ReadByte("RankID", i);
                packet.ReadUInt32("TransactionDate", i);
            }
        }

        [Parser(Opcode.CMSG_GUILD_QUERY_NEWS)]
        public static void HandleGuildQueryNews(Packet packet)
        {
            packet.ReadPackedGuid128("GuildGUID");
        }

        [Parser(Opcode.CMSG_GUILD_BANK_ACTIVATE)]
        public static void HandleGuildBankActivate(Packet packet)
        {
            packet.ReadPackedGuid128("Banker");

            packet.ResetBitReader();
            packet.ReadBit("FullUpdate");
        }

        [Parser(Opcode.CMSG_GUILD_SET_RANK_PERMISSIONS)]
        public static void HandlelGuildSetRankPermissions(Packet packet)
        {
            packet.ReadInt32("RankID");
            packet.ReadInt32("RankOrder");
            packet.ReadUInt32E<GuildRankRightsFlag>("Flags");
            packet.ReadUInt32E<GuildRankRightsFlag>("OldFlags");
            packet.ReadInt32("WithdrawGoldLimit");

            for (var i = 0; i < 8; ++i)
            {
                packet.ReadInt32E<GuildBankRightsFlag>("TabFlags", i);
                packet.ReadInt32("TabWithdrawItemLimit", i);
            }

            packet.ResetBitReader();
            var rankNameLen = packet.ReadBits(7);

            packet.ReadWoWString("RankName", rankNameLen);
        }

        [Parser(Opcode.CMSG_REQUEST_GUILD_REWARDS_LIST)]
        public static void HandleRequestGuildRewardsList(Packet packet)
        {
            packet.ReadTime("CurrentVersion");
        }

        [Parser(Opcode.SMSG_LF_GUILD_POST)]
        public static void HandleLFGuildPost(Packet packet)
        {
            var hasGuildPostData = packet.ReadBit("HasGuildPostData");
            if (hasGuildPostData)
            {
                packet.ResetBitReader();
                packet.ReadBit("Active");
                var len = packet.ReadBits(10);

                packet.ReadInt32("PlayStyle");
                packet.ReadInt32("Availability");
                packet.ReadInt32("ClassRoles");
                packet.ReadInt32("LevelRange");
                packet.ReadInt32("SecondsRemaining");

                packet.ReadWoWString("Comment", len);
            }
        }

        [Parser(Opcode.SMSG_GUILD_CHALLENGE_COMPLETED)]
        public static void HandleGuildChallengeCompleted(Packet packet)
        {
            packet.ReadInt32("ChallengeType");
            packet.ReadInt32("CurrentCount");
            packet.ReadInt32("MaxCount");
            packet.ReadInt32("GoldAwarded");
        }

        [Parser(Opcode.SMSG_GUILD_REPUTATION_REACTION_CHANGED)]
        public static void HandleGuildReputationReactionChanged(Packet packet)
        {
            packet.ReadPackedGuid128("MemberGUID");
        }

        [Parser(Opcode.SMSG_GUILD_ACHIEVEMENT_EARNED)]
        public static void HandleGuildAchievementEarned(Packet packet)
        {
            packet.ReadPackedGuid128("GuildGUID");
            packet.ReadInt32<AchievementId>("AchievementID");
            packet.ReadPackedTime("TimeEarned");
        }

        [Parser(Opcode.SMSG_GUILD_EVENT_PLAYER_JOINED)]
        public static void HandleGuildEventPlayerJoined(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadInt32("VirtualRealmAddress");

            var len = packet.ReadBits(6);
            packet.ReadWoWString("Name", len);
        }

        [Parser(Opcode.SMSG_GUILD_EVENT_PLAYER_LEFT)]
        public static void HandleGuildEventPlayerLeft(Packet packet)
        {
            var hasRemoved = packet.ReadBit("Removed");
            var lenLeaverName = packet.ReadBits(6);

            if (hasRemoved)
            {
                var lenRemoverName = packet.ReadBits(6);
                packet.ReadPackedGuid128("RemoverGUID");
                packet.ReadInt32("RemoverVirtualRealmAddress");
                packet.ReadWoWString("RemoverName", lenRemoverName);
            }

            packet.ReadPackedGuid128("LeaverGUID");
            packet.ReadInt32("LeaverVirtualRealmAddress");
            packet.ReadWoWString("LeaverName", lenLeaverName);
        }

        public static void ReadLFGuildRecruitData(Packet packet, params object[] indexes)
        {
            packet.ReadPackedGuid128("RecruitGUID", indexes);

            packet.ReadInt32("RecruitVirtualRealm", indexes);

            packet.ReadInt32("CharacterClass", indexes);
            packet.ReadInt32("CharacterGender", indexes);
            packet.ReadInt32("CharacterLevel", indexes);

            packet.ReadInt32("ClassRoles", indexes);
            packet.ReadInt32("PlayStyle", indexes);
            packet.ReadInt32("Availability", indexes);
            packet.ReadInt32("SecondsSinceCreated", indexes);
            packet.ReadInt32("SecondsUntilExpiration", indexes);

            packet.ResetBitReader();

            var lenName = packet.ReadBits(6);
            var lenComment = packet.ReadBits(10);

            packet.ReadWoWString("Name", lenName, indexes);
            packet.ReadWoWString("Comment", lenComment, indexes);
        }

        public static void ReadLFGuildApplicationData(Packet packet, params object[] indexes)
        {
            packet.ReadPackedGuid128("GuildGUID", indexes);
            packet.ReadInt32("GuildVirtualRealm", indexes);

            packet.ReadInt32("ClassRoles", indexes);
            packet.ReadInt32("PlayStyle", indexes);
            packet.ReadInt32("Availability", indexes);
            packet.ReadInt32("SecondsSinceCreated", indexes);

            packet.ResetBitReader();

            var lenName = packet.ReadBits(7);
            var lenComment = packet.ReadBits(10);

            packet.ReadWoWString("GuildName", lenName, indexes);
            packet.ReadWoWString("Comment", lenComment, indexes);
        }

        [Parser(Opcode.SMSG_LF_GUILD_RECRUITS)]
        public static void HandleLFGuildRecruits(Packet packet)
        {
            var recruitsCount = packet.ReadInt32("LFGuildRecruitDataCount");
            packet.ReadTime("UpdateTime");
            for (int i = 0; i < recruitsCount; i++)
                ReadLFGuildRecruitData(packet, i, "LFGuildRecruitData");
        }

        [Parser(Opcode.SMSG_LF_GUILD_APPLICATIONS)]
        public static void HandleLFGuildApplications(Packet packet)
        {
            packet.ReadInt32("NumRemaining");
            var applicationCount = packet.ReadInt32("LFGuildApplicationDataCount");
            for (int i = 0; i < applicationCount; i++)
                ReadLFGuildApplicationData(packet, i, "LFGuildApplicationData");
        }

        [Parser(Opcode.CMSG_PETITION_SHOW_LIST)]
        public static void HandlePetitionShowListClient(Packet packet)
        {
            packet.ReadPackedGuid128("PetitionUnit");
        }

        [Parser(Opcode.SMSG_PETITION_SHOW_LIST)]
        public static void HandlePetitionShowList(Packet packet)
        {
            packet.ReadPackedGuid128("Unit");
            packet.ReadUInt32("Price");
        }

        [Parser(Opcode.CMSG_PETITION_BUY)]
        public static void HandlePetitionBuy(Packet packet)
        {
            var length = packet.ReadBits(7);
            packet.ReadPackedGuid128("Unit");
            packet.ReadWoWString("Title", length);
        }

        [Parser(Opcode.CMSG_SIGN_PETITION)]
        public static void HandleSignPetition(Packet packet)
        {
            packet.ReadPackedGuid128("PetitionGUID");
            packet.ReadByte("Choice");
        }

        [Parser(Opcode.CMSG_QUERY_PETITION)]
        public static void HandleQueryPetition(Packet packet)
        {
            packet.ReadUInt32("PetitionID");
            packet.ReadPackedGuid128("ItemGUID");
        }

        [Parser(Opcode.CMSG_GUILD_SET_FOCUSED_ACHIEVEMENT)]
        public static void HandleGuildSetFocusedAchievement(Packet packet)
        {
            packet.ReadUInt32("AchievementID");
        }

        [Parser(Opcode.CMSG_GUILD_QUERY_MEMBER_RECIPES)]
        public static void HandleGuildQueryMemberRecipes(Packet packet)
        {
            packet.ReadPackedGuid128("GuildMember");
            packet.ReadPackedGuid128("GuildGUID");
            packet.ReadUInt32("SkillLineID");
        }

        [Parser(Opcode.SMSG_PETITION_SIGN_RESULTS)]
        public static void HandlePetitionSignResults(Packet packet)
        {
            packet.ReadPackedGuid128("Item");
            packet.ReadPackedGuid128("Player");
            packet.ReadBits("Error", 4);
        }

        public static void ReadPetitionSignature(Packet packet, params object[] indexes)
        {
            packet.ReadPackedGuid128("Signer", indexes);
            packet.ReadInt32("Choice", indexes);
        }

        [Parser(Opcode.SMSG_PETITION_SHOW_SIGNATURES)]
        public static void HandlePetitionShowSignatures(Packet packet)
        {
            packet.ReadPackedGuid128("Item");
            packet.ReadPackedGuid128("Owner");
            packet.ReadPackedGuid128("OwnerWoWAccount");
            packet.ReadInt32("PetitionID");

            var signaturesCount = packet.ReadInt32("SignaturesCount");
            for (int i = 0; i < signaturesCount; i++)
                ReadPetitionSignature(packet, i, "PetitionSignature");
        }

        public static void ReadPetitionInfo(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("PetitionID", indexes);
            packet.ReadPackedGuid128("Petitioner", indexes);

            packet.ReadInt32("MinSignatures", indexes);
            packet.ReadInt32("MaxSignatures", indexes);
            packet.ReadInt32("DeadLine", indexes);
            packet.ReadInt32("IssueDate", indexes);

            packet.ReadInt32("AllowedGuildID", indexes);
            packet.ReadInt32("AllowedClasses", indexes);
            packet.ReadInt32("AllowedRaces", indexes);
            packet.ReadInt16("AllowedGender", indexes);
            packet.ReadInt32("AllowedMinLevel", indexes);
            packet.ReadInt32("AllowedMaxLevel", indexes);

            packet.ReadInt32("NumChoices", indexes);
            packet.ReadInt32("StaticType", indexes);
            packet.ReadUInt32("Muid", indexes);

            packet.ResetBitReader();

            var lenTitle = packet.ReadBits(7);
            var lenBodyText = packet.ReadBits(12);

            var lenChoicetext = new uint[10];
            for (int i = 0; i < 10; i++)
                lenChoicetext[i] = packet.ReadBits(6);
            for (int i = 0; i < 10; i++)
                packet.ReadWoWString("Choicetext", lenChoicetext[i]);

            packet.ReadWoWString("Title", lenTitle);
            packet.ReadWoWString("BodyText", lenBodyText);
        }

        [Parser(Opcode.SMSG_QUERY_PETITION_RESPONSE)]
        public static void HandleQueryPetitionResponse(Packet packet)
        {
            packet.ReadInt32("PetitionID");

            var hasAllow = packet.ReadBit("Allow");
            if (hasAllow)
                ReadPetitionInfo(packet, "PetitionInfo");
        }

        [Parser(Opcode.SMSG_GUILD_MEMBER_RECIPES)]
        public static void HandleGuildMemberRecipes(Packet packet)
        {
            packet.ReadPackedGuid128("Member");
            packet.ReadInt32("SkillLineID");
            packet.ReadInt32("SkillStep");
            packet.ReadInt32("SkillRank");
            for (int i = 0; i < 0x12C; i++)
                packet.ReadByte("SkillLineBitArray", i);
        }

        [Parser(Opcode.SMSG_PETITION_DECLINED)]
        public static void HandlePetitionDeclined(Packet packet)
        {
            packet.ReadPackedGuid128("Decliner");
        }

        [Parser(Opcode.SMSG_PETITION_RENAME_GUILD_RESPONSE)]
        public static void HandlePetitionRenameGuildResponse(Packet packet)
        {
            packet.ReadPackedGuid128("PetitionGuid");
            var length = packet.ReadBits("NewGuildNameLength", 7);
            packet.ResetBitReader();

            packet.ReadWoWString("NewGuildName", length);
        }

        [Parser(Opcode.SMSG_TURN_IN_PETITION_RESULT)]
        public static void HandlePetitionTurnInResults(Packet packet)
        {
            packet.ReadBitsE<PetitionResultType>("Result", 4);
        }

        [Parser(Opcode.SMSG_GUILD_INVITE_DECLINED)]
        public static void HandleGuildInviteDeclined(Packet packet)
        {
            var nameLength = packet.ReadBits("NameLength", 6);
            packet.ReadBit("AutoDecline");

            packet.ResetBitReader();

            packet.ReadUInt32("VirtualRealmAddress");
            packet.ReadWoWString("Name", nameLength);
        }

        [Parser(Opcode.SMSG_GUILD_BANK_TEXT_QUERY_RESULT)]
        public static void HandleGuildQueryBankText434(Packet packet)
        {
            packet.ReadInt32("Tab");

            var textLength = packet.ReadBits("TextLength", 14);
            packet.ResetBitReader();

            packet.ReadWoWString("Text", textLength);
        }

        [Parser(Opcode.SMSG_GUILD_EVENT_TAB_MODIFIED)]
        public static void HandleGuildEventTabModified(Packet packet)
        {
            packet.ReadInt32("Tab");

            var nameLength = packet.ReadBits("NameLength", 7);
            var iconLength = packet.ReadBits("IconLength", 9);
            packet.ResetBitReader();

            packet.ReadWoWString("Name", nameLength);
            packet.ReadWoWString("Icon", iconLength);
        }

        public static void ReadLFGuildBrowseData(Packet packet, params object[] idx)
        {
            var guildNameLength = packet.ReadBits("GuildNameLength", 7, idx);
            var commentLength = packet.ReadBits("CommentLength", 10, idx);
            packet.ResetBitReader();

            packet.ReadPackedGuid128("GuildGUID", idx);

            packet.ReadUInt32("GuildVirtualRealm", idx);
            // packet.ReadInt32("GuildLevel", idx);
            packet.ReadInt32("GuildMembers", idx);
            packet.ReadInt32("GuildAchievementPoints", idx);
            packet.ReadInt32E<GuildFinderOptionsInterest>("PlayStyle", idx);
            packet.ReadInt32E<GuildFinderOptionsAvailability>("Availability", idx);
            packet.ReadInt32E<GuildFinderOptionsRoles>("ClassRoles", idx);
            packet.ReadInt32E<GuildFinderOptionsLevel>("LevelRange", idx);
            packet.ReadInt32("EmblemStyle", idx);
            packet.ReadInt32("EmblemColor", idx);
            packet.ReadInt32("BorderStyle", idx);
            packet.ReadInt32("BorderColor", idx);
            packet.ReadInt32("Background", idx);

            packet.ReadSByte("Cached", idx);
            packet.ReadSByte("MembershipRequested", idx);

            packet.ReadWoWString("GuildName", guildNameLength);
            packet.ReadWoWString("CommentLength", commentLength);
        }

        [Parser(Opcode.SMSG_LF_GUILD_BROWSE)]
        public static void HandleLFGuildBrowse(Packet packet)
        {
            var count = packet.ReadInt32("PostCount");

            for (var i = 0; i < count; ++i)
                ReadLFGuildBrowseData(packet, "Post", i);
        }
        
        [Parser(Opcode.CMSG_LF_GUILD_SET_GUILD_POST, ClientVersionBuild.V6_1_2_19802)]
        public static void HandleGuildFinderSetGuildPost612(Packet packet)
        {
            packet.ReadUInt32E<GuildFinderOptionsInterest>("Guild Interests"); // ok
            packet.ReadUInt32E<GuildFinderOptionsAvailability>("Availability"); // ok
            packet.ReadUInt32E<GuildFinderOptionsRoles>("Class Roles"); // ok
            packet.ReadUInt32E<GuildFinderOptionsLevel>("Level");
            packet.ReadBit("Listed");
            packet.ReadWoWString("Comment", packet.ReadBits(10));
        }

        [Parser(Opcode.CMSG_SAVE_GUILD_EMBLEM)]
        public static void HandleSaveGuildEmblem(Packet packet) 
        {
            packet.ReadPackedGuid128("Vendor");
            packet.ReadUInt32("EColor");
            packet.ReadUInt32("EStyle");
            packet.ReadUInt32("BColor");
            packet.ReadUInt32("BStyle");
            packet.ReadUInt32("Bg");
        }

        [Parser(Opcode.SMSG_PLAYER_SAVE_GUILD_EMBLEM)]
        public static void HandlePlayerSaveGuildEmblem(Packet packet)
        {
            packet.ReadInt32E<GuildEmblemError>("Error");
        }

        [Parser(Opcode.CMSG_GUILD_BANK_SWAP_ITEMS)]
        public static void HandleGuildBankSwapItems(Packet packet)
        {
            packet.ReadPackedGuid128("GuildBankSwapItems");
            packet.ReadByte("BankTab");
            packet.ReadByte("BankSlot");
            packet.ReadInt32("ItemID");
            packet.ReadByte("BankTab1");
            packet.ReadByte("BankSlot1");
            packet.ReadInt32("ItemID1");
            packet.ReadInt32("BankItemCount");
            packet.ReadByte("ContainerSlot");
            packet.ReadByte("ContainerItemSlot");
            packet.ReadByte("ToSlot");
            packet.ReadInt32("StackCount");

            packet.ResetBitReader();

            packet.ReadBit("BankOnly");
            packet.ReadBit("AutoStore");
        }
    }
}
