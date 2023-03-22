using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class GuildHandler
    {
        [Parser(Opcode.SMSG_QUERY_GUILD_INFO_RESPONSE)]
        public static void HandleGuildQueryResponse(Packet packet)
        {
            packet.ReadPackedGuid128("Guild Guid");

            var hasData = packet.ReadBit();
            if (hasData)
            {
                packet.ReadPackedGuid128("GuildGUID");
                packet.ReadInt32_Sanitize("VirtualRealmAddress");
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
                    packet.ReadWoWString_Sanitize("Rank Name", rankNameLen, i);
                }

                packet.ReadWoWString_Sanitize("Guild Name", nameLen);
            }
        }

        [Parser(Opcode.SMSG_GUILD_ROSTER)]
        public static void HandleGuildRoster(Packet packet)
        {
            packet.ReadUInt32("NumAccounts");
            packet.ReadPackedTime("CreateDate");
            packet.ReadUInt32("GuildFlags");
            var int20 = packet.ReadUInt32("MemberDataCount");

            packet.ResetBitReader();
            var bits2037 = packet.ReadBits(10);
            var bits9 = packet.ReadBits(11);

            for (var i = 0; i < int20; ++i)
            {
                packet.ReadPackedGuid128("Guid", i);

                packet.ReadUInt32("RankID", i);
                packet.ReadUInt32<AreaId>("AreaID", i);
                packet.ReadUInt32("PersonalAchievementPoints", i);
                packet.ReadUInt32("GuildReputation", i);

                packet.ReadSingle("LastSave", i);

                for (var j = 0; j < 2; ++j)
                {
                    packet.ReadUInt32("DbID", i, j);
                    packet.ReadUInt32("Rank", i, j);
                    packet.ReadUInt32("Step", i, j);
                }

                packet.ReadUInt32_Sanitize("VirtualRealmAddress", i);

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

                packet.ReadWoWString_Sanitize("Name", bits36, i);
                packet.ReadWoWString_Sanitize("Note", bits92, i);
                packet.ReadWoWString_Sanitize("OfficerNote", bits221, i);
            }

            packet.ReadWoWString("WelcomeText", bits2037);
            packet.ReadWoWString("InfoText", bits9);
        }

        [Parser(Opcode.SMSG_GUILD_BANK_QUERY_RESULTS, ClientVersionBuild.V7_2_0_23826)]
        public static void HandleGuildBankQueryResults(Packet packet)
        {
            packet.ReadUInt64("Money");
            packet.ReadInt32("Tab");
            packet.ReadInt32("WithdrawalsRemaining");

            var tabInfoCount = packet.ReadInt32("TabInfoCount");
            var itemInfoCount = packet.ReadInt32("ItemInfoCount");

            for (int i = 0; i < tabInfoCount; i++)
            {
                packet.ReadInt32("TabIndex", i);

                packet.ResetBitReader();

                var bits1 = packet.ReadBits(7);
                var bits69 = packet.ReadBits(9);

                packet.ReadWoWString("Name", bits1, i);
                packet.ReadWoWString("Icon", bits69, i);
            }

            for (int i = 0; i < itemInfoCount; i++)
            {
                packet.ReadInt32("Slot", i);

                packet.ReadInt32("Count", i);
                packet.ReadInt32("EnchantmentID", i);
                packet.ReadInt32("Charges", i);
                packet.ReadInt32("OnUseEnchantmentID", i);
                var int76 = packet.ReadInt32("SocketEnchant", i);
                packet.ReadInt32("Flags", i);

                Substructures.ItemHandler.ReadItemInstance(packet, i, "ItemInstance");

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

        [Parser(Opcode.SMSG_GUILD_REWARD_LIST)]
        public static void HandleGuildRewardsList(Packet packet)
        {
            packet.ReadTime("Version");

            var size = packet.ReadUInt32("RewardItemsCount");
            for (int i = 0; i < size; i++)
            {
                packet.ReadUInt32<ItemId>("ItemID", i);
                packet.ReadUInt32("Unk4", i);
                var achievementReqCount = packet.ReadInt32("AchievementsRequiredCount", i);
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_3_5_25848))
                    packet.ReadUInt64("RaceMask", i);
                else
                    packet.ReadUInt32("RaceMask", i);
                packet.ReadInt32("MinGuildLevel", i);
                packet.ReadInt32("MinGuildRep", i);
                packet.ReadInt64("Cost", i);

                for (int j = 0; j < achievementReqCount; j++)
                    packet.ReadInt32("AchievementsRequired", i, j);
            }
        }
    }
}
