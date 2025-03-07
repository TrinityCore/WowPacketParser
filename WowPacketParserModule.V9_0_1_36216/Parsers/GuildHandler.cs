using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class GuildHandler
    {
        [Parser(Opcode.CMSG_GUILD_INVITE_BY_NAME, ClientVersionBuild.V9_1_0_39185)]
        public static void HandleGuildInviteByName(Packet packet)
        {
            var nameLength = packet.ReadBits(9);
            var hasArenaTeamId = packet.ReadBit("HasArenaTeamId");

            packet.ReadWoWString("Name", nameLength);

            if (hasArenaTeamId)
                packet.ReadInt32("ArenaTeamId");
        }

        [Parser(Opcode.SMSG_GUILD_ROSTER, ClientVersionBuild.V9_1_0_39185)]
        public static void HandleGuildRoster(Packet packet)
        {
            packet.ReadUInt32("NumAccounts");
            packet.ReadPackedTime("CreateDate");
            packet.ReadUInt32("GuildFlags");
            var int20 = packet.ReadUInt32("MemberDataCount");

            packet.ResetBitReader();
            var welcomeTextLen = packet.ReadBits(11);
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
                    packet.ReadUInt32("DbID", i, "Profession", j);
                    packet.ReadUInt32("Rank", i, "Profession", j);
                    packet.ReadUInt32("Step", i, "Profession", j);
                }

                packet.ReadUInt32("VirtualRealmAddress", i);

                packet.ReadByteE<GuildMemberFlag>("Status", i);
                packet.ReadByte("Level", i);
                packet.ReadByteE<Class>("ClassID", i);
                packet.ReadByteE<Gender>("Gender", i);
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_5_43903))
                {
                    packet.ReadUInt64("GuildClubMemberID", i);
                    packet.ReadByteE<Race>("RaceID", i);
                }

                packet.ResetBitReader();

                var nameLen = packet.ReadBits(6);
                var noteLen = packet.ReadBits(8);
                var officersNoteLen = packet.ReadBits(8);

                packet.ReadBit("Authenticated", i);
                packet.ReadBit("SorEligible", i);

                Substructures.MythicPlusHandler.ReadDungeonScoreSummary(packet, i, "DungeonScoreSummary");

                packet.ReadWoWString("Name", nameLen, i);
                packet.ReadWoWString("Note", noteLen, i);
                packet.ReadWoWString("OfficerNote", officersNoteLen, i);
            }

            packet.ReadWoWString("WelcomeText", welcomeTextLen);
            packet.ReadWoWString("InfoText", infoTextLen);
        }

        [Parser(Opcode.CMSG_PETITION_BUY, ClientVersionBuild.V9_1_0_39185)]
        public static void HandlePetitionBuy(Packet packet)
        {
            var length = packet.ReadBits(7);
            packet.ReadUInt32("Unused910");
            packet.ReadPackedGuid128("Unit");
        }
    }
}
