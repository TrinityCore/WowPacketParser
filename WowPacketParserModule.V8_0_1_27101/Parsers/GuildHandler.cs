using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class GuildHandler
    {
        [Parser(Opcode.SMSG_QUERY_GUILD_INFO_RESPONSE, ClientVersionBuild.V8_1_0_28724)]
        public static void HandleGuildQueryResponse(Packet packet)
        {
            packet.ReadPackedGuid128("Guild Guid");
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V9_2_0_42423))
                packet.ReadPackedGuid128("PlayerGUID");

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

        [Parser(Opcode.SMSG_GUILD_ROSTER)]
        public static void HandleGuildRoster(Packet packet)
        {
            packet.ReadUInt32("NumAccounts");
            packet.ReadPackedTime("CreateDate");
            packet.ReadUInt32("GuildFlags");
            var int20 = packet.ReadUInt32("MemberDataCount");

            packet.ResetBitReader();
            uint welcomeTextLen = 0;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_2_0_30898))
                welcomeTextLen = packet.ReadBits(11);
            else
                welcomeTextLen = packet.ReadBits(10);
            var infoTextLen = packet.ReadBits(11);

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

                packet.ReadUInt32("VirtualRealmAddress", i);

                packet.ReadByteE<GuildMemberFlag>("Status", i);
                packet.ReadByte("Level", i);
                packet.ReadByteE<Class>("ClassID", i);
                packet.ReadByteE<Gender>("Gender", i);

                packet.ResetBitReader();

                var nameLen = packet.ReadBits(6);
                var noteLen = packet.ReadBits(8);
                var officersNoteLen = packet.ReadBits(8);

                packet.ReadBit("Authenticated", i);
                packet.ReadBit("SorEligible", i);

                packet.ReadWoWString("Name", nameLen, i);
                packet.ReadWoWString("Note", noteLen, i);
                packet.ReadWoWString("OfficerNote", officersNoteLen, i);
            }

            packet.ReadWoWString("WelcomeText", welcomeTextLen);
            packet.ReadWoWString("InfoText", infoTextLen);
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

        [Parser(Opcode.SMSG_GUILD_EVENT_MOTD, ClientVersionBuild.V8_2_0_30898)]
        [Parser(Opcode.CMSG_GUILD_UPDATE_MOTD_TEXT, ClientVersionBuild.V8_2_0_30898)]
        public static void HandleEventMotd(Packet packet)
        {
            var motdLen = packet.ReadBits(11);
            packet.ReadWoWString("MotdText", motdLen);
        }

        [Parser(Opcode.CMSG_GUILD_SET_RANK_PERMISSIONS, ClientVersionBuild.V8_2_5_31921)]
        public static void HandlelGuildSetRankPermissions(Packet packet)
        {
            packet.ReadByte("RankID");
            packet.ReadInt32("RankOrder");
            packet.ReadUInt32E<GuildRankRightsFlag>("Flags");
            packet.ReadInt32("WithdrawGoldLimit");

            for (var i = 0; i < 8; ++i)
            {
                packet.ReadInt32E<GuildBankRightsFlag>("TabFlags", i);
                packet.ReadInt32("TabWithdrawItemLimit", i);
            }

            packet.ResetBitReader();
            var rankNameLen = packet.ReadBits(7);

            packet.ReadWoWString("RankName", rankNameLen);

            packet.ReadUInt32E<GuildRankRightsFlag>("OldFlags");
        }
    }
}
