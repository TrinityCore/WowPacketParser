using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V3_4_0_45166.Parsers.V3_4_3_54261
{
    public static class GuildHandler
    {
        [BuildMatch(ClientVersionBuild.V3_4_3_54261)]
        [Parser(Opcode.SMSG_GUILD_ROSTER, true)]
        public static void HandleGuildRoster(Packet packet)
        {
            packet.ReadUInt32("NumAccounts");
            packet.ReadPackedTime("CreateDate");
            packet.ReadUInt32("GuildFlags");
            var membersCount = packet.ReadUInt32("MemberDataCount");

            packet.ResetBitReader();
            var welcomeTextLen = packet.ReadBits(11);
            var infoTextLen = packet.ReadBits(11);

            for (var i = 0; i < membersCount; ++i)
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
                packet.ReadUInt64("GuildClubMemberID", i);
                packet.ReadByteE<Race>("ClassID", i);

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

        [BuildMatch(ClientVersionBuild.V3_4_3_54261)]
        [Parser(Opcode.CMSG_GUILD_SET_RANK_PERMISSIONS, true)]
        public static void HandlelGuildSetRankPermissions(Packet packet)
        {
            packet.ReadInt32("RankID");
            packet.ReadInt32("RankOrder");
            packet.ReadUInt32E<GuildRankRightsFlag>("Flags");
            packet.ReadInt32("WithdrawGoldLimit");

            for (var i = 0; i < 6; ++i)
            {
                packet.ReadInt32E<GuildBankRightsFlag>("TabFlags", i);
                packet.ReadInt32("TabWithdrawItemLimit", i);
            }

            packet.ReadUInt32E<GuildRankRightsFlag>("OldFlags");

            packet.ResetBitReader();
            var rankNameLen = packet.ReadBits(7);

            packet.ReadWoWString("RankName", rankNameLen);
        }

        [BuildMatch(ClientVersionBuild.V3_4_3_54261)]
        [Parser(Opcode.SMSG_GUILD_RANKS, true)]
        public static void HandleGuildRankServer(Packet packet)
        {
            var count = packet.ReadUInt32("Count");

            for (var i = 0; i < count; ++i)
            {
                packet.ReadByte("RankID", i);
                packet.ReadInt32("RankOrder", i);
                packet.ReadInt32("Flags", i);
                packet.ReadInt32("WithdrawGoldLimit", i);

                for (var j = 0; j < 6; ++j)
                {
                    packet.ReadInt32E<GuildBankRightsFlag>("TabFlags", i, j);
                    packet.ReadInt32("TabWithdrawItemLimit", i, j);
                }

                packet.ResetBitReader();
                var RankNameLen = (int)packet.ReadBits(7);
                packet.ReadWoWString("RankName", RankNameLen, i);
            }
        }
    }
}
