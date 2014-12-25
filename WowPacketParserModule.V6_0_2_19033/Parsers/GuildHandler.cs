using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class GuildHandler
    {
        [Parser(Opcode.CMSG_GUILD_ROSTER)]
        public static void HandleGuildRosterRequest(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_GUILD_QUERY)]
        public static void HandleGuildQuery(Packet packet)
        {
            packet.ReadPackedGuid128("Guild Guid");
            packet.ReadPackedGuid128("Player Guid");
        }

        [Parser(Opcode.SMSG_GUILD_QUERY_RESPONSE)]
        public static void HandleGuildQueryResponse(Packet packet)
        {
            packet.ReadPackedGuid128("Guild Guid");

            var hasData = packet.ReadBit();
            if (hasData)
            {
                packet.ReadPackedGuid128("Guild Guid");
                packet.ReadInt32("VirtualRealmAddress");
                var rankCount = packet.ReadInt32("RankCount");
                packet.ReadInt32("EmblemStyle");
                packet.ReadInt32("EmblemColor");
                packet.ReadInt32("BorderStyle");
                packet.ReadInt32("BorderColor");
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

        [Parser(Opcode.SMSG_GUILD_MOTD)]
        public static void HandleNewText(Packet packet)
        {
            packet.ReadWoWString("MotdText", (int)packet.ReadBits(10));
        }

        [Parser(Opcode.CMSG_GUILD_BANK_BUY_TAB)]
        public static void HandleGuildBankBuyTab(Packet packet)
        {
            packet.ReadPackedGuid128("Banker");
            packet.ReadByte("BankTab");
        }

        [Parser(Opcode.CMSG_GUILD_QUERY_RANKS)]
        public static void HandleGuildRanks434(Packet packet)
        {
            packet.ReadPackedGuid128("GuildGUID");
        }

        [Parser(Opcode.SMSG_GUILD_RANK)]
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
                    packet.ReadEnum<GuildBankRightsFlag>("TabFlags", TypeCode.Int32, i, j);
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
            packet.ReadUInt32("MaxWeeklyRep");
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

                packet.ReadEnum<GuildMemberFlag>("Status", TypeCode.Byte, i);
                packet.ReadByte("Level", i);
                packet.ReadEnum<Class>("ClassID", TypeCode.Byte, i);
                packet.ReadEnum<Gender>("Gender", TypeCode.Byte, i);

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

        [Parser(Opcode.SMSG_GUILD_EVENT)]
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
                {
                    packet.ReadUInt32("ItemID", i);
                    packet.ReadUInt32("RandomPropertiesSeed", i);
                    packet.ReadUInt32("RandomPropertiesID", i);

                    packet.ResetBitReader();

                    var hasBonuses = packet.ReadBit("HasItemBonus", i);
                    var hasModifications = packet.ReadBit("HasModifications", i);
                    if (hasBonuses)
                    {
                        packet.ReadByte("Context", i);

                        var bonusCount = packet.ReadUInt32();
                        for (var j = 0; j < bonusCount; ++j)
                            packet.ReadUInt32("BonusListID", i, j);
                    }

                    if (hasModifications)
                    {
                        var modificationCount = packet.ReadUInt32() / 4;
                        for (var j = 0; j < modificationCount; ++j)
                            packet.ReadUInt32("Modification", i, j);
                    }
                }
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

        [Parser(Opcode.SMSG_GUILD_RECIPES)]
        public static void HandleGuildRecipes(Packet packet)
        {
            var count = packet.ReadInt32("Criteria count");

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("Skill Id", i);
                packet.ReadBytes("Skill Bits", 300, i);
            }
        }

        [Parser(Opcode.CMSG_GUILD_REQUEST_PARTY_STATE)]
        public static void HandleGuildUpdatePartyState(Packet packet)
        {
            packet.ReadPackedGuid128("GuildGUID");
        }

        [Parser(Opcode.SMSG_GUILD_PARTY_STATE_RESPONSE)]
        public static void HandleGuildPartyStateResponse(Packet packet)
        {
            packet.ReadBit("Is guild group");
            packet.ReadUInt32("Current guild members");
            packet.ReadUInt32("Needed guild members");
            packet.ReadSingle("Guild XP multiplier");
        }

        [Parser(Opcode.SMSG_GUILD_ACHIEVEMENT_DATA)]
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
            packet.ReadEnum<GuildRankRightsFlag>("Flags", TypeCode.UInt32);
            packet.ReadUInt32("NumTabs");

            var int16 = packet.ReadInt32("TabCount");

            for (var i = 0; i < int16; i++)
            {
                packet.ReadEnum<GuildBankRightsFlag>("Flags", TypeCode.Int32, i);
                packet.ReadInt32("WithdrawItemLimit", i);
            }
        }

        [Parser(Opcode.CMSG_GUILD_INVITE)]
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

        [Parser(Opcode.SMSG_GUILD_REWARDS_LIST)]
        public static void HandleGuildRewardsList(Packet packet)
        {
            packet.ReadTime("Version");

            var size = packet.ReadUInt32("RewardItemsCount");
            for (int i = 0; i < size; i++)
            {
                packet.ReadUInt32("ItemID", i);
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
            packet.ReadUInt32("EmblemStyle");
            packet.ReadUInt32("EmblemColor");
            packet.ReadUInt32("BorderStyle");
            packet.ReadUInt32("BorderColor");
            packet.ReadUInt32("BackgroundColor");
            packet.ReadInt32("Level");

            packet.ReadWoWString("InviterName", bits149);
            packet.ReadWoWString("OldGuildName", bits216);
            packet.ReadWoWString("GuildName", bits52);
        }

        [Parser(Opcode.SMSG_GUILD_BANK_LIST)]
        public static void HandleGuildBankList(Packet packet)
        {
            packet.ReadUInt64("Money");
            packet.ReadInt32("WithdrawalsRemaining");
            packet.ReadInt32("Tab");

            var int36 = packet.ReadInt32("TabInfoCount");
            var int16 = packet.ReadInt32("ItemInfoCount");

            for (int i = 0; i < int36; i++)
            {
                packet.ReadInt32("TabIndex", i);

                packet.ResetBitReader();

                var bits1 = packet.ReadBits(8);
                var bits69 = packet.ReadBits(7);

                packet.ReadWoWString("Name", bits1, i);
                packet.ReadWoWString("Icon", bits69, i);
            }

            for (int i = 0; i < int16; i++)
            {
                packet.ReadInt32("Slot", i);
                ItemHandler.ReadItemInstance(ref packet, i);
                packet.ReadInt32("Count", i);
                packet.ReadInt32("EnchantmentID", i);
                packet.ReadInt32("Charges", i);
                var int76 = packet.ReadInt32("SocketEnchant", i);
                packet.ReadInt32("OnUseEnchantmentID", i);

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

        [Parser(Opcode.SMSG_GUILD_BANK_LOG_QUERY_RESULT)]
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

        [Parser(Opcode.SMSG_GUILD_CHALLENGE_UPDATED)]
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

        [Parser(Opcode.SMSG_GUILD_RANKS_UPDATE)]
        public static void HandleGuildRanksUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("Officer");
            packet.ReadPackedGuid128("Other");
            packet.ReadInt32("RankID");
            packet.ReadBit("Promote");
        }
    }
}
