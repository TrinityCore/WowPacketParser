using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class GuildHandler
    {
        [Parser(Opcode.SMSG_ALL_GUILD_ACHIEVEMENTS)]
        public static void HandleGuildAchievementData(Packet packet)
        {
            var earnedAchievementCount = packet.ReadUInt32("EarnedAchievementCount");
            for (var i = 0; i < earnedAchievementCount; ++i)
                AchievementHandler.ReadEarnedAchievement(packet, "Earned", i);
        }

        [Parser(Opcode.CMSG_GUILD_SET_ACHIEVEMENT_TRACKING)]
        public static void HandleGuildSetAchievementTracking(Packet packet)
        {
            var count = packet.ReadUInt32("Count");
            for (var i = 0; i < count; ++i)
                packet.ReadInt32<AchievementId>("AchievementIDs", i);
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
                packet.ReadPackedGuid128("GuildGUID");
                packet.ReadInt32("VirtualRealmAddress");
                var rankCount = packet.ReadInt32("RankCount");
                packet.ReadInt32("EmblemColor");
                packet.ReadInt32("EmblemStyle");
                packet.ReadInt32("BorderColor");
                packet.ReadInt32("BorderStyle");
                packet.ReadInt32("BackgroundColor");

                packet.ResetBitReader();
                var nameLen = packet.ReadBits(7);

                for (var i = 0; i < rankCount; i++)
                {
                    packet.ReadInt32("RankID", i);
                    packet.ReadInt32("RankOrder", i);

                    packet.ResetBitReader();
                    var rankNameLen = packet.ReadBits(7);
                    packet.ReadWoWString("Rank Name", rankNameLen, i);
                }

                packet.ReadWoWString("Guild Name", nameLen);
            }
        }

        [Parser(Opcode.SMSG_GUILD_ACHIEVEMENT_DELETED)]
        public static void HandleGuildAchievementDeleted(Packet packet)
        {
            packet.ReadPackedGuid128("GuildGUID");
            packet.ReadUInt32<AchievementId>("AchievementID");
            packet.ReadPackedTime("TimeDeleted");
        }

        [Parser(Opcode.SMSG_GUILD_ACHIEVEMENT_EARNED)]
        public static void HandleGuildAchievementEarned(Packet packet)
        {
            packet.ReadPackedGuid128("GuildGUID");
            packet.ReadUInt32<AchievementId>("AchievementID");
            packet.ReadPackedTime("TimeEarned");
        }

        [Parser(Opcode.SMSG_GUILD_ACHIEVEMENT_MEMBERS)]
        public static void HandleGuildAchievementMembers(Packet packet)
        {
            packet.ReadPackedGuid128("GuildGUID");
            packet.ReadUInt32<AchievementId>("AchievementID");
            var memberCount = packet.ReadUInt32("MemberCount");

            for (int i = 0; i < memberCount; i++)
                packet.ReadPackedGuid128("MemberGUID", i);
        }

        [Parser(Opcode.SMSG_GUILD_BANK_LOG_QUERY_RESULTS)]
        public static void HandleGuildBankLogQueryResult(Packet packet)
        {
            packet.ReadInt32("Tab");
            var guildBankLogEntryCount = packet.ReadInt32("GuildBankLogEntryCount");
            var hasWeeklyBonusMoney = packet.ReadBit("HasWeeklyBonusMoney");

            for (int i = 0; i < guildBankLogEntryCount; i++)
            {
                packet.ReadPackedGuid128("PlayerGUID", i);
                packet.ReadInt32("TimeOffset", i);
                packet.ReadSByte("EntryType", i);

                packet.ResetBitReader();

                var hasMoney = packet.ReadBit("HasMoney", i);
                var hasItemID = packet.ReadBit("HasItemID", i);
                var hasCount = packet.ReadBit("HasCount", i);
                var hasOtherTab = packet.ReadBit("HasOtherTab", i);

                if (hasMoney)
                    packet.ReadInt64("Money", i);

                if (hasItemID)
                    packet.ReadInt32<ItemId>("ItemID", i);

                if (hasCount)
                    packet.ReadInt32("Count", i);

                if (hasOtherTab)
                    packet.ReadSByte("OtherTab", i);
            }

            if (hasWeeklyBonusMoney)
                packet.ReadInt64("WeeklyBonusMoney");
        }

        [Parser(Opcode.SMSG_GUILD_BANK_QUERY_RESULTS)]
        public static void HandleGuildBankQueryResults(Packet packet)
        {
            packet.ReadUInt64("Money");
            packet.ReadInt32("Tab");
            packet.ReadInt32("WithdrawalsRemaining");

            var tabInfoCount = packet.ReadInt32("TabInfoCount");
            var itemInfoCount = packet.ReadInt32("ItemInfoCount");
            packet.ReadBit("FullUpdate");

            for (int i = 0; i < tabInfoCount; i++)
            {
                packet.ReadInt32("TabIndex", i);

                packet.ResetBitReader();

                var nameLength = packet.ReadBits(7);
                var iconLength = packet.ReadBits(9);

                packet.ReadWoWString("Name", nameLength, i);
                packet.ReadWoWString("Icon", iconLength, i);
            }

            for (int i = 0; i < itemInfoCount; i++)
            {
                packet.ReadInt32("Slot", i);

                packet.ReadInt32("Count", i);
                packet.ReadInt32("EnchantmentID", i);
                packet.ReadInt32("Charges", i);
                packet.ReadInt32("OnUseEnchantmentID", i);
                packet.ReadInt32("Flags", i);
                Substructures.ItemHandler.ReadItemInstance(packet, i, "ItemInstance");

                packet.ResetBitReader();
                var socketEnchantmentCount = packet.ReadBits(2);
                packet.ReadBit("Locked", i);

                for (int j = 0; j < socketEnchantmentCount; j++)
                {
                    Substructures.ItemHandler.ReadItemGemData(packet, i, "Gems", j);
                }
            }
        }

        [Parser(Opcode.SMSG_GUILD_BANK_REMAINING_WITHDRAW_MONEY)]
        public static void HandleGuildBankMoneyWithdrawnResponse(Packet packet)
        {
            packet.ReadInt64("RemainingWithdrawMoney");
        }

        [Parser(Opcode.SMSG_GUILD_BANK_TEXT_QUERY_RESULT)]
        public static void HandleGuildBankTextQueryResult(Packet packet)
        {
            packet.ReadInt32("Tab");
            var textLength = packet.ReadBits(14);
            packet.ReadWoWString("Text", textLength);
        }

        [Parser(Opcode.CMSG_GUILD_BANK_REMAINING_WITHDRAW_MONEY_QUERY)]
        public static void HandleGuildZero(Packet packet)
        {
        }
    }
}
