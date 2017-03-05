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
            packet.Translator.ReadPackedGuid128("Guild Guid");
            packet.Translator.ReadPackedGuid128("Player Guid");
        }

        [Parser(Opcode.SMSG_QUERY_GUILD_INFO_RESPONSE)]
        public static void HandleGuildQueryResponse(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Guild Guid");

            var hasData = packet.Translator.ReadBit();
            if (hasData)
            {
                packet.Translator.ReadPackedGuid128("Guild Guid");
                packet.Translator.ReadInt32("VirtualRealmAddress");
                var rankCount = packet.Translator.ReadInt32("RankCount");
                packet.Translator.ReadInt32("EmblemColor");
                packet.Translator.ReadInt32("EmblemStyle");
                packet.Translator.ReadInt32("BorderColor");
                packet.Translator.ReadInt32("BorderStyle");
                packet.Translator.ReadInt32("BackgroundColor");

                for (var i = 0; i < rankCount; i++)
                {
                    packet.Translator.ReadInt32("RankID", i);
                    packet.Translator.ReadInt32("RankOrder", i);

                    packet.Translator.ResetBitReader();
                    var rankNameLen = packet.Translator.ReadBits(7);
                    packet.Translator.ReadWoWString("Rank Name", rankNameLen, i);
                }

                packet.Translator.ResetBitReader();
                var nameLen = packet.Translator.ReadBits(7);
                packet.Translator.ReadWoWString("Guild Name", nameLen);
            }
        }

        [Parser(Opcode.SMSG_GUILD_EVENT_MOTD)]
        public static void HandleEventMotd(Packet packet)
        {
            packet.Translator.ReadWoWString("MotdText", (int)packet.Translator.ReadBits(10));
        }

        [Parser(Opcode.CMSG_GUILD_BANK_BUY_TAB)]
        public static void HandleGuildBankBuyTab(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Banker");
            packet.Translator.ReadByte("BankTab");
        }

        [Parser(Opcode.CMSG_GUILD_GET_RANKS)]
        [Parser(Opcode.CMSG_GUILD_QUERY_RECIPES)]
        public static void HandleGuildGuildGUID(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("GuildGUID");
        }

        [Parser(Opcode.SMSG_GUILD_RANKS)]
        public static void HandleGuildRankServer434(Packet packet)
        {
            var count = packet.Translator.ReadUInt32("Count");

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt32("RankID", i);
                packet.Translator.ReadInt32("RankOrder", i);
                packet.Translator.ReadInt32("Flags", i);
                packet.Translator.ReadInt32("WithdrawGoldLimit", i);

                for (var j = 0; j < 8; ++j)
                {
                    packet.Translator.ReadInt32E<GuildBankRightsFlag>("TabFlags", i, j);
                    packet.Translator.ReadInt32("TabWithdrawItemLimit", i, j);
                }

                packet.Translator.ResetBitReader();
                var bits8 = (int)packet.Translator.ReadBits(7);
                packet.Translator.ReadWoWString("RankName", bits8, i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_ROSTER)]
        public static void HandleGuildRoster(Packet packet)
        {
            packet.Translator.ReadUInt32("NumAccounts");
            packet.Translator.ReadPackedTime("CreateDate");
            packet.Translator.ReadUInt32("GuildFlags");
            var int20 = packet.Translator.ReadUInt32("MemberDataCount");

            for (var i = 0; i < int20; ++i)
            {
                packet.Translator.ReadPackedGuid128("Guid", i);

                packet.Translator.ReadUInt32("RankID", i);
                packet.Translator.ReadUInt32("AreaID", i);
                packet.Translator.ReadUInt32("PersonalAchievementPoints", i);
                packet.Translator.ReadUInt32("GuildReputation", i);

                packet.Translator.ReadSingle("LastSave", i);

                for (var j = 0; j < 2; ++j)
                {
                    packet.Translator.ReadUInt32("DbID", i, j);
                    packet.Translator.ReadUInt32("Rank", i, j);
                    packet.Translator.ReadUInt32("Step", i, j);
                }

                packet.Translator.ReadUInt32("VirtualRealmAddress", i);

                packet.Translator.ReadByteE<GuildMemberFlag>("Status", i);
                packet.Translator.ReadByte("Level", i);
                packet.Translator.ReadByteE<Class>("ClassID", i);
                packet.Translator.ReadByteE<Gender>("Gender", i);

                packet.Translator.ResetBitReader();

                var bits36 = packet.Translator.ReadBits(6);
                var bits92 = packet.Translator.ReadBits(8);
                var bits221 = packet.Translator.ReadBits(8);

                packet.Translator.ReadBit("Authenticated", i);
                packet.Translator.ReadBit("SorEligible", i);

                packet.Translator.ReadWoWString("Name", bits36, i);
                packet.Translator.ReadWoWString("Note", bits92, i);
                packet.Translator.ReadWoWString("OfficerNote", bits221, i);
            }

            packet.Translator.ResetBitReader();
            var bits2037= packet.Translator.ReadBits(10);
            var bits9 = packet.Translator.ReadBits(11);

            packet.Translator.ReadWoWString("WelcomeText", bits2037);
            packet.Translator.ReadWoWString("InfoText", bits9);
        }

        [Parser(Opcode.SMSG_GUILD_NEWS)]
        public static void HandleGuildUpdateRoster(Packet packet)
        {
            var count = packet.Translator.ReadInt32("NewsCount");

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt32("Id", i);
                packet.Translator.ReadPackedTime("CompletedDate", i);
                packet.Translator.ReadInt32("Type", i);
                packet.Translator.ReadInt32("Flags", i);

                for (var j = 0; j < 2; ++j)
                    packet.Translator.ReadInt32("Data", i, j);

                packet.Translator.ReadPackedGuid128("MemberGuid", i);

                var int64 = packet.Translator.ReadInt32("MemberListCount", i);

                for (var j = 0; j < int64; ++j)
                    packet.Translator.ReadPackedGuid128("MemberList", i, j);

                packet.Translator.ResetBitReader();

                var bit80 = packet.Translator.ReadBit("HasItemInstance", i);
                if (bit80)
                    ItemHandler.ReadItemInstance(packet, i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_EVENT_PRESENCE_CHANGE)]
        public static void HandleGuildEventPresenceChange(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Guid");

            packet.Translator.ReadInt32("VirtualRealmAddress");

            var bits38 = packet.Translator.ReadBits(6);
            packet.Translator.ReadBit("LoggedOn");
            packet.Translator.ReadBit("Mobile");

            packet.Translator.ReadWoWString("Name", bits38);
        }

        [Parser(Opcode.SMSG_GUILD_KNOWN_RECIPES)]
        public static void HandleGuildRecipes(Packet packet)
        {
            var count = packet.Translator.ReadInt32("Criteria count");

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt32("Skill Id", i);
                packet.Translator.ReadBytes("Skill Bits", 300, i);
            }
        }

        [Parser(Opcode.CMSG_REQUEST_GUILD_PARTY_STATE)]
        public static void HandleGuildUpdatePartyState(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("GuildGUID");
        }

        [Parser(Opcode.SMSG_GUILD_PARTY_STATE)]
        public static void HandleGuildPartyStateResponse(Packet packet)
        {
            packet.Translator.ReadBit("Is guild group");
            packet.Translator.ReadUInt32("Current guild members");
            packet.Translator.ReadUInt32("Needed guild members");
            packet.Translator.ReadSingle("Guild XP multiplier");
        }

        [Parser(Opcode.SMSG_ALL_GUILD_ACHIEVEMENTS)]
        public static void HandleGuildAchievementData(Packet packet)
        {
            var int10 = packet.Translator.ReadUInt32("EarnedAchievementCount");
            for (var i = 0; i < int10; ++i)
            {
                packet.Translator.ReadInt32("Id", i);
                packet.Translator.ReadPackedTime("Date", i);
                packet.Translator.ReadPackedGuid128("Owner", i);
                packet.Translator.ReadInt32("VirtualRealmAddress", i);
                packet.Translator.ReadInt32("NativeRealmAddress", i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_PERMISSIONS_QUERY_RESULTS)]
        public static void HandleGuildPermissionsQueryResult(Packet packet)
        {
            packet.Translator.ReadInt32("RankID");
            packet.Translator.ReadInt32("WithdrawGoldLimit");
            packet.Translator.ReadUInt32E<GuildRankRightsFlag>("Flags");
            packet.Translator.ReadUInt32("NumTabs");

            var int16 = packet.Translator.ReadInt32("TabCount");

            for (var i = 0; i < int16; i++)
            {
                packet.Translator.ReadInt32E<GuildBankRightsFlag>("Flags", i);
                packet.Translator.ReadInt32("WithdrawItemLimit", i);
            }
        }

        [Parser(Opcode.CMSG_GUILD_INVITE_BY_NAME)]
        public static void HandleGuildInviteByName(Packet packet)
        {
            var bits16 = packet.Translator.ReadBits(9);
            packet.Translator.ReadWoWString("Name", bits16);
        }

        [Parser(Opcode.SMSG_GUILD_CRITERIA_UPDATE)]
        public static void HandleGuildCriteriaUpdate(Packet packet)
        {
            var int16 = packet.Translator.ReadUInt32("ProgressCount");
            for (int i = 0; i < int16; i++)
            {
                packet.Translator.ReadInt32("CriteriaID", i);
                packet.Translator.ReadTime("DateCreated", i);
                packet.Translator.ReadTime("DateStarted", i);
                packet.Translator.ReadTime("DateUpdated", i);
                packet.Translator.ReadInt64("Quantity", i);
                packet.Translator.ReadPackedGuid128("PlayerGUID", i);

                packet.Translator.ReadInt32("Flags", i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_REWARD_LIST)]
        public static void HandleGuildRewardsList(Packet packet)
        {
            packet.Translator.ReadTime("Version");

            var size = packet.Translator.ReadUInt32("RewardItemsCount");
            for (int i = 0; i < size; i++)
            {
                packet.Translator.ReadUInt32("ItemID", i);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_0_19678))
                    packet.Translator.ReadUInt32("Unk4", i);

                var int1 = packet.Translator.ReadInt32("AchievementsRequiredCount", i);
                packet.Translator.ReadUInt32("RaceMask", i);
                packet.Translator.ReadInt32("MinGuildLevel", i);
                packet.Translator.ReadInt32("MinGuildRep", i);
                packet.Translator.ReadInt64("Cost", i);

                for (int j = 0; j < int1; j++)
                    packet.Translator.ReadInt32("AchievementsRequired", i, j);
            }
        }

        [Parser(Opcode.SMSG_GUILD_EVENT_BANK_MONEY_CHANGED)]
        public static void HandleGuildEventBankMoneyChanged(Packet packet)
        {
            packet.Translator.ReadUInt64("Money");
        }

        [Parser(Opcode.SMSG_GUILD_INVITE)]
        public static void HandleGuildInvite(Packet packet)
        {
            var bits149 = packet.Translator.ReadBits(6);
            var bits216 = packet.Translator.ReadBits(7);
            var bits52 = packet.Translator.ReadBits(7);

            packet.Translator.ReadInt32("InviterVirtualRealmAddress");
            packet.Translator.ReadUInt32("GuildVirtualRealmAddress");
            packet.Translator.ReadPackedGuid128("GuildGUID");
            packet.Translator.ReadUInt32("OldGuildVirtualRealmAddress");
            packet.Translator.ReadPackedGuid128("OldGuildGUID");
            packet.Translator.ReadUInt32("EmblemColor");
            packet.Translator.ReadUInt32("EmblemStyle");
            packet.Translator.ReadUInt32("BorderColor");
            packet.Translator.ReadUInt32("BorderStyle");
            packet.Translator.ReadUInt32("BackgroundColor");
            packet.Translator.ReadInt32("Level");

            packet.Translator.ReadWoWString("InviterName", bits149);
            packet.Translator.ReadWoWString("OldGuildName", bits216);
            packet.Translator.ReadWoWString("GuildName", bits52);
        }

        [Parser(Opcode.SMSG_GUILD_BANK_QUERY_RESULTS)]
        public static void HandleGuildBankList(Packet packet)
        {
            packet.Translator.ReadUInt64("Money");
            packet.Translator.ReadInt32("Tab");
            packet.Translator.ReadInt32("WithdrawalsRemaining");

            var int36 = packet.Translator.ReadInt32("TabInfoCount");
            var int16 = packet.Translator.ReadInt32("ItemInfoCount");

            for (int i = 0; i < int36; i++)
            {
                packet.Translator.ReadInt32("TabIndex", i);

                packet.Translator.ResetBitReader();

                var bits1 = packet.Translator.ReadBits(7);
                var bits69 = packet.Translator.ReadBits(9);

                packet.Translator.ReadWoWString("Name", bits1, i);
                packet.Translator.ReadWoWString("Icon", bits69, i);
            }

            for (int i = 0; i < int16; i++)
            {
                packet.Translator.ReadInt32("Slot", i);
                ItemHandler.ReadItemInstance(packet, i);

                packet.Translator.ReadInt32("Count", i);
                packet.Translator.ReadInt32("EnchantmentID", i);
                packet.Translator.ReadInt32("Charges", i);
                packet.Translator.ReadInt32("OnUseEnchantmentID", i);
                var int76 = packet.Translator.ReadInt32("SocketEnchant", i);
                packet.Translator.ReadInt32("Flags", i);

                for (int j = 0; j < int76; j++)
                {
                    packet.Translator.ReadInt32("SocketIndex", i, j);
                    packet.Translator.ReadInt32("SocketEnchantID", i, j);
                }

                packet.Translator.ResetBitReader();
                packet.Translator.ReadBit("Locked");
            }

            packet.Translator.ResetBitReader();
            packet.Translator.ReadBit("FullUpdate");
        }

        [Parser(Opcode.SMSG_GUILD_BANK_LOG_QUERY_RESULTS)]
        public static void HandleGuildBankLogQueryResult(Packet packet)
        {
            packet.Translator.ReadInt32("Tab");
            var int32 = packet.Translator.ReadInt32("GuildBankLogEntryCount");
            for (int i = 0; i < int32; i++)
            {
                packet.Translator.ReadPackedGuid128("PlayerGUID", i);
                packet.Translator.ReadInt32("TimeOffset", i);
                packet.Translator.ReadSByte("EntryType", i);

                packet.Translator.ResetBitReader();

                var bit33 = packet.Translator.ReadBit("HasMoney", i);
                var bit44 = packet.Translator.ReadBit("HasItemID", i);
                var bit52 = packet.Translator.ReadBit("HasCount", i);
                var bit57 = packet.Translator.ReadBit("HasOtherTab", i);

                if (bit33)
                    packet.Translator.ReadInt64("Money", i);

                if (bit44)
                    packet.Translator.ReadInt32("ItemID", i);

                if (bit52)
                    packet.Translator.ReadInt32("Count", i);

                if (bit57)
                    packet.Translator.ReadSByte("OtherTab", i);

            }

            packet.Translator.ResetBitReader();
            var bit24 = packet.Translator.ReadBit("HasWeeklyBonusMoney");
            if (bit24)
                packet.Translator.ReadInt64("WeeklyBonusMoney");
        }

        [Parser(Opcode.SMSG_GUILD_CHALLENGE_UPDATE)]
        public static void HandleGuildChallengeUpdated(Packet packet)
        {
            for (int i = 0; i < 6; ++i)
                packet.Translator.ReadInt32("CurrentCount", i);

            for (int i = 0; i < 6; ++i)
                packet.Translator.ReadInt32("MaxCount", i);

            for (int i = 0; i < 6; ++i)
                packet.Translator.ReadInt32("Gold", i);

            for (int i = 0; i < 6; ++i)
                packet.Translator.ReadInt32("MaxLevelGold", i);
        }

        [Parser(Opcode.SMSG_GUILD_SEND_RANK_CHANGE)]
        public static void HandleGuildRanksUpdate(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Officer");
            packet.Translator.ReadPackedGuid128("Other");
            packet.Translator.ReadInt32("RankID");
            packet.Translator.ReadBit("Promote");
        }

        [Parser(Opcode.SMSG_GUILD_EVENT_RANK_CHANGED)]
        public static void HandleGuildEventRankChanged(Packet packet)
        {
            packet.Translator.ReadInt32("RankID");
        }

        [Parser(Opcode.CMSG_GUILD_SET_ACHIEVEMENT_TRACKING)]
        public static void HandleGuildSetAchievementTracking(Packet packet)
        {
            var count = packet.Translator.ReadUInt32("Count");
            for (var i = 0; i < count; ++i)
                packet.Translator.ReadInt32<AchievementId>("AchievementIDs", i);
        }

        [Parser(Opcode.SMSG_GUILD_COMMAND_RESULT)]
        public static void HandleGuildCommandResult(Packet packet)
        {
            packet.Translator.ReadUInt32E<GuildCommandError>("Result");
            packet.Translator.ReadUInt32E<GuildCommandType>("Command");
            var len = packet.Translator.ReadBits(8);
            packet.Translator.ReadWoWString("Name", len);
        }

        [Parser(Opcode.SMSG_GUILD_NAME_CHANGED)]
        public static void HandleGuildNameChanged(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("GuildGUID");

            var len = packet.Translator.ReadBits(7);
            packet.Translator.ReadWoWString("GuildName", len);
        }

        [Parser(Opcode.CMSG_GUILD_BANK_QUERY_TAB)]
        public static void HandleGuildBankQueryTab(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Banker");
            packet.Translator.ReadByte("Tab");

            packet.Translator.ResetBitReader();
            packet.Translator.ReadBit("FullUpdate");
        }

        [Parser(Opcode.CMSG_GUILD_BANK_UPDATE_TAB)]
        public static void HandleGuildBankUpdateTab(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Banker");
            packet.Translator.ReadByte("BankTab");

            packet.Translator.ResetBitReader();
            var nameLen = packet.Translator.ReadBits(7);
            var iconLen = packet.Translator.ReadBits(9);

            packet.Translator.ReadWoWString("Name", nameLen);
            packet.Translator.ReadWoWString("Icon", iconLen);
        }

        [Parser(Opcode.SMSG_GUILD_EVENT_LOG_QUERY_RESULTS)]
        public static void HandleGuildEventLogQueryResults(Packet packet)
        {
            var eventCount = packet.Translator.ReadInt32("EventEntryCount");
            for (int i = 0; i < eventCount; i++)
            {
                packet.Translator.ReadPackedGuid128("PlayerGUID", i);
                packet.Translator.ReadPackedGuid128("OtherGUID", i);
                packet.Translator.ReadByte("TransactionType", i);
                packet.Translator.ReadByte("RankID", i);
                packet.Translator.ReadUInt32("TransactionDate", i);
            }
        }

        [Parser(Opcode.CMSG_GUILD_QUERY_NEWS)]
        public static void HandleGuildQueryNews(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("GuildGUID");
        }

        [Parser(Opcode.CMSG_GUILD_BANK_ACTIVATE)]
        public static void HandleGuildBankActivate(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Banker");

            packet.Translator.ResetBitReader();
            packet.Translator.ReadBit("FullUpdate");
        }

        [Parser(Opcode.CMSG_GUILD_SET_RANK_PERMISSIONS)]
        public static void HandlelGuildSetRankPermissions(Packet packet)
        {
            packet.Translator.ReadInt32("RankID");
            packet.Translator.ReadInt32("RankOrder");
            packet.Translator.ReadUInt32E<GuildRankRightsFlag>("Flags");
            packet.Translator.ReadUInt32E<GuildRankRightsFlag>("OldFlags");
            packet.Translator.ReadInt32("WithdrawGoldLimit");

            for (var i = 0; i < 8; ++i)
            {
                packet.Translator.ReadInt32E<GuildBankRightsFlag>("TabFlags", i);
                packet.Translator.ReadInt32("TabWithdrawItemLimit", i);
            }

            packet.Translator.ResetBitReader();
            var rankNameLen = packet.Translator.ReadBits(7);

            packet.Translator.ReadWoWString("RankName", rankNameLen);
        }

        [Parser(Opcode.CMSG_REQUEST_GUILD_REWARDS_LIST)]
        public static void HandleRequestGuildRewardsList(Packet packet)
        {
            packet.Translator.ReadTime("CurrentVersion");
        }

        [Parser(Opcode.SMSG_LF_GUILD_POST)]
        public static void HandleLFGuildPost(Packet packet)
        {
            var hasGuildPostData = packet.Translator.ReadBit("HasGuildPostData");
            if (hasGuildPostData)
            {
                packet.Translator.ResetBitReader();
                packet.Translator.ReadBit("Active");
                var len = packet.Translator.ReadBits(10);

                packet.Translator.ReadInt32("PlayStyle");
                packet.Translator.ReadInt32("Availability");
                packet.Translator.ReadInt32("ClassRoles");
                packet.Translator.ReadInt32("LevelRange");
                packet.Translator.ReadInt32("SecondsRemaining");

                packet.Translator.ReadWoWString("Comment", len);
            }
        }

        [Parser(Opcode.SMSG_GUILD_CHALLENGE_COMPLETED)]
        public static void HandleGuildChallengeCompleted(Packet packet)
        {
            packet.Translator.ReadInt32("ChallengeType");
            packet.Translator.ReadInt32("CurrentCount");
            packet.Translator.ReadInt32("MaxCount");
            packet.Translator.ReadInt32("GoldAwarded");
        }

        [Parser(Opcode.SMSG_GUILD_REPUTATION_REACTION_CHANGED)]
        public static void HandleGuildReputationReactionChanged(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("MemberGUID");
        }

        [Parser(Opcode.SMSG_GUILD_ACHIEVEMENT_EARNED)]
        public static void HandleGuildAchievementEarned(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("GuildGUID");
            packet.Translator.ReadInt32<AchievementId>("AchievementID");
            packet.Translator.ReadPackedTime("TimeEarned");
        }

        [Parser(Opcode.SMSG_GUILD_EVENT_PLAYER_JOINED)]
        public static void HandleGuildEventPlayerJoined(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Guid");
            packet.Translator.ReadInt32("VirtualRealmAddress");

            var len = packet.Translator.ReadBits(6);
            packet.Translator.ReadWoWString("Name", len);
        }

        [Parser(Opcode.SMSG_GUILD_EVENT_PLAYER_LEFT)]
        public static void HandleGuildEventPlayerLeft(Packet packet)
        {
            var hasRemoved = packet.Translator.ReadBit("Removed");
            var lenLeaverName = packet.Translator.ReadBits(6);

            if (hasRemoved)
            {
                var lenRemoverName = packet.Translator.ReadBits(6);
                packet.Translator.ReadPackedGuid128("RemoverGUID");
                packet.Translator.ReadInt32("RemoverVirtualRealmAddress");
                packet.Translator.ReadWoWString("RemoverName", lenRemoverName);
            }

            packet.Translator.ReadPackedGuid128("LeaverGUID");
            packet.Translator.ReadInt32("LeaverVirtualRealmAddress");
            packet.Translator.ReadWoWString("LeaverName", lenLeaverName);
        }

        public static void ReadLFGuildRecruitData(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadPackedGuid128("RecruitGUID", indexes);

            packet.Translator.ReadInt32("RecruitVirtualRealm", indexes);

            packet.Translator.ReadInt32("CharacterClass", indexes);
            packet.Translator.ReadInt32("CharacterGender", indexes);
            packet.Translator.ReadInt32("CharacterLevel", indexes);

            packet.Translator.ReadInt32("ClassRoles", indexes);
            packet.Translator.ReadInt32("PlayStyle", indexes);
            packet.Translator.ReadInt32("Availability", indexes);
            packet.Translator.ReadInt32("SecondsSinceCreated", indexes);
            packet.Translator.ReadInt32("SecondsUntilExpiration", indexes);

            packet.Translator.ResetBitReader();

            var lenName = packet.Translator.ReadBits(6);
            var lenComment = packet.Translator.ReadBits(10);

            packet.Translator.ReadWoWString("Name", lenName, indexes);
            packet.Translator.ReadWoWString("Comment", lenComment, indexes);
        }

        public static void ReadLFGuildApplicationData(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadPackedGuid128("GuildGUID", indexes);
            packet.Translator.ReadInt32("GuildVirtualRealm", indexes);

            packet.Translator.ReadInt32("ClassRoles", indexes);
            packet.Translator.ReadInt32("PlayStyle", indexes);
            packet.Translator.ReadInt32("Availability", indexes);
            packet.Translator.ReadInt32("SecondsSinceCreated", indexes);

            packet.Translator.ResetBitReader();

            var lenName = packet.Translator.ReadBits(7);
            var lenComment = packet.Translator.ReadBits(10);

            packet.Translator.ReadWoWString("GuildName", lenName, indexes);
            packet.Translator.ReadWoWString("Comment", lenComment, indexes);
        }

        [Parser(Opcode.SMSG_LF_GUILD_RECRUITS)]
        public static void HandleLFGuildRecruits(Packet packet)
        {
            var recruitsCount = packet.Translator.ReadInt32("LFGuildRecruitDataCount");
            packet.Translator.ReadTime("UpdateTime");
            for (int i = 0; i < recruitsCount; i++)
                ReadLFGuildRecruitData(packet, i, "LFGuildRecruitData");
        }

        [Parser(Opcode.SMSG_LF_GUILD_APPLICATIONS)]
        public static void HandleLFGuildApplications(Packet packet)
        {
            packet.Translator.ReadInt32("NumRemaining");
            var applicationCount = packet.Translator.ReadInt32("LFGuildApplicationDataCount");
            for (int i = 0; i < applicationCount; i++)
                ReadLFGuildApplicationData(packet, i, "LFGuildApplicationData");
        }

        [Parser(Opcode.CMSG_PETITION_SHOW_LIST)]
        public static void HandlePetitionShowListClient(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("PetitionUnit");
        }

        [Parser(Opcode.SMSG_PETITION_SHOW_LIST)]
        public static void HandlePetitionShowList(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Unit");
            packet.Translator.ReadUInt32("Price");
        }

        [Parser(Opcode.CMSG_PETITION_BUY)]
        public static void HandlePetitionBuy(Packet packet)
        {
            var length = packet.Translator.ReadBits(7);
            packet.Translator.ReadPackedGuid128("Unit");
            packet.Translator.ReadWoWString("Title", length);
        }

        [Parser(Opcode.CMSG_SIGN_PETITION)]
        public static void HandleSignPetition(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("PetitionGUID");
            packet.Translator.ReadByte("Choice");
        }

        [Parser(Opcode.CMSG_QUERY_PETITION)]
        public static void HandleQueryPetition(Packet packet)
        {
            packet.Translator.ReadUInt32("PetitionID");
            packet.Translator.ReadPackedGuid128("ItemGUID");
        }

        [Parser(Opcode.CMSG_GUILD_SET_FOCUSED_ACHIEVEMENT)]
        public static void HandleGuildSetFocusedAchievement(Packet packet)
        {
            packet.Translator.ReadUInt32("AchievementID");
        }

        [Parser(Opcode.CMSG_GUILD_QUERY_MEMBER_RECIPES)]
        public static void HandleGuildQueryMemberRecipes(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("GuildMember");
            packet.Translator.ReadPackedGuid128("GuildGUID");
            packet.Translator.ReadUInt32("SkillLineID");
        }

        [Parser(Opcode.SMSG_PETITION_SIGN_RESULTS)]
        public static void HandlePetitionSignResults(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Item");
            packet.Translator.ReadPackedGuid128("Player");
            packet.Translator.ReadBits("Error", 4);
        }

        public static void ReadPetitionSignature(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadPackedGuid128("Signer", indexes);
            packet.Translator.ReadInt32("Choice", indexes);
        }

        [Parser(Opcode.SMSG_PETITION_SHOW_SIGNATURES)]
        public static void HandlePetitionShowSignatures(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Item");
            packet.Translator.ReadPackedGuid128("Owner");
            packet.Translator.ReadPackedGuid128("OwnerWoWAccount");
            packet.Translator.ReadInt32("PetitionID");

            var signaturesCount = packet.Translator.ReadInt32("SignaturesCount");
            for (int i = 0; i < signaturesCount; i++)
                ReadPetitionSignature(packet, i, "PetitionSignature");
        }

        public static void ReadPetitionInfo(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadInt32("PetitionID", indexes);
            packet.Translator.ReadPackedGuid128("Petitioner", indexes);

            packet.Translator.ReadInt32("MinSignatures", indexes);
            packet.Translator.ReadInt32("MaxSignatures", indexes);
            packet.Translator.ReadInt32("DeadLine", indexes);
            packet.Translator.ReadInt32("IssueDate", indexes);

            packet.Translator.ReadInt32("AllowedGuildID", indexes);
            packet.Translator.ReadInt32("AllowedClasses", indexes);
            packet.Translator.ReadInt32("AllowedRaces", indexes);
            packet.Translator.ReadInt16("AllowedGender", indexes);
            packet.Translator.ReadInt32("AllowedMinLevel", indexes);
            packet.Translator.ReadInt32("AllowedMaxLevel", indexes);

            packet.Translator.ReadInt32("NumChoices", indexes);
            packet.Translator.ReadInt32("StaticType", indexes);
            packet.Translator.ReadUInt32("Muid", indexes);

            packet.Translator.ResetBitReader();

            var lenTitle = packet.Translator.ReadBits(7);
            var lenBodyText = packet.Translator.ReadBits(12);

            var lenChoicetext = new uint[10];
            for (int i = 0; i < 10; i++)
                lenChoicetext[i] = packet.Translator.ReadBits(6);
            for (int i = 0; i < 10; i++)
                packet.Translator.ReadWoWString("Choicetext", lenChoicetext[i]);

            packet.Translator.ReadWoWString("Title", lenTitle);
            packet.Translator.ReadWoWString("BodyText", lenBodyText);
        }

        [Parser(Opcode.SMSG_QUERY_PETITION_RESPONSE)]
        public static void HandleQueryPetitionResponse(Packet packet)
        {
            packet.Translator.ReadInt32("PetitionID");

            var hasAllow = packet.Translator.ReadBit("Allow");
            if (hasAllow)
                ReadPetitionInfo(packet, "PetitionInfo");
        }

        [Parser(Opcode.SMSG_GUILD_MEMBER_RECIPES)]
        public static void HandleGuildMemberRecipes(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Member");
            packet.Translator.ReadInt32("SkillLineID");
            packet.Translator.ReadInt32("SkillRank");
            packet.Translator.ReadInt32("SkillStep");      
            for (int i = 0; i < 0x12C; i++)
                packet.Translator.ReadByte("SkillLineBitArray", i);
        }

        [Parser(Opcode.SMSG_PETITION_DECLINED)]
        public static void HandlePetitionDeclined(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Decliner");
        }

        [Parser(Opcode.SMSG_PETITION_RENAME_GUILD_RESPONSE)]
        public static void HandlePetitionRenameGuildResponse(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("PetitionGuid");
            var length = packet.Translator.ReadBits("NewGuildNameLength", 7);
            packet.Translator.ResetBitReader();

            packet.Translator.ReadWoWString("NewGuildName", length);
        }

        [Parser(Opcode.SMSG_TURN_IN_PETITION_RESULT)]
        public static void HandlePetitionTurnInResults(Packet packet)
        {
            packet.Translator.ReadBitsE<PetitionResultType>("Result", 4);
        }

        [Parser(Opcode.SMSG_GUILD_INVITE_DECLINED)]
        public static void HandleGuildInviteDeclined(Packet packet)
        {
            var nameLength = packet.Translator.ReadBits("NameLength", 6);
            packet.Translator.ReadBit("AutoDecline");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadUInt32("VirtualRealmAddress");
            packet.Translator.ReadWoWString("Name", nameLength);
        }

        [Parser(Opcode.SMSG_GUILD_BANK_TEXT_QUERY_RESULT)]
        public static void HandleGuildQueryBankText434(Packet packet)
        {
            packet.Translator.ReadInt32("Tab");

            var textLength = packet.Translator.ReadBits("TextLength", 14);
            packet.Translator.ResetBitReader();

            packet.Translator.ReadWoWString("Text", textLength);
        }

        [Parser(Opcode.SMSG_GUILD_EVENT_TAB_MODIFIED)]
        public static void HandleGuildEventTabModified(Packet packet)
        {
            packet.Translator.ReadInt32("Tab");

            var nameLength = packet.Translator.ReadBits("NameLength", 7);
            var iconLength = packet.Translator.ReadBits("IconLength", 9);
            packet.Translator.ResetBitReader();

            packet.Translator.ReadWoWString("Name", nameLength);
            packet.Translator.ReadWoWString("Icon", iconLength);
        }

        public static void ReadLFGuildBrowseData(Packet packet, params object[] idx)
        {
            var guildNameLength = packet.Translator.ReadBits("GuildNameLength", 7, idx);
            var commentLength = packet.Translator.ReadBits("CommentLength", 10, idx);
            packet.Translator.ResetBitReader();

            packet.Translator.ReadPackedGuid128("GuildGUID", idx);

            packet.Translator.ReadUInt32("GuildVirtualRealm", idx);
            // packet.Translator.ReadInt32("GuildLevel", idx);
            packet.Translator.ReadInt32("GuildMembers", idx);
            packet.Translator.ReadInt32("GuildAchievementPoints", idx);
            packet.Translator.ReadInt32E<GuildFinderOptionsInterest>("PlayStyle", idx);
            packet.Translator.ReadInt32E<GuildFinderOptionsAvailability>("Availability", idx);
            packet.Translator.ReadInt32E<GuildFinderOptionsRoles>("ClassRoles", idx);
            packet.Translator.ReadInt32E<GuildFinderOptionsLevel>("LevelRange", idx);
            packet.Translator.ReadInt32("EmblemStyle", idx);
            packet.Translator.ReadInt32("EmblemColor", idx);
            packet.Translator.ReadInt32("BorderStyle", idx);
            packet.Translator.ReadInt32("BorderColor", idx);
            packet.Translator.ReadInt32("Background", idx);

            packet.Translator.ReadSByte("Cached", idx);
            packet.Translator.ReadSByte("MembershipRequested", idx);

            packet.Translator.ReadWoWString("GuildName", guildNameLength);
            packet.Translator.ReadWoWString("CommentLength", commentLength);
        }

        [Parser(Opcode.SMSG_LF_GUILD_BROWSE)]
        public static void HandleLFGuildBrowse(Packet packet)
        {
            var count = packet.Translator.ReadInt32("PostCount");

            for (var i = 0; i < count; ++i)
                ReadLFGuildBrowseData(packet, "Post", i);
        }
        
        [Parser(Opcode.CMSG_LF_GUILD_SET_GUILD_POST, ClientVersionBuild.V6_1_2_19802)]
        public static void HandleGuildFinderSetGuildPost612(Packet packet)
        {
            packet.Translator.ReadUInt32E<GuildFinderOptionsInterest>("Guild Interests"); // ok
            packet.Translator.ReadUInt32E<GuildFinderOptionsAvailability>("Availability"); // ok
            packet.Translator.ReadUInt32E<GuildFinderOptionsRoles>("Class Roles"); // ok
            packet.Translator.ReadUInt32E<GuildFinderOptionsLevel>("Level");
            packet.Translator.ReadBit("Listed");
            packet.Translator.ReadWoWString("Comment", packet.Translator.ReadBits(10));
        }

        [Parser(Opcode.CMSG_SAVE_GUILD_EMBLEM)]
        public static void HandleSaveGuildEmblem(Packet packet) 
        {
            packet.Translator.ReadPackedGuid128("Vendor");
            packet.Translator.ReadUInt32("EColor");
            packet.Translator.ReadUInt32("EStyle");
            packet.Translator.ReadUInt32("BColor");
            packet.Translator.ReadUInt32("BStyle");
            packet.Translator.ReadUInt32("Bg");
        }

        [Parser(Opcode.SMSG_PLAYER_SAVE_GUILD_EMBLEM)]
        public static void HandlePlayerSaveGuildEmblem(Packet packet)
        {
            packet.Translator.ReadInt32E<GuildEmblemError>("Error");
        }

        [Parser(Opcode.CMSG_GUILD_BANK_SWAP_ITEMS)]
        public static void HandleGuildBankSwapItems(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("GuildBankSwapItems");
            packet.Translator.ReadByte("BankTab");
            packet.Translator.ReadByte("BankSlot");
            packet.Translator.ReadInt32("ItemID");
            packet.Translator.ReadByte("BankTab1");
            packet.Translator.ReadByte("BankSlot1");
            packet.Translator.ReadInt32("ItemID1");
            packet.Translator.ReadInt32("BankItemCount");
            packet.Translator.ReadByte("ContainerSlot");
            packet.Translator.ReadByte("ContainerItemSlot");
            packet.Translator.ReadByte("ToSlot");
            packet.Translator.ReadInt32("StackCount");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("BankOnly");
            packet.Translator.ReadBit("AutoStore");
        }

        [Parser(Opcode.CMSG_GUILD_QUERY_MEMBERS_FOR_RECIPE)]
        public static void HandleGuildQueryMembersForRecipe(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("GuildGUID");
            packet.Translator.ReadUInt32("SkillLineID");
            packet.Translator.ReadUInt32<SpellId>("SpellID");
            packet.Translator.ReadUInt32("UniqueBit");
        }

        [Parser(Opcode.SMSG_GUILD_MEMBERS_WITH_RECIPE)]
        public static void HandleGuildMembersWithRecipe(Packet packet)
        {
            packet.Translator.ReadUInt32("SkillLineID");
            packet.Translator.ReadUInt32<SpellId>("SpellID");
            var count = packet.Translator.ReadInt32("MembersCount");
            for (var i = 0; i < count; ++i)
                packet.Translator.ReadPackedGuid128("Member", i);
        }
    }
}
