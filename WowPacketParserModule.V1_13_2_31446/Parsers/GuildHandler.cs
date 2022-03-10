using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V1_13_2_31446.Parsers
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
                packet.ReadInt32("VirtualRealmAddress");
                var rankCount = packet.ReadUInt32("RankCount");
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
                    packet.ReadWoWString("RankName", rankNameLen, i);
                }

                packet.ReadWoWString("GuildName", nameLen);
            }
        }

        public static void ReadGuildRosterMemberData(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("Guid", idx);

            packet.ReadUInt32("RankID", idx);
            packet.ReadUInt32<AreaId>("AreaID", idx);
            packet.ReadUInt32("PersonalAchievementPoints", idx);
            packet.ReadUInt32("GuildReputation", idx);

            packet.ReadSingle("LastSave", idx);

            for (var j = 0; j < 2; ++j)
            {
                packet.ReadUInt32("DbID", idx, j);
                packet.ReadUInt32("Rank", idx, j);
                packet.ReadUInt32("Step", idx, j);
            }

            packet.ReadUInt32("VirtualRealmAddress", idx);

            packet.ReadByteE<GuildMemberFlag>("Status", idx);
            packet.ReadByte("Level", idx);
            packet.ReadByteE<Class>("ClassID", idx);
            packet.ReadByteE<Gender>("Gender", idx);

            packet.ResetBitReader();

            var nameLen = packet.ReadBits(6);
            var noteLen = packet.ReadBits(8);
            var officersNoteLen = packet.ReadBits(8);

            packet.ReadBit("Authenticated", idx);
            packet.ReadBit("SorEligible", idx);

            packet.ReadWoWString("Name", nameLen, idx);
            packet.ReadWoWString("Note", noteLen, idx);
            packet.ReadWoWString("OfficerNote", officersNoteLen, idx);
        }

        [Parser(Opcode.SMSG_GUILD_ROSTER)]
        public static void HandleGuildRoster(Packet packet)
        {
            packet.ReadInt32("NumAccounts");
            packet.ReadPackedTime("CreateDate");
            packet.ReadInt32("GuildFlags");
            var memberCount = packet.ReadUInt32("MemberDataCount");

            packet.ResetBitReader();
            uint welcomeTextLen = 0;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V2_5_1_38598)) // @TODO also valid for >= 1.14.0
                welcomeTextLen = packet.ReadBits(11);
            else
                welcomeTextLen = packet.ReadBits(10);
            var infoTextLen = packet.ReadBits(11);

            for (var i = 0; i < memberCount; ++i)
                ReadGuildRosterMemberData(packet, i);

            packet.ReadWoWString("WelcomeText", welcomeTextLen);
            packet.ReadWoWString("InfoText", infoTextLen);
        }
    }
}