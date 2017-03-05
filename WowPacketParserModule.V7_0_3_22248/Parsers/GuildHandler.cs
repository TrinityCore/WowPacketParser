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
            packet.Translator.ReadPackedGuid128("Guild Guid");

            var hasData = packet.Translator.ReadBit();
            if (hasData)
            {
                packet.Translator.ReadPackedGuid128("GuildGUID");
                packet.Translator.ReadInt32("VirtualRealmAddress");
                var rankCount = packet.Translator.ReadInt32("RankCount");
                packet.Translator.ReadInt32("EmblemColor");
                packet.Translator.ReadInt32("EmblemStyle");
                packet.Translator.ReadInt32("BorderColor");
                packet.Translator.ReadInt32("BorderStyle");
                packet.Translator.ReadInt32("BackgroundColor");

                packet.Translator.ResetBitReader();
                var nameLen = packet.Translator.ReadBits(7);

                for (var i = 0; i < rankCount; i++)
                {
                    packet.Translator.ReadInt32("RankID", i);
                    packet.Translator.ReadInt32("RankOrder", i);

                    packet.Translator.ResetBitReader();
                    var rankNameLen = packet.Translator.ReadBits(7);
                    packet.Translator.ReadWoWString("Rank Name", rankNameLen, i);
                }

                packet.Translator.ReadWoWString("Guild Name", nameLen);
            }
        }

        [Parser(Opcode.SMSG_GUILD_ROSTER)]
        public static void HandleGuildRoster(Packet packet)
        {
            packet.Translator.ReadUInt32("NumAccounts");
            packet.Translator.ReadPackedTime("CreateDate");
            packet.Translator.ReadUInt32("GuildFlags");
            var int20 = packet.Translator.ReadUInt32("MemberDataCount");

            packet.Translator.ResetBitReader();
            var bits2037 = packet.Translator.ReadBits(10);
            var bits9 = packet.Translator.ReadBits(11);

            for (var i = 0; i < int20; ++i)
            {
                packet.Translator.ReadPackedGuid128("Guid", i);

                packet.Translator.ReadUInt32("RankID", i);
                packet.Translator.ReadUInt32<AreaId>("AreaID", i);
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

            packet.Translator.ReadWoWString("WelcomeText", bits2037);
            packet.Translator.ReadWoWString("InfoText", bits9);
        }
    }
}
